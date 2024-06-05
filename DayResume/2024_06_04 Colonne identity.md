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