# C#
***
#### I - Pour transformer un dataTable en Json, on utilise JsonTools
***
On peut soit utiliser la méthode qui utilise les noms des colonnes : __JsonTools.DataTableToJSONWithColumnNames__
Soit utiliser la méthode qui utilise les indexes des colonnes : __JsonTools.DataTableToJSONWithColumnIndexes__

__Remarques__ : 
- Utiliser les indexes a une performance beaucoup plus grande, mais ça rend les codes très difficile à maintenir quand l'ordre des colonnes est important.

Exemple avec l'indexe : 
````
BEGIN
	SELECT ...
	FROM
	OPENJSON(@json_header)
	WITH (	idm_cost INT '$."1"'
			, idm_cost_family INT '$."2"'
			,family VARCHAR(50) '$."3"'
            ...
	) 
END 
````
Exemple avec Nom : 
````
select name , type_critere, valeur_critere
from OPENJSON (@json_criteria)
with (	name varchar(max) '$.name',
		type_critere varchar(max) '$.type',
		valeur_critere varchar(max) '$.value')
````

***
#### II - Chaque fois que tu modifies le valeur d'une variable en dur, il faut relancer l'application pour que les valeurs soient mise à jour.
***
Par exemple : 
- un string readonly.





