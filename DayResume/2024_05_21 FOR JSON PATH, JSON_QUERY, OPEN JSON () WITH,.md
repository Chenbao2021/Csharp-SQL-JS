# SQL
#### I - FOR JSON PATH
* __Transformation des résultats en JSON__ :
    Lorsque vous ajoutez __'FOR JSON PATH'__ à une requête SQL, SQL Server formate le résultat sous forme de document JSON.
* __Contrôle de la structure__ :
    La structure du JSON est déterminée par les alias donnés aux colonnes dans la clasue SELECT.
    __Les alias sont utilisés comme chemin pour créer une structure JSON imbriquée__.
    Par exemple, utilisation de points(.) dans les alias permet de créer des objets imbriqués.

* Exemples:
    ````
    SELECT 
        EmployeeID AS 'Employee.EmployeeID',
        FirstName AS 'Employee.FirstName',
        LastName AS 'Employee.LastName',
        Departments.DepartmentID AS 'Employee.Department.DepartmentID',
        DepartmentName AS 'Employee.Department.DepartmentName'
    FROM 
        Employees
    JOIN 
        Departments ON Employees.DepartmentID = Departments.DepartmentID
    FOR JSON PATH
    ````
    Donne
    ````
    [
        {
            "Employee": {
                "EmployeeID": 1,
                "FirstName": "John",
                "LastName": "Doe",
                "Department": {
                    "DepartmentID": 101,
                    "DepartmentName": "Sales"
                }
            }
        },
        {
            "Employee": {
                "EmployeeID": 2,
                "FirstName": "Jane",
                "LastName": "Smith",
                "Department": {
                    "DepartmentID": 102,
                    "DepartmentName": "Marketing"
                }
            }
        }
    ]
    ````
* __Avantages de 'FOR JSON PATH'__
    * __Flexibilité__: On peut créer des structures JSON complexes et imbriquées en contrôlant les alias des colonnes.
    * __Lisibilité__: Le JSON produit est plus lisible et conforme aux spécifications requises pour l'usage des données dans des applications web ou des API.
    
#### II - JSON_QUERY
* La fonction 'JSON_QUERY' en SQL Server est utilisée pour extraire un fragment JSON (un objet ou un tableau JSON) d'un document JSON plus large. 
* Elle est particulièrement utile pour obtenir des sous-éléments d'un document JSON sans les convertir en types de données SQL, en conservant leur format JSON.

* __Fonctionnement de 'JSON_QUERY'__
__'JSON_query'__ permet de sélectionner une portion d'un document JSON, telle qu'un objet ou un tableau, et de la renvoyer en tant que texte JSOn.Cela permet de manipuler des données JSON plus facilement dans SQL Server.

* __Syntaxe__
    ````
    JSON_QUERY ( expression [ , path ] )
    ````
    * __expression__ : Une expression qui évalue une chaîne de caractères au format JSON.
    * __path__(facultatif) : Un chemin qui spécifie l'élément JSON à extraire. Si ce chemin est omis, 'JSON_QUERY' renvoie l'intégralité de l'expression JSON.

* __Utilisation de 'JSON_QUERY' pour éviter les erreurs__
    Une des raisons d'utiliser 'JSON_QUERY' est d'éviter les erreurs qui se produisent lorsque vous tentez de sélectionner un élément JSON qui est lui-même un objet ou un tableau, et non une valeur scalaire.

* __exemple__ :
    * Code
        ````
        select @json_final = (
        	select 
        		@count as 'nbTotalRow',
        		-- CYU 30APR24 US68327
        		JSON_QUERY(@json_values) 'data'
        		FOR JSON PATH,WITHOUT_ARRAY_WRAPPER,INCLUDE_NULL_VALUES)
        select @json_final
        ````
    * @json_values est une chaîne de caractère représente un JSON, sans JSON_QUERY, @json_values serait traîté comme une simple chaîne de caractères.
        ````
        {
        	"nbTotalRow": 5,
        	"data": "[{\"id\": 1, \"name\": \"example1\"}, {\"id\": 2, \"name\": \"example2\"}]"
        }
        ````
    * L'utilisation de 'JSON_QUERY' en conjonction avec 'FOR JSON PATH' est nécessare lorsque vous devez insérer un fragment JSON déjà formé (comme un objet ou un tableau) dans le résultat JSON final, sans que ce fragment soit converti en une chaîne de caractères littérale.

#### III - OPEN JSON () WITH
__OPENJSON__ est une fonction en T-SQL qui permet de lire et de convertir des données JSON en ligne et colonnes.
__WITH__ est utilisée pour définir la structure que doivent avoir les données JSON une fois transformées en table.

Exemple:
````
SELECT id, name, age, city, zip
FROM OPENJSON(@json)
WITH (
    id INT 'strict $.id',
    name NVARCHAR(100) '$.name',
    age INT '$.age',
    city NVARCHAR(50) '$.address.city',
    zip NVARCHAR(10) '$.address.zip'
);
````

__Les erreurs fréquentes__
* Chemin JSON incorrect: Utilisation de chemin JSON incorrects dans la clause WITH entraînant des résultats inattendus.
* Types de données incompatibles: Conversion des types de données incorrects, par exemple, tenter de convertir un texte en 'INT'.
* Syntaxe JSON malformée: JSON malformé dans les variable ou colonne source.
* Omission de la clause 'strict': Non-utilisation de 'strict' pour des champs obligatoires, ce qui peut entraîner des données nulles ou par défaut non désirées.


