### SET ROWCOUNT
* __Portée__: Affecte toutes les opérations de requête suivantes jusqu'à ce qu'il soit réinitialisé.
* __Utilisation__: Limite le nombre de lignes affectées par des opérations 'SELECT', 'INSERT', 'UPDATE' ou 'DELETE'.
* __SYNTAXE__ : 
    ````
    SET ROWCOUNT 10;
    SELECT * FROM Employees;
    SET ROWCOUNT 0; -- Réinitialise la limite
    ````
* __Réinitialisation nécessaire__ :
    Vous devez réinitialiser 'ROWCOUNT' à 0 pour supprimer la limite, sinon elle affectera toutes les requêtes suivantes dans la session.
* __Compabilité__ : Principalement utilisé dans SQL Server.

### TOP
* __Portée__ : Affecte uniquement l'instruction 'SELECT', 'INSERT', 'UPDATE' ou 'DELETE' spécifique où il est utilisé.
* __Utilisation__ : Limite le nombre de ligne retournées ou affectées par une seule instruction.
* __Syntaxe__ : 
    ````
    SELECT TOP 10 * FROM Employees;
    
    -- Utilisation avec INSERT
    INSERT INTO NewTable (col1, col2)
    SELECT TOP 5 col1, col2 FROM OldTable;
    
    -- Utilisation avec UPDATE
    UPDATE TOP (10) Employees SET Salary = Salary * 1.1 WHERE Department = 'Sales';
    
    -- Utilisation avec DELETE
    DELETE TOP (10) FROM Employees WHERE Department = 'Sales';
    ````
* Simplicité: 
* Compatibilité: Utilisé princiaplement dans SQL Server.

### LIMIT
* __Portée__: Affecte uniquement l'instruction __'SELECT'__ spécifique où il est utilisé.
* __Utilisation__: Limite le nombre de ligne retournées par une seule instruction 'SELECT'.
* __Syntaxe__ : 
    ````
    SELECT * FROM Employees LIMIT 10;
    ````
* __Compatibilité__ : Utilisé principalement dans MySQL, PostgreSQL, SQLite.


