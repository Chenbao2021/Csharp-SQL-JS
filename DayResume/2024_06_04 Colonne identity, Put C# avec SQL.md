# Général
### I - Ne fais pas les choses en moitié
Quand on est développeur back-end (Service + SQL), il faut faire attention à ne pas mettre seulement les codes C# de service en ligne. Il faut aussi inclure les codes SQL en ligne, sinon en production les codes de service ne trouveront pas les codes SQL et cela créera une erreur fatale.

# SQL
### I - Pour ajouter une colonne Identity dans une table
* On vérifie si la colonne existe
* Sinon, on l'ajoute
    * Quand on ajoute une colonne identity, les valeurs identities pour les lignes existantes sont completés automatiquement.
````sql
IF NOT EXISTS (
    SELECT * 
    FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'VET_REF_INTRANET_MAJOR' 
      AND COLUMN_NAME = 'id'
)
	ALTER TABLE VET_REF_INTRANET_MAJOR ADD id INT IDENTITY;
GO
````
