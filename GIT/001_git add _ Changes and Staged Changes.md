# I - git add
1. __Changes__ (Aussi appelés "modifications non préparées" ou "unstaged changes"):
    * Ce sont les modifications que vous avez effectuées dans les fichiers de votre projet mais que vous n'avez pas encore préparées pour un commit. C'est à dire non poussé dans le Git lors de prochain push.
    * Ces changements sont visibles dans votre répertoire de travail.
    * Vous pouvez voir ces modifications avec la commande __"git status"__ ou __"git diff"__.

2. __Staged Changes__ (Changements préparés)
    * Ce sont les modifications que vous avez ajoutées à la zone de staging (ou "index") en utilisant la commande __"git add"__
    * Les changements dans cet état sont prêts à être inclus dans votre prochain commit.
    * Pour voir ces modifications, vous pouvez utiliser __"git status"__ pour voir la liste des fichiers staged, ou __"git diff --staged"__ pour voir les différences exactes qui ont été staged.

# II - git restore
Utilisée pour annuler des modifications dans votre répertoire de travail. Elle est polyvalente et peut être utilisée de différentes manières pour répondre à différents besoins:
1. __git restore [nom_de_fichier]__: 
    Cette commande restaure le fichier à son dernier état commité, effaçant toutes les modifications non staged (non préparées) que vous avez faites depuis le dernier commit.

2. __git restore --staged [nom_de_fichier]__ : 
    Cela enlève les fichiers de la zone de staging sans modifier le contenu du fichier dans votre répertoire de travail.



