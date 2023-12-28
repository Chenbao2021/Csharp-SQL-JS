# Recursive CTE
## I - Introduction
A recursive common table expression is a CTE that references itself.
By doing so, the CTE repeatedly executes, returns subsets of data, until it returns the complete result set.

A recursive CTE is useful in querying hierarchical data such as organiaztion charts where one employee reports to a manager or multi-level bill of materials when a product consists of many components, and each components itself als consists of many other components.

```
WITH expression_name(column_list)
AS
(
    initial_query
    UNION ALL
    recursive_query
)

SELECT * 
FROM expression_name
```

## II - Examples 
    1. Uses a recursive CTE to returns weekdays from __Monday__ to __Saturday__
    ```
    WITH cte_numbers(n, weekday) 
    AS (
        SELECT
            0,
            DATENAME(DW, 0)
        UNION ALL
        SELECT
            n + 1,
            DATENAME(DW, n + 1)
        FROM
            cte_numbers
        WHERE n < 6
    )   
    SELECT
        weekday
    FROM
        cte_numbers
    ```
    - DATENAME() function returns the name of the weekday based on a weekday number. (The anchor member returns the Monday)
    - The recursive member returns the next day starting from the __Tuesday__ till __Sunday__
    - The condition in the __WHERE__ clause is the termination condition that stops the execution of the recursive member.

2. Use a recursive CTE to get all subordinates of the top manager who does not have a manager
    ```
    WITH cte_org AS (
        SELECT       
            staff_id, 
            first_name,
            manager_id
            
        FROM       
            sales.staffs
        WHERE manager_id IS NULL
        UNION ALL
        SELECT 
            e.staff_id, 
            e.first_name,
            e.manager_id
        FROM 
            sales.staffs e
            INNER JOIN cte_org o 
                ON o.staff_id = e.manager_id
    )
    SELECT * FROM cte_org;
    
    ```
    - The anchor member gets the top manager and the recursive query returns subordinates of the top managers and so on.



