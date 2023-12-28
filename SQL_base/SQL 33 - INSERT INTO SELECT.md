# INSERT INTO SELECT
## I - Introduction
To insert data from other tables into a table, you use the following SQL Server __INSERT INTO SELECT__ statement:
```
INSERT [TOP (expression) [PERCENT]]
INTO target_table(column_list)
QUERY
```
- The query is any valid __SELECT__ statement that retrieves data from other tables.
- It must return the values that are corresponding to the columns specified in the __column\_list__


## II - Examples 
1. Inserts all addresses from the customers table into the addresses table:
    ```
    INSERT INTO sales.addresses(street, city, state, zip_code)
    SELECT street, city, state, zip_code
    FROM sales.customers
    ORDER BY first_name, last_name
    ```

2. Insert the top N of rows
    ```
    INSERT TOP(10)
    INTO sales.addresses(street, city, state, zip_code)
    SELECT
        street, city, state, zip_code
    FROM sales.customers
    ORDER BY first_name, last_name
    ```

3. Insert top 10 percent
    ```
    INSERT TOP (10) PERCENT  
    INTO sales.addresses (street, city, state, zip_code) 
    SELECT
        street,
        city,
        state,
        zip_code
    FROM
        sales.customers
    ORDER BY
        first_name,
        last_name;
    ```
4. 





