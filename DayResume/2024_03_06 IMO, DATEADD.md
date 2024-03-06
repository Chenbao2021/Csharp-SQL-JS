# Général
***
#### I - Lettres IMO
***
Le numéro OMI d'identification d'un navire n'est jamais réatttribué à un autre navire. Il est composé du préfixe de trois lettres IMO suivi de 8 chiffres(par exemple IMO 8712345)


# SQL
***
#### I - DATEADD : "sur les 3 dernières années"
***
DATEADD() : Une fonction SQL utilisée pour ajouter ou soustraire une quantité spécifiée d'une unité de temps à une date donnée. Elle prend trois paramètres:
- L'unité de temps à ajouter ou soustraire(Comme YEAR, MONTH, DAY, HOUR, etc.)
- La quantité à ajouter ou soustraire.
- La date de départ à laquelle effectuer l'opération.

Par exemple, pour réaliser: "sur les 3 dernières années" : 
````
SELECT *
FROM table_name
WHERE date_column >= DATEADD(YEAR, -3, GETDATE())
````

