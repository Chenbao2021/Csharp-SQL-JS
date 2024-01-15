# BETWEEN
## I - Introduction
The __BETWEEN__ is a logical operator that allow you to specify a range to test.
Syntax:
```
column | expression BETWEEN start_expression AND end_expression
```
Using BETWEEN make the query much more readable than using ">=" + "<=" + "AND" .

Note that if any input to the __BETWEEN__ is __NULL__, the result is __UNKNOWN__.

## II - Examples
1. Finds the products whose list prices are between 149.99 and 199.99 : 
    ```
    WHERE
        list_price BETWEEN 149.99 AND 199.99
    ```
2. Finds the orders that customers placed between __January 15,2017__ and __January 17,2017__
    ```
    WHERE
        order_date BETWEEN '20170115' AND '20170117'
    ```
    