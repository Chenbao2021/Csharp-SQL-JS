C'est très utile(et même indispensable) de comprendre comment lire les messages d'erreurs ``npm ERR!``, surtout quand on gère des projets complexes avec plusieurs dépendances.

# I - Qu'est ce que ``resolve`` dans ``npm``.
### qu'est-ce que le ``resolve`` dans ``npm``?
Le résolveur de dépendances(``resolver``) est le processus par lequel npm analyse toute les dépendances(et sous-dépendances) pour construire arbre de version cohérent à installer dans ``node_modules``.
* En résumé: __Qui dépend de quoi, en quelle version, et est-ce qu'on peut les faire coexister ?__

### Étapes de résolution dans ``npm install``(simplifié):
1. Lire le ``package.json``.
2. Analyser les ``dependencies`` et ``peerDependencies``
* Il regarde ses ``dependencies``(Installé même si c'est redondante)
* Puis ses ``peerDependencies``(Ce qu'il attend que le projet principal fornisse dans ses ``dependencies``)
3. Construire l'arbre des dépendances: npm tente de trouver une version compatible d echaque dépendance qui satisfait toutes les contraintes de version à tout les niveaux.
4. Gérer les conlits(``peerDependencies``)

### Métaphore: Le puzzle
Imagine que ton projet est un puzzle:
* Chaque pièce(librairie) a une forme(version).
* ``npm`` essaie de les emboîter toutes pour former une image complète.

Mais si une pièce(ex: ``@mf/react-app``) dit:
* Je ne m'emboîte qu'avec une pièce de forme 9.x, on a deux possibilité:
	* ``depedencies``: Alors je vais trouver une pièce qui peut m'adapter!
	* ``peerDependencies``: Je vais chercher parmit celles fournit par le jeu, s'il n'y a pas, echec.

### Remarques
1. L'ordre des librairies dans le ``package.json`` n'a pas d'importance: Grâce à résolution, l'odre des ``dependencies`` dans le ``package.json`` n'a absolutement aucune influence sur l'installation ou la résolution des dépendances par ``npm``.

2. L'ordre dans la commande ``npm install ...`` n' pas d'importance: ``npm`` collecte tous les paquets demandés et les analyser ensemble afin de construit un arbre global cohérent.
	Il ne les traite pas les paquets un par un dans l'ordre d'apparition.
	* __Regrouper les installation tant que c'est possible!__
	
# II - Exemples d'erreurs
## Premiere tentative
### Première partie du message
````js
npm ERR! code ERESOLVE
npm ERR! ERESOLVE unable to resolve dependency tree
````
* ``ERESOLVE``: C'est le code d'erreur. Ici, "E" pur "Error", "RESOLVE" pour "résolution".

* __npm n'arrive pas à résoudre l'arbre de dépendances__ car il y a une incompatibilité.

### Contexte du projet
````js
npm ERR! while resolving: chartering-estimator@0.3.2
````
* Il essaye d'installer les dépendances dans ton projet ``chartering-estimator`` version ``0.3.2``. C'est la qu'il rencontre le problème.

### Paquet trouvé(Celui que tu veux installer)
````
npm ERR! Found: @mf/react-grid@10.1.1
npm ERR! node_modules/@mf/react-grid
npm ERR! @mf/react-grid@"10.1.1" from the root project
````
* root project désigne le projet principal dans lequel tu fais ``npm install``, c'est-à-dire ton dossier actuel avec ton ``package.json``
* 1: npm a trouvé que la version installée ou demandée est ``10.1.1``.
* 2: Il l'a localisé dans le dossier ``node_modules``, donc elle est bien là.
* 3: Cette version a été demandée directement par ton projet.(Par ``package.json``)

### Problème rencontré
````
npm ERR! Could not resolve dependency:
npm ERR! peer @mf/react-grid@"^9.2.3" from @mf/react-app@3.4.5
npm ERR! node_modules/@mf/react-app
npm ERR!   @mf/react-app@"3.4.5" from the root project
````
* ``peer``de la ligne 2 veut dire:``@mf/react-grid`` ne va pas installer ``@ùf/react-grid`` soi-même, mais il a besoin que le projet principal l'installe avec une version compatible(``^9.2.3``)
	* ``peerDependencies``: N'est pas installé automatiquement comme ``dependencies``, c'est à projet qui l'utilise de le fournir.
	* Cela permet de forcer une cohérence globale dans le projet , sinon chacun installe ceux qu'ils ont dans ``dependencies`` et souvent des versions incompatible.
	* Pout les libs ou composants réutilisables.
* Le parque ``@mf/react-app@3.4.5`` ne peut fonctionner qu'avec ``@mf/react-grid`` version ``^9.2.3``, or on a installé ``@mf/react-grid@10.1.1``


## Deuxième tentative
### Installation redondante  d'une lib.
Là, tu as demandé ``@mf/react-grid@latest`` qui installe ``10.11.1``(La dernière)
En suite, npm dit:
````
npm ERR! Found: @mf/react-grid@9.2.5
````
Mais plus bas:
````
npm ERR! @mf/react-grid@10.1.1 from the root project
````
* Ici, le 10.1.1 n'est pas encore installé, c'est juste une tentative de résolution des dépendances(resolving).

Parce qu'on a déjà ``@mf/react-grid@9.2.5`` installé quelque part, via d'autres paquets(Par exemple: ``@ùg§react-app``), mais en forçant ``@mf/react-grid@10.1.1``, npm ne sait pas comment concilier ces deux versions.


### Conflit supplémentaire avec une autre lib
````
npm ERR! Conflicting peer dependency: ag-grid-community@32.3.4
npm ERR!   peer ag-grid-community@^32.3.3 from @mf/react-grid@10.1.1
````

