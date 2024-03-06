# SQL 

***
#### I - Date, Time, DateTime, DateTime2 en SQL
***
- __DATE__ : Ce type de données est utilisé pour stocker uniquement des informations sur la date, sans tenir compte de l'heure ou du fuseau horaire. Par exemple: "2024-03-04" .
- __TIME__ : Pour stocker uniquement des informations sur l'heure, sans tenir compte de la date ou du fuseau horaire. 
    Par exemple : "14:30:00".
- __DATETIME__ : Ce type de données est utilisé pour stocker à la fois la date et l'heure, mais il peut manquer la précision sur les fractions de seconde. Par exemple, "2024-03-04 14:30:00".
- __DATETIME2__ : Ce type de données est similaire à DATETIME, mais il permet une précision plus élevée, en stockant les fractions de seconde avec une précision allant jusqu'à 100 nanosecondes. Cela rend plus précis que DATETIME. Par exemple, "2024-03-04 14:30:00.1234567"