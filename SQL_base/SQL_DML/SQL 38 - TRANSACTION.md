# TRANSACTION
## I - Introduction

A transaction is a single unit of work that typically contains multiple T-SQL statements
If a transaction is successful, the changes are committed to the database. However, if a transaction has an error, the changes have to be rolled back.

When executing a single statement such as __INSERT__, __UPDATE__, and __DELETE__ , SQL Server uses the autocommit transaction(Each statement is a transaction).

To start a transaction explicitely:
1. BEGIN TRANSACTION;
2. Then execute one or more statements including __INSERT__, __UPDATE__, __DELETE__.
3. Finally, commit the transaction using the __COMMIT__ statement, or roll back by __ROLLBACK___.

## II - Examples 
1. A simple transaction example:
    ```
    BEGIN TRANSACTION;
    
    INSERT INTO invoices (customer_id, total)
    VALUES (100, 0);
    
    INSERT INTO invoice_items (id, invoice_id, item_name, amount, tax)
    VALUES (10, 1, 'Keyboard', 70, 0.08),
           (20, 1, 'Mouse', 50, 0.08);
    
    UPDATE invoices
    SET total = (SELECT
      SUM(amount * (1 + tax))
    FROM invoice_items
    WHERE invoice_id = 1);
    
    COMMIT;
    ```
2. 





