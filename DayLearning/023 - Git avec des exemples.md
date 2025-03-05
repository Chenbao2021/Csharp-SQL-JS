# Rappel : Les trois zones/phrases qui décrivent l'état des fichiers
1. __Working Directory__(ou __working Tree__)
    * C'est l'état actuel de vos fichiers sur votre système de fichiers.
    * Tout fichier que vous créez, supprimez ou modifiez se trouve d'abord dans cette zone.
        * Dans la commande __git status__, ces fichiers sont considérés comme _untracked_ s'ils n'ont pas encore été ajoutés(Par __git add__), ou _modified_ s'ils sont déjà suivis mais ont changé.
        
2. __Index__ (ou __Staging Area__)
    * Lorsqu'on fait ``git add <fichier``, on copie l'état actuel de ces fichiers __depuis Working Directory vers l'index__.
        * __Un fichier peut être dans l'index et non modifié dans le répertoire de travail__(Aucune modification en attente).
        * __Un fichier peut être modifié dans le répertoire de travail mais non ajouté à l'index__(``git status`` le ontre comme "untracked")
        * __Un fichier peut être ajouté à l'index(``git add``) mais encore modifié dans le répertoire de travail__(Cela signifie que l'index a une version intermédiaire qui n'est pas la dernière version).
    * À ce stade, on dit à Git: __Ces changements je veux les inclure dans mon prochain commit__.
3. __Local Repository__(ou __Local Head/Commits__)
    * Quand on fait ``git commit``, Git regarde ce qui est dans l'index, crée un __snapshot__ de ces fichiers, et l'ajoute en tant que __nouveau commit__ dans votre historique local(HEAD).
    * Ce commit fait partie de votre __Local Repository__, c'est-à-dire la base de données Git(.git) contenue dans votre projet.
    * On pousse(``git push``) nos commits du __local Repository__ vers le __Remote Repository__(Par ex. GitHub) enfin pour partager nos codes avec l'équipe.
    
***
# I - Extraction(--detach)

En Git, on parle parfois de __[checkout --detach]__  (Ou extraction en mode détaché / detached HEAD).
Cela signifie:
* On se place le dépôt local à l'état d'un commit __sans__ être sur une branche.

Autrement dit, le HEAD n'est plus attaché à une branche, mais directement à un commit.

## A. Qu'est ce que le mode détaché(detached HEAD)?
* Quand vous faites ``git checkout <commit_sha>`` ou ``git checkout --detach<commit_sha>``, vous basculez sur l'état exact di commit indiqué.
* Git vous prévient que vous êtes en mode detached HEAD: Si vous faites des changements et que vous validez(commit), ces validations __ne seront pas rattachées__ à une branche existante.
* C'est utile pour:
    * __Examiner__ ou __tester__ une version précise du code.
    * __Compiler__ un état ancien du code sans impacter la branche en cours.
    * Appliquer ponctuellement un correctif puis __créer une nouvelle branche__ si on le souhaite.

## B. Sauvegarder les modifications.
Quand on fait un commit dans un mode détaché, le commit n'appartient à aucune branche.
* Pour ne pas perdre ce travail, on peut ensuite créer une branche:
    ``git checkout -b ma-nouvelle-branche`` / ``git switch -c ma-nouvelle-branche``
* Cela rattache votre HEAD à cette nouvelle branche, et puis tu peux utiliser git-merge pour fusionner les deux branches.

## C. Sortir sans modifier.
1. __Ne pas faire de commit__
    Tant que vous n'avez pas commité, vos changements n'existent que dans votre copie de travail (Working directory).
2. __Annuler vos modifications locales(Optionnel)__
    * Si vous souhaitez __complètement supprimer__ ces modifications, vous pouvez faire:
        ````git
        git restore .
        ````
    * ou, plus ancennement:
        ````
        git checkout .
        ````
    ces commandes écrase tous les fichiers modifiés non committés et les ramène à leur état initial.
    
3. __Revenir à une branche existante__
    Ensuite, on peut simplement sortir du mode détaché en retournant sur une branche: ``git checkout main``.
    Ou ``git switch -`` pour revenir dans la branche originale.
    De cette façon, vos modifications locales seront __abandonnées__ et vous serez revenu sur la branche habituelle, sans aucun commit supplémentaire.

## D. Git refuse de rétablir
* ``git revert <sha-du-commit>`` va créer un __nouveau commit__ qui annule le commit ciblé.
* Pour faire cela, Git a besoin que l'arborescence de votre code soir __propre__(clean), c'est-à-dire __sans modifications non commitées__. 
* Sinon, Git ne sais pas quoi faire, car ces modifications sont basé sur le commit ciblé, alors il ne peut pas décider s'il va les supprimer ou garder.

***
# II - Rétablir
## A. Au sens de l'éditeur de texte
* C'est la fonction inverse de __Annuler(undo)__.
* C'est l'quivalent de __Redo__ en anglais.

## B. Rétablir dans le contexte __Git__(ou __Revert__ en anglais)
* Cela fait référence au fait de __créer un nouveau commit qui annule les modifications d'un commit précédent__.
* Par exemple, sur Github ou dans Visual Studio, si vous voyez un bouton __Rétablir ce commit__ ou __Revert__(Selon la langue), cela signifie:
    * Prends le commit X, et génère un nouveau commit qui fait l'inverse de toutes ses modifications.
    * Le code final revient donc à l'état qu'il avait __avant__ le commit X, mais __l'historique Git__ conserve tout de même la trace du commit X et du commit de revert.

En résumé, __Rétablir__ peut signifier __Refaire__(l'inverse d'Annuler) dans l'éditeur de code, ou __Revert__ dans le contexte Git(Créer un commit qui annule un autre commit).

***
# III - Réinitialiser 
La commande ``git reset`` permet de __déplacer le HEAD(et la référence de la branche courante) vers un autre commit__, en choisissant ce qu'on fait des modifications dans l'espace de travail(working directory) et dans l'index(staging area).
## A. git reset --soft <commit>
* __Conserve toutes les modifications dans l'index(On peut immédiatement faire ``git commit``)__.
* Ne change rien dans l'index.
* Ne change rien dans le working directory.
* Ex: ``git reset --soft HEAD`` :Réinitialise seulement HEAD(Le commit précédent redevient le dernier commit).

## B. git reset --mixed <commit> (mode par défaut)
* L'index(staging area) est vidé(Tous les fichiers qui étaient en staging sont déstagés).
* Les fichiers dans le répertoire de travail restent intacts(Les modifications restent sur ton disque, mais elles ne sont plus suivies dans la staging area) . Dans ce cas, on doit refaire un  ``git add`` avant de valider (``git commit``).

## C. git reset --hard <commit>
 * __Écrase__ toutes les modifications, même dans votre répertoire de travail.
 * Vous vous retrouvez exactement dans l'état du commit ``<commit>``.
 * C'est la forme la plus <destructive>; Vous perdez vos modifications locales non commitées.

## D. Conséquences d'un reset:
* L'historique local est __réécrit__: Les commits en trop deviennent inaccessiibles(orphanes), à moins d'avoir une autre référence qui les pointe.
* Si vous avez déjà poussé ces commits, vous devrez faire un __push --force__, mais qui risque de divergence d'historique.

## E. Différence revert(rétablir) et reset(Réinitialiser)
* __``git reset``__ est généralement préféré lorsque vous travaillez __seul__ ou quand vous n'avez pas encore aprtagé vos commits. Cela permet de nettoyer l'historique avant de le rendre public.
* __``git revert``__ est privilégié pour annuler des commits __déjà__ poussé sur un dépôt collaboratif, car il ne réécrit pas l'historique partagé.
