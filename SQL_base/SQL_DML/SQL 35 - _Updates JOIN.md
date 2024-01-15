# Updates JOIN
## I - Introduction
We can use a join clause in the __UPDATE__ statement to perform a cross-table update.
```
UPDATE t1
SET
    t1.c1 = t2.c2,
    t1.c2 = expression,
    ...
FROM
    t1
    [INNER | LEFT] JOIN t2 ON join_predicate
WHERE
    where_predicate;
```

## II - Examples 

1. Calculate the sales commission for all sales staffs:
```
UPDATE sales.commissions
SET sales.commissions.commission = c.base_amount * t.percentage
FROM sales.commissions c INNER JOIN sales.targets t ON c.target_id = t.target_id;
```






