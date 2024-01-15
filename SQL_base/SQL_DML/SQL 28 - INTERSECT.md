# INTERSECT
## I - Introduction
INTERSECT: combines result sets of two or more queries and returns distinct rows that are output by both queries.
- Both queries must have the same number and order of columns
- The data type of the corresponding columns must be the same or compatible

 JOIN       : Append the result set horizontally
 INTERSECT  : Select rows that appear in both table.  
## II - Examples 
1. 
    ```
    SELECT
        city
    FROM
        sales.customers
    INTERSECT
    SELECT
        city
    FROM
        sales.stores
    ORDER BY
        city;
    ```
    - The first query finds all cities of the customers
    - The second query finds the cities of the stores.
    - The whole query, returns the common cities of customers and stores.




