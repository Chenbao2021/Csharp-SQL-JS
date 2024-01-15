# SQL
***
#### 1 - Variable table et les tables temporaires
***
Tables temporaires
- Des objets crées par l'utilisateur dans tempDB, et généralement destinés à fournir un espace de travail __pour les résultats intermédiaires lors du traitement de données par une requête__.
- Traitées comme des tables permanentes (SELECT, INSERT, UPDATE, DELETE)
- Il existe deux types de table temporaire
    1. Les tables temporaires loacles
        - Crée avec le préfixe  __"#"__
        - Leur portée se limite à la session de l'utilisateur qui les a crées . (i.e. Seulement visibles de leur créateur)
        - N'est détruite qu'à la déconnection de la session de l'utilisateur qui l'a créée.
    2. Les tables temporaires globales
        - Créée avec le préfixe __"##"__.
        - Leur portée se limite à toutes les sessions de l'instance au sein de laquelle elles ont été créées. (Visible de toutes les sessions.)
        - N'est détruite que lorsque toutes les sessions qui la référencent seront déconnectées.
```
CREATE TABLE #Tab(
    Tab_ID int NOT NULL,
    Tab_Name VARCHAR(20)    
)
GO
```
Variable table
- Sont des objets temporaires créés par l'utilisateur dans tempDB, comme temporaire tables.
- La portée d'une table variable se limite à la durée de la requête au sein de laquelle elle a été créée.
- Il n'est pas possible de :
    - Modifier
    - Traoncature
    - SELECT INTO
    - 
```
DECLARE @Tab TABLE ( 
    Tab_ID int NOT NULL, 
    Tab_Name VARCHAR(20)   
)
```

En générale, une table temporaire est utilisé pour traiter un gros volume de données( comme suppression de masse avec TRUNCATE ). 

***
#### 2 - INSERT INTO AND SELECT INTO
***
Insert INTO SELECT
- Copies data from one table and inserts it into another table. => The target table exists in the database before data can be traited.
- Requires that the data types in source and target tables match
- The existing records in the target table are unaffected.
- Copy all columns from one table to another table:
    ```
    insert into table2
    SELECT * FROM table1
    WHERE condition;
    ```
- Copy only some columns from one table into another table:
    ```
    INSERT INTO table2(column1, column2, ...)
    SELECT column1, column2, ...
    FROM table1
    WHERE condition;
    ```

SELECT INTO
- Copy data from one table to a new table
- The new table does not need to exist in the database to transfer data from the source table. => They will be automatically created.
- Cannot bet used to insert data into an existing table.
- Does not copy constraints such as primary key and indexes from the source_table to the new_table.

Exemple (Formation SQL2: SELECT INTO + OPENJSON)
```
SELECT dbo.F_cyu_translation(nom_critere) as nom_critere, type_critere, valeur_critere
__INTO__ #tmp_critere
FROM OPENJSON(@json_criteria)
WITH (
    nom_critere varchar(max) '$.name',
    type_critere varchar(max) '$.type',
    valeur_critere varchar(max) '$.value'
)

```
*** 
#### 3 - DELETE, TRUNCATE, DROP
*** 
DELETE
- DELETE is a DML(Data manipulation Language) command
- Used to delete one or more rows(records) from a table .
- Used only to remove data from the table.
- After execution, returns the total number of rows removed from the table.

TRUNCATE
- TRUNCATE is a DDL(Data Definition Language) command that is used to delete all data from a table without removing the table structure.
- Used only to remove data from the table, not to remove a table from the database.
- It does not have a where clause.
- It resets auto-increment.
- Faster than DELETE(It doesn't scan every record before removing it)

DROP
- A DDL command
- Remove data stored in a table , and table structure , indexes, constraints and triggers from a database.


***
#### 4 - Ajouter "()" pour une SELECT.
***
Ajouter des parenthèses pour obtenir la valeur résultat d'une requête SELECT :
```
UPDATE #tab_information(countRegion)
SET countRegion = (SELECT COUNT(1) FROM #region_filter )
```

***
#### 5 - View is a permanant object, that is stored in the databse. We don't create a view inside a stored procedure.
***
If we do it in a stored procedure, what if the stored procedure is run twice ?
The purpose of a stored proc is encapsulate reusable code.


# C#
- CTR + K + K : Permet de mettre une marque dans une ligne de code (marche dans Visual studio et sql server)



