# I - Introduction
_REST__ = Representational State Transfer.
REST n'est pas un protocole ou un standard, mais plutôt __un ensemble de principes architecturaux__ qui guident la manière de concevoir des systèmes distribués.

1. Utilisation des méthodes HTTP de manière standardisée: 
REST utilise les méthodes HTTP pour les actions CRUD (Create, Read, Update, Delete) :
    * GET
    * POST
    * PUT
    * DELETE
2. Sans état (Stateless):
* Chaque requête HTTP à une API REST doit contenir __toutes les informations nécessaires__ pour __comprendre la requête__.
* Le serveur __ne doit pas stocker__ l'état de l'application entre les requêtes. Cela simplifie la conception du serveur et améliore la scalabilité.

3. Cacheable: possède des caches

4. Interface uniforme
L'interface entre le client et le serveur doit être uniforme, facilitant ainsi la communication entre les différentes parties du système sans que le client ait besoin de connaître les détails internes du serveur. 

# II - Avantages de REST
* __Simplicité et facilité d'utilisation__ : Utiliser les verbes HTTP standard et les URI rend REST intuitif et facile à comprendre.
* __Flexibilité__ :  Les données peuvent être retournées dans le format le plus approprié selon les besoins du client (JSON, XML, HTML, etc.).
* __Indépendance du langage__ : Les API REST peuvent être utilisées avec presque n'importe quel langage de programmation ou technologie capable de faire des requêtes HTTP.
* __Scalabilité __ : Le fait d'être sans état facilite la scalabilité du serveur puisque les ressources pour maintenir l'état du client ne sont pas nécessaires.
* __Performance __ : Le support du cache peut réduire le nombre de requêtes nécessaires et améliorer la vitesse de réponse des applications.

