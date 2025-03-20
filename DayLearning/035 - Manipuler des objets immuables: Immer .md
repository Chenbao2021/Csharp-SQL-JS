``immer`` est une bibliothèque JavaScript populaire qui facilite la manipulation des états immuables. Bien qu'il soit principalement utilisé avec ``useReducer`` ou Redux, il a d'autres cas d'usages intéressants.

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
3. Dans ``produce``, il n'y a pas besoin d'un ``return`` explicite parce qu'Immer gère la création et le retour du nouvel état en interne.
    1. Il crée une copie de ``state`` sous forme d'un proxy mutable(appelé ``draft``).
    2. Il permet de modifier directement ce ``draft``(Contrairement à un état immuable classique).
    3. Une fois la fonction exécutée, Immer génère et retourne automatiquement une version __immuable__ de l'état mise à jour.(Avec tous les modifications que t'as effectué sur ``draft``).
    4. __Donc, pas de ``return`` explicite !!__
 
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
#### 1. Utiliser ``produce`` d'Immer avec ``useState``.
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

#### 2. Éviter les erreurs de mutation accidentelle
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

#### 3. Facilite les mises à jour conditionnelles
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

#### 4. Utile dans des applications plus grandes
Dans une grande application avec beaucoup d'état imbriqués, utiliser ``produce`` dès le début permet d'éviter:
* Des copie-collées de ``...prevState``.
* Des erreurs d'oubli de copie qui causent des bugs difficiles à déboguer.
* Une complexité excessive dans les mises à jour des états.

### E - Quand ne pas utiliser Immer.
* Si le state est un simple ``boolean``, ``string`` ou ``number``.
* Pas d'objet imbriqué.

# II - Immer et Proxy.
Un ``proxy`` en JS est une fonctionnalité qui permet d'__intercepter__ et de __contrôler__ l'accès à un objet.
* C'est comme un "gardien", qui surveille les interaction avec un objet et permet d'ajouter un comportement personnalisé.
* Immer utilise ``proxy`` pour créer un état "mutable" en apparence, tout en restant réellement immuable.

### A. Comment fonctionne un ``Proxy`` en JS?
Un ``Proxy`` prend deux paramètres:
1. L'objet cible: Celui qu'on veut contrôler.
2. Un "handler": Un objet contenant des "traps"(``get``, ``set``, etc.) qui interceptent les accès.

Exemple simple d'un ``Proxy``
* ````js
    const user = {name: "Alyce", age: 25}
    const handler = {
        get(target, key) {
            console.log(`Accès à la clé : ${key}`);
            return target[key]; // Retourne la valeur originale
        },
        set(target, key, value) {
            console.log(`Modification : ${key} = ${value}`);
            target[key] = value;
            return true; // Indique que l’opération a réussi
        }
    }
    const proxyUser = new Proxy(user, handler);
    console.log(proxyUser.name); // 🔥 "Accès à la clé : name" puis "Alice"
    proxyUser.age = 30; // 🔥 "Modification : age = 30"
    console.log(proxyUser.age); // 🔥 "Accès à la clé : age" puis "30"
    ````
    * On intercepte les lectures et modifications(``get``, ``set``);
    * On peut empêcher des modifications ou ajouter des logs.
    * L'objet original ``user`` est directement affecté(car ici, on ne travaille pas en mode immuable).

### B. Pourquoi Immer utilise un ``Proxy`` ?
Immer utilise ``Proxy`` pour permettre de modifier un objet "comme si c'était mutable", tout en restant immuable.
* Problème sans ``Immer``:
    ````js
    const state = { user: { name: "Aluce", age: 25 }}:
    const newState = { ...state, user: { ...state.user, age: 30 } }
    ````
* Solution avec Immer(``Proxy``):
    ````js
    const state = { user: { name: "Alice", age: 25 } }
    const newState = produce(state, draft => {
        draft.user.age = 30
    });
    ````

### C. Fonctionnement du ``Proxy`` dans Immer.
Quand on utilise ``produce(state, draft => {...])``: 
1.  Immer crée un Proxu du ``state``(C'est le ``draft``).
2.  Toute modification sur ``draft`` est interceptée et stockée.
3.  À la fin, Immer génère une nouvelle version de l'objet, immuable.

Voici une version simplifié de ``Proxy``, tel qu'on ne modifie pas ``target``:
* ````js
    function createImmrLikeProxy(base) {
			let changes = {}; // stocker les changements.
			return new Proxy(base, {
				get(target, key) {
					return key in changes ? changes[key] : target[key];
				},
				set(target, key, value) {
					changes[key] = value;
					return true;
				},
				apply(target, thisArg, args) {
					return { ...target, ...changes };
				}
			})
		}
    ````
	* On écrit pas directement sur l'objet ``target``, sinon sur ``changes``.
	* On stocke les changements dans un objet temporaire.(``changes``).
	* À la fin, on fusionne les changements avec l'original pour créer un __nouvel objet immuable__.
