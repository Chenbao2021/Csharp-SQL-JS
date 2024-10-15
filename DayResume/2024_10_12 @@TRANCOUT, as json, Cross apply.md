# SQL
***
## I - @@TRANCOUNT
Quand on crée une procédure d'ajout, on peut utiliser une transaction pour éviter des inserts avec des informations fausses ou incomplets.

Exemple
````sql
BEGIN transaction T1
...
if(@error)
BEGIN
    if @@TRANCOUNT > 0
        rollback
    return;
END
...
COMMIT TRANSACTION T1
````
* ``@@TRANCOUNT``: Retourne le nombre de transaction actives pour la session du cours.
* ``rollback``: Cela annule tous les changements effectués par la transaction en cours et restaure la base de données à l'état qu'elle avait avant le début de la transaction.

## II - AS JSON
Quand on utilise openjson, on peut récupérer des informations avec ``as json``
Par exemple:
````js
...
from openjson ( @json_params )  
with (   
    ...
	[chapters_json] nvarchar(max) '$.chapters' as json	
) p;
````
* Ceci indique que la donnée extraite est traitée comme du JSON. Cette clause spécifie que le résultat doit être interprété ou formaté en tant que JSON.
* Et puis, on peut la manipuler avec openjson à nouveau.

## III Cross apply
``CROSS APPLY`` est une clause SQL qui permet de joindre chaque ligne d'une table avec les résultats d'une fonction **table-valued**(CROSS APPLY doit être suivi par une table), souvent utilisée pour travailler avec des données JSON ou XML.
````SQL
SELECT
    ...
FROM #params p
CROSS APPLY
OPENJSON(p.[chapters_json])
with (
    [id_chapter] int '$.value'
) hd
````
* Comme chaque ligne de #params est un NVARCHAR qui contient un JSON, donc on utilise OPENJSON sur chaque ligne.




