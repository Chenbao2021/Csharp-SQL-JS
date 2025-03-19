* ``immer`` est une bibliothèque JavaScript populaire qui facilite la manipulation des états immuables. Bien qu'il soit principalement utilisé avec ``useReducer`` ou Redux, il a d'autres cas d'usages intéressants.
* ``lodash`` est une bibliothèque JavaScript populaire offrant de nombreuses fonctions utilitaires pour manipuler les objets, tableaux, chaînes de caractères et bien plus. Elle est souvent comparée à ``immer`` pour certaines fonctionnalités, mais elles n'ont pas le même objectif.

# I. Immer
### A - Pourquoi utiliser Immer?
* Éviter les erreurs liées à la mutation d'état en React et Redux.
* Rend le code plus lisible et plus concis.
* Gère les objets et tableaux imbriqués plus facilement.
* Améliore la performance en évitant les copies inutiles.

### B - Comment fonctionne Immer?
Immer repose principalement sur la fonction ``produce``, qui crée un "draft"(brouillon) de l'état, permettant des modifications comme si l'objet était mutable.
``produce`` a deux formes:
1. ``produce(baseState, producerFunction)``
    C'est la forme standard où il prend l'état initial et une fonction qui modifie un "draft".(Retourne __immédiatement__ le nouvel état basé sur ``state``).
2. ``produce(producerFunction)``
    Cette version __retourne une fonction__ qui attend ensuite l'état initial. C'est très utile avec ``useState`` et ``setState``.(``setState`` appelle la fonction avec ``prevState`` automatiquement).

Puis, il génère un __nouvel état immuable__.
* ````js
    import { produce } from "immer";
    const state = { count: 0 };
    
    // Met à jour le state de manière immuable
    const newState = produce(state, (draft) => {
      draft.count += 1;
    });
    
    console.log(newState); // { count: 1 }
    console.log(state); // { count: 0 } (l'original reste inchangé)
    ````
    1. ``produce`` prend ``state`` comme base.
    2. Il crée un __draft__ modifiable.
    3. On modifie ce draft __comme un objet normal__.
    4. ``produce`` génère un nouvel état sans affecter l'original.

### C - Utilisation avec ``useReducer``(React)
Immer est souvent utilisé avec ``useReducer`` pour simplifier la gestion des états complexes.
Par exemple:
* ````js
    import {produce} from "immer";
    const reducer = (state, action) => {
        return produce(state, (draft) => {
            switch(action.type) {
                case "increment":
                    draft.count += 1;
                    break;
                ...
            }
        }
    }
    ````

``Immer`` est intégré nativement dans Redux.

### D - Utilisation en dehors de reducers
##### 1. Utiliser ``produce`` d'Immer avec ``useState``.
Lorsqu'on manipule des objets imbriqués, il faut faire attention à __ne pas modifier l'état directement__ et à __copier chaque niveau modifié__.
* Sans immer:
    ````js
    setState(prevState => ({
        ...prevState,
        user: {
            ...preState.user,
            name: "Bob"
        }
    }))
    ````
* Avec Immer:
    ````js
    setState(produce(draft => {
        draft.user.name = "Bob";
    }) )
    ````

##### 2. Éviter les erreurs de mutation accidentelle
React ne détecte __que les références modifiées__. Si on change une valeur __sans modifier la référence__, React ne re-render pas.
* Sans immer:
    ````js
    const increment = () => {
        state.count += 1;
        setState(state);
    }
    ````
* Avec immer:
    ````js
    const increment = () => {
        setState(produce(draft => { draft.count += 1; }))
    }
    ````
    Ou
    ````js
    setState(prevState => produce(prevState, draft => {draft.count += 1;});
    ````

##### 3. Facilite les mises à jour conditionnelles
Avec ``produce``, les mises à jour conditionnelles sont __plus propres et plus sûres__.
* Sans immer:
    ````js
    setState(prevState => ({
        ...prevState,
        users: prevState.users.map(user => user.name === 'Alice' ? {...user, age: user.age + 1} : user
        )
    } ))
    ````
    * Beaucoup des spread operator:
    * Difficile à lire et maintenir!
* Avec immer:
    ````js
    setState(produce(draf) => {
        const user = draft.user.find(user => user.name === "Alice");
        if(user) {
            user.age += 1;
        }
    })
    ````
    * Lisibilité améliorée.
    * Aucune copie manuelle inutile.

##### 4. Utile dans des applications plus grandes
Dans une grande application avec beaucoup d'état imbriqués, utiliser ``produce`` dès le début permet d'éviter:
* Des copie-collées de ``...prevState``.
* Des erreurs d'oubli de copie qui causent des bugs difficiles à déboguer.
* Une complexité excessive dans les mises à jour des états.

### E - Quand ne pas utiliser Immer.
* Si le state est un simple ``boolean``, ``string`` ou ``number``.
* Pas d'objet imbriqué.