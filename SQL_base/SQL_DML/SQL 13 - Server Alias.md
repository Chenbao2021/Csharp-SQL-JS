# Server Alias
## I - Introduction
We can assign a column or an expression a temporary name during the query execution, with a column __alias__
```
column | expression [AS] column_alias
```
Exemple: 
```
SELECT
    first_name + last_name AS "Full Name"
FROM
    sales.customers
ORDER BY
    first_name;
```

When assign a column an alias, we can use either the column name or the column alias in the __ORDER BY__ clause.
(The ORDER BY clause is the very last clause to be processed therefor the column aliases are known at the time of sorting).

Similiar, we can assign a table a lias name.

## II - Examples

    