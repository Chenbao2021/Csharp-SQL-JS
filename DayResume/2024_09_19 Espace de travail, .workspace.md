# React
****
## I - Formatteur
Un formateur est un outil qui organise automatiquement otn code en respectant des règles de style spécifiques.

## II - Espace de travail
C'est une manière d'organiser ton projet ou ton environnement de développement pour faciliter la gestion et la personnalisation.

**Espace de travail simple(sans fichier ``.code-workspace``)**:
* Par défaut, lorsque tu ouvres un dossier dans VS Code, cela devient ton espace de travail.
* Les paramètres que tu modifies(Comme les extensions activées, les préférences d'éditeur, etc.) sont appliqués spécifiquement à ce dossier.
**Espace de travail multi-dossiers(avec fichier ``.code-workspace``)**:
* Il est possible d'ouvrir plusieurs dossiers dans un seul espace de travail en utilisant un fichier ``.code-workspace``. Ce fichier contient des informations sur les dossiers inclus dans l'espace de travail ainsi que des paramètres persosnnalisés pour cet espace.
* C’est particulièrement utile lorsque tu travailles sur des projets qui dépendent de plusieurs répertoires ou microservices, comme par exemple un projet avec un frontend React et un backend Node.js.

## III - .workspace
Le fichier ``.workspace`` est un fichier pour gérer les paramètres spécifiques à un espace de travail. 
Un espace de travail dans VS Code peut englober plusieurs projets, dossiers ou fichiers, et ce fichier permet de définir des paramètres personnalisés pour cet espace de travail particulier, sans affecter la configuration globale de l'éditeur.

#### A. La clé ``folders``
Cette section contient la liste des dossiers inclus dans l'espace de travail.
````
"folders": [
  {
    "path": "."
  },
  {
    "path": "./otherProject"
  },
]
````
* Ici, ``"path": "."`` signifie que le répertoire actuel(Le dossier où ce fichier workspace se trouve) est inclus dans l'espace de travail.

#### B. La clé ``settings``:
Cette section contient des paramètres sépcifiques à cet espace de travail.
Ces paramètres remplacent ceux définis globalement pour VS Code.
1. __Paramètres de l'éditeur__:
    ````
    "editor.tabSize": 2,
    "editor.detectIndentation": false,
    "editor.insertSpaces": false,
    "editor.formatOnSave": true,
    ````
    * ``"editor.tabSize": 2`` : Définit la taille d'une tabulation à 2 espaces
    * ``"editor.detectIndentation": false``: Si cette option était activée, l'éditeur pourrait ajuster le nombre d'espaces ou de tabulations en fonction de la structure du fichier ouvert.
    * ``"editor.insertSpaces": false``
    * ``"editor.formatOnSave": true``: Active le formatage automatique du code chaque fois que tu sauvegardes un fichier dans cet espace de travail.

2. __Paramètres de foramtage par défaut__:
    ``"editor.defaultFormatter": "vscode.typescript-language-features"``
    Ce paramètre spécifie que le formatteur par défaut est celui de l'extension de VS Code dédiée à __TypeScript__. Cela signifie que, pour les fichiers où aucun formatteur spécifique n'est défini, le formatteur TypeScript sera utilisé.
