# SQL
***
## I - Difference entre CROSS APPLY(Dynamique) et JOINTURE(Statique).
Definition
* Nature
    * Une __jointure__ est utilisée pour combiner des lignes de deux tables basées sur une condition spécifiée. 
    * ``CROSS APPLY`` renvoie une combinaison des lignes de la table de gauche avec celles renvoyées par une expression de table de droite, qui est écakyée oiyr chaque ligne de la table de gauche.

* __Similitude avec les jointures__:``CROSS APPLY`` est utile lorsque vous avez une expression de table ou une fonction qui renvoie un jeu de résultats variable pour chaque ligne. __C'est une fonctionnalité qui va au-delà des capacités des jointures traditionnelles__.
    
* ``CROSS APPLY`` : Si la partie droite de l'expression retourne rien, la partie gauche sera exclut.

Exemple qui illustre l'incapabilité de jointure face à certains cas
* __Sujet :__ Récupérer les deux meilleures ventes (en termes de montant) pour chaque employé
* Une **join** nous permettrait de récupérer toutes les ventes d'un employé, mais pas de sélectionner un nombre spécifique de ligne.(Ici, les deux meilleures ventes)
    * On peut le faire avec ``ROW_NUMBER()`` dans une ``CTE``, mais compliqué et nécessite plus des étapes.
* Solution avec CROSS APPLY:
    ````SQL
    SELECT e.EmployeeID, e.Name, s.SaleID, s.SaleAmount
    FROM Employees e
    CROSS APPLY (
        SELECT TOP 2 SaleID, SaleAmount
        FROM Sales
        WHERE Sales.EmployeeID = e.EmployeeID
        ORDER BY SaleAmount DESC
    ) s;
    ````