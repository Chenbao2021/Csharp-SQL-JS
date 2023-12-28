# SELECT
## I - Introduction
In SQL, data is organized in a [row, column] format like a spreadsheet.
- Each row represents a unique record in a table
- Each column represents a field in the record.

We use __SELECT__ to query from a table, and the result is called __result set__.

## II - Examples
- Retrieve some columns of a table
```
SELECT
    first_name,
    second_name
FROM
    sales.customers
```

- Retrieve all columns from a table 
```
SELECT
    *
FROM
    sales.customers
```
