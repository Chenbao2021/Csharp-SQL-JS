# GROUP BY
## I - Introduction
The GROUP BY clause allows you to arrange the rows of a query in groups.
```
SELECT
    select_list
FROM
    table_name
GROUP BY
    column_name1,
    column_name2,
    ...
```

In practice, the GROUP BY clause is often used with aggregate functions for generating summary reports.
An aggregate function performs a calculation on a group and returns a unique value per group:
- COUNT() returns the number of rows in each group
- SUM()
- AVG()
- MIN()
- MAX()

If you want to reference a column or expression that is not listed in the GROUP BY clause, you must use that column as the input of an aggregate function(Because there is no guarantee that the column or expression will return a single value per group).

## II - Examples 
1. To query the number of orders plaed by the customer by year:
    ```
    SELECT
        customer_id,
        YEAR(order_date) order_date,
        COUNT(order_id) order_placed
    FROM
        sales.orders
    WHERE
        customer_id IN (1, 2)
    GROUP BY
        customer_id,
        YEAR(order_date)
    ORDER BY
        customer_id
    ```

2. To query the number of customers in every city and state:
    ```
    SELECT
        city,
        state
        COUNT(customer_id) customer_count
    FROM
        sales.customers
    GROUP BY
        city,
        state
    ORDER BY
        city,
        state;
    ```

3. To query the minimum and maximum list prices of all products with the model 2018 by brand:
    ```
    SELECT
        brand_name,
        MIN(list_price) min_price,
        MAX(list_price) max_price
    FROM
        production.products p
    INNER JOIN
        production.brands b 
        ON b.brand_id = p.brand_id
    WHERE
        model_year = 2018
    GROUP BY
        brand_name
    ORDER BY
        brand_name;
    ```

4. To query the n et value of every order:
sales.order_items(order_id, item_id, product_id, quantity, list_price, discount)
    ```
    SELECT
        order_id,
        SUM(
            quantity * list_price * (1 - discount)
        ) net_value
    FROM
        sales.order_items
    GROUP BY
        order_id;
    ```
