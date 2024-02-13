# SQL 
***
#### I - sp_helptext
***
Quand tu as une procédure et que tu veux juste regarder sa structure, alors tu peux utiliser sp_helptext pour découvrir sa structure.
Exemple : 
````
sp_helptext MED_CT_LINE_COST_PARAM_AMEND_RETRIEVE
````

***
#### II - CTE
***
- CTE(Common table expression) :  spécifie un jeu de résultats nommé temporaire .
    - Simplifie logique des requêtes complexes et le rendent plus facile de le maintenir.
    - Une fois crée, on peut l'utiliser comme des tables temporaires.
    - Exemple:
    ````
    WITH NFDExec (id_nfe, all_exec_full_name)
	AS
	(
		SELECT r.cost, STRING_AGG(e.full_name, ';')
		FROM #results r
		CROSS APPLY STRING_SPLIT(r.[$id_execution_list], ';') as ex
		INNER JOIN execution e ON e.id_execution = ex.value
		GROUP BY r.cost
	)
    ````

***
#### III - Apply(SQL)
***
- L'opérateur d'intra jointure APPLY permet de réaliser des jointures entre une ligne d'une table et le contenu d'une colonne de la même table


# C#
***
#### I - Select pour une dataTable
***
````
...
DataRow[] rows = dataTable.Select("id = " + idToFind);
if (rows.Length > 0)
{
    string name = rows[0]["name"].ToString();
    Console.WriteLine("Le nom correspondant à l'ID " + idToFind + " est : " + name);
}
````
Et si on a une chaîne de caractère :
````
...
DataRow[] rows = dataTable.Select("name = '" + nameToFind + "'");
...
````
***
#### II - Alimenter une dictionary
***
````
Dictionary<int, string> idToNameMap = new Dictionary<int, string>();
foreach (DataRow row in dataTable.Rows) 
{
    int id = Convert.ToInt32(row["id"];
    string name = Convert.ToString(row["name"])
    idToNameMap.Add(id, name);
}
````

