# ORDER BY
## I - Introduction
When you use the __SELECT__ to query data from a table, the order of rows in the result set is not guaranteed.
The only way to sort the result is to use the __ORDER BY__ clause.

Syntax:
```
    SELECT
        select_list
    FROM
        table_name
    ORDER BY
        column_name | expression [ASC | DESC];
```
1. column_name | expression     : Specify a column name or a expression, or multiple (if multiple, the result set is sorted by the first column, and then by second column, and so on.)
2. ASC | DSC                    : Whether the values in the specified column should be sorted in ascending or descending order, ASC default, NULL as the lowest value.

## II - Examples
1. Sort a result set by one column in ascending order
    ```
    SELECT
        first_name,
        last_name
    FROM
        sales.customers
    ORDER BY
        first_name;
    ```

2. Sort a result set by one column in descending order
    ```
    SELECT
    	firstname,
    	lastname
    FROM
    	sales.customers
    ORDER BY
    	first_name DESC;
    ```

3. Sort a result set by multiple columns and different orders
    ```
    SELECT
        city,
        first_name,
        last_name
    FROM
        sales.customers
    ORDER BY
        city DESC,
        first_name ASC;
    ```
