Comprendre comment manipuler __IndexedDB__ est essentiel pour bien utiliser cette base de données côté client.
Examinons les opérations courantes comme __ajouter__, __lire__, mettre à jour__, et __supprimer__ des données.

# I. Ouverture d'une base de données
````js
const openDatabase = (dbName, version) => {
    return new Promise((resolve, reject) => {
        const request = indexDB.open(dbName, version);
        
        // Création ou mise à jour de la structure de la base
        request.onupgradeneeded = (event) => {
            const db = event.target.result;
            if(!db.objectStoreNames.contains('my-store')) {
                // Un objet store est comme une table.
                // C'est ici qu'on précise quel champ va être utilisé comme clé avec l'objet "keyPath"
                db.createObjectStore('my-store', {keyPath: 'id'});
            }
        }
        
        request.onsuccess = (event) => {
            resolve(event.target.result); // La base est ouvert
        }
        
        request.onerror = (event) => {
            reject(event.target.error);
        }
    } 
}
````

# II - Ajouter ou modifier des données à IndexedDB
````js
const saveToIndexedDB = async(dbName, storeName, data) => {
    const db = await openDatabase(dbName, 1);
    // Débuter une transaction en mode lecture + écriture.
    const transaction = db.transaction(storeName, 'readwrite');
    // Accéder au store <storeName> pour manipuler ce store.
    const store = transaction.objectStore(storeName);
    
    // * Si une entrée avec la clé spécifique existe déjà, ``put`` remplace son contenu.
    // * Si elle n'existe pas, une nouvelle entrée est crée.
    store.put(data); 
    transaction.oncomplete = () => {
        console.log('Donnée sauvegardée avec succès');
    }
    transaction.onerror = (event) => {
        console.error('Erreur lors de la sauvegarde', event.target.error);
    }
}
````
* ``db.transaction``: Une transaction est une opération qui regroupe plusieurs interactions avec un base de données et garantit qu'elles sont exécutées de manière cohérente.
    * Atomicité
    * Drée limitée
    * Modes: ``readonly`` / ``readwrite``

# III - Récupérer des données
````js
const getFromIndexedDB = async (dbName, storeName, key) => {
    const db = await openDataBase(dbName, 1);
    const transaction = db.transaction(storeName, 'readonly');
    const store = transaction.objectStore(storeName);
    
    return new Promise((resolve, reject) => {
        const request = store.get(key);
        request.onsuccess = () => {
            resolve(request.result);
        }
        request.onerror = (event) => {
            reject("Error " + event.target.error);
        }
    })
}

// Exemple d'utilisation
getFromIndexedDB('my-db', 'my-store', 1).then((data) => {
    ...
})
````

# IV - Supprimer des données
````js
const deleteFromIndexedDB = async (dbName, storeName, key) => {
    ...
    store.delete(key);
    ...
}
````

# V - Parcourir toutes les données
````js
const getAllFromIndexedDB = async (dbName, storeName) => {
    ...
    return new Promise((resolve, reject) => {
        const request = store.getAll();
        ...
    })
    ...
}
````
