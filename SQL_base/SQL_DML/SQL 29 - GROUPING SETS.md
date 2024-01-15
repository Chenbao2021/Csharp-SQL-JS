# GROUPING SETS
## I - Introduction
A grouping set is a group of columns by which you group.
Typically, a single query with an aggregate defines a single grouping set.
The following query defines a grouping set that includes brand and category which is denoted as (brand, category):
```
SELECT brand, category, SUM(sales) sales
FROM sales.sales_summary
GROUP BY brand, category
ORDER BY brand, category
```
We want the result of 4 grouping set : (), (brand), (category) and (brand, category)
We can use __UNION ALL__ to get the result.
But it is quite lengthy and too slow, becayse SQL Server needs to execute four subqueries and combines the result sets.

To fix these problems, SQL Server provides a subclause of the GROUP BY clause called __GROUPING SETS__
```
SELECT
    column1,
    column2,
    aggregate_function(column3)
FROM
    table_name
GROUP BY
    GROUPING SETS(
        (column1, column2),
        (column1),
        (column2),
        ()
);
```

The __GROUPING__ function indicates whether a specified column in a __GROUP BY__ clause is aggregated or not.
It returns 1 for aggregated or 0 for not aggregated in the result set.
(Use for check which grouping set is using for the result)

## II - Examples 
1. Gets the sales data individually, group by brand, group by category, and group by brand and category:
```
SELECT
    brand, category, SUM(sales) sales
FROM
    sales.sales_summary
GROUP BY
    GROUPING SETS(
        (brand, category),
        (brand),
        (category),
        ()
    )
ORDER BY
    brand,
    category;
```

2. Query using GROUPING function
```
SELECT
    GROUPING(brand) grouping_brand,
    GROUPING(category) grouping_category,
    brand,
    category,
    SUM(sales) sales
FROM
    sales.sales_summary
GROUP BY
    GROUPING SETS(
        (brand, category),
        (brand),
        (category),
        ()
    )
ORDER BY
    brand,
    category;
```





