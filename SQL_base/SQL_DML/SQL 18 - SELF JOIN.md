# SELF JOIN
## I - Introduction
A self join allows you to join a table to itself:
- Query hierarchical data
- Compare rows within the same table.

A self join use INNER JOIN/LEFT JOIN  and table alis :
```
SELECT
    select_list
FROM
    T t1
LEFT JOIN
    T t2
    ON joint_predicate;
```
## II - Examples 
1. To get who is the manager of who:
    ```
    SELECT
        e.first_name + ' ' + e.last_name employee,
        m.first_name + ' ' + m.last_name manager
    FROM
        sales.staff e
    INNER JOIN
        sales.staff m
        ON m.staff_id = e.manager_id
    ORDER BY
        manager;
    ```
2. To find the customers located in the same city
    ```
    SELECT
        c1.city,
        c1.first_name + ' ' + c1.last_name customer_1,
        c2.first_name + ' ' + c2.last_name customer2
    FROM
        sales.customers c1
    INNER JOIN
        sales.customers c2
        ON c1.customer_id > c2.customer_id
    AND
        c1.city = c2.city
    ORDER BY
        city,
        customer_1,
        customer_2
    ```