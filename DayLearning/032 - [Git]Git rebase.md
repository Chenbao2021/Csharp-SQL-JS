# 0 - Introduction
Git rebase est une commande intermédiaire à avancée dans Git. Elle est __essentielle__ pour gérer proprement l'historique des commits.
Ses deux principaux cas d'utilisation sont:
1. ``git rebase -i branch-name``: Réécrit l'historique en appliquant les commits de la branche actuelle sur ``branch-name``.
    * Git déplace temporairement les nouveaux commits(Par rapport à ``branch-name``).
    * Il replace la branche actuelle sur la nouvelle base.
    * En suite, il réapplique vos commits un par un.
2. ``git rebase -i HEAD~2``: Réécrire les 2 derniers commits(actuel + précédent) de la branche actuelle.  (Comme si notre branche est à ``HEAD~2``, et l'autre à ``HEAD``, on peut dire que leur __ancêtre commun__ est ``HEAD~2`` et on réapplique les changements(``HEAD~1`` et ``HEAD``) en gardant leur ordre).
3. __Ajouter chaque fois l'option ``i``, c'est plus clair.__
# I - Peut-on modifier le message d'un commit?
Parfois on veut modifier le message d'un ancien commit, voici plusieurs façon pour le modifier:
* __Dernier commit(Non poussé) :__
    ````git
    git commit --amend -m "Nouveau message"
    ````
* __Ancien commit(Non poussé) :__
    ````git
    git rebase -i HEAD~n
    ````
    * Remplace ``pick`` par ``reword`` pour modifier un message de commit.
    * Enregistre et ferme l'éditeur.
* __Commits déjà poussés: Après modification ci-dessus, force le push :__
    ````git
    git push --force-with-lease
    ````

Ici, on voit une première cas d'utilisation de ``git rebase``, c'est à dire modifier le message d'un commit.

# II - Que signifie ``git rebase -i HEAD~n``?
Cette commande permet de  réécrire l'historique des ``n`` derniers commits en mode interactif(C'est à dire par l'ordre, un après l'autre, comme si ``HEAD~`` est le dernier noeud commun).
* ``git rebase -i``: Lance un rebase interactif.
* ``HEAD~n``: Sélectionne les ``n`` derniers commits(ex: ``HEAD~3`` prend les 3 derniers(Y comprit l'actuel)).

__Étapes détaillées__:
1. Exécuté:
    ````sh
    git rebase -i HEAD~3
    ````
2. Git ouvre un éditeur(Vim, par défaut) avec la liste des 3 derniers commits(Du plus vieux aux plus récéntes)__[Voir IV]__:
    ````git
    pick 123abc version_1 commit
    pick 456def version_2 commit
    pick 789ghi version_3 commit
    ````
3. Remplace ``pick`` par ``reword`` pour modifier un message.
    ````git
    pick 123abc Premier commit
    pick 456def Deuxième commit
    reword 789ghi Troisième commit
    ````
4. Enregistre et ferme l'éditeur.
5. Git ouvre une fenêtre pour modifier le message du commit sélectionné.
6. Modifier le message, enregistre et ferme.
7. Git applique les modifications.

On voit deux mot clés ``pick`` et ``reword``, en effet, on a encore d'autres, voici une liste des actions disponibles dans ``git rebase -i``:
|commandes|Effet|
|--|--|
|``pick`` ou ``p``| Conserver le commit tel quel.|
|``reword`` ou ``r``|Modifier uniquement le message du commit|
|``edit`` ou ``e``|Permet de modifier le commit(fichiers, message, etc.)|
|``squash`` ou ``s``|Fusionne ce commit avec le précédent en gardant le messages de commit après fusion.|
|``fixup`` ou ``f``|Comme ``squash``, mais ignore le message de ce commit..|
|``drop`` ou ``d``|Supprime le commit.|
|``exec`` ou ``e``|Exécute une commande shell après le commit.|
* Les commandes sont faites un après l'autre, donc si on a un fixup suivie de squash, alors squash execute en premier, une fois termine, on exécute fixup avec le résultat.
# III - Comment voir la liste des commits précédents?
Utilise ``git log`` pour afficher les commits.
* Version détaillée: ``git log``
* Version compacte: ``git log --oneline``
* Limiter le nombre de commits affichés: ``git log -n 5``

# IV - Comment manipuler l'éditeur(Vim)?
Vim est l'éditeur par défaut sur Linux/macOS.
* Touche ``i`` -> Modifier
* Touche ``Échap(ESC)`` -> Quitter le mode édition, et permet de saisir des commandes.
    * ``:wq``, puis touche ``Entrée`` pour enregistrer et fermer.
    * ``:q!``, puis touche ``Entrée`` pour quitter sans enregistrer.

Si tu veux utiliser un autre éditeur que Vim:
* Pour Nano: ``git config --global core.editor nano``
* Pour VS Code: ``git cofig --global core.editor "code --wait"``

# V - Quand utiliser ``git rebase``?
``git rebase`` permet de réécrire l'historique Git et est principalment utilisé dans deux cas:
#### Cas 1: Nettoyer l'historique avant un push
Avant de partager son travail, il est __mieux de garder un historique propre__ en fusionnant ces commits.


#### Cas 2: Mettre à jour une branche sans créer de merge commits
Quand on travaille sur une branche(``feature``) dérivée de ``main``, et que ``main`` avance avec de nouveau commits, on veut __intégrer ces changements__ proprement. Deux solutions possibles:
1. Avec ``git merge``(Création d'un commit de merge):
    ````git
    git checkout feature
    git merge main
    ````
    * __Résultat__: Un commit "Merge branch 'main' into feature" est crée. 
2. Avec ``git rebase``(Réécriture de l'historique, pas de commit de merge)
    ````git
    git checkout feature
    git rebase -i main
    ````
    * __Résultat__: Les commits de ``feature`` sont repositionnées(``main`` actuel suivi de tes commits sur ta branche) __au sommet de ``main``__ dans le même ordre __un après l'autre__, sans commit de merge.
    S'il y a un commit crée un conflit:
        1. Tu le résous.
        2. Tu fais ``git add .``
        3. Tu continues le rebase avec ``git rebase --continue`` pour __passer à commit suivant__.
# VI - Est-il obligatoire d'utiliser ``git rebase`` ?
__Il n'est pas obligatoire__, mais très recommandé __dans un workflow collaboratif__ pour garder un historique lisible.
#### Quand utiliser ``git merge`` plutôt que ``git rebase``?
* __Sur une branche publique partagée avec d'autres développeurs__.
    (Rebaser une branche publique peut poser problème car cela réécrit l'historique et crée des conflits pour les autres, mais pour une branche privé ou tu es le seul à développer, c'est recommandé)
* __Lorsqu'on veut garder une trace explicite des fusions :__
    (Exemple: Historique d'un gros projet où il est important de voir quand une fonctionnalité a été fusionnée.)

#### Alternative à ``git rebase``:
* ``git merge``: Si l'historique n'a pas besoin d'être nettoyé.
* ``git cherry-pick``: Si on veut récupérer __un seul commit__ d'une autre branche.

# VII - Exemple d'une rebase.
1. Vérifier qu'on est bien sur notre branche local(``feature-branch`` par exemple):
    ``git checkout feature-branch``
2. Lance un rebase interactif sur les 10 derniers commits.
    ``git rebase -i HEAD~10``
3. Git ouvre un éditeur avec la liste des 10 derniers commits.
    ````git
    pick 123abc Commit 1
    pick 456def Commit 2
    pick 789ghi Commit 3
    pick abc123 Commit 4
    pick def456 Commit 5
    pick ghi789 Commit 6
    pick 321cba Commit 7
    pick 654fed Commit 8
    pick 987ihg Commit 9
    pick 789xyz Commit 10
    ````
4. Modifier les lignes.
	* Garder ``pick`` sur le premier commit(Celui qui servira à stocker tous les autres).
	* Remplacer ``pick`` par ``squash``(``s``) pour les 9 autres. 
		````git
		pick 123abc Commit 1
		squash 456def Commit 2
		squash 789ghi Commit 3
		squash abc123 Commit 4
		squash def456 Commit 5
		squash ghi789 Commit 6
		squash 321cba Commit 7
		squash 654fed Commit 8
		squash 987ihg Commit 9
		squash 789xyz Commit 10
		````
		* ``Git rebase`` rejoue les commits dans l'ordre chronologique, c'est à dire du plus ancien vers le plus récent. Il ne peut pas fusionner un commit plus ancien dans un plus récent, car au moment ou il rejoue l'ancien, le plus récent n'existe pas encore!
5. Enregistrer et fermer l'éditeur(``:wq`` dans Vim)
6. Git te demande maintenant de modifier le message du commit final:
	* Supprimer les messages de commits intermédiaires si tu veux un message propre.
	* Garder uniquement un message comme:``Feature complète: Ajout de nouvelles fonctionnalités et corrections de bugs``.
7. Enregistrer et fermer l'éditeur.

