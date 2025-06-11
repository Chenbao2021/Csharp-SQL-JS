
# Contexte
1. Azure propose une service pour déployer des static web applications.( Inclu les SPA)
2. On peut automatiser les déployements des applicationa SPA grâce à Github Action.
3. Pour se fait, on doit mettre en place un fichier ``.yml``.

# I - Les commandes Shell dans Github Actions.
Comme les runners GitHub sont par défaut des mini-machines Linux Ubuntu, alors on peut exécuter n'importe quelle commande shell Linux(bash). Cela nous permet de déboguer efficacement.

Exemple concrèts utiles:
|Cas|commande|
|--|--|
|Vérifier si ``dist/index.html`` est bien généré|``cat dist/index.html``|
|Lister les fichiers prêts pour l'upload|``find dist``|
|S'assurer que ``vite.config.ts`` est bien là|``ls -l vite.config.ts``|
|Lire une variable d'environnement|``echo $GITHUB_REF``|

## A. Les commandes les plus utilisés
|Commandes|Utilité|
|--|--|
|``ls``|Voir les fichiers et dossiers|
|``find``|Lister récursivement tous les fichiers|
|``cat``|Lire le contenu complet d'un fichier|
|``head``Lire les premières lignes||
|``echo``|Afficher du texte|

# II - Déploiement d'une App avec GitHub Actions, Artifacts et GitHub Packages.
Un workflow est un fichier ``.yml``.
## A. Structure d'un Workflow GitHub Actions
````yaml
on:
	push:
		branches: [dev]
		paths:
			- client/**
			- .github.workflows/deploy.yml
	workflow_dispatch:
````
Ce bloc signifie:
* Le workflow se déclenche quand on pousse sur la branche ``dev``, et uniquement si certains fichiers sont modifiés.
* On peut aussi le déclencher manuellement via l'interface GitHub.

## B. Définir son repertoir de travail
````yaml
defaults:
	run:
		working-directory: ${{ inputs.project-folder }}
````
Cela sert à :
* Exécuter tous les ``run:`` dans le dossier spécifié, sans avoir besoin de faire ``cd``.Mais pas pour les ``uses``.
* Très utile en monorepo(ex: ``client/``, ``serveur`` et ``SQL/``)

## C. Artifacts: Upload et Download
### Upload
````yaml
- name: Archive site
	uses: actions/upload-artifact@v4
	with:
		name: client
		path: client/dist/**

- name: Archive config
	uses: actions/upload-artifact@v4
	with:
		name: client_route
		path: client/staticwebapp.config.json
````
* Dans ``./artifact``, on aura donc deux repertoire, dont ``client`` contiendra tous les fichiers de ``client/dist``, ``client_route`` qui contiendra le fichier ``client/staticwebapp.config.json``.

### Download
````yaml
- name: Download artifacts
	uses: actions/download-artifact@v4
	with:
		path: ./artifacts
````

## D. Préparation de la config avec une Action Composite
````yaml
runs: 
	using: "composite"
	steps:
		- name: Nettoyer les anciennes configs
			shell: bash
			run: rm -f ./artifacts/client/config.json ./artifacts/client/config.dev.json
		
		- name: Utiliser la config de l'environnement
			shell: bash
			run: |
        if [ -f ./artifacts/client/config.${{ inputs.environment }}.json ]; then
          mv ./artifacts/client/config.${{ inputs.environment }}.json ./artifacts/client/config.json
        else
          echo "⚠️ config.${{ inputs.environment }}.json manquant"
        fi
````
Objectif:
* Garder un seul fichier ``config.json``, propre et prêt pour le déploiement.
* Basculer dynamiquement entre les environnements(``dev``, ``production``, etc.)

## E. Accès à GitHub Packages(registre privé npm)
````yaml
- uses: action/setup-node@v4
	with:
		node-version: '20'
		cache: 'npm'
		cache-dependency-path: client/package-lock.json
		registry-url: 'https://npm.pkg.github.com'
		scope: '@totalenergiescode'
````
````yaml
- run: npm install
	env: 
		NODE_AUTH_TOKEN: ${{ secrets.GITHUB_TOKEN }}
````
Ce que ça fait:
* Configure ``npm`` pour utiliser GitHub Packages pour les packages comme ``@totalenergiescode/ui``
* Injecte le jeton ``GITHUB_TOKEN`` automatiquement.
* Permet d'installer des librairies privées de ton organisation.

## F. Déploiement
````yaml
- name: Deploy to Azure Static Web Apps
	uses: Azure/static-web-app-deploy@v1
	with:
		app_location: ./artifact/client # Le dossier qui contient le code source d'application.
		api_location: ""
		output_location: ""
		config_file_location: ./artifacts/client_route/staticwebapp.config.json
		azure_static_web_apps_api_token: ${{ secrets.AZURE_STATIC_WEB_APPS_API_TOKEN }}
````
Ce que ça fait:
* Prend les fichiers de ``client``
* Appliquer la config spécifique à l'environnement
* Déploie vers ton application hébergée sur Azure.
* En cas d'erreur type: Fichier not fund. On peut utiliser ce code pour debuguer:
	````yaml
	- name: Debug liste les fichiers
		run: ls -R ./artifacts
	````

### Explication du proprieté ``app_location``:
``app_location`` est le dossier qui contient le code front-end de ton app (Par exemple, le code React).
	Cette valeur est relative à la racine du dépôt GitHub. (La où se trouve le fichier ``.github/``)

* Cas normal (Sans ``skip_app_build``):
	On laisse Azure faire le ``npm install && npm run build`` pour toi.
	````yaml
	app_location: client
	output_location: build
	````
	* ``client/`` contient ton code source React.
	* Azure exécute la commande de build(``npm run build``) dans ce dossier.
	* Le résultat est dans ``client/build``.

* Cas avec ``skip_app_build: true``:
	On fait le build dans un job précédent, et on veut juste donner à Azure le dossier déjà prêt.
	````yaml
	app_location: ./artifact/client
	skip_app_build: true
	````	
	* Ici, ``app_location`` = emplacement des fichiers prêts à déployer, comme si c'était ``output_location``.
 
