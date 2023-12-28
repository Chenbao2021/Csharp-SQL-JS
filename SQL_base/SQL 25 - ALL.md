# ALL
## I - Introduction
The SQL Server __ALL__ operator compares a scalar value with a single-column list of values returned by a subquery.
The __ALL__ operator returns TRUE if all the pairs evaluates to TURE.

## II - Examples 
1. scalar_expression > ALL( subquery )
```
SELECT
    product_name
    list_price
FROM
    production.products
WHERE
    list_price > ALL (
        SELECT
            AVG(list_price) avg_list_price
        FROM
            production.products
        GROUP BY
            brand_id
    )
ORDER BY
    list_price;
```
2. scalar_expression < ALL( subquery )
Similiar to the code below, just we change the '>' to '<'


