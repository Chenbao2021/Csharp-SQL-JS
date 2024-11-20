# 0 - Concepts à retenir
* Call Stack : Exécute les instructions synchrones
* Microtask Queue : Les promesses et microtâches ont la priorité sur les tâches normales
* Task Queue : Les tâches asynchrones sont traitées après les microtâches.
* Event Loop : Coordonne le cycle en exécutant la Call Stack, puis les queues.

# I - Le Moteur JavaScript
Le moteur JavaScript(comme v8 utilisé dans Chrome et Node.js) exécute le code JavaScript. Il repose sur plusieurs composants clés:
1. Call Stack(Pile d'exécution) :
    * Une pile où les instructions et les appels de fonctions sont empilés pour être exécutés dans l'ordre.
    * JS est __monothreadé__, ce qui signifie qu'il exécute une seule tâche à la fois dans la pile.
2. Head(Tas) :
    * Une région mémoire utilisée pour stocker des objets et des données dynamiques.
3. Event Loop :
    * Un mécanisme qui coordonne l'exécution des tâches asynchrones et synchrones en gérant différentes files d'attente.
4. Queus(Files d'attente) :
    * Les __Taks Queue__ et __Microtask Queue__ gèrent les tâches asynchrones et leur priorité.

# II - Les composants du moteur JavaScript en détail
1. __Call Stack__(pile d'exécution)
    * Call Stack a une structure __LIFO__(Last In, First Out), où les appels de fonctions sont empilés au fur et à mesure qu'ils sont invoqués, et retirés une fois exécutés.
    * Ajout des tâches dans __Task Queue__ et __Microtask Queue__ s'est fait pendant la phase de __Call Stack__.
2. __Task Queue__(File des tâches)
    * La __Task Queue__ contient des tâches provenant d'API asynchrones comme:
        * __setTimeout__, __setInterval__
        * des événements DOM(ex: clics).
    * Les tâches de cette file sont exécutées une fois que la __Call Stack__ est vide.
3. __Microtask Queue__
    * La __Microtask Queue__ a une priorité plus élevée que la __Task Queue__. Les microtâches incluent:
        * __Promesses__(résolution de ``Promise`` via ``.then`` ou ``catch``)
        * __Mutation Observer__
    * Les microtâches sont vidées après la Call Stack mais avant __Task Queue__.

4. __Event Loop__
    * L'__Event Loop__ est le mécanisme central qui coordonne la Call Stack, la Microtask Queue, et la Task Queue:
        1. Vérifie si la Call Stack est vide.
        2. Exécute toutes les tâches dans la __Microtask Queue__
        3. Passe à la __Task Queue__ et exécute la première tâche.

# III - Erreurs fréquentes
#### A. Conflits entre tâches asynchrones et synchrones
__Erreur:__ Les développeurs pensent que les valeurs modifiées par des tâches asynchrones sont immédiatement disponibles, mais NON !
````JS
let data = null;
fetch("https://api.example.com/data")
  .then((response) => response.json())
  .then((result) => {
    data = result;
  });
console.log(data); // Problème : `data` est encore `null`
````
* La ``fetch`` est asynchrone, mais ``console.log(data)`` est exécuté immédiatement.
* ``data`` est mis à jour plus tard, lorsque la promesse est résolue.

#### B. Fuites de microtâches(Promesses non résolues)
__Erreur__: Une promesse reste en attent dans la __Microtask Queue__, bloquant potentiellement d'autres opérations.
Exemple:
````JS
Promise.resolve().then(() => {
  console.log("Microtask 1");
  return new Promise(() => {
    // La promesse n'est jamais résolue
  });
}).then(() => {
  console.log("Microtask 2");
});
````
* La chaîne de promesses est interrompue: ``Microtask 2`` n'est jamais exécutée car la promesse intermédiaire reste bloquée.

Donc, il faut toujours gérer les promesses!(gérer = Resolve/Reject)

#### C. Mauvaise gestion des délais avec ``setTimeout``
__Erreur__ : Supposer qu'un ``setTimeout`` à ``0 ms`` est exécuté immédiatement.
Mais en effet les tâches ``setTImeout`` sont placées dans la __Task Queue__, tandis que les promesses vont dans la __Microtask Queue__, qui est prioritaire.

#### D. Promesses non chaînées
__Erreur__: Ne pas retourner une promesse, ce qui déorganise la chaîne dans la __Microtask Queue__.
````js
Promise.resolve()
  .then(() => {
    console.log("Étape 1");
    Promise.resolve().then(() => {
      console.log("Étape 2 interne");
    });
  })
  .then(() => {
    console.log("Étape 3");
  });
````
* La promesse interne n'est pas chaînée, donc ``Etape 2 interne`` exécutée indépendamment, après ``Étape 3``.
