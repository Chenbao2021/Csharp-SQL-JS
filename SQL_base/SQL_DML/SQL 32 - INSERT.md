# INSERT
## I - Introduction
To add one or more rows into a table, you use the __INSERT__ statement.
```
INSERT INTO table_name (column_list)
VALUES (value_list);
```

We can't insert directly a value into the identity column, but we can (Google)

## II - Examples 
1. Insert and return inserted values
```
INSERT INTO sales.promotions(
    promotion_name,
    discount,
    start_date,
    expired_date
) OUTPUT inserted.promotion_id, inserted.promotion_name
VALUES (
    '2018 FALL Promotion',
    0.15,
    '20181001',
    '20181101'
)
```







