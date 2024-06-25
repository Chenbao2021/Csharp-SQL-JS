# SQL
#### Code
````sql
WITH ct AS (
	SELECT ROW_NUMBER() OVER (ORDER BY sea_passage_num ASC) AS [Index], *
	FROM nsh_voy_sea_passage sp
	WHERE sp.voyage_id = 287304
)
select [index] from ct where sea_passage_num = 2
````
Ce code donné un index à chaque ligne de la requête avec la clause where.

#### Un autre exemple avec sa explication 
````sql
SELECT 
    ROW_NUMBER() OVER (PARTITION BY Department ORDER BY Salary DESC) AS RowNum,
    EmployeeID,
    FirstName,
    LastName,
    Department,
    Salary
FROM 
    Employees
````
1. ``ROW_NUMBER()``: C'est une fonction de fenêtre qui attribue un numéro unique à chaque ligne dans le résultat de la requête. Ce numéro commence à 1 pour la première ligne de chaque partion.
2. ``OVER``: C'est une clause qui définit la manière dont les lignes sont partitionnées et ordonnées pour la fonction de fenêtre.
3. ``PARTITION BY Department`` : Cette partie de la clause ``OVER`` spécifie que les lignes adns chaque partition doivent être ordonnées par la colonne ``Salary`` en ordre décroissant.
Cela signifie que le salaire le plus élevé dans chaque département recevra le ``ROW_NUMBER()`` de 1.
4. Autres colonnes: ``EmployeeID``, ``FirstName``, ``LastName``, ``Department``, ``Salary`` sont simplement des colonnes supplémentaires que nous incluons dans le résultat de la requête.

#### Différents façons de partitionner une table
En SQL Server, la clause ``OVER`` permet de définir des fenêtres pour les fonctions de fenêtre telles que ``ROW_NUMBER()``, ``RANK()``, ``DENSE_RANK()``, ``NTILE()``.
La clause OVER utilise ``PARTITION BY`` et ``ORDER BY`` pour diviser le jeu de résultats en partition et appliquer la fonction de fenêtre à chaque partition individuellement.

Cas d'utilisation:
1. Partitionner par une seule colonnes
    * Partitionne le résultats par la colonne 'Department'
2. Partitionner par plusieurs colonnes
    * Partitionne les résultats par les colonnes 'Department' et 'JobTitle'
3. Sans partition (Une seule partition pour tout le jeu de résultats)
4. Partitionner par une expression
    ``ROW_NUMBER() OVER (PARTITION BY YEAR(HireDate) ORDER BY Salary DESC) AS RowNum``
5. Partitionner par une fonction de transformation
    ``ROW_NUMBER() OVER (PARTITION BY LEFT(FirstName, 1) ORDER BY Salary DESC) AS RowNum``
6. Partitionner par une sous-chaîne
    ``ROW_NUMBER() OVER (PARTITION BY SUBSTRING(LastName, 1, 3) ORDER BY Salary DESC) AS RowNum``
7. Partitionner par une date tronquée
    ``ROW_NUMBER() OVER (PARTITION BY YEAR(HireDate), MONTH(HireDate) ORDER BY Salary DESC``
8. Partitionner par une combinaison de constantes et de colonnes (Utiliser const comme une commentaire)
    ``ROW_NUMBER() OVER (PARTITION BY 'ALL', Department ORDER BY Salary DESC) AS RowNum``

#### Qu'est ce qu'une fonction de fenêtre
Une fonction de fenêtre, ou fonction analytique, est une fonction SQL qui permet de calculer des valeurs sur un ensemble de lignes liées à la ligne courante sans avoir besoin de grouper les résultats.
Les fonctions de fenêtre sont spécifiées en utilisant la clause ``OVER``, qui détermine la fenêtre ou le cadre sur lequel la fonction s'applique.

__Caractéristiques des fonctions de fenêtre__
1. Calcul sur plusieurs lignes: Elles permettent de calculer des valeurs sur pluseurs lignes tout en conservant l'accès à chaque ligne individuelle.
2. Partitionnement et ordonnancement: Elles peuvent partitionner les résultats en groupes logiques et ordonner les lignes dans chaque groupe.
3. Pas de regroupement nécessaire: Contrairement aux fonctions d'agrégation(Comme 'SUM', 'AVG'), les fonctions de fenêtre n'exigent pas que les lignes soient regroupées.

__Exemples dse fonctions de fenêtre__
1. __ROW_NUMBER__: Attribue un numéro de ligne unique à chaque ligne dans chaque partition. Les valeurs ex aequo reçoivent des numéros de ligne différents, sans tenir compte de l'égalité
2. __RANK__ : Comme ROW_NUMBER, mais en tenir compte des ex aequo, c'est à dire les valeurs identiques reçoivent le même rang, et le rang suivant est incrémenté en conséquence.
3. __SUM__ : Calcule la somme des salaires par département
4. __LAG__ : Accéde à la valeur de la ligne précédente dans la même partition.
