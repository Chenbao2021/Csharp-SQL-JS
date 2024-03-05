# SQL
***
#### 1- TRIM, LTRIM, RTRIM 
***
- TRIM() : Cette fonction supprime les espaces en début et en fin (gauche et droite) d'une chaîne de caractères. 
    ````
    SELECT TRIM('   Hello   ') AS TrimmedString;
    ````

- RTRIM() : Cette fonction supprime les espaces en fin (droite) d'une chaîne de caractères. "RTRIM" signifie "Right Trim" (suppression de la droite). 
- LTRIM() : Cette fonction supprime les espaces en début(gauche) d'une chaîne de caractères. "LTRIM" signifie "Left Trim" (suppression de la gauche).

***
#### 2 - Update les valeurs d'une colonne dans une table
***
Pour mettre à jour les valeurs d'une colonne dans une table SQL Server, on peut utiliser la clause UPDATE.
Par exemple
````
UPDATE ma_table
SET colonne_a_mettre_a_jour = nouvelle_valeur
WHERE condition;
````
Si on veut mettre à jour les valeurs d'une colonne dans une table en utilisant des valeurs provenant d'une autre table, on peut utiliser une requête UPDATE avec une clause JOIN.
````
UPDATE table_cible
SET table_cible.colonne_a_mettre_a_jour = table_source.nouvelle_valeur
FROM table_cible
INNER JOIN table_source ON table_cible.clé_primaire = table_source.clé_primaire;
````

Exemple 1 :  Supposons qu'on a deux tables "Employes" et "NouvellesSalaires". On souhait mettre à jour la colonne "salaire" dans la table "employes" en utilisant les nouveaux salaires de la table "nouvellesSalaires", en utilisant l'identifiant unique de l'employé comme clé primaire pour faire correspondre les lignes entre les deux tables.
````
UPDATE employes
SET employes.salaire = nouvelles_salaires
FROM employes
INNER JOIN nouvelles_salaires ON employes.id_employe = nouvelles_salaires.id_employe
WHERE employes.departement = 'Ventes';
````

Exemple 2 : US68389
````
UPDATE #data_table
SET #data_table.type = vetm_barge_type.barge_type 
--SELECT vetm_barge_type.barge_type 
FROM #data_table
LEFT JOIN vet_barge ON #data_table.id_barge = vet_barge.id_barge
LEFT JOIN vetm_barge_type ON vet_barge.id_type = vetm_barge_type.id_barge_type
  
````

***
#### 3- INSERT INTO ... SELECT
***
Cette instruction permet d'insérer des données dans une table en sélectionnant des données à partir d'une autre table ou à partir du résultat d'une requête SELECT. 
````
INSERT INTO nom_de_table (list_de_colonnes)
SELECT liste_de_valeurs
FROM nom_de_la_table_source
WHERE conditions;
````
Exemple:
````
INSERT INTO employes_archive (nom, age, salaire)
SELECT nom, age, salaire
FROM employes
WHERE age > 40;
````

***
#### 4 - [] = Casting en chaîne de caractère en SQL.
***
En SQL, lorsqu'on a des noms des colonnes qui contiennent des caractères spéciaux, on peut l'englober par un [] pour la considérer comme des chaînes de caractères.

