# OFFSET FETCH
## I - Introduction
The __OFFSET__ and __FETCH__ clauses are the options of the ORDER BY clause.
Both allow us to limit the number of rows to be returned by a querry.

- OFFSET: Specifies the number of rows to skip before starting to return rows from the query.
- FETCH: Specifies the number of rows to return after the OFFSET clause has been processed.

We can't use __FETCH__ without using __OFFSET__.

Syntaxe:
```
ORDER BY column_list [ASC | DESC]
OFFSET 
    offser_row_count {ROW | ROWS}
FETCH {FIRST | NEXT} 
    fetch_row_count {ROW | ROWS} ONLY
```
- row_count  : Can be a constant, variable or parameter that >= 0.
- FETCH     : It is optional, .
- FIRST/NEXT: Synonyms respectively, we can use them interchangeably.
- ROW/ ROWS : Same as FIRST/NEXT.

They are good to implement the __query paging solution__ (Divide a large data into a smalller discrete pages).
## II - Examples
1. To skip the first 10 products and return the rest, you use the __OFFSET__ clause as shown in the following statement:
    ```
    SELECT
        product_name,
        list_price
    FROM
        production.products
    ORDER BY
        list_price,
        product_name 
    OFFSET 10 ROWS;
    ```
2. As question 1, but now we only want the enxt 10 products:
    ```
    SELECT
        product_name,
        list_price
    FROM
        production.products
    ORDER BY
        list_price,
        product_name 
    OFFSET 10 ROWS 
    FETCH NEXT 10 ROWS ONLY;
    ```
3. To get the top 10 most expensive products :
    ```
    SELECT
        product_name,
        list_price
    FROM
        production.products
    ORDER BY
        list_price DESC,
        product_name 
    OFFSET 0 ROWS 
    FETCH FIRST 10 ROWS ONLY;
    ```
4. 
