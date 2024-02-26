# Vocabulaire
- Debrancher des codes = Commenter des codes 
- Remonter des codes = récupérer des codes.

# SQL
***
#### I - DateDiff
***
L'utilisation de DATEDIFF permet de calculer la différence entre deux variables de type Date données,
Syntaxe SQL: 
````
DATEDIFF(datepart, startdate, enddate)
````
- datepart : Unités dans lesquelles DATEDIFF signale la différence entre startdate et enddate.
    Parmi les unités datepart couramment utilisées, citons __month__ et __second__ .
    Aussi on a __day__, __week__, __hour__, etc.

````
DATEDIFF(DAY, dcd.actual_bl_date, @today) = @delay_LOAD_P
````

***
#### II - Tables temporaires
***
Dans le pool SQL dédié, les tables temporaires existent au niveau de la session.
- Elles sont visibles seulement pour la session dans laquelle elles ont été créées et sont automatiquement supprimées quand cette session se ferme.

Les tables temporaires offrent un gain de performances, car __leurs résultats sont écrits en local__ et non dans un stockage distant.

Les tables temporaires sont créées en faisant simplement précéder le nom de votre table de __#__ .

Dans une session, si vous appelez deux fois le même procédure qui crée la même table temporaire, alors vaut mieux de vérifier l'existance de la table temporaire: 
````
IF OBJECT_ID('tempdb..#stats_ddl') IS NOT NULL
BEGIN
    DROP TABLE #stats_ddl
END
````
Donc, il est courant de voir la commande __DROP TABLE #....__ dans une procédure stocké lorsqu'on a finit d'utiliser la table temporaire.

***
#### III - Différence Vue matérialisé et Tables temporaires
***
La principale différence réside dans la persistance des données:
- Une vue matérialisée stocke les données de manière permanente.
- Une table temporaire stocke les données temporairement pendant la durée d'une session ou d'une transaction.
