# Générales
***
#### I - Les raccourcis
***
- ctrl + N : Créer une nouvelle fiche.
- ctrl + E : Exécuter la requête en surbrillance
- ctrl + G : Aller à la ligne. 

***
#### II - Écran C++ et MetaTrace
***
Lorsqu'on veut savoir un Edit correspond à quelle champs dans la base de donnée et quelle table, on peut ouvrir MetaTrace pour observer les messages qui sont passés. Voir les messages de type "SQL WCF".

***
#### III - Liens utiles - Là où on peut trouver tous les raccourcis des applications
***
Cette répertoire se trouve dans : S/tfe/Med/Exploit/Lien utiles 
En plus, dans la répertoire /DBAD Medissys tu peux trouver des .exe pour laquelle tu peux consulter les procédures dans les autres base de données.

# SQL
***
#### I - Vérifier si une table contient une colonne.
***
On peut faire une SELECT dans la table __INFORMATION_SCHEMA.COLUMNS__, en donnant le nom de la table et le nom de la colonne: 
````
SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'vet_barge' AND COLUMN_NAME LIKE '%PI%';
````

