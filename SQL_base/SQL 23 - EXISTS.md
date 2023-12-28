# EXISTS
## I - Introduction
The EXISTS operator is a logical operator that allows you to check whether a __subquery__ returns any row.
The EXISTS operator returns TRUE if the subquery returns one or more rows

With EXISTS, the subquery is a SELECT statement only.

Note : Event though the subquery returns a NULL value, the EXISTS operator is still evaluated to TRUE.
    ```
    EXISTS(SELECT NULL)
    ```
## II - Examples 
1. The following example finds all customers who have placed more than two orders
    ```
    SELECT
        customer_id,
        first_name,
        last_name
    FROM
        sales.customers c
    WHERE
        EXISTS (
            SELECT
                COUNT(*)
            FROM
                sales.orders o
            WHERE
                o.customer_id = c.customer_id
            GROUP BY
                customer_id
            HAVING COUNT(*) > 2
        )
    ORDER BY
        first_name,
        last_name
    ```


