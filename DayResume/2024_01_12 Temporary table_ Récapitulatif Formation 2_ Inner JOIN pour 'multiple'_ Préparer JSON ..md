***
# SQL
***

1. Toujours renvoyer le même format de résultat dans une procédure, c'est à dire même nombre des tables.
    Pour se faire, on déclare les tables qu'on va retourner en premier ( et on l'alimenter au fur et à mésure ),
    et retournent les trois tables après chaque exécution même s'ils sont vides.
    __Exemple de la formation 2__:
        On a deux modes possibles : 'R' et 'C'. Et pour le mode 'C' on veut que le nombre des lignes de chaque table.
        Mais on retourne toujours 4 tables, avec 3 tables d'informations(même s'ils sont vides), et une table technique (code_error, message_error, nombre des lignes dans chacun des tables).

2.  Stocker le code_erreur et message_erreur dans une table technique, et autre informations techniques(Comme nombre des lignes dans la table, etc.)
    Cela permet de toujours renvoyer les même chôses.

3. On peut déclarer une table __sans l'alimenter tout de suite__, on l'alimente quand on a besoin.
4. Suite de 3, Un procédure executé __après__ la déclaration d'une table temporaire peut accéder à cette table, ce qui n'est pas le cas pour une fonction. 
    Donc on peut déclarer une table temporaire dans une procédure A, et l'accéder dans une procédure B qui est exécuté au sein de la procédure A, Cela permet d'alimenter une table temporaire.  
    =>  Procédure ne renvoient pas des résultats, mais ils alimentent. 
5. Le plus important dans une procédure est qu'il n'y a pas d'exception, tout est geré.
6. ville_nom_reel, et ville_name_reel , c'est probablement une faute de frappe, mais il faut confirmer avec ton supérieur.
7. Ajouter des commentaires 
8. Utiliser __INNER JOIN__ pour traiter des cas de 'multiple', jointure sur une valeur prcisé d'une parametre precisé.
Exemple:
    ```
    SELECT r.* 
    FROM region_cyu r 
    INNER JOIN #tmp_criteria c1 
    ON c1.name = 'region_sn' and c1.valeur_criteria = r.short_name
    ```
    Explication du code:
    - D'abord la requête fait un tri sur la table c1, où il  prend que les name = 'region_sn'
    - Puis parmi les lignes qui ont la valeur 'region_sn', il fait une jointure sur la critère.
    - Exemple, on a deux valeurs pour c1 ('PACA', 'GE'). Donc après la jointure, on aura que les lignes de la table c1 qui 
    ont des valeurs 'PACA' et 'GE'. 
9. Avec une table, on peut utiliser UPDATE pour des fonctions comme STR_AGG, SUBSTRING, LEFT , ça rend les codes plus lisible.

10. Préparation d'une requête JSON:
- Partie SELECT: Quand il y a des critère 'multiple', on doit ajouter des jointures internes :
    ```
    SELECT @request = 'SELECT * FROM region_cyu r'
    SELECT @requestJoinSN = @request + 'INNER JOIN #tmp_criteria as c1 ON c1.name = 'region_sn' AND c1.valeur_critere = r.short_name' 
    ```
- Partie WHERE: Quand il y a des critères comme 'LIKE', 'COMPARE|INF', 'COMPARE|SUP', on doit construire la clause where:
    ```
    DECLARE @where varchar(max) = ''
    ...
    SELECT @where = @where + CASE WHEN @where <> '' THEN ' AND ' ELSE '' END
    + CASE WHEN type_criteria = 'like' THEN nom_critere + ' LIKE '' ' + valeur_critere + '''  ' ... END
    ...
    ```

***
# C#
***
Quand on veut qu'une colonne soit auto sized :
```
mfGridTrackLogs.Columns[m_sTra_table_name].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
```
- mfGridTrackLogs = (Name) de la table MFControl.MFGrid qui est à l'affichage.
- .Columns[column_name] = Expliciter quel colonne on veut manipuler.
- .AutoSizeMode = Détermine si la colonne modifie automatiquement sa largeur et comment elle définit sa largeur par défaut.
- .Fill = La largeur de colonne s'ajuste afin que les largeurs de toutes les colonnes remplissent exactement la zone d'affichage du contrôle.