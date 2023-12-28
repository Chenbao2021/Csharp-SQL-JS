# Subquery
## I - Introduction
A subquery is a query nested inside another statement such as SELECT, INSERT, UPDATE or DELETE.
A subquery could make the query more readable, and maintenable.

You can use subquery in many places:
- With IN or NOT IN
- With ANY or ALL
- With EXISTS or NOT EXISTS
- In UPDATE, DELETE, or INSERT statement
- In the FROM clause

## II - Examples 
1. To find the sales orders of the customers located in New York
    ```
    SELECT
        order_id,
        order_date,
        customer_id
    FROM
        sales.orders
    WHERE
        customer_id IN (
            SELECT
                customer_id
            FROM
                sales.customers
            WHERE
                city = 'New York'
        )
    ORDER BY
        order_date DESC;
    ```
    - We must always enclose the SELECT query of a subqeury in parentheses ()
    - A subquery can be nested within another subquery, SQL Server supports up to 32 levels of nesting.

2. If a subqeury returns a single value, it can be used anywhere an expression is used.
    In the following example, a subquery is used as a column expression named max_list_price in a SELECT statement.
    ```
    SELECT
        order_id,
        order_date,
        (
            SELECT
                MAX(list_price),
            FROM
                sales.order_items i
            WHERE
                i.order_id = o.order_id
        ) AS max_list_price
    FROM
        sales.orders o
    ORDER BY
        order_date DESC;
    ```
3. A subquery that is used with the IN operator returns a set of zero or more values.
    The following query finds the names of all mountain bikes and road bikes products that the Bike Store sell.
    ```
    SELECT
        product_id,
        product_name
    FROM
        production.products
    WHERE
        category_id IN (
            SELECT
                category_id
            FROM
                production.categories
            WHERE
                category_name = 'Mountain Bikes'
            OR category_name = 'Road Bikes'
        );
    ```
4. The Any operator return TRUE if one of a comparison pair evaluates to TRUE; Otherwise, it returns FALSE.
    The following query finds the products whose list prices are greater than or equal to the average list price of any product brand.
    ```
    SELECT
        product_name,
        list_price
    FROM
        production.products
    WHERE
        list_price >= ANY (
            SELECT
                AVG(list_price)
            FROM
                production.products
            GROUP BY
                brand_id
        )
    ```
5. The All operator works similarily like the Any operator. It returns TRUE if all comparison pairs evaluate to TRUE.

6. EXISTS
    The EXISTS operator returns TRUE if the subquery return results; Otherwise, it returns FALSE.
    The following query finds the customers who bought products in 2017:
    ```
    SELECT
        customer_id,
        first_name,
        last_name,
        city
    FROM
        sales.customers c
    WHERE
        EXISTS (
            SELECT
                customer_id
            FROM
                sales.orders o
            WHERE
                o.customer_id = c.customer_id
            AND YEAR(order_date) = 2017
        )
    ORDER BY
        first_name,
        last_name
    ```
7. In the FROM clause
    ```
    SELECT
        AVG(order_count) average_order_count_by_staff
    FROM
        (
            SELECT
                staff_id,
                COUNT(order_id) order_count
            FROM
                sales.orders
            GROUP BY
                staff_id
        )
    ```



