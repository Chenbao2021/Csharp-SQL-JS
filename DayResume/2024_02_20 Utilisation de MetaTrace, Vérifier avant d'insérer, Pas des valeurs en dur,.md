# SQL
***
#### I - Ne pas utiliser des valeurs en dur
***
Les valeurs d'une ligne peuvent être modofié, même les clé primaires(Exemple, on a supprimé une ligne sans faire exprès, puis on la rajoute, le clé primaire change car il incrémente).
Donc vaut toujours mieux utiliser une requête select pour obtenir une valeur, par exemple:
````
INSERT INTO dem_com_substatus(status_id, name, claim_in, claim_out, flag)
VALUES ((select status_id from dem_com_status where name = 'Estimated'), 'TB RISK - Missing Docs', 'N', 'Y', 0)
````

***
#### II - Vérifies toujours l'existance d'une valeur avant de l'insérer
***
Car les codes SQL peuvent relancer à plusieurs reprises dans le scriptsender, d'où la nécessité de vérifier si les données ont été déjà inséré dans la base de donnée.
Mais en générale il est important de faire cela, même dans les autres situations.
Exemple : 
````
IF NOT EXISTS (select 1 from dem_com_substatus where status_id = (select status_id from dem_com_status where name = 'Estimated')  and name = 'TB RISK - Missing Docs')
	INSERT INTO dem_com_substatus(status_id, name, claim_in, claim_out, flag)
	VALUES ((select status_id from dem_com_status where name = 'Estimated'), 'TB RISK - Missing Docs', 'N', 'Y', 0)
````

# C#
***
#### I - Utilisation de MetaTrace
***
US69092: Je veux savoir quelle procédure a utilisé par le bouton __Time Bar report__ dans la table __Retrieve Trading Claims__ . 
Mais c'est une table crée en C++ et j'ai aucune moyen de voir les codes(Même si c'est en C#). 
Dans ce cas la, on peut utiliser MetaTrace, pour connaître quelle procédure cette méthode a appelé.
Exemple :
````
ExecuteQuery : MED_SURCS_TIME_BAR_REPORT5 NULL, NULL, 'Y', NULL, NULL, 2, 'CHENBAOYU' - bytes sent:  402-bytes received: 33 787/time: 538ms
````
Aussi, dans MetaTrace on peut voir quelle service a eté utilisé pour exécuter une requête dans la colonne service.
