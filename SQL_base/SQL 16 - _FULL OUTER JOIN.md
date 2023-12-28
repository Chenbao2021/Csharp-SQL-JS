# FULL OUTER JOIN
## I - Introduction
The __FULL [OUTER] JOIN__ clause returns a result set that includes rows from both left and right tables.
When no matching rows exist for the row in the left table, the columns of the right table will contain __NULL__.
Likewise, when no matching rows exist for the row in the right table, the column of the left table will contain __NULL__.

```
FROM
    products p
FULL JOIN 
    categories c
    ON c.category_id = p.category_id
```

## II - Examples 
1. Find the members who do not participate in any project and projects which do not have any members, we add a __WHERE__ clause to the above query: 
    ```
    SELECT 
        m.name member, 
        p.title project
    FROM 
        pm.members m
        FULL OUTER JOIN pm.projects p 
            ON p.id = m.project_id
    WHERE
        m.id IS NULL OR
        P.id IS NULL;
    ```
    