# AND
## I - Introduction
The __AND__ is a logical operator that allow you to combine two Boolean expressions.
It returns __TRUE__ only when both expressions evaluate to TRUE.
|       |TRUE   |FALSE  |UNKNOWN|
|-----  |------ |------ |-----  | 
|TRUE   |TRUE   |FALSE  |UNKNOWN|
|FALSE  |FALSE  |FALSE  |FALSE  |
|UNKNOWN|UNKNOWN|FALSE  |UNKNOWN|

When use more than one logical operator, __AND__ is always evaluates first, except you add parentheses.

## II - Examples
1. Using multiple AND operators example:
    ```
    SELECT
        *
    FROM
        production.products
    WHERE
        category_id = 1
    AND list_price > 400
    AND brand_id = 1
    ORDER BY
        list_price DESC;
    ```
2. Using the AND operator with other logical operators
    ```
    SELECT
        *
    FROM
        production.products
    WHERE
        brand_id = 1
    OR brand_id = 2
    AND list_price > 1000
    ORDER BY
        brand_id DESC;
    ```

    