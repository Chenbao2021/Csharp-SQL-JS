# SELECT DISTINCT
## I - Introduction
Sometimes we want to get only distinct values in a specified column of a table, to do that, we can use __SELECT DISTINCT__ . 
In other words, it removes the duplicate values in the column from the result set, including NULL.

Syntaxe:
```
    SELECT DISTINCT
    	column_name1,
    	column_name2 ,
    	...
    FROM
    	table_name;
```
The query uses the combination of values in all specified columns in the __SELECT__ list to evaluate the uniqueness.

## II - Examples
1. DISTINTC multiple columns
    ```
    SELECT DISTINCT
        city,
        state
    FROM
        sales.customers
    ```
2. 