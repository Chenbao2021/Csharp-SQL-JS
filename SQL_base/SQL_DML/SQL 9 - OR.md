# OR
## I - Introduction
The __OR__ is a logical operator that allow you to combine two Boolean expressions.
It returns __TRUE__  when either of the conditions evaluates to TRUE.
|       |TRUE   |FALSE  |UNKNOWN|
|-----  |------ |------ |-----  | 
|TRUE   |TRUE   |TRUE   |   TRUE|
|FALSE  |TRUE   |FALSE  |UNKNOWN|
|UNKNOWN|TRUE   |UNKNOWN|UNKNOWN|

When use more than one logical operator, __OR__ is evaluated after __AND__.
## II - Examples
1. Using multiple OR operators example:
    ```
    SELECT
        *
    FROM
        production.products
    WHERE
        category_id = 1
    OR list_price > 400
    ORDER BY
        list_price DESC;
    ```
2. Replace multiple __OR__ operators by the __IN__ operator as shown in the following query:
    ```
    SELECT
        product_name,
        brand_id
    FROM
        production.products
    WHERE
        brand_id IN (1, 2, 3)
    ORDER BY
        brand_id DESC;
    ```

    