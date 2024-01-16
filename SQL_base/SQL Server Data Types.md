***
# I - Introduction
***
A data type is an attribute that specifies the type of data that these objects can store.
- __Exact numeric data types__ : integer, decimal, or monetary amount, etc.
- __Approximate numeric data types__ : float, real, etc.
- __Date & Time data types__ : Datetime, Date, Time, datetime2, etc.
- __Character strings data types__ : char(fixed-length), varchar(variable-length)
- __Unicode character string data types__ : nchar, nvarchar
- __Binary string data types__: binary, varbianry
- __Other data types__ : cursor, rowversion, hierarchyid, etc.

***
# II - INT
***
BIGINT(8 bytes) > INT(4 bytes) > SMALLINT(2 bytes) > TINYINT(1 byte)
It is good pratice to use the smallest integer data type that can reliably contain all possible values.

SQL Server converts the integer constant greater thant 2147483647 to DECIMAL and not to BIGINT.

***
# III - DECIMAL
***
To store numbers that have fixed precision and scale, you use the __DECIMAL__ data type.
    
    DECIMAL(p, s)

NUMERIC and DECIMAL are synonyms. The DEC are short name of DECIMAL.

***
# IV - BIT
***
__BIT__ data type is an integer data type that can take a value of 0, 1, or __NULL__
SQL Server optimizes storage of __BIT__ columns.
SQL server converts any nonzero value to 1, 'True' to 1, and 'False' to 0.

***
# V - CHAR, NCHAR, VARCHAR, NVARCHAR
***
CHAR:
- To store the fixed-length, non-Unicode character strings .
- Use CHAR only when the sizes of values in the column are fixed. (If the string value is less than the length specified , SQL Server will add trailing space. However, whan you select this string value, SQL Server removes the trailing spaces before returning it).

NCHAR:
- To store the fixed-length, unicode character strings.
- NCHAR need 2 bytes to store a character, and CHAR only need 1 byte
- You muqr prefix the Unicode character string constants with the letter __N__, exemple :
    ```
    INSERT INTO test.sql_server_nchar(val)
    VALUES
        (N'あ')
    ```
VARCHAR:
    If no length specify, default value is 1.
    The storage size of a __VARCHAR__ is the actual length of the data stored plus 2 bytes.
    If we attempts to insert a new string data whose length is greater than the string length of the column, SQL server issued an error:
    ```
    String or Binary data would be truncated.
    ```
NVARCHAR: ...

***
# VI - DATETIME2, DATE, TIME
***
DATETIME2:
- To store both date and time in the database.
    ````
    DATETIME2[(fractional second precision)]
    ````
- The fractional second precision ranges from 0 to 9

- To insert a datetime2 value:
    - GETDATE()
        ````
        INSERT INTO production.product_colors (color_name, created_at)
        VALUES
            ('Red', GETDATE()); 
        ````
    - Literal value
        ```
        INSERT INTO production.product_colors (color_name, created_at)
        VALUES ('Green', '2018-06-23 07:30:20');
        ```
    - Default value of the created_at column:
        ```
        ALTER TABLE production.product_colors 
        ADD CONSTRAINT df_current_time 
        DEFAULT CURRENT_TIMESTAMP FOR created_at;
        ```
Date:
- Has only the date component, it takes 3 bytes to store a Date value.
    The default style is : 'YYYY-MM-DD'

TIME:
- time of a day based on 24-hour clock.
