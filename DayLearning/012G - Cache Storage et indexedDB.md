# Commentaire personnel
* Pour suvegarder dans indexedDB, il faut qu'on la fasse manuellement, avec un fetch personnalisé.
* On ne modifie pas les données dans storage cache, mais dans indexedDB si. On peut personnaliser notre fetch pour sauvegarder/ récupérer nos données depuis indexedDB au lieu/en plus de les sauvegarder dans cache storage.  
***

Lorsqu'on utilise une PWA, on peut trouver des données dans le cache storage, mais ausi dans indexDB.
En effet ces deux mécanismes sont utilisés pour le stockage, mais leur usages diffèrent en fonction de la nature des données et des besoins de l'application.
***
# 1. Cache Storage
__Cache Storage__ est une API Web utilisée principalement pour stocker des __ressources statiques__ et des __réponses HTTP__.:
1. __Précaching des fichiers statiques__ (HTML, CSS, JS, images, etc.)
2. __Caching dynamiques__(runtime caching).
3. __Avantages de Cache Storage__:
    * Optimisé pour les ressources HTTP.
    * Permet un accès rapide aux réponses réseau et fichiers statiques.
    * Intégré avec le Service Worker, ce qui facilite le remplacement des ressources obsolètes.

# 2. IndexedDB
__IndexDB__  est une base de données NoSQL côté client:
1. __Données structurées__:
    * IndexedDB est utilisée pour stocker des données complexes(Comme des objets JSON ou des relations) qui ne sont pas directement liées à des requêtes réseau ou des ressources statiques.
    * Ex: Données modifiées localement en mode offline, historique, données utilisateurs.
2. _Cas d'utilisation typique__
    * __Synchronisation offline/online__:
        Lorsqu'une application permet des actions en offline, comme modifier des données ou enregistrer des brouillons, ces modifications sont souvent stockées dans indexedDB.
    * __Stockage de données volumineuses__:
        IndexedDB peut gérer de grands volumes de données(Comme catalogues de produit ou des données de cache applicatif).

