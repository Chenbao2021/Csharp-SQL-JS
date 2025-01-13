## I. Introduction
Les fonctions auto-exécutées en JavaScript(IIFE, ou __Immediately Invoked Function Expression__) sont des fonctions qui s'exécutent immédiatement après leur définition. 
Elles permettentent:
* Isoler du code, d'éviter de polluer l'espace global(Exemple, définir des variables temporairement)
* Exécuter une logique sans avoir besoin d'appeler la fonction explicitement.

## II. Syntaxe d'une IIFE
Une IIFE utilise généralement une fonction anonyme ou nommée encapsulée entre des parenthèses, suivie immédiatement de ``()`` pour l'exécuter.
````js
(
    function() {
        console.log("Cette fonction est exécutée immédiatement !")
    }();
)
````
1. ``(function() {...})`` ou ``(() => {...})()``: Ceci définit une fonction anonyme.
2. ``()``: Ces parenthèses déclenchent l'exécution immédiate de la fonction.

## III. Avantages des IIFE
### 1. Encapsulation du scope.
Le code à l'intérieur d'une IIFE est isolé du reste, ce qui empêche les variables ou les fonctions déclarées dans son corps d'affecter l'espace global.

### 2. Éviter les conflits de variables.
Dans un projet avec plusieurs scripts, les IIFE aident à éviter les collisions de noms de variables.

### 3. Utilisation immédiate d'un code logique.
Les IIFE sont idéales pour initialiser des variables ou exécuter un bloc de code une seule fois sans définir des fonctions nommée :
````js
var compteur = (function(){
    var count = 0;
    return function () {
        return ++count;
    }
})();
console.log(compteur()); // 1
console.log(compteur()); // 2
````

## IV. IIAF et useEffect
````js
useEffect(() => {
  const fetchData = async () => {
    try {
      const response = await fetch("/api/data");
      const result = await response.json();
      setData(result);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  (async () => {
    console.log("1. fetchData completed");
    await fetchData(); // Bloque jusqu'à ce que fetchData soit terminé
    console.log("2. fetchData completed");
  })();
   console.log("3. ffect execution continues...");
}, []);
````
Comme useEffect n'accepte pas une fonction async comme callback, alors si on veut attendre la résolution de ``fetchData()`` avant de passer à autre chose, on doit utiliser des IIAF pour contourner cette limite. 
Cela permet d'appeler des fonctions asynchrones et d'utiliser ``await`` .

Mais attention, les async/promesse sont exécuté une fois les codes synchrones terminé, du coup ici dans le console, on les aura dans l'ordre: 3 -> 1 -> 2. Car c'est __microtaks queue__ qui les gèrent.

## V. Event Loop
### 1. Le modèle de l'Event Loop en JavaScript
JS utilise un modèle basé sur une __file d'attente d'exécution unique__ pour gérer les tâches. Ce modèle est divisé en plusieurs étapes clés:
1. __Call Stack__(pile d'appels):
    * Contient le code JS asynchrone en cours d'exécution.
    * Les appels de foncions y entrent et en sortent dans l'ordre d'exécution.
2. __Tas Queue__(file des tâches):
    * Contient les tâches asynchrones comme les timers(``setTimeout``, ``setInterval``, les événements du DOM, etc.)
    * Les tâches de cette file sont exécutées uniquement lorsque la pile d'appels est vide.
3. __Microtask Queue__(file des microtâches):
    * Contient les microtâches comme les résolutions de promesses(``Promise.then``, ``catch``, ``finally``) et les fonctions asynchrones(``async/await``).
    * Les microtâches sont __traitées avant les tâches normales de la ``Task Queue``.

### 2. Pourquoi les ``async``/``await`` et promesses s'exécutent "à la fin"?
1. __Suspension de l'exécution__:
    * L'exécution de la fonction ``async`` est suspendue à l'endroit où le mot-clé ``await`` est utilisé. Et pendant cette suspension, JS continue à exécuter le reste du code synchrone. Et puis, une fois que la promsse est résolue ou rejeté, son callback sera placé dans la __microtask queue__ pour être exécuté dès que possible après le code synchrone.
        * Pour ``fetch`` ou ``timeout``, la __résolution ou le rejet de la promesse est délégué à un autre mécanisme__, souvant hors du thread principal JS.
    * Le code derrière le ``await`` ou le ``.then()`` est placé dans la __microtask queue__.
2. __Exécution différée__:
    * JS exécute le code __ligne par ligne__, et chaque élément est soit exécuté immédiatement, soit __programmé pour une exécution future__(C'est à dire à exécuter plus tard).
    * La file d'exécution JS termine d'abord tout le code synchrone dans la __pile d'appels__.
    * Ensuite, toutes les __microtask__ dans la microtask queue sont exécutées.
    * Enfin, les tâches de la __task queue__ (Comme les timers) sont traitées.

### 3. Exemple avec explication détaillée
Dans la situation où une tâche C est ajoutée à la __mocrotask queue__ pendant l'exécution de la tâche A dans la __macrotask queue__, voici ce qui se passe:
#### Règles de traitement par l'Event Loop.
1. Une tâche de la __task queue__ est en cours d'exécution(ici, __A__).
2. Pendant cette exécution, une microtask __C__ est ajoutée à la __microtask queue.__.
3. Une fois que la tâche en cours __A__ est terminée, l'Event Loop:
    * Vérifie s'il y a des microtasks dans la __microtask queue__.
    * Exécute __toutes les microtasks__ avant de passer à la prochaine macrotask(ici, __B__).

### 4. Pourquoi ce comportement est important?
1. __Prédictibilité :__
    * En plaçant les promesses dans la __microtask queue__, elles s'exécutent __immédiatement après le code synchrone__, mais avant les autres tâches asynchrones comme les timers.
2. __Performance et fiabilité :__
    * Les microtâches sont légères et raides à exécuter, ce qui garantit une latence minimale dans le traitement de tâches critiques(Comme la gestion de données promises).
3. __Consistance avec le modèle d'asynchronisme :__
    * Cela permet d'assurer que les promesses respectent toujours leur comportement asynchrone, même si elles sont déjà résolues.

