## I - Contexte
Mettre à jour des librairies peuvent poser toujours des problèmes, car souvent des nouveaux fonctionnalités ajoutés, et des anciens qui sont devenus obsolète, notamment celles que t'utilise dans ton projet!

### Rappel: dependencies de Package.json
Dans un fichier ``package.json``, les numéros de version dans les dépendances(``dependencies`` ou ``devDependencies``) suivent généralement le __versionnage sémantique__ (semver = semantic versioning), qui est sous la forme:
````json
"nom-du-package": "MAJEURE.MINEURE.PATCH"
````
* __MAJEURE__: Elle change quand il y a des changements incompatibles avec les versions précédentes.
* __MINEURE__: Elle change quand des fonctionnalités sont ajoutées de façon rétrocompatible.(_retro: avant, retro + compatible_)
* __PATH__: Elle change quand il y a des corrections de bugs rétrocompatibles.

Les symboles en préfixe: ``^``, ``~``, etc.
* ``^17.0.2``: Accepte les mises à jour mineures et de patch.
* ``~17.0.2``: Accepte seulement les mises à jour de patch.
* ``17.0.2`` : Version exacte, aucune mise à jour acceptée automatiquement.
* ``*`` : N'importe quelle version.
* ``>=1.2.0``: Version supérieur ou égale à 1.2.0
* ``>=1.0.0 <2.1.0``: On donne un intervalle plus large.

Par contre ``npm i [package]@latest`` écrase la version du package même si tu avais un ``^`` avant.
Sinon, sans ``latest``, il va chercher la version compatible dans cette plage autorisée.(Comme : ``axios: ^1.3.0`` peut installer tous les version qui ``>= 1.3.0`` et ``< 2.0.0``)

### Quelques commandes ``npm``
1. ``npm i [package]``: Installer le package selon la version spécifiée dans le ``package.json``.
2. ``npm i [package]@latest``: Installe la toute dernière version disponible de la lib depuis le registre npm. Et met à jour le ``package.json``(Et ``package-lock.json``) avec cette version précise.
3. ``npm update lib``: Met à jour la lib dans les limites définies par ton ``package.json``.
	* ``npm update`` suivie de ``npm outdated``: Met à jour tous les paquets vers la version "Wanted".(Selon les contraintes dans ``package.json``)

## II - Créer une branche dédiée pour tester les mises à jour des packages.
Et cela n'impactera pas la branche ``master`` tant que tu ne fais pas de merge.

### Pourquoi créer une branche dédiée est une bonne pratique ?
1. __Isolation totale__: Tu peux expérimenter librement sans impacter le code stable sur ``master`` ou ``main``.
2. __Rollback facile__: Si les mises à jour posent problème, tu peux simplement supprimer ou réinitialiser la branche.
3. __Code review possible__: Tu peux créer un pull request et la faire relire avant de merger les mises à jour.
4. __CI/CD indépendant__: Si tu as un pipeline de test automatique, il s'exécutera sur ta branche test aussi.

### Étapes à suivre
````bash
git switch -c update-packages
````
Ensuite:
````bash
npm outdated # Pour voir les packages obsolètes.
npm update # Mettre à jour les packages mineurs automatiquement.
npm install <package>@latest # Pour mise à jour manuelle de certains.
````

