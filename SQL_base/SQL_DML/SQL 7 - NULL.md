# NULL
## I - Introduction
__NULL__ is used to indicate the absence of any data value.
Typically, the result of a logical expression is TRUE or FALSE. However, when NULL is involved in the logical evaluation, the result is UNKNOWN.
The result of the following comparison are UNKNOWN:

    NULL = 0
    NULL <> 0
    NULL > 0
    NULL = NULL
The NULL does not equal anything, even itself. 

If in a query you use "column_name = NULL" in a WHERE clause, you will get an empty result set.
Because WHERE clause returns rows that cause its predicate evaluates to TRUE. However, when NULL is involved in the logical evaluation, the result is UNKNOWN.

So to test whether a value is __NULL__ or not, we always use __IS NULL__ / __IS NOT NULL__ operator:
- column_name IS NULL

## II - Examples
1. Get customers who has phone information:
    ```
    SELECT
        customer_id,
        first_name,
        last_name,
        phone
    FROM
        sales.customers
    WHERE
        phone IS NOT NULL
    ORDER BY
        first_name,
        last_name;
    ```

    