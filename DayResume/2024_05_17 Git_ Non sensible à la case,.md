# Git
### I - Faut faire gaffe à ceux qui est inclu dans le commit
Par exemple le changement des noms des fichiers n'est pas inclus dans le commit .
Voici la raison :
* Sur certains système de fichiers, comme ceux utilisés par Windows et macOS par défaut, __les noms de fichiers ne sont pas sensibles à la case__. Cela signeifie que 'filename.txt' et 'FileName.txt' sont considérés comme identiques.
*  Git détermine les changements dans un projet en comparant le contenu des fichiers, et non en suivant les opérations de renommage spécifiques.
* Lorsqu'on renomme un fichier, Git considère cela comme une suppression du fichier ancien et l'ajout d'un nouveau fichier avec un nouveau nom.

### II - Git status
Affiche l'état des fichiers dans le repertoire de travail

### III - Git diff --staged
Affiche la différence entre les codes dans l'index et les codes de HEAD(dernière commit)
