* __git status__ : Fournit une vue d'ensemble de l'état du répertoire de travail et de l'index, indiquant quels fichiers ont été modifiés, lesquels sont prêts à être commis, et lesquels ne sont pas suivis.
* __git diff --staged__ : Montre les différences entre le staging area et la dernière version commise, permettant de voir exactement ce qui sera inclus dans le prochain commit.

# git status
La commande __git status__ fournit des informations sur l'état du répertoire de travail et de l'index (staging area). Elle est utilisée pour voir :
* Quels changements ont été apportés.
* Quels sont prêts à être commis.
* Quels ne le sont pas encore.

####  Détaillés
Informations affichés
* __Current Branch__: Affiche la branche sur laquelle vous travaillez actuellement
* __Uncommitted Changes__: Indique s'il y a des modifications non commises dans le répertoire de travail ou le staging area.
* __Changes to be Committed__ (staged Changes): Affiche les fichiers qui ont été ajoutés à l'index et sont prêts à être commis.
* __Changes Not Staged for Commit__ : Liste les fichiers qui ont été modifiés mais ne sont aps encore ajoutés à l'index.
* __Untracked Files__ : Montre les fichiers qui ne sont pas suivis par Git.

# git diff --staged
La commande __'git diff -staged'__ affiche les différence entre les fichiers qui sont actuellement dans le staging area et la dernière version commise de ces fichiers.
Cela permet de voir exactement quelles modifications seront incluses dans le prochain commit.

#### Différences
La sortie montre les différences entre l'__index__(staging area) et la __HEAD__ (le dernier commit sur la branche active). Les modifications sont affichées de manière similaire à un diff Unix standard, avec des lignes ajoutées, modifiées ou supprimées.

Supposons que vous avez un fichier example.txt et que vous avez ajouté quelques lignes et supprimé d'autres, __après__ avoir utilisé __git add example.txt__, la commande git diff --staged pourrait afficher :
````
diff --git a/example.txt b/example.txt
index 83db48f..f7351cd 100644
--- a/example.txt
+++ b/example.txt
@@ -1,4 +1,4 @@
-Hello, this is an example.
+Hello, this is a modified example.
 This file demonstrates how git diff works.
 We added some lines.
-And removed some lines.
+And changed some lines.
````

#### Utilisation Conjointe
En combinant 'git status' et 'git diff --staged', vous pouvez gérer efficacement les changements:
1. Vérifier l'état actuel: git status
2. Ajouter les changements au staging area: git add example.txt
3. Vérifiez les différences avant de commiter: git diff --staged
4. Créez un commit avec les changements validés: git commit -m "Update example.txt with new content"