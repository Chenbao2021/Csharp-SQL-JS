# INSERT MULTIPLE ROWS
## I - Introduction
To add multiple rows to a table at once, you use the following form of the __INSERT__ statement:
```
INSERT INTO table_name (column_list)
VALUES 
    (value_list_1),
    (value_list_2),
    (value_list_3),
    ...
    (value_list_n);
```

## II - Examples 

1. Inserting multiple rows example:
    ```
    INSERT INTO sales.promotions (
        promotion_name,
        discount,
        start_date,
        expired_date
    )
    VALUES
        (
            '2019 Summer Promotion',
            0.15,
            '20190601',
            '20190901'
        ),
        (
            '2019 Fall Promotion',
            0.20,
            '20191001',
            '20191101'
        ),
        (
            '2019 Winter Promotion',
            0.25,
            '20191201',
            '20200101'
        );
    ```
2. Inserting multiple rows and returning the inserted id list example:
    ```
    INSERT INTO 
    	sales.promotions ( 
    		promotion_name, discount, start_date, expired_date
    	)
    OUTPUT inserted.promotion_id
    VALUES
    	('2020 Summer Promotion',0.25,'20200601','20200901'),
    	('2020 Fall Promotion',0.10,'20201001','20201101'),
    	('2020 Winter Promotion', 0.25,'20201201','20210101');
    ```
3. 




