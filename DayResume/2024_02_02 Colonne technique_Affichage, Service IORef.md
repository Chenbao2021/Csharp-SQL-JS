# SQL
***
#### I - Colonne technique et colonne d'affichage
***
Quand on fait un select pour retourner des données depuis SQL, on peut retourner deux fois une colonne,
où une colonne sert pour l'affichage, et l'autre pour effectuer des opérations.

# C#
***
#### I - Pour obtenir une DataTable depuis la service IORef
***
- Pour appeler une service, on doit passer par "__IORef.RefDirector.RefManager__"
- On peut soit appeler la méthode simple, soit appeler la méthode avec un filtre:
    - __.GetRefTable(IORefType RefCode)__
    - __.GetFilteredRefTable(IORefType RefCode, string sFilter)__

- Construire un sFilter avec __string.Format__
-   Exemple:
    ````
    string.Format("ref_name = 'cost_amount_type' and value2_char = 'Y'")
    ````
