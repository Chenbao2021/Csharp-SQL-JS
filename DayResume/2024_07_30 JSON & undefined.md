# React
### I - undefined et JSON.stringify en React
En Javascript, ``undefined`` peut causer des problèmes lors de la conversion d'un objet en JSON parce que 'JSON.stringify' omet(supprimer) les propriétés avec des valeurs ``undefined``.
Cela peut entraîner une réponse JSON qui ne correspond pas aux attentes.

##### Pourquoi 'undefined' pose problème
1. Omission des propriétés:
    Lors de la conversion d'un objet en JSON, les propriétés avec des valeurs ``undefined`` sont omises(Ignorer/ Supprimer).
2. Incohérence des Données: 
    Cela peut entraîner des incohérences dans les données retournées par une API. Par exemple, si votre API retourne des objets avec des propriétés omises, cela peut causer des erreurs lors de la consommation de ces donénes côté client. 

En javascript, quand on traite un JSON qui contient undefined, on obtient une exception.
Pour résoudre ce problème, on doit assurer que les valeurs 'undefined' sont correctement gérées Avant d'envoyer la réponse JSON du serveur ou de traiter le JSOn du côté client.

### Solution - Cas dans une requête dynamique.
Retourner null au lieu de undefined,
et attention, quand on utilise une requête dynamique, on doit mettre ``'null'`` entre guimet au lieu de ``null``
