# I- Les bases de processus
### A. Qu'est ce qu'un processus?
Un processus est une instance en cours d'exécution d'un programme.
* Quand tu lances ```npm start``, tu ne fais pas juste lire du code - Tu crées un processus qui va tourner sur ta machine pour exécuter ce code.

### B. Différence entre processus et programme.
|Programme|Processus|
|--|--|
|Un fichier statique(ex: ``node.exe``)| Le programme en cours d'exécution|
|Ne fait rien tant qu'on ne le lance pas| État actif dans la mémoire vive|
|Peut être lancé plusieurs fois| Chaque lancement = Un processus différent avec un PID unique|
* Le PID = Process ID -> Un numéro unique assigné à chaque processus en cours.

### C. Comment un processus est lancé via ``npm start``?
1. On ouvre un programme(``chrome``, ``VS Code``, etc.), ici on va ouvrir le ``VS Code``.
2. Une fois qu'il est ouvert, un processus ``VS Code`` est crée pr le système d'exploitation.
3. On tape une commande comme : ``npm run dev``
	* Et cela déclenchera l'exécution d'un script dans ``package.json``(ex: ``react-script start``), qui lui-même utilise __Node.js__.
4. Conséquence: Un processus Node est créé et reste en activité.(Normalement il est indépendent du processus qui l'a créé, mais Visual Studio gère activement les processus enfants).

### D. Comment un processus est arrêté proprement(``ctrl + c``)
✅ Les méthodes classiques:
* Dans le terminal:
	* ``ctrl + C``: Envoie un signal d'arrête au processus courant(Grâce à son PID).
* Depuis le système:
	* ``taskkill / PID 19312 /F``(Windows)
	* ``kill -9 19312``(Linux/ Mac)

⚠️ Mauvais cas:
* Fermer juste la fenêtre du terminal ne garantit pas l'arrêt du processus. __Il peut continuer à tourner en arrière-plan__.

### E. Ce qu'est un processus en arrière-plan(background)
|Avant-plan|Arrière-plan|
|--|--|
|Tu vois sa sortie dans le terminal|Il tourne "silencieusement" en fond|
|Tu peux l'arrêter avec ``Ctrl + C``|Tu dois le tuer avec ``taskkill``|
|Ex: ``npm start`` dans un terminal|Ex: Un processus Node lancé sans fenêtre ou via un outil comme ``pm2``|


# II - Les ports et le réseau local.
### A. Ce qu'est un port réseau(``localhost: 3000``).
Un port est comme une porte numérique que les applications utilisent pour envoyer ou recevoir des données sur le réseau.
* Ton ordi a une seule adresse IP (Souvent ``127.0.0.1`` pour local), mais plein de ports (numérotés de 0 à 65535).
* ``localhost`` est un alias pour l'adresse IP 127.0.0.1. C'est une boucle locale: Ton ordi communique avec lui-même.

### B. Pourquoi chaque serveur(React, Vite, Express...) écoute un port.
Chaque processus serveur(ex: un projet React, une API, un back-end Express) écoute sur un port précis.
Les adresse IP sont comme les adresse postale de la maison, et les ports sont comme les numéros d'apprtement/Boîte aux lettres.

### C. Vérifier quel port est déjà utilisé(Sur Windows)
* Commande:
	````bash
	netstat -ano | findstr :3000
	````
* Résultat:
	````bash
	TCP 127.0.0.1:3000 LISTENING 12345
	````
	* = Le port 3000 est utilisé par le processus 12345.

### D. Libérer un port bloqué
Tu fais:
````bash
taskkill /F /PID 12345
````
-> Cela tuera le serveur ou l'app qui occupait le port.
* Quand tu as fermé le terminal sans faire ``ctrl + C``
* Le serveur est bloqué/ freeze
* Tu changes de projet et tu veux relancer propre.


# III - npm, Node et les serveurs de développement
### A. Ce que fait ``npm start``
1. Appelle un script du ``package.json``
Exemple:
````json
"scripts": {
	"start": "react-scripts start"
}
````
* Donc, ``npm start`` = "Exécute ``react-scripts start``', qui est lui-même une commande Node.
2. Lance un serveur de dev comme Vite, Webpack Dev Server, etc.
Selon ton stock, il va :
	* Transformer ton code(Babel, TypeScript, ...)
	* Ouvrent un port.
	* Watchent tes fichiers.
	* Mettent à jour automatiquement ton navigateur avec hot-reload.

### B. Ce que fait ``npm install``/ Changement de branche.
1. Les ``node_modules`` peuvent changer

	Quand tu changes de branche avec:
	````bash
	git checkout <Autre-branche>
	````
	* Le ``package.json`` et ``package-lock.json`` peuvent changer.
	
	Les dépendances sont recalculées, et les versions peuvent être incompatibles avec les modules précédents.

2. Il faut redémarrer les processus.

	* Parce que les anciens modules sont chargés __en mémoire__ dans le serveur déjà lancé.
	* Tu risques d'avoir des erreurs type:
		* Cannot find module 'X'
		* Mismatched version of React or Webpack.


# IV - Débug courant & Bonnes pratiques
### A. Comment réagir si:
1. ``localhost: 3000`` reste bloqué. -> Tue le
2. Un autre processus bloque le port. -> Tue le
3. Tu as un ``SYN_SENT``, ou ``TIME_WAIT``
	* Ces messages indique que:
		* Une connexion est en attente de réponse(``SYN_SENT``)
		* Ou une ancienne connexion est en train d'être nettoyée(``TIME_WAIT``)
	* Tu peux les ignorer dans a plus part des cas.
	* Mais si ça traîne, un ``taskkill`` ou un redémarrage du terminal peut tout nettoyer.

### B. Redémarrer clean:
1. Fermer les anciens serveurs.
	* ``ctrl + C``
	* ou ``taskkill /F /PID x``
2. Supprimer les ``node_modules``(Si tu changes de branche ou rencontres des erreurs étranges)
	````bash
	rm -rf node_modules
	npm i
	````
3. Redémarrer le serveur
	````
	npm start
	````
### C. Automatiser le nettoyage avec des scripts ``.bat`` ou ``npm run clean``.
Tu peux créer un fichier : ``reset-dev.bat``, puis tu l'exécute.
````bat
@echo off
echo Fermeture des serveurs sur le port 3000...
for /f "tokens=5" %%a in ('netstat -ano ^| findstr :3000') do taskkill /F /PID %%a
echo Suppression de node_modules...
rd /s /q node_modules
echo Réinstallation des dépendances...
npm install
echo Redémarrage du serveur...
npm start
pause
````
* Tu lances ça -> Et ça tue l'ancien serveur, réinstalle et redémarre pour toi.

# V Le fichier ``.bat``
### A. Qu'est ce qu'un fichier ``.bat`` ?
Un fichier ``.bat`` est un __fichier texte__ contenant une liste de commandes Windows que tu veux exécuter dans l'ordre, automatiquement.

C'est comme écrire un petit programme ou macro, mais avec des commandes simples de terminal Windows(cmd.exe).

### B. Comment créer un fichier ``.bat``?
On le créer comme un fichier ``.txt``, mais avec le suffixe ``.bat``.

Une fois crée, on peut:
* Double-cliquer dessus pour exécuter(Si ce fichier se trouve dans le répertoire de ton projet, tu peux l'exécuter directement, sinon il faut un ``cd``).
* Ou le lancer dans un terminal (cmd ou PowerShell).

### C. Exemple complet avec explication détaillé.
