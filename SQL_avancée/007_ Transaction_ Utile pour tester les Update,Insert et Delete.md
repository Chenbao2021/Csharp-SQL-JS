# Introduction
A transaction is a single unit of work that typically contains multiple T-SQL statements.

When executing a single statement such as ``INSERT``, ``UPDATE``, and ``DELETE``, SQl Server uses the autocommit transaction. In this case, __each statement is a transaction__.

To start a transaction explicitely:
1. you use the ``BEGIN TRANSACTION`` or ``BEGIN TRAN`` statement first.
2. Then execute one or more statements including ``INSERT``, ``UPDATE``, and ``DELETE``.
3. Finally, commit the transaction using the ``COMMIT``, or roll back using the ``ROLLBACK``.

# II - Utiliser les transactions pour tester des opérations sans compromettre l'intégrité de la base de donnée
Procéder ainsi est utile pour vérifier le résultat des opérations avant de les valider définitivement.
1. __Démarrer une transaction__:
    * Utilisez ``BEGIN TRANSACTION`` pour commencer une transaction
2. __Effectuer les opérations de test__:
    * Effectuez les insertions, modifications ou suppresssions que vous souhaitez tester.
3. __Vérifier les résultats__:
    * Vérifiez les résultats des opérations à l'aide de requêtes ``SELECT``
4. __Annuler ou valider les modifications__:
    * Si vous souhaitez annuler les modifications, utilisez ``ROLLBACK TRANSACTION``
    * Sinon, utilisons ``COMMIT TRANSACTION`` pour les conserver.

Exemple: 
````SQL
BEGIN TRANSACTION;
UPDATE e
SET
	position = null
FROM Employees e

-- Utilises SELECT pour visualiser les modifications
select * from Employees

-- Utilise ROLLBACK tant que c'est en test
ROLLBACK TRANSACTION;

-- Utilises commit que si le résultat de test sont cohérant
-- COMMIT TRANSACTION
````
* Isolation: Pendant que la transaction est ouverte, les autres utilisateurs ne voient pas les modifications non validées.
* Annulation sécurisée: Utiliser ROLLBACK TRANSACTION permet de revenir à l'état précédent si les résultats ne sont pas ceux escomptés.
* Validation conditionnelle:  Vous pouvez décider de valider les modifications seulement si vous êtes satisfait des résultats.

# III - Les désavantages d'utiliser transactions
1. Verrouillage des ressources:
    Les transactions peuvent entraîner des verrous sur les ressources (lignes, tables) qu'elles manipulent, ce qui peut entraîner des blocages (deadlocks) ou des conflits de verrouillage, surtout dans des systèmes à haute concurrence.

2. Possibilité de blocage (Deadlocks) :

3. Risques d'utilisation incorrecte :
    * Une utilisation incorrecte des transactions (par exemple, oublier de faire un COMMIT ou un ROLLBACK) peut entraîner des problèmes de cohérence des données.
    * Les erreurs dans la gestion des transactions peuvent conduire à des situations où les données restent dans un état intermédiaire non souhaité.

# IV - Erreurs fréquents
1. __Oubli de COMMIT ou ROLLBACK__
    Ne pas terminer une transaction par un ``COMMIT`` ou un ``ROLLBACK`` peut laisser la transaction ouverte, entraînant des verrous persistants et des blocages dans la base de données.
2. __Transaction imbriquées incorrectes__
    Éviter les transactions imbriquées, s'il le faut, bien comprendre ce que tu vas faire.
3. __Mauvaise gestion des erreurs__
    à revoir si besoin
4. __Deadlocks(Interblocages)__
    Les transactions peuvent provoquer des deadlocks lorsque deux transactions ou plus attendent indéfiniment les ressources verrouillées par l'autre.
    Exemple: 
    ````SQL
    BEGIN TRANSACTION;
    UPDATE TableA SET ColumnA = 'Value' WHERE Id = 1;
    UPDATE TableB SET ColumnB = 'Value' WHERE Id = 1;
    COMMIT TRANSACTION;
    ````
    ````
    BEGIN TRANSACTION;
    UPDATE TableB SET ColumnB = 'Value' WHERE Id = 2;
    UPDATE TableA SET ColumnA = 'Value' WHERE Id = 2;
    COMMIT TRANSACTION;
    ````
    
5. __Mauvaise utilisation des niveaux d'isolation__
    Utiliser un niveau d'isolation inapproprié peut entraîner des problèmes de concurrence, comme des lectures fantômes, des lectures sales ou des lectures non répétables.
    ````SQL
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    BEGIN TRANSACTION;
    -- Opérations
    COMMIT TRANSACTION;
    ````
    
6. __Verrouillage excessif__
    Les transactions longues peuvent entraîner un verrouillage excessif, affectant les performances et l'accès concurrent.
    => Réduire la durée des transactions en exécutant des opérations courtes et en validant fréquemment.