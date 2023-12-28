# INNER JOIN (Comme intersection)
## I - Introduction
The inner join clause allows you to query data from two or more related tables.
```
FROM
    products p
INNER JOIN 
    categories c
    ON c.category_id = p.category_id
```
For each row in the products table, the inner join clause matches it with every row in the categories table based on the values of the category_id column:
- If both rows have the same value in the category_id column, the inner join forms a new row whose columns are from the rows of the caregories and products tables according to the columns in the select list and includes this new row in the result set.
- If the row in the products table doesn't match the row from the categories table, the inner join clause just ignore these rows and does not include them in the result set. 

## II - Examples
Inner join on three tables
```
SELECT
    product_name,
    category_name,
    brand_name,
    list_price
FROM
    production.products p
INNER JOIN production.categories c ON c.category_id = p.category_id
INNER JOIN production.brands b ON b.brand_id = p.brand_id
ORDER BY
    product_name DESC;
```
    