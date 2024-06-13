# C#
#### I - Envoyer JSON
Lorsque front-end envoie des données par une requête GET ( sous forme de paramètres ) au back-end, back-end peut transformer ces paramètres en une flux JSON afin que la procédure prend en entré seulement un  ``@json varchar(max)``

````C
public IHttpActionResult Get([FromUri] string emission_date = null )
{
		var parameters = new Dictionary<string, object>
			{
				{"emission_date", parsedDate }
			};

		// CYU 12JUN24 TS71691: Giving JSON to Database
		string jsonParam = JsonConvert.SerializeObject(parameters);

		Dictionary<string, object> dico = new Dictionary<string, object>
		{
			["json"] = jsonParam
		};
		return Ok(VRNDAL.GetAllExt(dico));
}
````
1. On spécifié les paramètres que la requête GET peut récupérer par [FromUri] dans la définition de la fonction
2. On crée une dictionnaire A avec tous les paramètres
3. On crée un autre dictionnaire B qui contient un attribut "json" qui contient la dictionnaire A.

# GIT
* ``GIT BRANCH`` : Afficher les branches locales
* ``GIT BRANCH -r``: Afficher les branches distanciels

# SQL
#### I - Toujours mettre un ISNULL(, 'default_value') pour une valeur nullable.
Cela évitera à des erreurs potentiels

#### II - Utiliser CONVERT plutôt que CAST
Au lieu d'utiliser CAST on utilise CONVERT pour rendre un code plus lisible.
Exemple:
````SQL
CONVERT(BIT, CASE WHEN ISNULL(DISCHARGE_AGREED, 'N') = 'Y' AND ISNULL(DISCHARGE, 'N') = 'Y' THEN 1 ELSE 0 END) [major_agreed_by_vet_discharge]
````

#### III - Trouver les lignes qui sont dans une table mais pas dans l'autre
1. EXCEPT
    ````sql
    SELECT colonne1, colonne2, ...
    FROM select1
    EXCEPT
    SELECT colonne1, colonne2, ...
    FROM select2;
    ````

2. LEFT JOIN
    ````
    SELECT t1.*
    FROM table1 t1
    LEFT JOIN table2 t2 ON t1.id = t2.id
    WHERE t2.id IS NULL;
    ````

#### IV - Utilisation de CTE(Common Table Expression)
````SQL
WITH UniqueIds AS (
    SELECT id
    FROM table1
    EXCEPT
    SELECT id
    FROM table2
)
SELECT t1.*
FROM table1 t1
JOIN UniqueIds u ON t1.id = u.id;
````
