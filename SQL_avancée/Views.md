***
# I - Introduction
***
1. A view is a named query stored in the database catalog that allows you to refer it later. Useful for abstracting or hiding complex queries.
2. By definition, a view do not share data except for indexed views.
3. Advantages of view:
    - Security  : Restrict users to access directly to a table and allow them to access a subset of data via views
    - Simplicity: Simplify __the complex query__ (with joins and conditions) using a set of views
    - Consistency: Using a nice and readable view Name in a query rather than repeating a complex formulan or logic.

# II - Create VIEW
```
CREATE VIEW [OR ALTER] schema_name.view_name[(column_list)]
AS
    select_statement;
```
For example, A view based on the jonction of three tables:
```
CREATE VIEW sales.daily_sales
AS
SELECT
    year(order_date) AS y,
    month(order_date) AS m,
    day(order_date) AS d,
    p.product_id,
    product_name,
    quantity * i.list_price AS sales
FROM
    sales.orders AS o
INNER JOIN sales.order_items AS i
    ON o.order_id = i.order_id
INNER JOIN production.products AS p
    ON p.product_id = i.product_id;
```
Then we can query data against the underlying tables using a simple __SELECT__ statement:
```
SELECT 
    * 
FROM 
    sales.daily_sales
ORDER BY
    y, m, d, product_name;
```

# III - Drop VIEW
```
DROP VIEW [IF EXISTS] schema_name.view_name;
```
Exemple of removing several views at once:
```
DROP VIEW IF EXISTS
    sales.staff_sales,
    sales.product_catalogs;
```

# IV - Rename View
- Must know: All objects that depend on the view mat fail, like stored procedures, user_defined functions, triggers, queries, other views, and client applications.
- Therefore, after renaming the view, we must ensure that all objects that reference the view's old name use the new name.
- Rename view using Server Server Management Studio(SSMS):
    1. In object Explorer, expand the Database, choose the database name which contrains the view that you want to rename and expand the views folder.
    2. Right-click and select Rename
    3. Enter a new name
- Reaname view programmatically :
    ```
    EXEC sp_rename
        @objname = 'sales.product_catalog'
        @newname = 'product_list'
    ```
    - sp_rename is a stored procedure

# V - List Views
To list all views in a SQL Server Database, we query the sys.views or sys.objects catalog view.
```
SELECT 
    OBJECT_SCHEMA_NAME(v.object_id) schema_name,
    v.name
FROM
    sys.views as v;
```
- OBJECT_SCHEMA_NAME() : Get the schema names of the views.

#VI - Get information about a View in SQL Server
1. To get the information of a view, we use the system catalog __sys.sql_module__ and the __OBJECT_ID()__ function:
    ```
    SELECT
        definition,
        uses_ansi_nulls,
        uses_quoted_identifier,
        is_schema_bound
    FROM
        sys.sql_modules
    WHERE
        object_id
        = object_id(
                'sales.daily_sales'
            );
    ```
    - OBJECT_ID() : Returns an identification number of a __schema-scoped__ database object.

2. We can also use the sp_helptext stored procedure
    ```
    EXEC sp_helptext 'sales.product_catalog' ;
    ```
3. Using OBJECT_DEFINITION() and OBJECT_ID():
    ```
    SELECT OBJECT_DEFINITION(OBJECT_ID('sales.staff_sales')) view_info;
    ```

#VII - Indexed View
Regular SQL Server views are the saved queries, they don't improve the underlying query performance.

Unlike regular views, indexed views are materialized views that stores data physically like a table hence may provide some the performance benefit if they are used appropriately.

To create a such indexed view:
1. Create a view that uses the __WITH SCHEMABINDING__ option which binds the view to the schema of the underlying tables.
2. Create a unique clustered index on the view, this materialized the view.

Because of the __WITH SCHEMABINDING__ option, if you want to change the structure of the underlying tables which affect the indexed view's definition, you must drop the indexed view first before applying the changes.

When the data of the underlying tables changes, the data in the indexed view is also automatically updated.
Example:
1. Create view with "WITH SCHEMABINDING" option:
    ```
    CREATE VIEW product_master
    WITH SCHEMABINDING
    ```
2. Before creating a unique clustered index for the view, let's examine the query I/O cost statistics by quering data from a regular view and using the __SET STATISTICS IO__ command:
```
SET STATISTICS IO ON
GO

SELECT 
    * 
FROM
    production.product_master
ORDER BY
    product_name;
GO 
```
3. Add a unique clustered index to the view
```
CREATE UNIQUE CLUSTERED INDEX 
    ucidx_product_id 
ON production.product_master(product_id);
```

