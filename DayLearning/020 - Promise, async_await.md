La clé pour comprendre le fonctionnement de ``await`` réside dans __la façon dont les promesses sont gérées dans votre fonction__.

## Ce que fait réellement ``await``.
Quand on utilise ``await`` devant une fonction asynchrone(qui retourne une promesse), JavaScript:
1. Suspend l'exécution de la fonction courante jusqu'à ce que la promesse soit résolue ou rejetée.
2. Reprendre l'exécution une fois que la promesse est complétée.
3. Remarque: Une fonction async retourne nécessairement une promesse résolue.

__Mais attention__: Cela fonction uniquement si toutes les promesses internes dans cette fonction sont bien chaînée et retournées correctement!
* __``await`` attend uniquement la promesse retournée par une fonction!__

C'est à dire, si une fonction asynchrone ``uploadLocalDatas`` contient des appels API internes, mais que ces appels ne sont pas __attendus correctement avec__ ``await`` ou ne font pas partie de la promesse retournée, alors  ``await uploadLocalDatas`` se termine sans vérifier que les appels internes soient tous terminés.

__Comment résoudre ce problème?__
1. __Assurez-vous que chaque tâche asynchrone est attendue__: Dans ``uploadLocalDatas``, ajoutez ``await`` à chaque appel asynchrone ou regroupez-les dans un ``Promise.all``.
2. __Exemple avec ``Promise.all``__
    ````js
    async function uploadLocalDatas(): Promise<void> {
        return Promise.all([apiCall1(), apiCall2(), apiCall3()]).then(() => ...);
    }
    ````

## forEach ne gère pas les promesses.
La méthode ``forEach`` __ne sait pas gérer les promesses__. Elle exécute toutes les itérations de manière synchrone, sans attendre les promesses déclenchées par chaque itération.
Même si vous mettez un ``await`` dans le corps de la boucle.
````js
files.forEach(async (file) => {
    return uploadFile(file); // Cette promesse est déclenchée mais pas attendue par forEach
});
console.log("This will execute before all uploads are complete!");
````
1. ``forEach`` parcourt les éléments et exécute le callback
2. Le callback est asynchrone, donc il retourne immédiatement une promesse pour chaque itération.
3. Mais ``forEach`` n'a pas de retour, donc il ne collecte pas ces promesses ni les attend.

__Utiliser ``for...of`` ou ``Promise.all``__
* Option1: ``for...of`` avec ``await`` pour traitement séquentiel
    ````js
    for (const file of files) {
        await uploadFile(file); // Chaque upload est attendu avant de passer au suivant
    }
    console.log("All uploads are complete!");
    ````
* Option2: ``Promise.all`` pour traitement parallèle
    ````js
    const uploadPromises = files.map((file) => uploadFile(file));
    await Promise.all(uploadPromises); // Attend que toutes les promesses soient résolues
    console.log("All uploads are complete!");
    ````

## Promise.all
* __Exécution parallèle__.
* __Regroupement des promesses__: Regrouper toutes les tâches asynchrones dans un tableau, ce qui facilite leur gestion. Une fois toutes les promesses résolues, on peut traiter les résultats en une seul étape.

__Gestion des erreurs dans ``Promise.all``__
* ``Promise.all`` rejette si une seule de promesses échoue. Pour éviter un blocage complet, utilisez ``Promise.allSettled``:
    ````js
    const uploadPromises = files.map((file) => uploadFile(file));
    const results = await Promise.allSettled(uploadPromises);
    results.forEach((result, index) => {
        if (result.status === "fulfilled") {
            console.log(`File ${index + 1} uploaded successfully.`);
        } else {
            console.error(`Error uploading file ${index + 1}:`, result.reason);
        }
    });
    ````

__Le tableau de pormesses contienne des promesses valides__
* Pour une fonction asynchrone, on doit assurer qu'il retourne une promesse en utilisant ``Promise.resolve()`` ou tout simplement ``true`` si c'est dans une fonction async/await. 
    * __Pour éviter un rejet inattendu__
* Une fonction async/await encapsule automatiquement le résultat dans une promesse. Donc ``return await ...`` n'est pas nécessaire dans une fonction async. Cette syntaxe est utile lorsqu'on veut gérer les erreurs directement dans la fonction avec un ``try...catch``.

__On peut encapsuler des Promise.All dans des Promise.All__

## Appels API optionnels
On peut gérer ce cas en structurant correctement les promesses pour inclure des cas où les appels sont soit résolus, soit ignorés en fonction de vos besoins.
* __Approche1__: Utiliser des conditions pour inclure uniquement les appels nécessaires
    ````js
    const allPromises = data.map((item) => {
        const promises = [];
        // Ajouter une promesse conditionnellement
        if (item.shouldCallApi1) {
            promises.push(apiCall1(item));
        }
        ...
        return promises.length > 0 ? Promise.all(promises): Promise.resolve();
    ````
* __Approche2__: Utiliser l'opération ternaire.
    ````js
    const allPromises = data.map((item) => {
        return Promise.all([
            item.shouldCallApi1 ? apiCall1(item) : Promise.resolve("Skipped API 1"),
        ]);
    });
    ````

## Aller plus loin
* Si on retourne une promesse dans une fonction async/await, celui-la sera directement utilisé comme retour, on ne l'encapsule pas dans une autre promesse.
* Cas où on a explicitement encapsuler une promesse dans une promesse:
    ````js
    async function example() {
        return Promise.resolve(Promise.resolve(42));
    }
    
    example().then((result) => console.log(result)); // Affiche : Promise { 42 }
    ````
    Pour obtenir la valeur ``42``, il faudrait un ``await`` ou un ``then`` supplémentaire avec le result.
* Quand une fonction ``async`` retourne une valeur simple comme ``true``, cette valeur est automatiquement encapsulée dans une promesse __résolue__(fulfilled). Il suffit d'utilise ``then``  ou ``await`` pour accéder à la valeur encapsulée 

