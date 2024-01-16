***
# I - Introduction
***
User-defined functions help you simplify your development by encapsulating complex business logic and make them available for reuse in every query.
- scalar-valued functions : Return a single value
- table-valued functions : Return rows of data

A user-defined function cannot take a table variable as input,ouput parameter, but it can return a table variable.

***
# II - Server Scalar functions
***
Scalar function takes one or more parameters and returns a single value.
````
CREATE FUNCTION [schema_name.]function_name(parameter_list)
RETURNS data_type AS
BEGIN
    statements
    RETURN value
END
````
To modify a scalar function
````
ALTER FUNCTION ...
...
````
To remove a scalar function
```
DROP FUNCTION ...
```

***
# III - Server Table-value Functions
***
A table-valued function returns data of a table type, you can use the table-valued function just like you would use a table.
the return table is a table variable.
Exemple: 
````
CREATE FUNCTION udfProductInYear (
    @model_year INT
)
RETURNS TABLE
AS
RETURN
    SELECT 
        product_name,
        model_year,
        list_price
    FROM
        production.products
    WHERE
        model_year = @model_year;
````
- The __RETURNS TABLE__ specifies that the function will return a table.

The multiple-statement table-valued functions(MSTVF)
A multi-statement table-valued function is a table-valued function that returns the result of multiple statements.
It is very useful because you can execute multiple queries within the function and aggregate results into the returned table.
Exemple:
````
CREATE FUNCTION udfContacts()
    RETURNS @contacts TABLE (
        first_name VARCHAR(50),
        last_name VARCHAR(50),
        email VARCHAR(255),
        phone VARCHAR(25),
        contact_type VARCHAR(20)
    )
AS
BEGIN
    INSERT INTO @contacts
    SELECT 
        first_name, 
        last_name, 
        email, 
        phone,
        'Staff'
    FROM
        sales.staffs;

    INSERT INTO @contacts
    SELECT 
        first_name, 
        last_name, 
        email, 
        phone,
        'Customer'
    FROM
        sales.customers;
    RETURN;
END;
````


