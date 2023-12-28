# UNION
## I - Introduction
UNION allow you to combine results of two SELECT statements into a single result set .
- The number and the order of the columns must be the same in both queries.
- The data types of the corresponding columns must be the same or compatible.

UNION: Removes all duplicate rows from the result sets.
UNON ALL: Retains the duplicate rows

JOIN: Appends the result sets horizontally.
UNION: Appends the result set vertically.


## II - Examples 

1. UNION and ORDER BY example
```
SELECT
    select_list
FROM
    table_1
UNION
SELECT
    select_list
FROM 
    table_2
ORDER BY 
    order_list
```



