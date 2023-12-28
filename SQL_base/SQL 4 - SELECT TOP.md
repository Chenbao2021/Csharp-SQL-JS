# SELECT TOP
## I - Introduction
This clause will limit the number of rows or percentage of rows returned in a query result set.
It must be used in conjunction with __ORDER BY__.

Syntax:
```
    SELECT TOP (expression) [PERCENT] [WITH TIES]
    FROM table_name
    ORDER BY column_name;
```
- expression    : Evaluated to a float value if PERCENTAGE is used, otherwise, it is converted to a BIGINT value.
- PERCENTAGE    : Indicates that the query returns the first N percentage of rows, SQL Server rounds it up to the next whole number.
- WITH TIE      : I want the most expensive product, but 2 products with same price, __WITH TIE__ option allows to return both.

## II - Examples

1. Using TOP with a constant value
    ```
    SELECT TOP 10
        product_name, 
        list_price
    FROM
        production.products
    ORDER BY 
        list_price DESC;
    ```
2.  Using TOP to return a percentage of rows
    ```
    SELECT TOP 1 PERCENT
        product_name, 
        list_price
    FROM
        production.products
    ORDER BY 
        list_price DESC;
    ```
3.  Using TOP WITH TIES to include rows that match the values in the last row
    ```
    SELECT TOP 3 WITH TIES
        product_name, 
        list_price
    FROM
        production.products
    ORDER BY 
        list_price DESC;
    ```