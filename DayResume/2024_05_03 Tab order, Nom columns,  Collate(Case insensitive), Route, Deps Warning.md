# React
#### I - Route - Dynamic segment
Source : https://reactrouter.com/en/main/route/route
If a path segment starts with __":"__ then it becomes a __"dynamic segment"__.
When the route matches the URL, the dynamic segment will be parsed from the URL and provided as __params__ to other router APIs.

* Possible to have multiple dynamic segments in one route path: /:a/:b
* useParams() let you get values.  [const routeParams = useParams();]
* Dynamic segment cannot be "partial"
    * Faux: "/teams-:teamId"
    * Faux: "/:category--:productId"

#### II - React Dependans Warning
Add this rules in your eslint configuration file.
````
{
  "plugins": ["react-hooks"],
  // ...
  "rules": {
    "react-hooks/rules-of-hooks": 'error',
    "react-hooks/exhaustive-deps": 'warn' // <--- THIS IS THE NEW RULE
  }
}`
````

# SQL
#### I - Collate
Quand on veut une condition Where soit insensitive, on peut utiliser le mot-clé Collate pour rendre les codes plus lisible, au lieu d'utiliser or pour lister toutes les combinaisons.
Exemple1:
````
...
WHERE cls.reference COLLATE Latin1_General_CI_AS LIKE '[A-Z]V[0-9]%'
````
Exemple2:
````
...
INNER JOIN TableB b
ON a.Name COLLATE Latin1_General_CI_AS = b.Name COLLATE Latin1_General_CI_AS
````

# C# 
#### I - Tab order
L'ordre de tabulation est très important pour les gens qui s'habituent de travailler avec les tabulations.
Pour mettre un ordre de tabulation, voici les étapes:
1. View -> Tab Order
2. Clique dans l'ordre pour mettre en place l'ordre de tabulation que tu veux.
3. Si tu veux annuler la mise en place, clique "Echap"

#### II - Noms des columns 
On doit mettre la conversion qu'on veut dans cette table: **iom_preset_grid_columns_properties**.
Il faut que le propriété __IncludeGlobalSettings__ soit mit à true (__C'est aussi nécéssaire quand on veut mettre une critère dans un preset, qui ne prend en compte que les components avec IncludeGlobalSettings = true__)
Quelques valeurs importants:
|Nom column| Description|
|--|--|
|id_grid|Le nom de la grille, le nom que tu vois dans la liste des propriétés à gauche|
|fct_name|La fonction d'initialisation d'écran|
|Column_name|Le nom de la colonne original|
|Column_fullname|Le nom que tu veux afficher dans l'écran|
