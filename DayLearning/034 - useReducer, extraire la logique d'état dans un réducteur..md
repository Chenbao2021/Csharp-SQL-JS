useReducer est un Hook React qui nous permet d'ajouter un __réducteur__ à votre composant.

# I - Extraire la logique d'état dans un réducteur.
* __Réducteur__: Une fonction extérieur au composant, qui consolide toute la logique de mise à jour d'état. Facilité la gestion des états qui sont dispersées dans des nombreux gestionnaires d'événements.
   * _Gestionnaire d'événement_: Ce sont des fonctions de type: ``handle...``, ``onClick...``, etc.
   * Souvent les juniors voient des __réducteurs__ avec de switch, mais un __reducteur__ n'a pas besoin d'être avec un switch, même avec un if/else, une opération ternaire peut marcher! C'est juste une fonction qui décrit comment une action va mettre à jours les données (state renvoyé par useReducer). 
### A. Consolider la logique d'état avec un réducteur.
Lorsque votre projet évolue, la quantité de logique(gestionnaire d'événement) qu'il contient grandit également.  Pour réduire cette complexité et garder votre logique en un seul endroit facile d'accès, on peut __déplacer__ dans une fonction unique à l'extérieur du composant, appelée __réducteur__.

La gestion de l'état avec des réducteurs diffère légèrement d'une définition directe de l'état. Plutôt que de dire à React __quoi faire__ en définissant létat, on dit __ce que l'utilisateur vient de faire__ en émettant des __actions__ à partir de nos gestionnaires d'événements(La logique de mise à jour de l'état se situe ailleurs).
* Au lieu de __définir__, on __dispatch__.
* Exemple:
    ````js
    const handleDeleteTask = (taskId) => {
        disatch({
            type: 'deleted',
            id: taskId
        })
    }   
    ````
    * L'objet qu'on passe à ``dispatch`` est appelé une action. Par convention, il a ``type`` qui décrit ce qui s'est passé.

Votre logique d'état se situera dans une __fonction de réduction__. Elle prend deux arguments, l'état courant et l'objet d'action, puis renvoie le nouvel état.
* Exemple:
  ````js
    function tasksReducer(tasks, action) {
        switch(action.type) {
            case 'deleted': {
                return tasks.filter((t) => t.id !== action.id)
            }
        }
    }
    ````

Pour finir, on doit connecter le ``taskReducer`` à notre composant:
* ````js
    const [task, dispatch] = useReducer(tasksReducer, initialTasks);
    ````
    * On doit lui passer un état inital. (On peut la définir dans le fichier de ``taskReducer``).
    * Il renvoie une valeur d'état ainsi qu'un moyen de le redéfinir (En l'occurrence, la fonction de dispatch).

### B. Comparaison de ``useState`` et ``useReducer``: Les réducteurs ne sont pas sans inconvénients! 
* __Taille du code__: On doit écrire la fonction de réduction et __dispatcher__ les actions. Utiliser reducer sauf si plusieurs gestionnaires d'événements modifient l'état local de façon similaire.
* __Lisibilité__: Quand la logique devient complexe, reducer permet de séparer proprement le __comment__ de la logique du __ce qui est arrivé__ des gestionnaire d'événements.
* __Débogage__: C'est plus facile à déboguer avec reducer.
* __Tests__: Réducer est une fonction pure qui ne dépend pas de votre composant. C'est à dire on peut l'exporter et la tester en isolation.

### C. Écrire les réducteurs correctement: Gardez ces deux points à l'esprit.
* __Les réducteurs doivent être purs.__ Les mêmes entrées doivent toujours produire les mêmes sorties.  Ils ne doivent pas:
    * Envoyer des requêtes.
    * Planifier des timers.
    * Traiter des effets secondaires.

* __Chaque action décrit une interaction utilisateur unique, même si ça entraîne plusieurs modifications des données__.
    Par exemple, sur le bouton _Réinitialiser_ d'un formulaire comportant cinq champs gérés par un réducteur, il sera plus logique de dispatcher une seule action ``reset_form`` plutôt que cinq actions ``set_field`` distinctes.
    * Si on journalise chaque action d'un réducteur, ce journal doit être suffisamment claire pour qu'on permet de reconstruire l'ordre et la nature des interactions et de leurs traitements.

# II - useReducer(reducer, initialArg, init?)
### A. Référence
Appelez ``useReducer`` au niveau racine de votre composant pour gérer son état avec un __réducer__.
* ````js
    function reducer(state, action) { ... }
    function RootComponent() {
        const [state, dispatch] = useReducer(reducer,  {age: 42});
    }
    ````
    * ``reducer``: La fonction de réduction qui spécifie comment votre état est mis à jour. Il prend l'état et l'action en paramètres et renvoyer le prochain état. L'état et l'action peuvent être de n'importe quels types.
    * ``initialArg``: La valeur à partir de laquelle l'état dépend du paramètre ``init`` qui suit.
* __Valeur renvoyée__
    ``useReducer`` renvoie un tableau avec exactement deux valeurs:
    * L'état courant. Lors du premier rendu, il est défini avec ``init(initialArg)`` ou ``initialArg``(S'il n'y a pas d'argument ``init``).
    * La fonction ``dispatch`` qui vous permet de mettre à jour l'état vers une valeur différente et ainsi redéclencher un rendu.

La fonction ``dispatch`` permet de mettre à jour l'état avec une valeur différente et de déclencher un rendu. Le seul paramètre à passer à la fonction est l'action:
* ````js
    function handleClick() {
        dispatch({ type: 'incremented_age' });
    }
    ````

Limitations:
* La fonction ``dispatch`` ne met à jour l'état que pour le prochain rendu.
* React __met à jour l'état par lots__. Il met à jour l'écran une fois que tous les gestionnaires d'événements ont été exécutés et on appelé leurs fonctions ``set``.

### B. Utilisations
Éviter de recréer l'état initial:
* React sauvegarde l'état initial une fois et l'ignore lors des rendus suivants.
    ````js
    function createInitialState(username) {
      // ...
    }
    
    function TodoList({ username }) {
      const [state, dispatch] = useReducer(reducer, createInitialState(username));
      // ...
    ````
    Bien que le résultat de ``createInitialState(username)`` soit seulement utilisé pour le premier rendu, on continu d'appeler cette fonction à chaque rendu. C'est du gâchi si elle crée de grands tableaux ou effectue des calculs coûteux.
* Pour corriger ça, on peut __la passer comme fonction d'initialisation__ au ``useReducer`` en tant que troisième argument.
    ````js
    const [state, dispatch] = useReducer(reducer, bruteState, createInitialState)
    ````
    On a bien passé la fonction elle-même, et pas le résultat d'exécution. Par conséquent l'état initial n'est pas recréé après l'initialisation.

### C. Dépannage.
##### 1. J'ai dispatché une action, mais la console m'affiche l'ancienne valeur.
__L'état comporte comme un instantané__. Mettre à jour un état planifie un nouveau rendu avec sa nouvelle valeur, mais n'affecte pas la variable JS ``state`` dans vos gestionnaires d'événement en cours d'exécution.

Si vous avez besoin de deviner la prochain valeur de l'état,  on peut la calculer en appelant nous-même notre réducteur:
* Exemple 1
    ````js
    const action = {type: `ìncremented_age`}
    dispatch(action);
    const nextState = reducer(state, action); //Suite des exécution on va utiliser nextState, au lieu de state
    ````

* Exemple 2
    ````js
    const handleMultipleUpdates = () => {
        let nextState = state;
        nextState = reducer(nextState, action);
        nextState = reducer(nextState, action);
        // Autre actions. Puis on met à jour le state.
        dispatch({type: 'set', payload: nextState));
    }
    ````

##### 2. Pour chaque ``case`` dans ``dispatch``, il faut un ``retourner``.
L'erreur fréquent dans une fonction reducer est d'oublier de retourner un nouvel état, et dans ce cas ``useReducer`` va prendre la valeur retourné par la fonction ``reducer`` pour mettre à jour le ``state``.
Gérer ce cas avec le ``default`` dans ``switch`` en cas où aucune case n'est déclenchée.

##### 3. Déclencher une action avec une faute de frappe dans le type.
Si le type d'action contient une faute, le ``switch`` ne le reconnaîtra pas. 
Par exemple, au lieu de ``increment`` on a ``incremen`` avec un t en moins.

Donc, il faut mieux créer une const/objet pour éviter les fautes:
* ````js
    const INCREMENT = "increment";
    ````

##### 4. Utiliser useReducer pour des états très simples qui peuvent être géré par useState.
Dans ce cas, ``useReducer`` ajoute une complexité inutile!

##### 5. Exécuter du code asynchrone dans le réducteur.
Les réducteurs doivent être purs et ne pas contenir d'effets secondaires(ex: appels API, timers);
* Les réducteurs doivent être purs (Ils doivent toujours retourner le même état pour les mêmes entrées).

Solution possible, déclencher les effets asynchrones dans un ``useEffet``, puis mettre à jour l'état avec ``dispatch``.
* ````js
    useEffect(() => {
      async function fetchData() {
        const response = await fetch("/api/data");
        const data = await response.json();
        dispatch({ type: "dataLoaded", payload: data });
      }
      fetchData();
    }, []);
    ````

##### 6. Mauvaise gestion des mises à jour successives
Enchaîner plusieurs ``dispatch`` peut donner un état incorrect si on ne tient pas compte de l'état mis à jour:
* ````js
    const handleMultipleUpdates = () => {
      dispatch({ type: "increment" });
      dispatch({ type: "increment" });
    };
    ````
    React regroupe les mises à jours et peut ne pas se comporter comme attendu.

React regroupe les mises à jour et peut ne pas se comporter comme attendu.
* ````js
    const handleMultipleUpdates = () => {
      let nextState = reducer(state, { type: "increment" });
      nextState = reducer(nextState, { type: "increment" });
      dispatch({ type: "set", payload: nextState });
    };
    ````



