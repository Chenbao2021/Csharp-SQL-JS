# SQL
***
#### I - char(10), char(13)
***
les deux sont des types de données de caractère fixe qui représentent respectivement:
- char(10): Saut de ligne(LF - Line Feed) 
- char(13): Chariot(CR - Carriage Return)

Ils sont souvent utilisés ensemble pour représenter une nouvelle ligne dans de nombreux systèmes.

Exemple:
````
INSERT INTO #tmp_propulsion_type_value
	 SELECT STRING_AGG(PROPELLER_TYPE, CHAR(10)+CHAR(13))
	FROM VET_VESSEL_PROPELLER
	WHERE ID_VESSEL = @id_vessel AND isnull(dbo.VET_VESSEL_PROPELLER.PROP_SEQ,  -1) >= 0
	GROUP BY id_vessel
````
***
#### II - STRING_AGG
***

La fonction __STRING_AGG__ est une fonction d'agrégation qui permet de concaténer les valeurs de chaînes de caractères en une seule chaîne.
=> Souvent utilisé avec GROUP BY.

Exemple:
Voir l'exemple de I.


