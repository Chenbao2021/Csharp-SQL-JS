# LIKE
## I - Introduction
The __LIKE__ is a logical operator that determines if a character string matches a specified pattern.
Syntax:
```
column | expression LIKE pattern [ESCAPE escape_character]
```
__Pattern__ is a sequence of characters to search for in the column or expression. It can include the following valid wildcard characters:
- %                     : Any string of zero or more chatacters.
- _                     : Any single character.
- [list of characters]  : Any single character within the specified set.
- [character-character] : Any single character within the specified range.
- [^list of characters] : Any single character not within a list or a range.

Escape character : Treat the wildcard characters as the regular characters.
```
WHERE
    comment LIKE '%30!%%' ESCAPE '!';
```
## II - Examples
1 - Finds the customers whose last name strats with the letter __z__ :
```
WHERE
    last_name LIKE 'z%'
```

2 - Finds the customers whose last name ends with the string __er__ :
```
WHERE
    last_name LIKE '%er'
```

3 - Finds the customers whose last name starts with the letter __t__ and ends with the letter __s__ :
```
WHERE
    last_name LIKE 't%s'
```

4 - Finds the customers whose the second character of last name is the letter __u__ :
```
WHERE
    last_name LIKE '_u%'
```

5 - Finds the customers where the first character in the last name is __Y__ or __Z__ :
```
WHERE
    last_name LIKE '[YZ]'
```

6 - Finds the customers where the first character in the last name is not the letter in the range __A__ though __X__ :
```
WHERE
    last_name LIKE '[^A-X]%'
```
    