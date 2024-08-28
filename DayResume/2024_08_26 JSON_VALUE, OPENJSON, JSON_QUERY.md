# SQL
#### I - JSON_VALUE, OPENJSON et JSON_QUERY
OPENJSON : Transforme un JSON en une table SQL.
* Si c'est un objet : Une table avec des colonnes ``key``, ``value``, ``type``.
* Si c'est une table, alors on utilise ``WITH`` pour définir la table. 

JSON_VALUE: Récupérer une valeur simple
JSON_QUERY: Récupère un objet ou un tableau JSON sous forme de chaîne de caractères.

* JSON_VALUE
    * __Définition__: Extraire une valeur scalaire (Nombre, chaîne) à partir d'une expression JSON.
    * Exemple:
    ````SQL
    DECLARE @json NVARCHAR(100) = N'{"name": "Alice", "age": 30}';
    SELECT JSON_VALUE(@json, '$.name') AS Name;  -- Retourne "Alice"
    ````
* OPENJSON
    * __Définition__ : ``OPENJSON`` convertit du JSON en une table où chaque ligne représente un élément du JSON.
    * Exemple: 
    ````SQL
    DECLARE @json NVARCHAR(100) = N'[
        {"name": "Alice", "age": 30},
        {"name": "Bob", "age": 25}
    ]';
    SELECT * 
    FROM OPENJSON(@json)
    WITH (
        name NVARCHAR(50),
        age INT
    );
    ````

* JSON_QUERY
    * __Définition__ : ``JSON_QUERY`` renvoie un objet JSON ou un tableau JSON complet d'un JSON.
    * Exemple: 
    ````
    DECLARE @json NVARCHAR(100) = N'{
        "name": "Alice",
        "details": {"age": 30, "city": "Paris"}
    }';
    
    SELECT JSON_QUERY(@json, '$.details') AS Details;  -- Retourne {"age": 30, "city": "Paris"}
    ````
    
* Les erreurs les plus fréquentes:
    1. Type incompatible dans ``JSON_VALUE`` : Essayer d'extraire un objet avec ``JSON_VALUE`` au lieu d'une valeur scalaire.
    2. Mauvaise usage de ``JSON_QUERY`` pour des scalaires: Utilisation de ``JSON_QUERY`` pour extraire une valeur scalaire, au lieu de ``JSON_VALUE``.
    3. Manque de ``WITH`` dans ``OPENJSON``: Ne pas définir correctement les colonnes dans la clause ``WITH``.

#### II - Exemples qui utilise à la fois OPENJSON et JSON_QUERY
* Exemple 1: Extraction d'objets JSON imbriqués dans un tableau JSON
    Vous avez un tableau JSON où chaque élément contient un objet JSON imbriqué. Vous voulez extraire des valeurs scalaires ainsi que l'objet JSON imbriqué complet pour un traitement ultérieur.
    ````
    DECLARE @json NVARCHAR(MAX) = N'[
        {
            "id": 1,
            "name": "Alice",
            "contact": {"email": "alice@example.com", "phone": "123-456"}
        },
        {
            "id": 2,
            "name": "Bob",
            "contact": {"email": "bob@example.com", "phone": "789-012"}
        }
    ]';
    SELECT 
        id,
        name,
        JSON_QUERY(contact) AS FullContact
    FROM OPENJSON(@json)
    WITH (
        id INT,
        name NVARCHAR(50),
        contact NVARCHAR(MAX)
    );
    ````
    * ``OPENJSON(@json)`` : Ouvre le tableau JSON et le convertit en une table.
    *  ``WITH`` : Définit les colonnes 'id', 'name', et 'contact'.
    *  ``JSON_QUERY(contact)`` : Extrait l'objet JSON complet 'contact'(email et phone) pour chaque entrée, tout en laissant les autres valeurs scalaires(id et name) accessible directement.

* Exemple 2: Extraction d'objets scalaires et d'objets JSON partiels
    Vous avez un objet ``JSON`` contenant plusieurs champs, dont certains sont eux-même des objets ``JSON``. Vous voulez extraire à la fois des valeurs scalaires et des sous-objets ``JSON`` pour chaque enregistrement.

    ````
    DECLARE @json NVARCHAR(MAX) = N'{
        "orderId": 12345,
        "customer": {
            "name": "Alice",
            "contact": {"email": "alice@example.com", "phone": "123-456"}
        },
        "items": [
            {"product": "Laptop", "price": 1200},
            {"product": "Mouse", "price": 20}
        ]
    }';
    
    SELECT 
        JSON_VALUE(@json, '$.orderId') AS OrderId,
        JSON_VALUE(@json, '$.customer.name') AS CustomerName,
        JSON_QUERY(@json, '$.items') AS Items;
    ````
    
#### III - Cas où on doit utiliser WITH ou non 
L'utilisation de la clause WITH avec OPENJSON dépend du niveau de contrôle que vous souhaitez avoir sur la structure de sortie et les types de données.
    
* Cas où vous êtes obligé d'utiliser WITH:
    1. Définir des colonnes spécifiques avec des types de données : 
        ````
        DECLARE @json NVARCHAR(MAX) = N'[
            {"id": 1, "name": "Alice", "age": 30},
            {"id": 2, "name": "Bob", "age": 25}
        ]';
        SELECT id, name, age
        FROM OPENJSON(@json)
        WITH (
            id INT,
            name NVARCHAR(50),
            age INT
        );
        ````
    2. Extraction de données imbriquées:
        ````
        DECLARE @json NVARCHAR(MAX) = N'{
            "orderId": 123,
            "customer": {
                "name": "Alice",
                "contact": {"email": "alice@example.com", "phone": "123-456"}
            }
        }';
        SELECT 
            JSON_VALUE(@json, '$.orderId') AS OrderId,
            name,
            email
        FROM OPENJSON(@json)
        WITH (
            name NVARCHAR(50) '$.customer.name',
            email NVARCHAR(50) '$.customer.contact.email'
        );
        ````
    3. Conversion de types
        WITH permet de convertir les types de données, par exemple, convertir une valeur JSON en 'INT' ou 'DATETIME'.

#### IV - Cas où on n'a pas besoin d'utiliser openJson
1. Extraction de valeurs scalaires simples
2. 
