Dans __IndexedDB__, une __transaction__ est un mécanisme essentiel pour interagir avec la base de données de manière sûre et efficace.
****
# Concept de transaction
* Une transaction est un __contexte opérationnel__ qui regroupe un ou plusieurs opérations de lecture et/ou d'écriture sur une ou plusieurs tables(object stores).
* Les transactions garantissent l'atomicité: Soit toutes les opérations réussissent, soit aucune d'elles n'est appliqué.
* Une transaction est toujours liée à un __scope__ (Défini par les tables sur lesquelles elle agit) et à un __mode__ (``readonly`` et ``readwrite``)
***
# Étapes d'une transaction
1. __Créer la transaction__
    * Une transaction est créée via ``transaction()`` sur un objet ``IDBDatabase``
    ``const transaction = db.transaction(['storeName1', 'storeName2'], 'readwrite');``
2. __Accéder au store via la transaction__
    * Utilisez ``transaction.objectStore('storeName1')`` pour récupérer l'objet store.
3. __Effectuer des opérations__
    * Vous pouvez appeler des méthodes comme ``add``, ``get``, ``put``, ``delete`` sur l'object store.
4. __Écouter les événements__
    * Une transaction possède des événements (``oncomplete``, ``onerror``, ``onabort``) pour suivre son état.
5. Exemple
    ````JS
    const transaction = db.transaction(['storeName', ...], 'readwrite')
    const store = transaction.objectStore('storeName');
    
    const request = store.add({ id: 1, name: 'Alice' })
    
    transaction.oncomplete = () => {
        console.log('Transaction réussie!');
    }
    transaction.onerror = (event) => {
        ...
    }
    ````
***
# Durée de vie d'une transaction
* Une transaction reste __active__ tant que des requêtes sont en cours(Dans le fil d'attente).
* On peut utiliser ``await transaction.done`` pour atteindre la fin d'une transaction.
* Elle devient automatiquement __inactive__ lorsqu'il n'ya plus de requêtes en attente et ne peut plus être utilisée.
* Si une erreur survient, elle est automatiquement annulée(rollback).
***
# Scope d'une transaction
Une transaction peut inclure plusieurs object stores:
````js
const transaction = db.transaction(['store1', 'store2'], 'readwrite');
const store1 = transaction.objectStore('store1');
````

La définition du scope a plusieurs avantages:
* __Isolation des données__: Verrouiller que les données nécessaires, car pendant une transaction, les donénes nécessaires seront tous verouillés.
* __Contrôle de la concurrence__: Pour un store 'A', pendant qu'une transaction1 exécute, les autres transactions seront bloqués et attendent que la transaction1 termine.
* __Optimisation des performances__:Bien définir un scope pour ne pas empêcher les autres transactions.
* __Lisibilité et maintenabilité du code__: Sur quelles données il s'agit.
* __Gestion explicite des dépendances__
* __Prévention des erreurs potentielles__
***
# Les événements: .onsuccess, .oncomplete, .onerror
````js
const request = store.get(1);
````
* ``request.onsuccess``: Réagir à une __opération réussie__.
* ``request.oncomplete``: Gérer la fin d'une transaction.
* ``onerror``: Gérer les erreurs.

Ce mécanisme est appelé __gestion des événements__. Il repose sur le modèle d'événements de JavaScript et le fonctionnement des objets qui implémentent l'interface __EventTarget__.

#### Concepts clés du modèle d'événements:
1. __Événements:__
    * Les événements signalent qu'une action ou un changement a eu lieu (Par exemple, une requête réussie, une erreur)
    * IndexedDB émét des événements spécifiques comme ``success``, ``complete``, et ``error``.
2. __Gestionnaires d'événements :__ 
    * Les propriétés comme ``.onsuccess`` ou ``.onerror`` sont des __gestionnaires d'événements__(ou __event handlers__).
    * Ils permettent de définir une fonction qui sera appelée en réponse à un événement.
3. __Propagation et ciblage:__
    * L'objet qui émet l'événement passe cet événement au gestionnaires défini.
4. __Alternative: Utilisation de ``addEventListener``__
    * ``.onsuccess`` et les autres gestionnnaires ne sont pas la seule manière d'écouter les événements, on peut également utiliser ``addEventListener``, qui fait partie du modèle d'événement DOM plus générale.
    ````js
    const request = store.get(1);
    request.addEventListener('error', (event) => {
        ... 
    });
    ````



