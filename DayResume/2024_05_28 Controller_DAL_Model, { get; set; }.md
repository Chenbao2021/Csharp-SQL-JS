# Général
#### I - Controller
Le rôle du controller est de gérer les interactions avec les utilisateurs, de __traiter les requêtes entrantes__ et de __retourner les réponses appropriées__.

__Responsabilités principales__ :
* Recevoir les requêtes HTTP (GET, POST, PUT, DELETE, etc.).
* Appeler les méthodes appropriées des services ou __du modèle__ pour traiter les données.
* Retourner les vues (dans une application MVC classique) ou __les réponses JSON/XML__ (dans une API RESTful).

#### II - DAL (Data Access Layer)
Le Data Access Layer (DAL) est la couche de l'application qui est responsable de l'accès aux données. Il encapsule toutes les interactions avec la base de données, __fournissant une abstraction__ pour les opérations CRUD (Create, Read, Update, Delete).

__Responsabilité principales__ : 
* Gérer les connexions à la base de données.
* Exécuter les requêtes SQL ou appeler les procédures stockées.
* Mapper les résultats des requêtes aux objets métier (model).

#### III - Model
Le model représente __les données de l'application__ et __la logique métier__.
Dans le cadre d'une application MVC, le modèle est utilisé pour gérer les données de l'application, y compris leur état et leur comportement.

__Responsabilités principales :__
* Représenter les entités de la base de données sous forme d'objets C# (par exemple, __des classes__). En C#, un modèle est souvent représenté par une classe, avec ses propres attributs et méthodes.
* Contenir la logique métier de base (par exemple, les validations).
* Parfois, inclure des méthodes pour les opérations spécifiques liées aux données.

#### IV - Analogie (Gardient de la porte, serveur, cuisinier)
Controller comme le Gardien de la Porte :
* __Gardien de la Porte__ : Accueille les clients (utilisateurs), prend leurs commandes (requêtes HTTP), et les dirige vers le serveur approprié pour s'occuper de leur demande.
* __Model comme le Serveur__ : Prend les commandes du client, vérifie les détails et les exigences (logique métier), et s'assure que la commande est correcte avant de la transmettre au cuisinier.
* __DAL comme le Cuisinier__ : Prépare la nourriture (données) en suivant les recettes (requêtes SQL) et s'assure que tout est correctement cuisiné (stocké/retrouvé dans la base de données) avant de l'envoyer au serveur.

# C#
#### I - ``{ get; set; }``
``{ get; set; }`` est une syntaxe pour les propriétés auto-implémentées en C#.
Cela signifie que le compilateur C# générera automatiquement un champ privé pour stocker la valeur de la propriété et fournira les méthodes d'accès pour lire (``get``) et modifier (``set``) cette valeur.
* ``get``:  permet de lire la valeur de la propriété. Lorsque vous accédez à ``person.full_name``, vous utilisez le get pour obtenir la liste des noms.
* ``set``: permet d'assigner une nouvelle valeur à la propriété. Lorsque vous faites ``person.full_name = new List<string>()``, vous utilisez le set pour définir la valeur de la propriété à une nouvelle instance de List<string>.

Ces deux méthodes sont souvent utilisés implicitement(Lorsqu'on accéder ou modifier une propriété), comme lorsqu'on appel à la fonction 'Add' de la liste.

