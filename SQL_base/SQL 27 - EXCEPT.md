# UNION
## I - Introduction
__EXCEPT__ compares the result sets of two queries and returns the distinct rows from the first query that are not output by the second query.
- The number and order of columns must be the same in both queries
- The data types of the corresponding columns must be the same or compatible

## II - Examples 
1. Use the __EXCEPT__ operator to find the products that have no sales and sorts the products by their id in ascending order:
    ```
    SELECT product_id
    FROM production.products
    EXCEPT
    SELECT product_id
    FROM sales.order_item
    ORDER BY product_id;
    ```




