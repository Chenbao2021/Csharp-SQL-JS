# ROLLUP
## I - Introduction
The __ROLLUP__ is a subclause of the __GROUP BY__ clause which provides a shorthand for defining multiple __grouping sets__.
__ROLLUP__ does not create all possible grouping sets based on the dimension columns as __CUBE__,
but __ROLLUP__ assumes a hierarchy among the dimension columns and only generates grouping sets based on this hierarchy.
Exemple:
- CUBE(d1, d2) generates : (d1, d2), (d1), (d2), ()
- ROLLUP(d1, d2, d3) generates : (d1, d2, d3), (d1, d2), (d1), ()

The __ROLLUP__ is commonly used to calculate the aggregates of hierarchical data such as sales by year>quarter>month.

## II - Examples 

1. Calculate the sales amount by brand(subtotal) and both brand and category(total):
    ```
    SELECT
        brand,
        category,
        SUM(sales) sales
    FROM
        sales.sales_summary
    GROUP BY
        ROLLUP(brand, category)
    ```
    If you change the order of brand and category, the result will be different as shown in the following query.
2. Perform a partial roll-up
```
SELECT brand, category, SUM(sales) sales
FROM sales.sales_summary
GROUP BY brand, ROLLUP(category);
```







