
# SQL
#### I - Faire SELECT avant de faire UPDATE ou DELETE,
Car si SELECT marche correctement, alors UPDATE et DELETE marchent aussi.

```
// 1 - Vérifier si les lignes sont bien ceux qu'on attend.
SELECT c.last_name
FROM tab_name c
WHERE c.id = 2;
// 2- SELECT marche correctement(i.e la condition WHERE est correcte), on update les données : 
UPDATE c SET c.last_name = 'saidi'
FROM tab_name c
WHERE c.id = 2;
```

#### II - Ajouter une colonne s'il n'existe pas, et une valeur par défaut
- COL_LENGTH(tabName, columnName) : Retourne la longueur défini d'une colonne en octet. (ex : varchar(40) -> 40)
```
IF COL_LENGTH('formation_employee_cyu', 'status') IS NULL
    ALTER table formation_employee_cyu ADD STATUS CHAR(1) NOT NULL DEFAULT 'A'
GO
``` 

#### III - En SQL Server, quand il y a plusieurs paramètres pour un procedure, on les sépare par des ','
    ```
    EXEC CYU_SQL_PlusPeuplee_003_V2_Dynamic @x=10, @yyyy = 2012;
    ```
#### IV - LIMIT VS OFFSET ... FETCH ... VS TOP()
En SQL SERVER , il n'y a pas de LIMIT statement,
mais on peut utiliser TOP() ou OFFSET..FETCH à la place.
TOP est plus simple à utiliser, et OFFSET FETCH peut être utilisé pour implémenter la pagination.

Dans TOP on peut utiliser CASE pour des cas différents:
```
SELECT TOP(
		CASE
			WHEN COL_LENGTH('villes_france_free', 'ville_population_' + @YYYY) IS NOT NULL THEN @X
			ELSE 0
		END
)
...
```

#### V - Supprimer une contrainte
Lorsqu'on veut supprimer une colonne d'une table, on peut rencontrer cet erreur:
```
Msg 5074, Niveau 16, État 1, Ligne 112
The object 'DF__formation__STATU__091F48A3' is dependent on column 'status'.
```
En effet le 'DF__formation__STATU__091F48A3' est une contrainte, et on doit la supprimer avant de supprimer la colonne:
```
ALTER TABLE formation_Employee_cyu
DROP CONSTRAINT [DF__formation__STATU__091F48A3];
```
Et finalement:
```
ALTER TABLE formation_Employee_cyu
DROP COLUMN status
```

#### VI - OVER clause + PARTITION BY
OVER            :La clause OVER définit une fenêtre ou un ensemble de lignes spécifié par l'utilisateur dans un jeu de résultats de requête
PARTITION BY    :Qui divise le jeu de résultats de la requête en partitions.

#### VII - SET AND CASE
We can use UPDATE AND SET together as below:
```
UPDATE cyu
SET cyu.status = 
    CASE
        WHEN cyu.status = 'A' THEN 'L'
        WHEN cyu.staus = 'L' THEN 'D'
    END
FROM
    fromation_Employee_cyu cyu
WHERE 
    id = @id
```

# C#
#### I - DisableForeColor
Fore = Devant
Couleur du text de l'edit disable, c'est le couleur du texte quand la proprieté "Enabled" est fausse.

#### II - VerifyScreen
C'est une fonction qui permet de vérifier si l'écran est correctement implementé, il vérifie surtout deux points:
- Si les valeurs de combos sont correctes
- Si les Edits qui sont Mandatory sont correctements remplit.

    