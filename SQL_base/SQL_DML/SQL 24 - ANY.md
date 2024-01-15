# ANY
## I - Introduction
The __ANY__ operator is a logical operator that compares a scalar value with a single-column set of values returned by a subquery.
scalar value = 标量值
```
scalar_expression comparison_operator ANY (subquery)
```

Note that the SOME operator is equivalent to the ANY operator

## II - Examples 
1. The following example finds the products that were sold with more than two units in a sale order:
    ```
    SELECT
        product_name,
        list_price
    FROM
        production.products
    WHERE
        product_id = ANY (
            SELECT
                product_id
            FROM
                sales.order_items
            WHERE
                quantity >= 2
        )
    ORDER BY
        product_name;
    ```

