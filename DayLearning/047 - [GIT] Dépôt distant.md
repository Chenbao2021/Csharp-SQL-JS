# I - Dépôts distants
### A. Les commandes à connaître.
* ``git branch -r``: Permet de voir les __branches distantes__ dans Git.
* ``git fetch``: Permet de récupérer __les dernières informations__ sur les branches distantes(Nouvelles branches, commits récents, etc.). Mais si une branche a été supprimée sur le dépôt distant, elle reste affichée en tant que __dépôt distant local__.
* ``git fetch --prune``: Ce commande fait la même chose que ``git fetch``, mais elle permet de nettoyer les __dépôt distant local__ fantôme.
* ``git branch -vv``: Vérifie si les branches locales pointent vers une branche distante encore existante. Si c'est le cas, tu verras ``gone]`` dans le retour.


### B. Liéer une branche local à une branche distantielle.
Quand on fait ``git branch -vv``, Git affiche pour chaque branche local:
* Le dernier commit
* Et la branche distante suivie (upstream)

S'il voit que la branche upstream n'existe plus dans les références distantes connues(Dèaprès le dernière ``git fetch --prune``), alors il affiche ``[gone]``.

Quand on dit une branche locale pointe vers une branche distante, cela veut dire qu'elle a une branche "upstream" configurée -> Et c'est ce lien qui permet de savoir où ``git pull`` ou ``git push`` vont agir par défaut.
* Lier une branche local à une branche distante: ``git branch --set-upstream-to=origin/nom-de-branche nom-de-branche-local``
* Enlever le lien(Détacher l'upstream): ``git branch --unset-upstream``

Mais on peut aussi le faire avec ``git switch``:
* Créer et suivre une branche distante: ``git switch --track origin/feature-x``
	* Crée une branche locale ``feature-x``
	* La lie à ``origin/feature-x``
	* Te positionne dessus.
	* Équivaut à : ``git checkout -b feature-x origin/feature-x``

* Relier une branche locale à une branche distantielle (i.e: Il veut juste dire c'est la branche distante à utiliser par défaut quand on fait ``git pull`` ou ``git push``, et normalement que ces deux branches ont un rapport entre eux):
	* Soit on configure seulement le lien de suivi: ``git branch --set-upstream-to=origin/branch-name local-branch-name``, puis on fait simplement ``git pull``/``git push``.
	* Soit on pousse notre branche local et on fait le lien de suivie: ``git push --set-upstream origin ma-branche`` (Si la branche n'existe pas en distantielle)

# II - Supprimer une branche: Les options ``-d`` et ``-D``
### A. Les deux options de ``branch`` pour supprimer les branches.
Avec git, on a deux options pour supprimer une branche:
* ``git branch -d <branche>``
	
	C'est la suppression "sécurisée", il peut refuser de supprimer la branche si elle n'a pas été complètement mergée __dans la branche actuelle__.
* ``git branch -D <branche>``

	C'est la suppression "forcée", il supprime la branche même si elle n'a pas été mergée.
	Les commits qui ne sont pas dans une autre branche(ex: ``main``) peuvent devenir perdus.

### B. Comment Git détermine si une branche est "complètement mergée" dans la branche actuelle?
Quand on fait ``git branch -d feature/82703``, Git vérifie si la pointe de ``feature/82703`` est atteignable à partir de la branche actuelle(sur la quelle tu es).
C'est à dire si la branche de destination contient donc tous les commits de ``feature/82703``.
* Avec la commande ``git log feature/82703 --not main``, il affiche les commits de ``feature/82703`` qui ne sont pas dans ``main``. Et s'il n'y a rien, tu peux les supprimer tranquillement.

### C. Pour voir ce que Git voit.
Voici une commande utile: ``git log feature/82703 --not main``
* Elle affiche les commits de ``feature/82703`` qui ne sont pas dans ``main``.
* S'il n'y a rien: Tu peux supprimer avec ``-d`` en toute confiance.

### D. Tu dois être sur la bonne branche!
Quand on fait ``git branch -d feature/A``, Git regarde si la branche ``feature/A`` est complètement mergée dans __la branche sur laquelle tu es actuellement__. Donc si tu es sur la branche ``B``, Git vérifie:
* Est-ce que tous les commits de ``A`` sont déjà dans ``B`` ?

Donc, pour l'option ``-d``, on doit sur la bonne branche. (Comme ``master``).

# III - Les bases de Git: Travailler avec des dépôts distants.
Le mot __distant__ ne veut pas forcément dire que le dépôt est ailleurs sur le réseau ou Internet, mais seulement qu'il ne fait pas partie du même dépôt Git initialisé localement(C'est-à-dire, du même ``git init``).

### A. Afficher les dépôts distants
Pour visualiser les serveurs distants qu'on a enregistré, on peut lancer la commande ``git remote``.
Cette commande liste tous les noms des différentes références distantes qu'on a spécifiées.
* Si on a cloné un dépôt, il est affiché avec le nom ``origin``.(Le nom par défaut que Git attribue)
* On peut spécifier ``-v`` pour montrer l'URL que Git a stockée pour chaque nom correspondant.


### B. Ajouter des dépôts distants
1. ``git remote add [remote_name] [url]``: Ceci permet d'ajouter un nouveau dépôt distant Git.
2. ``git remote set-url [remote_name] [new_url]``: Ceci permet de modifier l'URL d'un remote.

Une fois ajouté, on peut utiliser ``remote_name`` au lieu de l'URL complète. Par exemple, pour récupérer les informations sur la branche ``remote_name``, on peut faire ``git fetch pb``.
Et dans ``remote_name`` on peut trouver toutes les branches à distantes.

### C. Récupérer et tirer depuis des dépôts distants
* ``git fetch [remote_name]``: Tire les données dans votre dépôt local mais sous sa propre branche (``remote_name/branche_name``). Elle ne les fusionne pas automatiquement avec aucun de vos travaux ni ne modifie votre copie de travail.
	* Git télécharge les nouveaux objets(commits, arbres, etc.) depuis le dépôt distant.
	* Il met à jour les branches distantes locales, comme ``origin/main``, ``origin/dev``, etc. Ils sont en __lecture seul__ et ne servent que pour refléter l'état actuel du dépôt distant.
	* Mais ton code locale ne change pas.
* ``git pull``: Tire les données dans votre dépôt local et fusionner sur ta branche actuelle.
	
	En effet, ``git pull`` fait deux choses:
	1. ``git fetch ``: Il récupère les dernières changements de la branche distante correspondante. 
	2. ``git merge origin/main``: Il fusionne les changements récupérés dans ta branche actuelle.

	❗Mais attention! __Ceci ne met à jour que la branche actuelle__=> Celle sur la quelle tu es.

### D. Retirer le dépôt distants
Si on souhait retirer un dépôt distant pour certaines raisons, on peut utiliser ``git remote rm [remote_name]``.
Une fois qu'on a supprimé la référence à un dépôt distant de cette manière, toutes les branches de suivi à distance et les réglages de configuration associés à ce dépôt distant sont aussi effacés.








