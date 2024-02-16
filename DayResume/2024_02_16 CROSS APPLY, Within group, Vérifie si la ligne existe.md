# SQL 
***
#### I - Cross Apply
***
La requête en question : 
````
SELECT STRING_AGG(c.code, ',')
FROM (SELECT 685 as 'idm_cost', ';2;256;' as 'id_context_list') r
CROSS APPLY string_split(r.id_context_list, ';') as cx
INNER JOIN phm_ds_context c on c.id_context = cx.value
GROUP BY r.idm_cost
````

Dans cette requête SQL, l'utilisation de CROSS APPLY et INNER JOIN ensemble est un moyen de combiner les résultats de deux opérations différentes sur les données.
1. CROSS APPLY avec string_split : Permet de séparer la chaîne de caractère en plusieurs lignes en fonction du délimiteur.
    - L'alias __cx__ représente le résultat de l'opération 'CROSS APPLY string_split(r.id_context_list, ';') '.
        CROSS APPLY renvoie une table virtuelle contenant les valeurs séparé en fonction du délimiteur, et ces valeurs séparés seront dans une colonne 'value'.
    - 
2. INNER JOIN avec phm_ds_context : Permet de filtrer les lignes de la table phm_ds_context en fonction des valeurs générées par la fonction 'string_split'. 

Un autre cas d'utilisation de CROSS APPLY : Lors de l'utilisation de fonctions de table en ligne.
- Supposons que vous ayez une fonction de table en ligne qui prend une entrée de ligne et retourne un ensemble de lignes. Vous pouvez utiliser CROSS APPLY pour appliquer cette fonction à chaque ligne d'une autre table.
fn_GetSalesByProduct : Prend un ID de produit en entrée et retourne les ventes associées à ce produit.
    ````
    SELECT p.ProductID, s.SaleDate, s.Amount
    FROM Products p
    CROSS APPLY fn_GetSalesByProduct(p.ProductID) s;
    ````
    - Nous utilisons 'CROSS APPLY' pour appliquer la fonction 'fn_GetSalesByProduct' à chaque ligne de la table 'Products', ce qui nous donne les ventes associées à chaque produit.
    Cela permet d'obtenir les ventes de chaque produit dans une seule requête, en appliquant la fonction à chaque ligne de la table des produits.
    - Un plus, à quoi ressemble la fonction fn_GetSalesByProduct ? : 
    ````
    CREATE FUNCTION fn_GetSalesByProduct (@ProductID INT)
    RETURNS TABLE
    AS
    RETURN (
        SELECT SaleDate, Amount
        FROM Sales
        WHERE ProductID = @ProductID
    );
    ````

***
#### II - WITHIN GROUP
***
WITHIN GROUP is a clause used with some aggregate and anlytic function where you want an order-related result.
For string_agg you use it to mandate that the list is sorted in a certain order.
````
SELECT STRING_AGG(c.code, ',') within group (order by c.code asc)
FROM (SELECT 685 as 'idm_cost', ';1;4;32;' as 'id_context_list') r
CROSS APPLY string_split(r.id_context_list, ';') as cx
INNER JOIN phm_ds_context c on c.id_context = cx.value
GROUP BY r.idm_cost
````

WITHIN GROUP can also be used with the functions PERCENTILE_DISC and PERCENTILE_CONT. In fact, with these functions, the claus is mandatory.

***
#### III - Avant d'insérer une ligne, vérifie si la ligne existe déjà dans la table
***
Si on insére une ligne avec un key primaire déjà présent dans la table, ça peut provoquer une exception.
D'où la nécessité de vérifier avant d'insérer
````
if not exists (select 1 from phm_ds_reference where ref_name = 'pri_escalation_method' and code = 'G')
	INSERT INTO phm_ds_reference (ref_name, code, short_name, full_name, fore_color, value_char)
	VALUES('pri_escalation_method','G', 'Ratio Float Complex', 'Ratio Float Complex', '-16777216', 'Y')
GO
````


