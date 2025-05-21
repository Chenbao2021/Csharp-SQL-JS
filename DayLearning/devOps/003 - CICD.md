# I - CI : Continuous Integration
La CI est déclenchée après que ton code soit poussé dans le dépôt distant(remote) - Typiquement sur GitHub, GitLab, etc.
### Détail du processus:
1. Tu écris du code localement.
2. Tu fais:
	````
	git add .
	git commit -m "Nouvelle fonctionnalité"
	git push origin main
	````
3. __Une fois que GitHub reçoit ce ``push``, il déclenche le pipeline CI s'il existe(ex: via un fichier ``.github/workflows/*.yml``).

Donc:
* Elle se déclenche du côté du dépôt distant, pas localement.
* On peut vérifier l'exécution sur l'onglet "Actions"(Github) ou "CI/CD"(GitLab).

## A - Exemple GitHub expliqué en détaillé:
Un fichier comme ``.github.workflows/ci.yml`` peut contenir:
````yaml
# 📛 Nom du pipeline (facultatif)
name: CI Pipeline

# 🔔 Quand est-ce que ce pipeline se lance ?
on:
	push:
		branches: [main] # déclenché ce pipeline quand on fait un git push sur main
	pull_request:
	  branches: [main]    # aussi lors d'une PR vers main

# 🧩 Liste des jobs (tu peux en mettre plusieurs en parallèle ou en séquence)
jobs:
  build-and-test: # C'est juste le nom du job, pas de signification particulière.
    runs-on: ubuntu-latest   # machine virtuelle Ubuntu

    steps: # Une suite d'opérations à effectuer dans l'ordre
      - name: 📥 Récupérer le code
        uses: actions/checkout@v3 # Action officielle GitHub pour "cloner" le repo. Car VM n'a pas ton code.

      - name: 🧱 Installer Node.js
        uses: actions/setup-node@v3 # Installer Node.js, c'est l'environnement pour exécuter tes codes.
        with:
          node-version: 18 # Préciser la version du node.

      - name: 📦 Installer les dépendances
        run: npm install

      - name: ✨ Linter
        run: npm run lint

      - name: 🧪 Tests
        run: npm test

      - name: 🏗️ Build
        run: npm run build

			- name : 🔥 Deploy to Firebase Hosting
        uses: w9jds/firebase-action@v13
				with:
          args: deploy --only hosting
        env:
          FIREBASE_TOKEN: ${{ secrets.FIREBASE_TOKEN }}
````
* S'il y a plusieurs fichiers, alors GitHub les exécute tous si leurs conditions ``on:`` sont remplie. 
	On peut très bien les organiser par rôle:
	
### À la fin d'un pipeline CI, que se passe-t-il ?
Si toutes les étapes réussissent sans erreur, alors le pipeline est marqué réussi. 
Et si on a une étape de déploiement dans un fichier séparé ou dans une autre partie du pipeline, elle peut s'exécuter(CD).
* Les résultats (``build/``, ``dist/``, etc.) restent dans la machine virtuelle temporaire.
* Si on veut faire quelques choses (Comme déployer le site), il faut:
	* Soit l'uploader (Vers Firebase, Netflix, etc.)
	* Soit le stocker comme artifact(Fichier temporaire que GitHub peut conserver).
		````yaml
		- name: Upload build
			uses: actions/upload-artifact@v3
			with: 
				name: react-build
				path: build/
		````

## B - Pourquoi automatiser avec CI ce que je fais déjà localement avec VS Code?
### 1. La sécurité d'un système neutre, automatique et constant.
Ton pC local n'est pas une vérité absolue. Avec CI:
* Exécute toujours tout de manière propre. (Pas de Node18 au lieu de Node20)
* Sur une machine vide à chaque fois. (Pas des règles historiques, )
* Avec exactement ce qu'il y a dans ton repo Git. (Détecte s'il y a des oublis des fichiers qui ne sont pas commités)
* Sur la même version de Node, d'OS, etc. 
* Sans oublier d'étapes. (La complexité du projet augmente, nouveau collègue, etc.).


# II - CD: Continuous Deployment, "On va mettre le code chez les utilisateurs."
Continuous Deployment, c'est mettre automatiquement à jour ton application en ligne dès que ton code est prêt (buil dOK, tests passés).
* C'est-à-dire qu'on a plus besoin de lancer ``az deploy``, ``scp``, ou ``firebase deploy`` manuellement.
	C'est GitHub Actions(Ou un autre outil CI/CD) qui le fait tout seul.

### Continuous Delivery (Livraison Continue)
Une fois que le code est testé, construit et prêt à être déployé automatiquement dans un environnement de production,
on doit le déployer pour l'utilisateur. 
Mais on veut que ce déploiement soit automatique aussi! Qu'on a pas besoin de cliquer sur un bouton "Déployer" pour le faire.

__On veut ZÉRO intéraction humaine__.

## A - Exemple concrèt avec Firebase
### Objectif
Quand on pousse sur ``main``
1. Le projet React est installé.
2. Il est construit(``npm run build``)
3. Il est déployé automatiquement vers:
	* Firebase Hoisting OU
	* Azure Static Web App.

### Prérequis (valable pour les deux)
Tu dois avoir __les secrets enregistrées__ dans Github(Dans Setting > Secrets and variables > Actions)
* Pour Firebase: ``FIREBASE_TOKEN``.
* Pour Azure: ``AZURE_STATIC_WEB_APPS_API_TOKEN``.

### Exemple Codes avec Firebase
````yaml
name: Deploy to Firebase Hosting

on:
	push: 
		branches: [main]
jobs:
	build_and_deploy:
		runs-on: ubuntu-latest
		steps:
			...

			-name: deploy to Firebase
			uses: FirebaseExtended/ action-hosting-deploy@v0
			with:
				repoToken: "${{ secrets.GITHUB_TOKEN }}" 
				firebaseServiceAccount: "${{secrets.FIREBASE_TOKEN}}"
				channelId: live
````
* Ton ``firebase.json`` doit être configuré pour dire que tu déploies le dossier ``build``.

### Codes avec Azure cloud
````yaml
...
````



