# SQL
### I - GO statement
GO n'est pas une commande SQL, sinon une commande SSMS ou SQLamd.

__Cette comamnde permet de forcer l'envoie du lot de commande SQL et d'attendre le retour du serveur avant de poursuivre.__

Exemple : On ajoute __GO__ après CREATE TABLE et avant CREATE VIEW pour assurer le bonne fonctionnement des requêtes:
```
CREATE TABLE TOTO(C INT);
GO
CREATE VIEW 
    TITI
AS SELECT * FROM TOTO; 
```

### II - WITH statement
WITH let you name a SQL statement which might be one of the following statement "SELECT", "INSERT", "UPDATE", "DELETE".

Exemple : 
```
WITH
    statement_Name
AS (
    SELECT requester_id as id from requestAccepted
    UNION ALL
    SELECT accepter_id as id from requestAccepted
)

SELECT 
    id
FROM 
    statement_name
```
### III - Undefined Column name: Dynamic query or Case 
We have this query to do :

Une procédure stockée pour obtenir le nom des X villes les plus peuplées en YYYY, ainsi que le nom du département associé (X = un entier, YYYY = une année)

We have YYYY as a parameter , and we need the value of YYYY to decide which column of population to use(We have 3 columns that represent populations on 1999, 2010 and 2012).

#### Case : 
```
...
    ORDER BY
        CASE
			WHEN @YYYY = '1999' THEN v.ville_population_1999
			WHEN @YYYY = '2010' THEN v.ville_population_2010
			WHEN @YYYY = '2012' THEN v.ville_population_2012
			ELSE v.ville_population_2012
		END DESC
...
GO
```
#### Dynamic query :
```
CREATE PROCEDURE CYU_SQL_PlusPeuplee_003_V2_Dynamic
	@X VARCHAR(255),
	@YYYY VARCHAR(20)
AS -- D:\tuto_SQL\CYU_FORMATION_001.sql
BEGIN
	DECLARE @DynamicSQL NVARCHAR(MAX);
	SET @DynamicSQL =	
	'
	SELECT TOP(' + @X + ')
        v.ville_nom AS nom,
        d.departement_nom AS ''departement nom''
    FROM
        villes_france_free v
    LEFT JOIN
        departement d
        ON v.ville_departement = d.departement_code
    ORDER BY
		v.ville_population_' + @YYYY + ' DESC';

	EXEC sp_executesql @DynamicSQL;
END
GO
```
### IV - Insert Syntax
```
DROP PROCEDURE CYU_FORMATION_INSERT_EMPLOYEE
GO
CREATE PROCEDURE CYU_FORMATION_INSERT_EMPLOYEE
	@first_name		VARCHAR(255), 
	@last_name		VARCHAR(255),
	@start_date		DATETIME,
AS
BEGIN
	INSERT INTO formation_Employee_cyu(first_name, last_name, start_date)
	VALUES
	(@first_name, @last_name, @start_date)
END
GO
```

### V - Add column and Delete column in a table

Exemple of ADD column
```
ALTER TABLE formation_Employee_cyu
ADD status char(1) NULL, user_update VARCHAR(255), date_update datetime
```

Exemple of Delete column
```
ALTER TABLE formation_Employee_cyu
DROP COLUMN status, user_update, date_update
```

# C#
### I - F7 to display codes in Visual Studio Code

### II - About the function DB.DataSetLoadWithParams(procedure_name, dicProcsParams).
- procedure_name must be the name of a procedure that stored in the data base.
- dicProcsParams is a Dictionary Object that contains couple of values, and the key of these values must be identite as the name of parameters of procedure.
- If there is any SELECT statement in the Procedure, then the query result will be returned , and we can use it by these steps:
```
  DataSet dsRetour = DB.DataSetLoadWithParams(sqlQuery, dicProcsParams);
  if (sqlQuery == "CYU_FORMATION_INSERT_EMPLOYEE") {
      m_iSelectedId = dsRetour.Tables[0].Rows[0].Field<int>("id");
  }
```

### III - DataTable and Linq requêtes
Pourquoi LINQ? Pour fasciliter les développeur C# d'intéragir avec la base de données.

Si DataSet = Base de données, DataTable = Table, alors LINQ = SQL .

Exemple:
```
    private DataTable m_dtSourceEmployee;

    bool nameExist = m_dtSourceEmployee
        .AsEnumerable()
        .WHERE(row => row.Field<string>("first_name") == "YU")
        .Count() != 0;
```

In LINQ, AsEnumerable() method is used to convert the specific type of given list to its IEnumerable() equivalent type.

Il faut que la classe implémente IEnumerable pour activer la foreach syntaxe.
Et ici, il faut utiliser .AsEnumerable pour pouvoir appeler la méthode .WHERE() .
    