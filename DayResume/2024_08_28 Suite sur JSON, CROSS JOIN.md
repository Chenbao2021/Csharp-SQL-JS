# SQL 
#### I - Cas où on n'a pas besoin d'utiliser openJson
1. Extraction de valeurs scalaires simples
    Vous avez un objet JSON simple, et vous voulez extraire une ou deux valeurs scalaires.
    * Mauvaise :
        ````
        SELECT
            value as Name
        FROM OPENJSON(@json)
        WHERE [key] = 'name'
        ````
    * Correcte :
        ````
        SELECT JSON_VALUE(@json, '$.name') as Name;
        ````
2. Extraction d'un sous-objet/sous-tableau JSON complet (Similiares)
3. Accéder à une valeur spécifique dans un tableau JSON
    Vous avez un tableau JSON et vous souhaitez extraire une valeur spécifique à partir de celui-ci.
    * Mauvaise:
        ````
        SELECT 
            value AS Name
        FROM OPENJSON(@json)
        WHERE [key] = 'name' AND JSON_VALUE(@json, '$.id') = 2;
        ````
    * Correcte:
        ````
        SELECT 
            JSON_VALUE(@json, '$[1].name') AS Name; -- Accéder directement au 2ème élément (indice 1)
        ````
4. Accéder à des données JSON dans une colonne d'une table
    Vous avez une colonne dans une table qui stocke des données JSON, et vous souhaitez accéder à une valeur spécifique dans ces données.
    * Mauvaise: 
        ````
        SELECT 
            p.ProductID, 
            value AS Color
        FROM Products p
        CROSS APPLY OPENJSON(p.ProductDetails)
        WHERE [key] = 'color';
        ````
    * Correcte : 
        ````
        SELECT 
            p.ProductID,
            JSON_VALUE(p.ProductDetails, '$.color') AS Color
        FROM Products p;
        ````

5. Exemple pratique : OPENJSON et JSON_VALUE
    ````
    -- Insérer toutes les barges dans #request_tow
    select 
    	json_value(c.value, '$.barge.id') as 'iSpotTowIds',
    	ISNULL(json_value(c.value, '$.quality[0]'), 'No quality') as 'sTowCargos',
    	json_value(c.value, '$.subQuality') as 'sTowCargosMoreInfos',
    	json_value(c.value, '$.barge.type') as 'sBargeType'
    into #request_tow
    from openjson(@json, '$.requests[0].convoys') as c
    ````
#### II - CROSS APPLY
``CROSS APPLY`` permet de lier chaque ligne d'une table à un ensemble de résultats générés dynamiquement par une fonction de table ou une sous-requête.
=> Lier des données provenant d'une sous-requête ou d'une fonction de table avec chaque ligne de la table principale.

Fonctionnement de ``CROSS APPLY``
1. __Évaluation Ligne par Ligne__: Prend chaque ligne de la table source et exécute une expression ou une fonction de table pour cette ligne.
2. __Retour de Lignes Multiples__ : Si l'expression ou  la fonction retourne plusieurs lignes, chaque ligne sera combinée avec la ligne d'origine.
3. __Filtrage et transofrmation__: Vous pouvez appliquer des conditions et transformer les résultats pour obtenir des donénes spécifiques, ce qui donne une grande flexibilité pour travailler avec des données complexes, comme JSON.

* Scénario 1 : Utilisation avec des Fonctions de Table :
    ````
    -- Fonction qui retourne les commandes d'un client donné
    CREATE FUNCTION GetCustomerOrders(@CustomerID INT)
    RETURNS TABLE
    AS
    RETURN 
    (
        SELECT OrderID, OrderDate, TotalAmount 
        FROM Orders 
        WHERE CustomerID = @CustomerID
    );
    ````
    ````
    -- Utilisation de CROSS APPLY avec la fonction
    SELECT 
        c.CustomerID,
        c.CustomerName,
        o.OrderID,
        o.OrderDate,
        o.TotalAmount
    FROM Customers c
    CROSS APPLY GetCustomerOrders(c.CustomerID) o;
    ````
    Résultat: Une table où chaque client est associé à chacune de ses commandes.

* Scénario 2 : Utilisation avec ``OPENJSON`` pour manipuler des données JSON :
    ````
    -- Exemple de données JSON
    INSERT INTO @Products (ProductID, ProductDetails) 
    VALUES (1, N'{"color": "red", "size": "M"}'),
           (2, N'{"color": "blue", "size": "L"}');
    ````
    ````
    -- Utilisation de CROSS APPLY avec OPENJSON pour extraire les détails produits
    SELECT 
        p.ProductID, 
        j.[key] AS Attribute, 
        j.value AS Value
    FROM @Products p
    CROSS APPLY OPENJSON(p.ProductDetails) j;
    ````
* Scénario 3 : Filtrage et Transformation Dynamique des Données : 
    Supposons que vous vouliez lister les produits avec un certain attribut, mais seulement si cet attribut est présent dans le JSON:
    ````
    SELECT 
        p.ProductID, 
        j.value AS Color
    FROM @Products p
    CROSS APPLY OPENJSON(p.ProductDetails) j
    WHERE j.[key] = 'color';
    ````
    * La clause ``WHERE j.[key] = 'color'`` est utilisée pour filtrer les résultats afin de ne garder que la ligne où la clé JSON est ``color``. Cela permet de récupérer uniquement la valeur associée à ``color`` dans le JSON.
    Et comme CROSS APPLY ne retourn que les lignes avec des résultats, donc on aura que les produit avec color dans product details.

* Exemple avec OPENJSON
``CROSS APPLY`` est une forme de jointure qui applique une fonction de table(Comme ``OPENJSON``) à chaque ligne d'une table source(``Products`` dans ce cas). Il permet de décomposer ou de démultiplier les données JSON pour chaque ligne de la table source.
Par exemple: 
    ````
    SELECT 
        p.ProductID, 
        value AS Color
    FROM Products p
    CROSS APPLY OPENJSON(p.ProductDetails)
    WHERE [key] = 'color';
    ````
    
