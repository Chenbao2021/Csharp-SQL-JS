***
### I - Vue matérialisée
***
- Une vue matérialisée est une copie persistante(Permanente, physiquement sur disque) des données résultant d'une requête sur une ou plusieurs tables.
- Mises à jour périodiquement à l'aide de processus de rafraîchissement automatique ou manuel pour refléter les modifications dans les tables sources.
- Souvent utilisées pour améliorer les performances des requêtes en évitant le recalcul fréquent de données complexes ou en permettant l'indexation des résultats précalculés.

Exemple : Supposons que nous ayons deux tables dans notre base de données: 'client' et 'commandes'.
Nous voulons créer une vue matérialisée pour stocker le nombre total de commandes par client.
````
-- Création de la vue matérialisée
CREATE MATERIALIZED VIEW vue_commandes_par_client AS
SELECT c.client_id, c.nom, COUNT(*) AS nombre_commandes
FROM clients c
JOIN commandes o ON c.client_id = o.client_id
GROUP BY c.client_id, c.nom;

-- Rafraîchissement de la vue matérialisée (optionnel)
REFRESH MATERIALIZED VIEW vue_commandes_par_client;
````
- La vue matérialisée 'vue_commandes_par_client' sotcke le nombre total de commandes par client.
- Les données sont calculées une fois lors de la création de la vue, puis stockées physiquement dans la base de données.
- Les données peuvent être rafraîchies périodiquement pour refléter les modifications dans les tables source.



***
### II - Table temporaire
***
- Une table temporaire est une table qui existe temporairement en mémoire ou sur le disque pendant la durée d'une session de base de données ou d'une transaction.
- Aussi pour stocker des résultats intermédiaires ou des données temporaires lors du traitement de requêtes complexes.

Exemple: Supposons que nous voulions stocker temporairement les commandes passées au cours d'une session utilisateur pour des opérations de traitement:
````
-- Création de la table temporaire
CREATE TABLE #temp_commandes AS
SELECT *
FROM commandes
WHERE date_commande >= '2024-01-01'; -- Exemple de filtre de date pour les commandes récentes

-- Sélection des données de la table temporaire
SELECT *
FROM #temp_commandes;
````
- Les données sont stockées en mémoire ou sur le disque pendant la durée de la session utilisateur ou de la transaction.
- La table temporaire est automatiquement supprimée à la fin de la session ou de la transaction qui l'a créée.

***
### III - Session 
***
Dans SQL Server, une session fait référence à la période pendant laquelle un utilisateur se connecte à une instance de base de données SQL Server et exécute des opérations sur celle-ci .
Une session débute lorsque l'utilisateur se connecte à la base de données et se termine lorsque la connexion est fermée.

Quelques points importants à retenir sur les sessions dans SQL Server :
1. Connexion et Authentification:
Une session commence par l'établissement d'une connexion à l'instance de SQL Server.
L'utilisateur fournit des informations d'authentification telles qu'un nom d'utilisateur et un mot de passe pour accéder à la base de données.

2. Exécution de Requêtes
3. Transactions:
Une session peut inclure une ou plusieurs transactions. Les transactions permettent de regrouper plusieurs opérations SQL en une unité logique et cohérente.
4. Gestion de la Session : Pendant la session, SQL Server attribue un identifiant de session unique à chaque connexion.Cet identifiant peut être utilisé pour identifier et gérer la session côté serveur.
5. Terminaison de la Session:
    - Une session se termine lorsque l'utilisateur se déconnecte explicitement de la base de données ou lorque la connexion est interrompue de manière inattendue(Par exemple, en cas de panne du serveur, de déconnexion réseau, etc.)
    - À la fin de la session, les ressources allouées à cette session sont libérées et les verrous détenus sont relâchés.

***
### IV - Utiliser Vue matérialisé au lieu de créer une nouvelle table
***
Une vue matérialisé contient toujours  les données les plus récèntes de la table de source,
Or si on crée une nouvelle table et on stocke les données dédans, la consistance des données n'est pas garantie.
