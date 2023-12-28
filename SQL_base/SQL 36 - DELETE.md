# DELETE
## I - Introduction
To remove one or more rows from a table completely, you use the __DELETE__ statement:
```
DELETE [TOP(expression)[PERCENT]]
FROM table_name
[WHERE search_condition];
```

## II - Examples 

1. Removes 21 random rows from the product_history table:
    ```
    DELETE TOP(21)
    FROM production.product_history;
    ```

2. Remove all products whose model year is 2017
    ```
    DELETE
    FROM
        production.product_history
    WHERE
        model_year = 2017;
    ```

3. Delete all rows from a table example
    ```
    DELETE FROM production.product_history;
    ```





