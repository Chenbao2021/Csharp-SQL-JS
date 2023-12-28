# IN
## I - Introduction
The __IN__ is a logical operator that allow you to test whether a specified value matches any value in a list.
Syntax:
```
column | expression IN (v1, v2, v3)
<=> column = v1 OR column = v2 OR column = v3
```
In addition to a list of values, you can use a subquery that returns a list of values with the __IN__ operator as shown below:
```
column|expression IN (subquery)
```
In this syntax, the subquery is a SELECT statement that returns a list of values of a single column.

Note that if a list contains __NULL__, the result of __IN__ or __NOT IN__ will be unknown.


## II - Examples
1. Using SQL Server IN with a list of values example:
    ```
    SELECT
        product_name,
        list_price
    FROM
        production.products
    WHERE
        list_price IN (89.99, 109.99, 159.99)
    ORDER BY
        list_price;
    ```
2. Using SQL Server IN operator with a subquery example : 
    ```
    SELECT
        product_name,
        list_price
    FROM
        production.products
    WHERE
        product_id IN (
            SELECT
                product_id
            FROM
                production.stocks
            WHERE
                store_id = 1 AND quantity >= 30
        )
    ORDER BY
        product_name;
    ```

    