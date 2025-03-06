# 0. Question
Dans mon ``feature/79975``, les codes sont basé sur ``master v1``. Et pendant mes développement, ``master v1`` est passé à ``master v3``. Du coup quand je merge ma ``feature/79975`` dans ``master v3``, les codes non modifiés de ``feature/79975`` seront traités comment ?

# I. Comprendre la fusion à trois voies(_Three-way merge_)
Lorsque tu fais un merge de ta branche ``feature/79975`` dans ``master v3``, Git utilise une __fusion à trois voies__, cela signifie qu'il va comparer trois états:
1. __L'ancêtre commun(``master v1``)__ -> Le point où ``feature/79975`` et ``master v3`` avaient le même état.
2. __La branche ``feature/79975``__ -> Contient les modifications que tu as apportées à partir de ``master v1``.
3. __La branche ``master v3``__ -> Contient les évolutions qui ont été faites après ``master v1``.

Git va examiner ces trois versions pour décider:
* __Quels changements peuvent être appliqués directement(sans conflits).__
* __Quels changements sont en conflit et nécessitent une intervention manuelle.__

### A - Cas des codes non modifiés dans feature/79975
Si certaines lignes de code dans ``feature/79975`` sont __restées exactement comme dans ``master v1``__, alors:
* Git détecte que __seule la branche master v3 a changé ces lignes__.
* __Git applique directement les changements de ``master v3``__ à ces lignes, car __ta branche ne s'y oppose pas.__

En resume, ceux qui sont important pour Git, __c'est le changements par rapport au dernière ancêtre commun des deux branche__, c'est ce qu'on appelle __la fusion à trois voies__.

### B - Cas des conflits lors du merge.
Git ne sait pas quelle version de code à garder, donc on doit résoudre le conflit manuellement.

# II - Outils et bonnes pratiques pour gérer les conflits.
#### Rebase avant le merge pour minimiser les conflits.
Avant de merger ta feature, il est recommandé de faire un ``rebase`` sur la dernière version de ``master``.
Cela te permet d'appliquer les changements progressivement et de gérer les conflits __au fur et à mesure__.
````git
git switch feature/79975
git rebase -i master
````
Cette commande va prendre la version la plus récente de la branche ``master``, puis appliquer les commits de ta brache ``rebase`` sur la branche ``feature/79975`` __un après l'autre__.
* Les commits de ta branche seront des "incoming"(logique, car ce sont eux qui vont être appliqué dans la branche ``master``).

S'il y a un commit crée un conflit:
1. Tu le résous.
2. Tu fais ``git add .``
3. Tu continues le rebase avec ``git rebase --continue`` pour __passer à commit suivant__.

Puis, tu reviens sur ``master`` et ut merges sans de conflits.

# III - Conclusion.
Ce point qu'on a discuté est une base __avancée__ de Git. Beaucoup des gens utilisent Git quoditiennement sans jamais vraiment se poser la question de __comment il gère les conflits en profondeur__. 

En résume: __Git ne regarde pas juste les différences entre ta branche et ``master``, mais il analyse les changements par rapport au dernier ancêtre commun__.
* Ce qu'on appelle __ancêtre commun__ est la dernière version où les deux branches étaient identiques.

__Moralité__: Git ne fusionne pas des fichiers mais des différences par rapport à un point commun!
