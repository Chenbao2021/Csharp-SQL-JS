#### 1. La différence entre exec et exec()
* ``EXEC @requete``
    On exécute __une procédure stockée__ dont le nom est contenu dans la variable '@requete', cette méthode ne prend pas en charge l'exécution de requêtes SQL arbitraires contenues dans la variable.
    Exemple: ``EXEC @procedureName (où @procedureName = 'sp_help')``

* ``EXEC(@requete)``
    On exécute une instruction SQL dynamique dont le texte est contenu dans la variable ``@requete``. Cette méthode permet d'exécuter des requêtes SQL arbitraires construites dynamiquement.
    Exemple: ``EXEC (@query) (où @query = 'SELECT * FROM sys.objects')``

#### 2. La différence entre exec() et exec sp_executesql
* ``EXEC()``
    * Simple et directe à utiliser
    * Moins sécurisé car il est plus sujet aux attaques par injection SQL.

* ``EXEC sp_executesql``
    * Supporte les paramètres de manière sécurisée, réduisant le risque d'injection SQL.
    * Utilisation de paramètres d'entrée et de sortie.

#### 3. Requête dynamic et table temporaire
En SQL Server, les tables temporaires créées dans un contexte d'exécution dynamique ne sont pas accessible dans l'environnement d'appel principal.
Pour contourner le problème:
* Soit on utilise une table temporaire globale (##tmp)
* Soit on insère les résultats directement dans une table temporaire créée dans le contexte principal.

#### 4. Astuce d'utiliser TOP dans une requête dynamic
D'abord, on crée la table temporaire, puis on récupère les données avec SELECT TOP
````
-- Ajouter condition pour limiter les lignes s'il le faut.
IF @maxLines IS NOT NULL
BEGIN
	SET @get_vrn = @get_vrn +  ' 
		SELECT TOP (@maxLines)
		'''' AS donne_table,
		* FROM #GET_VRN;
		';
END
ELSE
BEGIN
	 SET @get_vrn = @get_vrn + '
		SELECT  			
		'''' AS donne_table,
		* FROM #GET_VRN
		';
END
````

