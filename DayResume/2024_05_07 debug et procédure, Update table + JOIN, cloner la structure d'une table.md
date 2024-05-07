# SQL
### I - Update a table using another table by JOIN.
````
UPDATE nsm
SET full_name = tmp.full_name, display_order = tmp.displayOrder
FROM nshm_speed_mode nsm
INNER JOIN #tmp_nshm_speed_mode_update tmp ON nsm.id_speed_mode = tmp.idSpeedMode
````
* Source de la mise à jour - __'FROM'__ et __'JOIN'__ :
    * __FROM nshm_speed_mode nsm__ : Cette clause spécifie que l'alias nsm fait référence à la table réelle nshm_speed_mode, qui est la table source sur laquelle la mise à jour sera appliquée.
    * __INNER JOIN #tmp_nshm_speed_mode_update tmp ON nsm.id_speed_mode = tmp.idSpeedMode__ : Cela signifie que la mise à jour ne s'applique qu'aux lignes de nsm pour lesquelles il existe un enregistrement correspondant dans tmp.
    * __INNER JOIN__ est plus prioritaire que __FROM__ 

### II - Cloner une table
Si on veut cloner la structure d'une table, on peut procéder ainsi:
````
select *
INTO #tmp
FROM nshm_speed_mode
WHERE id_speed_mode = 0
````
Cette méthode recopie la structure de la table nshm_speed_mode dans #tmp

### III - Debug et procédure
La première chose qu'on fait quand on doit écrire une nouvelle procédure, est d'__implémenter un debug mode__ !!! qui s'active grâce à une variable @debug.
Par exemple: 
````
...
if @debug = 1
	begin
		select '' '#tmp_nshm_speed_mode_update',CASE cancelled WHEN 1 THEN 'C' ELSE 'A' END as 'cancelled', * from #tmp_nshm_speed_mode_update
	end
...
````
* Cela peut t'aider à vérifier si les données qui arrivent dans la base de données sont correctes.

La deuxième chose à faire est de __sauvegarder un exemple de JSON de test__ quelque part, car tester directement dans SQL Server est dix fois plus rapide que de faire le test en utilisant l'application.
