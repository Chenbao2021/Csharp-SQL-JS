***
# I - Stored Procedure Basics
***
We use a stored procedure to wrap a query:
```
CREATE PROCEDURE useProductList
AS
BEGIN
    SELECT product_name, list_price
    FROM production.products
    ORDER BY product_name
END;
```
- useProductList = The name of the stored procedure
- AS : Separates the heading and the body of the stored procedure
- BEGIN, END : If the store procedure has one statement, they are optional. 
- The procedure is stored in the database catalog once executed.

To modify a stored procedure, we use ALTER :
```
 ALTER PROCEDURE uspProductList
    AS
    BEGIN
        ...
    END;
```

To delete a store procedure, we use the drop procedure:
```
DROP PROCEDURE sp_name;
```

***
# II - Parameters
***
```
ALTER PROCEDURE uspFindProducts(@min_list_price AS DECIMAL)
AS
BEGIN
    SELECT
        product_name,
        list_price
    FROM 
        production.products
    WHERE
        list_price >= @min_list_price
    ORDER BY
        list_price;
END;
```
- Every parameter must start with the @ sign.
- We need to specify the data type of the parameter. Ex : (AS) DECIMAL.
- The parameter must be surrounded by the opening and closing brackets.
- To use a parameter, we also need to add the @ sign.

Creating optional parameters
- Specify default values for parameters so that when you call stored procedures, you can skip the parameters with default values.
    ```
    ALTER PROCEDURE proc_name (
        @min_list_price AS DECIMAL = 0
        , @max_list_price AS DECIMAL = 999999
        , @name AS VARCHAR(max)
    )
    AS
    ...
    ```
- But the max_list_price may not be robust, because you may have products with the list prices that are greater than that.
    A typical technique to avoid this is to use __NULL__ as the default value for the parameters.
    In the __WHERE__ clause, we changed the condition to handle __NULL__ value for the @max_list_price parameter:
    ```
    ...
    WHERE
        list_price >= @min_list_price AND
        (@max_list_price IS NULL OR list_price <= @max_list_price) AND
        product_name LIKE '%' + @name + '%'
    ...
    ```
***
# III - Variables
***
A variable is an object that holds a single value of a specific type:
- INTEGER
- DATE
- VARYING CHARACTER STRING

We typically use variables in the following cases:
- As a loop counter to count the number of times a loop is performed.
- To hold a value to be tested by a control-of-flow statement such as __WHILE__
- To store the value returned by a stored procedure or a function.

Declaring a variable:
- DECLARE @model_year SMALLINT, @product_name VARCHAR(MAX);

Assiging a value to a variable:
- SET @model_year = 2018;

Using variables in a query:
- as parameters

Put everything together : 
```
DECLARE @model_year SMALLINT;
SET @model_year = 2018;

SELECT product_name, model_year, list_price
FROM production.procuts
WHERE model_year = @model_year
ORDER BY product_name
```

Store query result in a variable
1. Declare a variable named @product_count with the integer data type:
```
DECLARE @product_count INT;
```
2. Use the __SET__ statement to assign the query's result set to the variable:
```
SET @product_count = (
    SELECT
        COUNT(*)
    FROM
        production.products
)
```
3. Output the content of the @product_count variable:
SELECT @product_count;

Selecting a record into variables:
1. Declare variables that hold the product name and lsit price:
```
DECLARE
    @product_name VARCHAR(MAX),
    @list_price DECIMAL(10, 2)
```

2. Assign the column names to the corresponding variables:
```
SELECT
    @product_name = product_name,
    @list_price = list_price
FROM
    production.products
WHERE
    product_id = 100;
```

Accumulating values into a variable
```
CREATE PROC uspGetProductList(@model_year SMALLINT = 2023)
AS
BEGIN
    DECLARE @product_list VARCHAR(MAX);
    SET @product_list = '';
    SELECT @product_list = @product_list + product_name + CHAR(10)
    FROM production.products
    WHERE model_year = @model_year
    ORDER BY product_name;
    
    PRINT @product_list;
END;
```
***
# IV - Output Parameters
***
Output parameter is used to return data back to the calling program.
In the following stored procedure, we finds products by model year and returns the number of products via the @product_count output parameter:

```
CREATE PROCEDURE uspFindProductByModel (
    @model_year SMALLINT,
    @product_count INT OUTPUT
) AS 
BEGIN
    SELECT product_name, list_price
    FROM production.products
    WHERE model_year = @model_year
    
    SELECT @product_count = @@ROWCOUNT;
END;
```




