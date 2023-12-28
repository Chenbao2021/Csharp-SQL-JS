# WHERE
## I - Introduction
We use __WHERE__ to set up one or more condition on our query, to avoid unecessary rows.
A logical expression is often called a predicate in SQL.
SQL use a three-valued predicate logic, that means a predicate can be evaluate to :TRUE, FALSE, or UNKNOWN.
__WHERE__ clause won't return any row that causes the predicate to evaluate to FALSE or UNKNWON.

Syntaxe:
```
    SELECT
        select_list
    FROM
        table_name
    WHERE
        search_condition;
```
## II - Examples
1. Finding rows by using a simple equality
    ```
        SELECT
            product_id,
            product_name,
            category_id,
            model_year,
            list_price
        FROM
            production.products
        WHERE
            category_id = 1
        ORDER BY
            list_price DESC;
    ```
2. Finding rows that meet two conditions
    ```
    SELECT
        product_id,
        product_name,
        category_id,
        model_year,
        list_price
    FROM
        production.products
    WHERE
        category_id = 1 AND model_year = 2018
    ORDER BY
        list_price DESC;
    ```
3. Finding rows with the value between two values
    ```
    SELECT
        product_id,
        product_name,
        category_id,
        model_year,
        list_price
    FROM
        production.products
    WHERE
        list_price BETWEEN 1899.00 AND 1999.99
    ORDER BY
        list_price DESC;
    ```
4. Finding rows that have a value in a list of values
    ```
    SELECT
        product_id,
        product_name,
        category_id,
        model_year,
        list_price
    FROM
        production.products
    WHERE
        list_price IN (299.99, 369.99, 489.99)
    ORDER BY
        list_price DESC;
    ```
5. Finding rows whose values contain a string
    ```
    SELECT
        product_id,
        product_name,
        category_id,
        model_year,
        list_price
    FROM
        production.products
    WHERE
        product_name LIKE '%Cruiser%'
    ORDER BY
        list_price;
    ```
    