# Contexte
Après avoir fait ``npm i serve --global``, on ne peut pas lancer ``serve`` avec cette message d'erreur:
````js
serve : The term 'serve' is not recognized as the name of a cmdlet, function, script file, or operable program. Check the spelling of the name, or if a path was included, verify that the path is    
correct and try again.
````
En effet, c'est parce qu'on a pas précisé dans les variable d'environnement, ou se trouve l'exécutable ``serve``.
Pour la trouver, on utilise la commande : ``npm prefix --global`` (Pour les node.js récents)
Et puis on l'ajoute dans la variable ``PATH`` de ``environnement variable`` .
(Si oublie, demande à ChatGPT .)

Par contre, on a remarqué qu'on peut faire: ``npx serve ./dist``.
Dans la suite, on découvrir le mécanisme derrière.
# Rappel
### A. Installation local / Installation global
Une __installation locale__ ne vit qu'à l'intérieur d'un projet donné(Dans **node_modules/bin**), et ne __pollue__(La version d'exécutable utilisé par un programme peut différer d'un autre) pas l'environnement système.
Tandis qu'une __installation globale__ est accessible à l'ensemble du système.(Donc deux version d'une exécutable ne peut pas coexister).

# NPX(Node Package Executor)
### A. Rôle de ``npx``
``npx`` est un outil inclus avec ``npm``, qui:
* Exécute des binaires(Des programmes exécutables, comme ``serve``) sans que vous ayez à les installer localement ou globalement.
* Recherche automatiquement le binaire spécifié dans:
  * Les __dépendances/installation locales__ du projet(Dans le dossier ``node_modules/bin``).
  * Les __installations globales__.(Indiqué par la commande ``npm prefix --global``, normalement ressemble à : ``C:\Users\<Utilisateur>\AppData\Roaming\npm``)
  * Le registre npm, si le binaire n'est trouvé nulle part.(Téléchargé temporairement le package directement)

### B. Pourquoi ``serve ./dist`` échoue sans ``npx``.
Lorsqu'on tape simplement ``serve ./dist``, le système:
* Cherche un exécutable nommé ``serve`` dans le ``PATH``.
* Si le dossier contenant les binaires globaux de ``npm`` n'est pas dans le ``PATH``, la commande échoue, car elle ne peut pas localiser ``serve``.

### C. Pourquoi ``npx`` contourne le problème.
``npx`` contourne le problème en :
* Cherchant automatiquement dans le dossiers globaux et locaux de ``npm``, sans dépendre de la configuration du ``PATH``.
* Téléchargeant et exécutant le package directement depuis le registre npm si nécessaire.

### D. Utiliser sans installer.
* __Gain de temps__: Pas besoin d'installer l'outil si vous en avez juste besoin ponctuellement.
* __Pas d'encombrement__: éviter d'installer des outils inutiles globalement ou dans vos projets.
* __Toujours à jours__:
  * Pour une version spécifique: ``npx cowsay@1.4.0``, même si une autre version est déjà installée.
* __Simplifie le chemin d'exécutable__:
    En utilisant ``npx``, on a plus besoin de saisir:
    * Local: ``./node_modules/.bin/webpack``
    * Global: ``C:\Users\<Utilisateur>\AppData\Roaming\npm\serve``

### E. Des exemples
* npx create-reactèapp myApp
* npx vite
* npx serve ./dist

# NPM(Node Package Manager)
 ### A. Qu'est ce que ``npm``?
``npm`` est un outil qui accompagne __Node.js__. Son rôle principal est de gérer les __packages__(ou librairies) JavaScript.

### B. Les fonctionnalités principales de ``npm``
1. Installer des packages:
    * Localement: ``npm install <package_name>``
    * Globalement: ``npm install --global <package_name>``
2. Désintaller des packages:
   ``npm uninstall <package_name>``
3. Mettre à jour des packages:
   ``npm update <package_name>``   
4. Exéciter des scro^ts définis dans ``packages.json``
  * Exemple: Si dans ``package.json``, il y a un script:
    ````
    "script" {
      "start" : "node app.js"
    }
    ````
    On peut donc l'exécuter avec:
    ````js
    npm run start
    ````
