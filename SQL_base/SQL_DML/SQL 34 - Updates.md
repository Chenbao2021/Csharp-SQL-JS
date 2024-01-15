# Updates
## I - Introduction
To modify existing data in a table, we use the following __UPDATE__ statement:
```
UPDATE table_name
SET c1=v1, c2=v2, ... , cn=vn
[WHERE condition]
```
- The WHERE clause is optional . If you skip the __WHERE__ clause, all rows in the table are updated.


## II - Examples 

1. Update a single column in all rows example
    ```
    UPDATE sales.taxes
    SET updated_at = GETDATE()
    ```

2. Update multiple columns example:
    ```
    UPDATE sales.taxes
    SET max_local_tax_rate += 0.02,
        avg_local_tax_rate += 0.01
    WHERE
        max_local_tax_rate = 0.01;
    ```




