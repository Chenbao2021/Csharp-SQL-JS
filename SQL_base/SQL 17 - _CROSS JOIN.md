# CROSS JOIN
## I - Introduction
The __CROSS JOIN__  Joined every row from the first table(T1) with every row from the second table(T2).
Returns a Cartesian product of rows from both tables.

Unlike INNER JOIN or LEFT JOIN, CROSS JOIN doesn't establish a relationship between the joined tables.


```
FROM
    products p
CROSS JOIN 
    categories c
```

## II - Examples 
1. Returns the combinations of all products and stores. (The result set can be used for stocktaking procedure during the month-end and year-end closings) :
    ```
    SELECT
        product_id,
        product_name,
        store_id,
        0 AS quantity
    FROM
        production.products
    CROSS JOIN sales.stores
    ORDER BY
        product_name,
        store_id;
    ```
    