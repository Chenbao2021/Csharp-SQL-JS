# HAVING
## I - Introduction
The HAVING clause is often used with the GROUP BY clause to filter groups based on a specified list of conditions.
```
SELECT
    select_list
FROM
    table_name
GROUP BY
    group_list
HAVING
    conditions;
```
1. GROUP BY summarizes the rows into groups
2. HAVING applirs one or more conditions to these groups
3. Only groups that make the conditions evauate to TRUE are included in the result.

SQL Server processes the HAVING clause after the GROUP BY clause, so we can't refer to the aggregate function specified in the select list by using the column alias. So we must use the aggregate function expression in the HAVING clause explicitely.

```
SELECT
    column_name1,
    column_name2,
    aggregate_function (column_name3) alias
FROM
    table_name
GROUP BY
    column_name1,
    column_name2
HAVING
    aggregate_function (column_name3) > value;
```

## II - Examples 
1. To find the customers who placed at least two orders per year:
    ```
    SELECT
        customer_id,
        YEAR(order_date),
        COUNT(order_id) order_count
    FROM
        sales.orders
    GROUP BY
        customer_id,
        YEAR(order_date)
    HAVING
        COUNT(order_id) >= 2
    ORDER BY
        customer_id;
    ```

2. To finds the sales orders whose net values are greater than 20000:
    ```
    SELECT
        order_id,
        SUM(
            quantity * list_price * (1 - discount)
        ) net_value
    FROM
        sales.order_items
    GROUP BY
        order_id
    HAVING
        SUM(
            quantity * list_price * ( 1 - discount )
        ) > 20000
    ORDER BY
        net_value;
    ```

3. To finds the maximum and minimum list prices in each product category, then it filters out the ategory which has the maximum list price greater than 4000 or the minimum list price less than 500:
    ```
    SELECT
        category_id,
        MAX(list_price) max_list_price,
        MIN(list_price) min_list_price
    FROM
        production.products
    GROUP BY
        category_id
    HAVING
        MAX(list_price) > 4000 OR MIN(list_price) < 500;
    ```

4. To finds product categories whose average list prices are between 500 and 1000 : 
    ```
    SELECT
        category_id,
        AVG(list_price) avg_list_price
    FROM
        production.products
    GROUP BY
        category_id
    HAVING
        AVG(list_price) BETWEEN 500 AND 1000;
    ```
