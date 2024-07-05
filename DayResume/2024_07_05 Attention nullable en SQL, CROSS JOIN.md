# SQL 
### I - Vérifie si une attribut utilisé est nullable ou pas.
````sql
SELECT ROW_NUMBER() OVER (ORDER BY sp.eta ASC) AS [Index], vp.*, sp.sea_passage_num, sp.start_num, sp.end_num
	INTO #voy_portcall_with_order
	FROM nsh_voy_sea_passage sp
	left join nsh_voy_portcall vp on sp.voyage_id = vp.voyage_id and vp.num = sp.end_num
	WHERE sp.voyage_id = 278343
````
Ici, sp.eta peut contenir des valeurs null, ce qui le rend de faux résultats dans un order by.

### II - CROSS JOIN pour fusionner deux tables.
Quand on a deux tables, Tab_A(col1, col2, col3) et Tab_B(colA, colB).
Et on veut insérer une ligne (col1, col2, col3, colA, colB) dans une table, alors on peut utiliser CROSS JOIN pour le réaliser.
````sql
Select 
    *
INTO TAB_C
FROM
    TAB_A
    CROSS JOIN TAB_B
````
=> Donc TAB_C aurait une structure comme: (col1, col2, col3, colA, colB)

Rappel: CROSS JOIN est une opération qui combine chaque ligne de la première table avec chaque ligne de la deuxième table. Il produit un produit cartésien des deux tables, ce qui signifie que si la première table contient N lignes et la deuxième table contient M lignes, le résultat du CROSS JOIN contiendra N*M lignes.

