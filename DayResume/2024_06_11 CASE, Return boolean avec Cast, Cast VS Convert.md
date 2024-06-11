# SQL
### I - Utilisation de CASE
On peut utiliser CASE dans deux cas différentes :
* Cas 1:
    ``CASE CONTRACT WHEN 'Y' THEN 1 ELSE 0 END [contract],``
* Cas 2: 
    ``CASE WHEN CONTRACT_AGREED = 'Y' AND CONTRACT = 'Y' THEN 1 ELSE 0 END [major_agreed_by_vet_contract],``

### II - Retourner Booléan avec Cast
En SQL Server, il n'existe pas de type de données booléen natif .
Mais on peut simuler des valeurs booléennes en utilisant des types de données ``BIT``, ``INT`` ou ``CHAR``.

Le type de données BIT est souvent utilisé pour représenter des valeurs booléennes, où 1 est considéré comme vrai (true) et 0 comme faux (false).

Exemple:
````
CASE CONTRACT WHEN 'Y' THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END [contract],
````

### III - Différence CAST et CONVERT
* ``CAST(expression AS data_type)``
    Utilisation simple et directe, principalement pour des conversions de types de données standard.
* ``CONVERT(data_type, expression [,style])``
    spécique à SQL Server et offre plus de flexibilité, notamment pour le formatage des dates et des heures.

__Principales différences__
1. Confirmité aux standards
    * ``CAST`` est conforme au standard SQL et est portable entre différents systèmes de gestion de base de données.
    * ``CONVERT`` est spécifique à SQL Server
2. Flexibilité
    * ``CONVERT`` offre plus de flexibilité avec l'argument ``style`` pour le formatage des dates et des chaînes.
3. Utilisation typique
    * Utilise ``CAST`` pour des conversions simples et lorsque la portabilité du code est importante.

### IV- Exécution d'une requête dynamic
````SQL
EXEC sp_executesql @get_vrn, N'@vet_req_mep_F15216 DATE, @emission_date DATE', @vet_req_mep_F15216, @emission_date;
````