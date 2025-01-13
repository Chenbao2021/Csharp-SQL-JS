# Rappel
JS est __monothread__ et utilise un modèle basé sur l'__event loop__ pour gérer les tâches.
Cela signifie: 
* Les instructions synchrones sont excutées immédiatement dans l'ordre.
* Les instructions asynchrones sont mises en file d'attente pour une exécution ultérieure après les tâches synchrones.

***
# Raison d'utiliser Asynchrone

On voudrait une méthode puisse:
* Démarrer une opération longue en appelant une fonction.
* Avoir une fonction pour démarrer l'opération et rendre la main immédiatement, afin que le programme puisse cntinuer de réagir aux autres évènements.
* Recevoir une notification du résultat de l'opération, lorsqu'elle termine.

***
# Promise
Une promesse est un objet renvoyé par une fonction asynchrone , c'est comme une boîte  contenant une tâche qui va être terminée plus tard. Et il peut avoir deux résultats:
* Succès: La tâche est terminée avec succès (résolue).
* Échec: La tâche échoue(rejetée).

On peut attacher des gestionnaires(``.then()``, ``.catch()``) à cette promesse, qui sont des méthodes de la classe.
Les __gestionnaires__ sont des fonctions qu'on __attache__ à la promesse pour dire:
* Que faire en cas de réussite.
* Que faire en cas d'échec.

##### I - Principe simplifié d'une Promesse
Une promesse a trois états:
* pending
* fulfilled
* rejected

Promesse est un objet/une classe Javascript, prend une paramètre ``executor``(fonction) pour s'initialiser.

On doit fournit une fonction ``executor`` comme callback, qui doit recevoir ``resolve`` et ``reject``. Ces deux fonctions contrôlent le changement d'état de la promesse:
* ``resolve(value)`` -> Passe l'état à ``fulfilled`` et transmet ``value`` à ``.then(value)``.
* ``reject(reason)`` -> Passe l'état à ``reject`` et transmet ``reason`` à ``.catch(value)``.

Donc, pour déclencher la méthode ``.then()``, il est obligatoire d'appeler la fonction ``resolve`` à l'intérieur de l'exécuteur de la promesse. 
* Par contre, ``.then`` s'utilise aussi avec fonction async/await, dans ce cas, tous ceux qui sont retournés par la fonction async sont considérés comme la valeur résolue de la promesse.

##### II - Le chainage de promesses.
Avec promesse, on peut appeler une deuxième ou plus des ``.then()``. Ceux qui évite d'avoir d'imbrication des appels de fonction asynchrones comme dans les fonctions callbacks.
````js
fetchPromise
  .then((reponse) => {
    if (!reponse.ok) {
      throw new Error(`Erreur HTTP : ${reponse.status}`);
    }
    return reponse.json();
  })
  .then((json) => {
    console.log(json[0].name);
  })
  .catch((error) => {
    console.error(`Impossible de récupérer les produits : ${error}`);
  });
````

##### III - ``.all()`` et ``.any()``
* ``Promise.all([...])``: On a besoin que toutes les promesses soient tenues, mais leur exécutions sont indépendants.
    * Then reçoit un tableau contenant toutes les réponses, dans le même ordre que le tableau des promesses passé à ``.all()``.
    * Rompue si au moins une des promesses du tableau a été rompue.
    * 
    ````js
    Promise.all([fetchPromise1, fetchPromise2])
      .then((reponses) => {
        for (const reponse of reponses) {
          console.log(`${reponse.url} : ${reponse.status}`);
        }
      })
      .catch((error) => {
        console.error(`Erreur de récupération : ${error}`);
      });
    ````
* ``Promise.any([...])
***

# Async/Await

Le mot clé ``async`` fournit une façon plus simple de travailler avec du code asynchrone utilisant les promesses.

Dans une fonction async, on peut ajouter ``await`` avant un appel à une fonction renvoyant une promesse. Et dans ce cas la, le code patiente jusqu'à ce que la promesse soit réglée et recevoir le résultat.

Si on n'est pas dans une fonction async, JS ne peut pas suspendre l'exécution de manière synchrone, et donc on doit utiliser ``.then(value)`` si on veut enchaîner quelques choses après la promesse.

__Utilisation de Promise.all avec async/await__
````js
async function getUser(id) {
    return new Promise((resolve) => {
       setTimeout(() => resolve({id, bale: 'user'}), 1000); 
    });
}
async function getOrders(userId);
async function main() {
    const users = await Promise.all([getUser(1), getUser(2), getUser(3)]);
    
    for(const user of users) {
        const orders = await getOrders(user.id);
    }
}
````

Exemple d'une fonction async/await:
````js
useEffect(() => {
  const fetchData = async () => {
    console.log("1. Fetching data...");
    const response = await fetch("/api/data");
    const result = await response.json();
    console.log("2. Data fetched:", result);
    setData(result);
  };

  console.log("3. Before calling fetchData");
  fetchData();
  console.log("4. After calling fetchData");
}, []);
````
* Le console va afficher 3. et 4. avant 1 et 2(async a une priorité moins élévé).
* Le thread principal ne sera pas bloqué comme fetchData est une fonction async et exécuté sans ``await``. 
***
# Introducing Workers
__Workers__ : Enable you to run some task in a separate ``thread`` of execution.

A Javascript program is _single-threaded_. And a _thread_ is a sequence of instruction that a program follows, then it can only do one thing at a time.

Your main code and your worker code __never__ get direct access to each other's variables. Workers and the main code run in completely separate worlds, and only interact by sending each other messages.(Event listener)
* Workers can't access the DOM(the window, document, page elements, and so on).

There are three different sorts of workers:
* Dedicated workers: Used by a single script instance.
* Shared workers: Shared by several different scripts running in different windows
* Service workers: Like proxy servers, caching resources so that web app can work when the user is offline.

#### Keep the worker code in a separate script from the main code
main.js (Main thread)
* Create a new worker, giving it the code in "generate.js"     `
    ```js
    const worker = new Worker("./generate.js");
    ````

* When the user clicks "Generate primes", send a message to the worker.  (#generate is of a html button)
    ````js
    document.querySelector("#generate").addEventListener("click", () => {
      const quota = document.querySelector("#quota").value;
      worker.postMessage({
        command: "generate",
        quota,
      });
    });
    ````

* When the worker sends a message back to the main thread.
    ````js
    worker.addEventListener("message", (message) => {
      document.querySelector("#output").textContent =
        `Finished generating ${message.data} primes!`;
    });
    ````

generate.js (Extra thread)
* Listen for messages from the main thread. 
    ````js
    addEventListener("message", (message) => {
      if (message.data.command === "generate") {
        generatePrimes(message.data.quota);
      }
    });
    ````
* Then send back a message to the main thread once the execution terminate:
    ````js
    function generatePrimes(quota) {
        ...
        postMessage(primes.length)
    }
    ````
    * postMessage is a native function of ``worker``.













