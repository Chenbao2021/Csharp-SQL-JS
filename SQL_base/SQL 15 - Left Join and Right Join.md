# Left JOIN and Right Join
## I - Introduction
The __LEFT JOIN__ returns all rows from the left table and the matching rows from the right table,
if no matching rows are found in the right table, __NULL__ are used.
```
FROM
    products p
LEFT JOIN 
    categories c
    ON c.category_id = p.category_id
```
The __LEFT JOIN__ clause returns all rows from the left table(T1) and matching rows( or __NULL__ values ) from the right table.
- For each row from the T1 table, the query compares it with all the rows from the T2 table. If a pair of rows causes the join predicate to evaluate to __TRUE__ , the column values from these rows will be combined to form a new row chich is then included in the result set.
- If a row from the left table(T1) does not have any matching row from the T2 table, the query combines column values of the row from the left table with __NULL__ for each column value from the right table.

SQL Server processes the __WHERE__ clause after the __LEFT JOIN__ clause.

__LEFT JOIN__ , conditions in __ON__ and __WHERE__ clause:
- If the condition is in __ON__ , then all values will be displayed, with a value = NULL if no matched
- If the condition is in __WHERE__, then all the __FALSE__ evaluation won't be displayed.
- For the INNER JOIN clause, there is no difference.

## II - Examples 

    