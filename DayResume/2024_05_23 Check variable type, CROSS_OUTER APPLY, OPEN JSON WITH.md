# SQL
#### I - Vérifier que tous les types sont cohérants
Par exemple : select @A = B from tab_B , vérifies bien que le type de @A est bien égale à B.
Cela rend tes codes __plus rigoureux__, même si parfois char et varchar, varchar et int, int et smallint s'entreutilise.

#### II - OPEN JSON () WITH () as table_from_JSON
La fonction __'OPENJSON'__ en SQL Server est utilisée pour anlyser des données JSON et les transformer en une table relationnelle.
````
from OPENJSON(@json_params) 
    WITH (list_nshm_propulsion_mode NVARCHAR(MAX) '$.data' AS JSON) as pm
````
1. __'OPENJSON(@json_params)' :__
    * __'@json_params'__ est une variable qui contient une chaîne JSON. La fonction __'OPENJSON'__ est utilisée pour analyser cette chaîne JSON.
    * __'OPENJSON'__ retourne une table avec trois colonnes par défaut: 'key', 'value' et 'type'.
2. __'WITH' Clause:__
    * La clause __'WITH'__ est utilisée pour définir la structure de la table de sortie. Elle spécifie les colonnes que vous voulez extraire et leur type de données.
3. __'list_nshm_propulsion_mode NVARCHAR(max) '$.data' AS JSON'
    * __'list_nshm_propulsion_mode'__ est le nom de la colonne dans la table de sortie.
    * __'$.data'__ est un chemin JSON utilisé pour accéder à une partie spécifique du document JSON. __Le symbole '$' représente la racine du coument JSON__, et __'.data'__accède à l'élément 'data' sous la racine.
    * __'AS JSON'__ indique que la valeur extraite est elle-même un objet JSON.
4. __'as pm'__ 
    * un alias pour la table résultante de 'OPENJSON'. Cet alias peut être utilisé pour faire référence à cette table dans d'autre parties de la requête SQL.

L'enselbme de résultats est nommé 'pm' pour une utilisation ultérieure dans la requête.

#### III - CROSS APPLY
__'CROSS APPLY'__ est une opération de jointure spéciale en SQL Server qui permet d'__appliquer une fonction de table à chaque ligne d'une table , et de joindre les résultats de cette fonction de table à la ligne d'origine__.

* OPEN JSON est une fonction de table.

__Exemple d'utilisation__
* Imaginons que nous avons une table __'Employees'__ qui contient des informations sur les employés, et une fonction de table __'GetEmployeeProjects'__ qui retourne les projects assignés à un employé spécifique.

* Fonction de table 'GetEmployeeProjects':
    ````
    CREATE FUNCTION GetEmployeeProjects(@EmployeeID INT)
    RETURNS TABLE
    AS
    RETURN 
    (
        SELECT ProjectId, ProjectName
        FROM Projects
        WHERE EmployeeID = @EmployeeID
    )
    ````
* Utilisation de CROSS APPLY
    Pour obtenir une liste de tous les employés avec leur projets, nous pouvons utiliser 'CROSS APPLY':
    ````
    SELECT 
        e.EmployeeID,
        e.EmployeeName,
        e.ProjectID,
        e.ProjectName
    FROM
        Employee e
    CROSS APPLY
        GetEmployeeProjects(e.EmployeeID) p
    ````
    * 'Employees e' : La table principale contenant les employés.
    * 'GetEmployeeProjects(e.EmployeeID)': La fonction de table appelée pour chaque ligne de 'Employees', utilisant 'EmployeeID' comme paramètre.
    * 'p': Alias pour les résultats de la fonction de table, qui sont ensuite joints aux colonnes de 'Employees'.

__Comparaison avec 'OUTER APPLY'__
La différence principale est que __'CORSS APPLY'__ ne renvoie que les lignes de la table principale pour lesquelles la fonction de table retourne des résultats. Si la fonction de table ne retourne aucun résultat pour une ligne donnée, cette ligne n'apparaît pas dans le résultat final.

Par contre, 'OUTER APPLY' inclut également les lignes de la table principale pour lesquelles la fonction de table ne retourne aucun résultat, en ajoutant des valeurs NULL pour les colonnes de la fonction de table.

Quand une colonne de la table retourné par la fonction ne doit pas être null, utilises CROSS JOIN pour éviter d'insertion des valeurs null 



