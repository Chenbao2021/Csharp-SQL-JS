*******
# I - Introduction
*******
DDL est un sous-ensemble de SQL, c'est un langage qui permet de décrire les données et __lerus relations__ dans une base de données.
- Structure de la base de données
- C'est un ensemble des commandes SQL utilisés pour créer, modifier et supprimer la structure de la base de données, et pas pour manipuler les données.
- CREATE/ DROP/ ALTER/ TRUNCATE/ COMMENT/ RENAME

***
# II - Table manipulation
***
#### 1 - CREATE TABLE
#### 2 - DROP TABLE
#### 3 - CHANGE TABLE NAME
#### 4 - ALTER TABLE ADD Column
```
ALTER TABLE table_name
ADD 
    column_name_1 data_type_1 column_constraint_1,
    column_name_2 data_type_2 column_constraint_2,
    ...
```
- First, specify the name of the table in which you want to add the new column
- Second, specify the name of the column, its data type, and constraint if applicable. 

#### 5 - ALTER TABLE ALTER COLUMN
SQL Server allows you to perform the following changes to an existing column of a table:
- Modify the data type
- Change the size
- Add a __NOT NULL__ constraint
    ```
    ALTER TABLE table_name
    ALTER COLUMN column_name new_data_type(size) NOT NULL;
    ```    
***

#### 6 - ALTER TABLE DROP COLUMN
```
ALTER TABBLE table_name
DROP COLUMN column_name;
```
- Delete the constraint first before removing the column

# III - Identity column
***
```
IDENTITY[(seed, increment)]
```
- The __seed__ is the value of the first row loaded into the table.
- The __increment__ is the incremental value added to the identity value of the previous row.
- The default value of couple is (1,1)

SQL Server does __not reuse__ the identity values, if you insert a row into the identity column and the insert statement is failled or rolled back, then the identity value is lost and will not be generated again.

*** 
# IV - Server Sequence 
***
A sequence is simply a list of numbers, in which their orders are important.
For example, the {1, 2, 3} is a sequence, while the {3, 2, 1} is an entirely different sequence.

Sequence, different from the identity columns, are not associated with a table.
__A sequence can be shared accross multiple tables__

When to use sequences :
- The application requires a number before inserting values into the table.
- The application requires sharing a sequence of numbers across multiple tables or multiple columns within the same table.
- The application requires to restart the number when a specified value is reached.
- ...

***
# V - SQL Server TRUNCATE TABLE
***
Sometimes, you want to delete all rows from a table. In this case, you typically use the __DELETE__ statement without a __WHERE__ clause.
```
DELETE FROM sales.customer_groups;
```
But beside the __DELETE FROM__ statement, you can also use __TRUNCATE TABLE__ to delete all rows from a table.
```
TRUNCATE TABLE sales.customer_groups;
```

The __TRUNCATE TABLE__ has the following advantages over the __DELETE__ statement:
(
    A SQL transaction is a grouping of one or more SQL statements that interact with a database,
    A transaction in its entirety can commit to a database as a single logical unit or rollback(become undone) as a single logical unit.
    <=> An atomic that involves multiple stateemnts, queries, and operations.
) 
1. Use less transaction log
    - The __DELETE__ statement removes rows one at a time and inserts an entry in the transaction log for each removed row.
    - The __TRUNCATE TABLE__ statement deletes the data by deallocating the data pages used to store the table data and inserts only the page deallocation in the transaction logs.(It doesn't check rows one by one).
2. Use fewer locks
3. Identity reset
    - If the table to be truncated has an identity column, the counter for that column is reset to the seed value when data is deleted by the __TRUNCATE TABLE__ statement but not the __DELETE__ statement.

# VI - SQL Server Computed Columns
To query the full names of people in the __persons__ table, you normally use the __CONCAT()__ function or the __+__ operator as follows:
```
SELECT 
    person_id, 
    first_name + ' ' + last_name AS full_name,
    dob
FROM
    persons
ORDER BY
    full_name;
```
But add the __full_name__ expression in every query is not convenient.
There is a feature called : computed columns , that allows you to add a new column to a table with the value derived from the values of other columns in the same table.
```
ALTER TABLE persons
ADD full_name AS (first_name + ' ' + last_name)
```
- Every time you query data from the __person__ table, SQL Server computess the value for the __full_name__ column based on the expression and returns the resumt.
- SQL Server physically stores the data of the computed columns on disk.
- When you change data in the table, SQL Server computes the result based on the expression of the computed columns and stores the results in these persisted columns physically.
- When you query the data from the persisted computed columns, SQL Server just needs to retrieve data without doing any calculation.

***
# VII - Server Temporary Tables
***
Temporary tables are tables that exist temporarily on the SQL Server.
- Useful for storing the immediate result sets that are accessed multiple times.

There are two ways to create temporary tables :
- Via __SELECT INTO__
- Via __CREATE TABLE__

The name of a temporary table always begin by : #
(For a global temporary table, the name begins by : ##)

SQL Server drops a temporary table automatically when you close the connection that created it.
SQL Server drops a global table once the connection that created it closed and the queries against this table from other connection completes.
Or Manual deletion by __DROP TABLE__

***
# VIII - Synonym in SQL Server
*** 
A synonym is an alias or alternative name for a database object such as a table, view, stored procedure, user-defined function, and sequence. 
```
CREATE SYNONYM [schema_name_1.] synonym_name
FOR object;
```
- Specify the target object that you want to assign a synonym in the __FOR__ clause.
- provide the name of the synonym after the __CREATE SYNONYM__ keywords

Example
```
CREATE SYNONYM suppliers
FOR test.purchasing.suppliers;
````

Listening all synonyms of a database:
````
SELECT name, base_object_name, type
FROM sys.synonyms
ORDER BY name
````

When to use synonyms :
- Simplify object names : If you refer to an object from another database (even from a remote server), you can create a synonym in your database and reference to this object as it is in your database.
    
- Enable seamless object name changes : When you rename a table or any other object(View, Stored Procedure, User-defined function, etc.), the existing database objects that reference to this table need to be manually modified to reflect the new name.Beside, all current applications that use this table need to be changed and possibly to be recompiled.
To avoid this, you can __rename the tabla and create a synonym for the old name__ to keep existing applications function properly. 

***
# IX - What is a schema in SQL Server 
***
#### Introduction
- A schema is a collection of database objects including tables, views, triggers, stored procedures, indexes, etc.
- A schema is associated with a username which is known as the schema owner, who is the owner of the logically related database objects.
- A schema always belong to one database, a database may have one or multiple schemas.
- An object within a schema is qualified using the __schema_name.object_name__, two tables in two schemas can share the same name.

The __CREATE SCHEMA__ statement allow you to create a new schema in the current database.
```
CREATE SCHEMA schema_name
    [AUTHORIZATION owner_name]
```

To list all schemas in the current database:
```
SELECT 
    s.name AS schema_name, 
    u.name AS schema_owner
FROM 
    sys.schemas s
INNER JOIN sys.sysusers u ON u.uid = s.principal_id
ORDER BY 
    s.name;
```
After create a shcema, we can create objects for the schema:
```
CREATE TABLE customer_services.jobs(
    job_id INT PRIMARY KEY IDENTITY,
)
```
####  Alter schema
The __ALTER SCHEMA__ statement allows you to transfer a securable from a shcema to another within the sama database.
(A securable = A resource to which the Database Engine authorization system control access, for instance, a table is a securable)

It is recommended to drop and recreate procedure, function, view or trigger in the new schema instead of using the __ALTER SCHEMA__ statemenet for moving.

Exemple :
1. Create a new table named __offices__ in the __dbo__ schema and insert some rows: 
    ```
    CREATE TABLE dbo.offices
    (
        office_id INT PRIMARY KEY IDENTITY,
        office_name NVARCHAR(40) NOT NULL,
        office_address NVARCHAR(255) NOT NULL,
        phone VARCHAR(20)
    )
    ```
2. Create a stored procedure that finds office by office id
    ```
    CREATE PROC usp_get_office_by_id (
        @id INT
    ) AS
    BEGIN
        SELECT *
        FROM dbo.offices
        WHERE office_id = @id;
    END
    ```
3. After that, transfer this __dbo.offices__ table to the __sales__ schema:
    ```
    ALTER SCHEMA sales TRANSFER OBJECT::dbo.offices;
    ```
4. Then, manually modify the stored procedure to reflect the new schema:
    ```
    FROM dbo.offices -> FROM sales.offices
    ```



