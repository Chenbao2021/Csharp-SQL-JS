# CTE
## I - Introduction
CTE = common table expression, allows you to define a temporary named result set that available temporarily in the execution scope of a statement such as __SELECT__, __INSERT__, __UPDATE__, __DELETE__, or __MERGE__

```
WITH expression_name[(column_name [,...])]
AS
    (CTE_definition)
SQL_statement;
```
- The number of columns must be the same as the number of columns defined in the CTE_definition
- We prefer to use CTE rather than to use subqueries because CTE are more readable.


## II - Examples 

1. Uses a CTE to return the sales amounts by sales staffs in 2018:
    ```
    WITH cte_sales_amounts (staff, sales, year) AS (
        SELECT    
            first_name + ' ' + last_name, 
            SUM(quantity * list_price * (1 - discount)),
            YEAR(order_date)
        FROM    
            sales.orders o
        INNER JOIN sales.order_items i ON i.order_id = o.order_id
        INNER JOIN sales.staffs s ON s.staff_id = o.staff_id
        GROUP BY 
            first_name + ' ' + last_name,
            year(order_date)
    )
    
    SELECT
        staff, 
        sales
    FROM 
        cte_sales_amounts
    WHERE
        year = 2018;
    ```

2. Use the CTE to return the average number of sales orders in 2018 for all sales staffs:
    ```
    WITH cte_sales AS (
        SELECT 
            staff_id, 
            COUNT(*) order_count  
        FROM
            sales.orders
        WHERE 
            YEAR(order_date) = 2018
        GROUP BY
            staff_id
    
    )
    SELECT
        AVG(order_count) average_orders_by_staff
    FROM 
        cte_sales;
    
    ```

3. Using multiple SQL Server CTE in a single query:
    ```
    WITH cte_category_counts (
        category_id, 
        category_name, 
        product_count
    )
    AS (
        SELECT 
            c.category_id, 
            c.category_name, 
            COUNT(p.product_id)
        FROM 
            production.products p
            INNER JOIN production.categories c 
                ON c.category_id = p.category_id
        GROUP BY 
            c.category_id, 
            c.category_name
    ),
    cte_category_sales(category_id, sales) AS (
        SELECT    
            p.category_id, 
            SUM(i.quantity * i.list_price * (1 - i.discount))
        FROM    
            sales.order_items i
            INNER JOIN production.products p 
                ON p.product_id = i.product_id
            INNER JOIN sales.orders o 
                ON o.order_id = i.order_id
        WHERE order_status = 4 -- completed
        GROUP BY 
            p.category_id
    ) 
    
    SELECT 
        c.category_id, 
        c.category_name, 
        c.product_count, 
        s.sales
    FROM
        cte_category_counts c
        INNER JOIN cte_category_sales s 
            ON s.category_id = c.category_id
    ORDER BY 
        c.category_name;

    ```



