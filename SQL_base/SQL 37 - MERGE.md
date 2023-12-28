# MERGE
## I - Introduction
SUppose, you have two table called source and target tables, and you need to update the target table based on the values matched from the source table. There are three cases:
- The source table has some rows that do not exist in the target table -> INSERT
- The target table has some rows that do not exist in the source table -> DELETE
- The source table has some rows with the same keys as the rows in the taret table. However, these rows have different values in the non-key columns -> UPDATE

If you use the INSERT, DELETE, UPDATE statement individualy, you have to construct three separate statements to update the data to the target table with the matching rows from the source table.

However, SQL Server provides the __MERGE__ statement that allows you to perform three actions at the same times:
```
MERGE target_table USING source_table
ON metge_condition
WHEN MATCHED THEN update_statement
WHEN NOT MATCHED THEN insert_statement
WHEN NOT MATCHED BY SOURCE THEN DELETE;
```

## II - Examples 
1. To update data to the __sales.category__(target table) with the values from the __sales.category_staging(source table)__,
you use the following __MERGE__ statement:
    ```
    MERGE sales.category t 
        USING sales.category_staging s
    ON (s.category_id = t.category_id)
    WHEN MATCHED
        THEN UPDATE SET 
            t.category_name = s.category_name,
            t.amount = s.amount
    WHEN NOT MATCHED BY TARGET 
        THEN INSERT (category_id, category_name, amount)
             VALUES (s.category_id, s.category_name, s.amount)
    WHEN NOT MATCHED BY SOURCE 
        THEN DELETE;
    ```






