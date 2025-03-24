# I - Introduction à Lodash.
On a vu hier ``Immer``, qui facilite la manipulation des objets immuable, et maintenant on va s'intéresser à ``loadash``, qui fournir des utilitaires pour manipuler efficacement __toutes sortes de données__ (objets, tableaux, chaînes, fonctions, etc.)

Par exemple, pour effectuer une copie profonde et manipuler des données:
````js
import _ from "lodash";
const newState = _.cloneDeep(objectImbriqué);
````
* On a donc crée un nouveau objet, et on peut la manipuler comme on veut.
* Le ``_``(underscore) qu'on importe avec Lodash est une __convention de nommage__ très courante dans la communauté JS de Lodash.(Qui est un successeur de la bibliothèque _Undersocre.js_)

# II - Fonctions essentielles de Lodash.
### A. Manipulation des tableaux.
* ``_.chunk``: Découper un tableau en sous-tableaux
    ````js
    const arr = [1, 2, 3, 4, 5, 6];
    console.log(_.chunk(arr, 2)); // [[1, 2], [3, 4], [5, 6]]
    ````
* ``_.uniq``: Supprimer les doublons.
    ````js
    console.log(_.uniq([1, 2, 2, 3, 3, 3])); // [1, 2, 3]
    ````
* ``_.flattenDeep``: Aplatit un tableau profondément imbriqué.
    ````js
    const nested = [1, [2, [3, [4]]]];
    console.log(_.flattenDeep(nested)); // [1, 2, 3, 4]
    ````

### B. Manipulation des objets.
* ``_.get``: Accéder à une valeur imbriquée en évitant les erreurs.
    ````js
    const user = {profile: {name: "Alice"}}:
    console.log(_.get(user, "profile.name", "Default Name"); // Alice.
    console.log(_.get(user, "profile.age", "N/A"); // N/A.
    ````
	* Mais si on fait une faute de frappe, la méthode ``_get`` va juste retourner un ``undefined`` au lieu d'erreur. Donc faut faire attention à dex chemins données! 
	On peut faire avec autocompletion, __en précisant les chemins antérieur__ jusqu'à l'objet qu'on veut retourner.
* ``_.has``: Peut vérifier si une propriété existe ou pas.
	````js
	const safeGet(obj, path) {
		if(!_.has(obj, path)) {
			cosole.warn(`chemin invalide: ${path}`)
		}
		return _.get(obj, path);
	}
	````
* ``_.set``: Modifier un objet imbriqué sans soucis. // __Ceci modifie directement l'objet!!!__
    ````js
    const user = { profile: {name: "Alice" }};
    _.set(user, "profile.age", 25);
    console.log(user); // {profile: {name: "Alice", age: 25}}
    ````
* ``_.merge``(Récursive) vs ``_.assign``(Superficielle): Fusionner des objets.
  * ``_.assign``: Il copie les propriétés propres d'un ou plusieurs objets dans un objet cible, __sans descendre dans les sous-objets__.
	````js
	import _ from "lodash";
	const obj1 = { a: 1, b: { x: 10 } };
	const obj2 = { b: { y: 20}, c: 3 };
	const result = _.assign({}, obj1, obj2);
	// { a:1, b:{y: 20}, c: 3 }
	````
	  * ``obj2.b`` écrase ``obj1.b``, donc l'ordre est important.
	  * On a un objet vide comme première paramètre pour éviter de modifier obj1.
	
  * ``_.merge``: Il fusionne récursivement les propriétés des objets. Donc s'il y a des objets imbriqués, ils sont combinés au lieu d'être écrasés.
	````js
	const result = _.merge({}, obj1, obj2);
	// { a: 1, b: {x: 10, y: 20}, c: 3}
	````
    * Ici, on a ``b`` contient à la fois ``x`` et ``y`` par fusion profonde.

* ``_.pick`` et ``_.omit``: Extraire ou exclure des clés d'un objet.
    ````js
    const obj = { name: "Alice", age: 25, city: "Paris"}
    
    console.log(_.pick(obj,   ["name", "age"])); // {name: "Alice", age: 25}
    console.log(_.omit(obj,  ["age"]); // {name: "Alice", city: "Paris"}
    ````

### C. Manipulation des chaînes de caractères.
* ``_.capitalize``: Première lettre en majuscule.
    ````js
    console.log(_.capitalize("Hello world"));
    ````
* ``_.camelCase``, ``_.kebabCase``, ``_.snakeCase``:
    ````js
    console.log(_.camelCase("hello world")); // helloWorld;
    console.log(_.kebabCase("hello world")); // hello-world;
    console.log(_.snackCase("hello world")); // hello_world;
    ````
* ``_.truncate``: Tronquer une chaîne.
    ````js
    console.log(_.truncate("This is a long text", {length: 10})); // "This is..."
    ````

### D. Optimisation des performances
``_.debounce`` et ``_.throttle`` sont deux techniques essentielles pour optimiser __la performance des événements__ en JS, notamment pour éviter des appels trop fréquents de fonctions coûteuses(API, DOM, updates, etc.).

* ``_.debounce`` (__Faire une pause avant d'exécuter le code__, Évite les appels trop fréquents)
    * Recherche en temps réel(Évite de spammer une API sur chaque frappe).
    * Attendre que l'utilisateur ait fini de taper avant d'exécuter une action.
        * ````js
            const debouncedLog = _.debounce(() => console.log("Hello"), 500);
            debouncedLog(); // Attend 500ms avant d'exécuter console.log
            debouncedLog(); // Timer réinitialisé. Attend un nouveau 500ms avant d'exécuter le console.log.
            ````
    * Éviter d'appeler un __resize event__ à chaque pixel redimensionné.
    * Exemple 1:
        * Sans ``_.debounce``:
            ````js
            const fetchResults = (query) => {
              console.log(`Fetching results for: ${query}`);
            };
            
            document.getElementById("search").addEventListener("input", (event) => {
              fetchResults(event.target.value);
            });
            ````
            * ``hello`` -> 5 requêtes API envoyées rapidement.
        * Avec ``_.debounce``:
            ````js
            const fetchResults = _.debounce((query) => {
                ...
            }, 500)
            ...
            ````
            * L'API est appelée seulement __après 500ms sans frappe supplémentaire__. Si l'utilisateur tape rapidement, la fonction est annulée et remise à zéro à chaque frappe.
    * Exemple 2:
        Debounce pour attendre 500ms avant d'exécuter la recherche.
        ````js
        const debouncedSearch = useCallback(_.debounce((searchTerm) => {
            fetch(...);
        }, 500 ), []);
        ````
* ``_.throttle``: Limite la fréquence d'exécution.
    * __Scroll event__: Empêcher un ``console.log()`` d'être spamé à chaque pixel.
    * __Resize event__: Éviter de recalculer le layout trop souvent.
    * __Click event__: Limiter les clics sur un bouton "Acheter" pour éviter les doubles commandes.
    * Exemple 1, pour un scroll event:
        ````js
        useEffect(() => {
            const throttledScroll = _.throttle(() => {
                    setScrollY(window.scrollY);
            }, 1000)
            window.addEventListener("scroll", throttledScroll);
            return () => window.removeEventListner("scroll", throttledScroll);
        }, [])
        ````

# III - Utiliser ``lodash`` avec ``immer``.
* Pourquoi utiliser ``lodash`` et ``immer`` ensemble?
    * __Immer__ simplifie la gestion des états immuables.
    * __Lodash__ simplifie la manipulation des objets et tableaux(ex: ``_.set``, ``_.merge``, ``_.omit``, etc.).
    * Combinés ensemble, ils permettent de gérer des objets imbriqués facilement sans risque de mutation accidentelle.

* Exemple1, set une proprieté:
    ````js
    setUser(produce(user, draft => {
       _.set(draft, "profile.age", 30); // Immer + lodash ! 
    }));
    ````
    * __Immer__ permet d'éditer un état immuable via un proxy.
    * __Lodash__ , ``_.set`` permet de modifier facilement une propriété imbriquée.
    * Moins de code, plus propre!

* Exemple2, ``_.Merge`` avec Immer:
    ````js
    const [user, setUser] = useState({ name: "Alice", profile: { age: 25 }});
    const updateProfile = () => {
        setUser(produce(user, draft => {
            _.merge(draft, { profile: {age: 30, city: "Paris"} });  
        }))
    }
    ````



