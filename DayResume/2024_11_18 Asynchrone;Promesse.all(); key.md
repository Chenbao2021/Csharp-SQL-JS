# React
***
## I - Asynchrone
Imaginons on a ce code:
````js
upload.start();
console.log('Je suis en arrière')
````
Bien que console.log est écrit après, il s'exécute en avant.
Parceque upload.start() est une opérations asynchrone(Comme des promesses, des callbacks ou des actions liées à des API).

* Event Loop
    Toutes actions asynchrones sont __mise en file d'attente dans l'Event Loop__, et le reste du code continue de s'exécuter immédiatement.

Pour résoudre ce problème, on peut
* Utiliser une fonction async/await si tu veux que l'ordre soit respecté. Dans ce cas, on utilise un ``await`` pour attendre qu'elle soit terminé avant d'exécuter le ``console.log``.
* Déplacer ``console.log`` dans un callback ou une ``then`` si on ne peut pas utiliser ``async/await``.


## II - Promise.all(...)
La méthode ``Promise.all`` permet d'attendre que toutes les promesses soient résolues(ou qu'une seule échoue). Avec ```map``, on peut transformer chaque fichier en une promesse correspondant à son téléversement.

Exemple:
````JS
let objListCompleted = []
const uploadPromises = addedDocs.map((addedDoc) => {
    return new Promise<void>((resolve, reject) => {...Ajout des éléments dans objListCompleted...}
}

await Promise.all(uploadPromises);
return objListCompleted;
````
## III - Key
