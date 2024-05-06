# SQL
#### I - OUTER APPLY
* Fonctions table_valued: Des fonctions qui retournent un ensemble de données structuré comme une table.

L'opérateur 'OUTER APPLY' dans SQL Server fonctionne de manière similaire à 'LEFT JOIN', mais avec des fonctions table-valued qui prennent des colonnes de la table externe comme paramètre.
Exemple 1: Nous avons 2 tables Employes et Ventes. Nous voudrons afficher le nom de chaque employé avec le montant le plus élevé de ses ventes.
````
SELECT E.Nom, V.Montant
FROM Employes E
OUTER APPLY (
    SELECT TOP 1 Montant
    FROM Ventes
    WHERE EmployeId = E.Id
    ORDER BY Montant DESC
) V;
````
* Pour chaque employé dans la table 'Employes', la sous-requête dans 'OUTER APPLY' est exécutée.

Exemple 2: Nous voulons lister chaque professeur avec la liste de ses cours formatée en JSON
````
SELECT P.Nom, C.CoursJSON
FROM Professeurs P
OUTER APPLY (
    SELECT C.NomCours, C.Salle
    FROM Cours C
    WHERE C.ProfesseurId = P.Id
    FOR JSON PATH
) AS C(CoursJSON);
````
* Pour chaque professeur dans la table 'Professeur', la sous-requête 'OUTER APPLY' est exécutée.
* 'FOR JSON PATH' convertit les lignes sélectionnées de la table 'Cours' en une chaîne JSON. Le 'PATH' donne une structure claire où chaque objet dans le JSON représente un cours.

Exemple:
````
select b.*
into #tmp_nshm_speed_mode_update
from OPENJSON(@json_params) 
WITH (list_nshm_speed_mode NVARCHAR(MAX) '$.data' AS JSON) as a
	OUTER APPLY OPENJSON (list_nshm_speed_mode) 
	WITH (	idSpeedMode int '$.id',
			shortName varchar(max) '$.shortName',
			fullName varchar(max) '$.fullName',
			displayOrder int '$.displayOrder',
			isDefault bit '$.isDefault',
			dateUpdLocal datetime '$.dateUpdLocal',
			cancelled bit '$.cancelled'
	) as b
````
* Traitement des données JSON avec 'OPENJON' et 'WITH':
    * 'OPENJON': Une fonction qui parse le JSON pour en faire une table SQL
    * 'WITH': Spécifie le schéma de sortie de la table générée par 'OPENJSON'. Dans ce cas, il extrait la propriété 'data' du JSON sous forme de colonne nommée 'list_nshm_speed_mode', qui est elle-même traitée comme un objet JSON('AS JSON').
* Utilisation de 'OUTER APPLY' pour décomposer plus loin le JSON:
    * "OUTER APPLY" exécute la fonction OPENJSON pour chaque ligne de la table résultante de 'a'



#### II - Les options pour formater le résultat d'une requête SQL en JSON
* FOR JSON PATH
C'est une option en SQL Server qui permet de formater le résultat d'une requête SQL en JSON.
'PATH' permet de contrôler la structure du JSON de sortie en spécifiant les noms des propriétés JSON dans les alias des colonnes.
Par exemple: info.address.rue et info.address.codePostale sera imbriqué.

* INCLUDE_NULL_VALUES
Par défaut, 'FOR JSON' n'inclut pas les paires clé-valeurs pour les colonnes qui ont des valeurs NULL dans le JSON de sortie. L'option 'INCLUDE_NULL_VALUES' change ce comportement en incluant toutes les clés avec des valeurs null.

* WITHOUT_ARRAY_WRAPPER
Par défaut, 'FOR JSON' encapsule le JSON généré dans un tableau. 
    * Avec tableau(par défaut): Le JSON est retourné comme un tableau d'objets.
    * Sans tableau("WITHOUT_ARRAY_WRAPPER"): Le JSON est retourné comme un seul objet JSON ou une séquence d'objets sans un tableau global.

#### III - JSON_QUERY
Particulièrement utile pour manipuler et récupérer des données structurées comme des objets ou des tableaux JSON dans un champ JSON plus grand.
* Fonctionnement de JSON_QUERY
'JSON_QUERY' prend deux arguments:
    * expression: Une expression JSON, généralement une colonne de table qui contient des données JSON.
    * path: une expression de chemin qui spécifie la propriété JSON à extraire. Ce chemin suit la syntaxe des chemins JSON standard.
* Quand utiliser JSON_QUERY
    * Extraire des objets ou des tableaux complets sans risquer de rtourner des valeurs scalaires.
    * Préserver la structure de l'objet ou du tableau JSON lors de l'extraction.

Exemple: nous avons une table Employes avec une colonne Details contenant des données JSON sur chaque employé, incluant des informations sur leurs compétences et leurs projets.

````
CREATE TABLE Employes (
    Id INT,
    Nom NVARCHAR(50),
    Details NVARCHAR(MAX)
);

INSERT INTO Employes (Id, Nom, Details)
VALUES 
(1, 'Jean Dupont', '{"competences": ["SQL", "C#"], "projets": [{"nom": "Projet A", "annee": 2022}, {"nom": "Projet B", "annee": 2023}]}'),
(2, 'Marie Curie', '{"competences": ["SQL Server", "PowerBI"], "projets": [{"nom": "Projet C", "annee": 2021}]}');
````
Si on veut extraire l'array des projets pour chaque employé:
````
SELECT 
    Nom,
    JSON_QUERY(Details, '$.projets') AS Projets
FROM 
    Employes;
````

