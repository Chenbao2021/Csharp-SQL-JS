***
#### I - Introduction
***
##### 1 - Définition du mot
Une collection est un ensemble d'objets ou d'éléments regroupés selon un critére commun ou une caractéristique partagée.
Ces objets peuvent être de nature diverse : Livres, pièces de monnaie, oeuvres d'art, timbre, etc.

##### 2 - En C#
Une collection fait référence à une structure de données qui permet de stocker et de manipuler un groupe d'objets de manière efficace.
Les collections en C# peuvent prendre différentes formes, telles que les listes, les dictionnaires, les ensembles , les piles et les files, etc. 
Ces collections fournissent des fonctionnalités pour ajouter, supprimer, rechercher et parcourir les éléments qu'elles contiennent.
***
#### II - Difference entre __Collection__ et __IEnumerable__
***
En C# :
- IEnumerable est une interface qui fournit une façon standard de parcourir les éléments d'une collection. Elle définit une méthode GetEnumerator() qui renvoie un énumérateur(IEnumerator) permettant d'itérer sur les éléments de la collection.
- Collection en C# est une structure de données qui stocke un groupe d'objets. 

Nombreuses collections en C# implémentent l'interface IEnumerable.Cela signifie que ces collections peuvent être utilisées dans des contextes où IEnumerable est attendu.  
Par exemple:
-   Boucle foreach 
-   Méthode LINQ.

De nombreuses structures de collection standard de C# implémentent l'interface IEnumerable, par exemple:
- List<T>
- Dictionary<TKey, TValue>
- HashSet<T>
- Queue<T>
- Stack<T>

***
#### III - Différence entre .Cast() et .ToList()
***
##### 1 - Cast<T>()
Cette méthode est utilisé pour convertir une collection non générique en une collection générique de type spécifié.
Elle est utile lorsque vous avez une collection qui contient des éléments d'un type de base ou non générique, et que vous voulez la convertir en une collection de type générique.
````
ArrayList list = new ArrayList();
list.Add("un");
list.Add("deux");
list.Add("trois");

List<string> stringList = list.Cast<string>().ToList();
````

##### 2 - ToList()
Pour créer une nouvelle list à partir des éléments d'une séquence.
Il est souvent utilisé avec LINQ pour matérialiser les résultats de requêtes LINQ dans une liste.

- séquence : Une collection d'éléments qui peuvent être itérés séquentiellement, mais qui ne spécifie pas nécessairement une structure de données spécifique. Il peut être une liste, un tableau, une collection, ou même une séquence générée par une expression LINQ.
    Une séquence peut se référer à n'importe quelle collection d'éléments.

***
#### IV - Exemple pratique
***
````
dtParams.Columns.Cast<DataColumn>()
.Where(column => column.ColumnName.StartsWith("$"))
.ToList()
.ForEach(column => column.ColumnName = column.ColumnName.Substring(1));
````
1. '.Cast<DataColumn>()' avant '.Where()': La méthode '.Where()' est une méthode d'extension LINQ qui opère sur des __séquences génériques__ , cependant la collection 'DataTable.Columns' n'est pas générique, elle retourne un 'DataColumnCollection', qui n'implémente pas 'IEnumerable<DataColumn>', mais plutôt 'IEnumerable'. => Donc on doit d'abord convertir la collection en une séquence de DataColumn a l'aide de '.Cast<DataColumn>()'.
2. '.ToList()' avant '.ForEach()': La méthode '.ForEach()' est une méthode qui agit sur une __liste générique__. Or on a une IEnumerable<T>, donc on doit appliquer '.ToList()' pour convertir la séquence résultante en une liste générique afin de pouvoir utiliser la méthode '.ForEach()'.