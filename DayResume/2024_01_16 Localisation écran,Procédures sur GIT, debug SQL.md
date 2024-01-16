# SQL
***
#### 1 -  Astuce pour debug
***
1. Passer une variable debug, et dans le code sql, on execute l'opération si la variable debug vaut 0.
    C'est à dire si on fait des tests, on met la variable debug = 1, comme ça, certains opérations ne seront pas provoqué.
````
if @debug = 0
BEGIN
    ...
END
````

***
#### 2 - Recuperer les procédures depuis GIT
***
1. Conencter : http://vs-meta-tfs02.domaine.formatel.fr:8080/tfs/Totsa/Med/Metafactory/_git/Sql
2. Deuxième ligne à gauche : clique scrollbar , puis selectionne "Sql"
3. Deuxième ligne à droite : cliquer sur Cloner, copier ligne de commande HTTP
4. Ouvrir Visual Studio, puis premier ligne milieu gauche, Git, puis cloner un dépôt
5. Dans le chemin, on met toujorus : D:\Git\Totsa\Med\SQL
6. En bas à droite, l'icon crayon, peut montrer les modifications effectués

***
#### 3 - Avant chaque modification des codes SQL dans le cloud
***
1. Dans la page modification GIT, haut droite, on a quatre icons, 
2. Avant de envoyer un code dans le serveur : Recuperer / Tirer / Synchroniser / Envoyer.


# C#
***
#### 1 - Private puis internal puis public
***
Private : la méthode/variable n'est visible qu'à l'intérieur de la classe.
Intrnal : Les types ou les membres __internal__ sont accessible par du code faisant partie de la même compilation.
Public  : La méthode ou variable est accessible partout.

En effet, comment deux fichiers sont dans la même compilation(même assembly) ?
- __Projet unique__ : Les fichiers source faisant partie d'un même projet sont généralement compilés dans un même assembly.
    Un projet peut être considéré comme une unité logique regroupant plusieurs fichiers source.
- __Espace de noms(namespace)__ : Les fichiers qui partagent le même espace de noms ont tendance à appartenir au même assembly
- Configuration de compilation. 
- Référence partagées. 
- Assemblage explicite.

***
#### 2 - Ne jamais accéder à une colonne par index, toujours par un enum privé.
***
- La valeur de l'énum c'est l'index de la colonne
- Aucun accès externe a cette datable ne doit être fait en dehors de cette classe de gestion. Ce sont des get/set de la classe qui manipuleront la datatable.
    List[0] => List.FirstOrDefault()

***
#### 4 - Localisation d'un écran 
***
Quand on est dans Medissys , et on veut connaître la référence et localisation d'un écran :
1. On click __F5__ pour ouvrir une nouvelle fênetre qui permet de visualiser tous les écrans qui sont ouverts.
2. En bas , on trouve un bouton __Local Screen__, et c'est la où on peut trouver le dll de l'écran.(Ex: MEDC_MappingRef.FrmReferentiel)
3. N'oublies pas de faire 1 maj de ressource.



