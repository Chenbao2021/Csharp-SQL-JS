# Générale
***
#### I - Tester en version release pendant le dévéloppement de SQL
***
Quand on a finit de dévélopper les codes C# pour l'écran, on le lance en release et pas en production.

Avant de lancer Medissys en Release, il faut regénérer en tâche de fond, car la version utilisée par Medissys en release sera la dernière version qui est généré en tâche de fond.


# C#
***
#### I - Trouver une procédure grâce à sa iRéférence
***
1. Lance directement le nom de iRéférence dans SQL Server.
2. Si l'étape 1 ne marche pas, enleve MED, IOS, etc , et fait une recherche dans les dossiers dans "n:\medissys\serveur".
    Exemple, pour 

***
#### II - AutosizeColumnsMode et column width
***
Il est utile quand on fait une petite écran : 
```
m_grCostRefFamily.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
```
- Il peut rendre l'application très lente, quand on a un gros écran avec beaucoup des données
- Il fixe la grille, on ne peut plus redimensionner.

Si on veut mettre une minWidth pour une colonne, sans utiliser la méthode précédante:
````
foreach(DataGridViewColumn dc in  m_grCostRefFamily.Columns)
{
    dc.MinimumWidth = 155;
}  
m_grCostRefFamily.Columns[0].MinimumWidth = 50;
m_grCostRefFamily.Columns[0].Width = 50;
````
***
#### III - Quelques fonctions logiques de SQL Server
***
- NULLIF(input, value): Si l'input est égale à value, alors on la remplace par null.
- IIF(expression, value_1, value2): Si expression est vrai, alors retourn value_1, sinon retourne value_2
- CHOOSE(index, val_1, val_2 [, value_n]): Retourne l'élément à l'index spécifié à partir d'une liste de valeur.
    Exemples:
    - Selon le Month(date), distribuer le nom du mois
***
#### IV - DataColumn.Expression
***
Exemple de code: 
````
m_dtCostRefFamily.Columns[TechnicalColumn.col_status_row_color_hidden].Expression = $"IIF(status = 'C',{Color.Red.ToArgb()}, '{Color.Black.ToArgb()}')";
````


