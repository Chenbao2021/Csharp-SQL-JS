# I - Fonctions avancées: Closures, fonctions retournées, currting, callback, etc.
## A. Closures - La base du JS
Une closure signifie qu'une __fonction interne garde accès aux variables de la fonction externe__, même après que cette dernière ait terminé son exécution.
````js
function outer() {
	const secret = "Bao";

	function inner() {
		console.log(secret);
	}

	function change(val) {
		secret = val;
	}

	return {inner, change}
}

const store = outer();
store.fn(); // "Bao"
````
* On __enferme__ une variable dans une fonction externe(``outer``), et on l'expose des fonctions internes qui peuvent __lire ou la modifier - Sans que cette variable ne soit accessible de l'extérieur directement__.
	Et c'est justement l'idée de __useState__ de React!

C'est super puissant pour faire:
* Des compteurs privés.
* Des états personnalisés.
* Des __mini-stores__ comme dans React ou Vue.


## B. Fonctions retournées(factories)(Créer une fonction personnalisée/configuré)
````js
function createMultiplier(factor) {
	return function(x) {
		return x * factor;
	}
}

const double = createMultiplier(2);
const triple = createMultiplier(3);
````
* C'est de la __programmation fonctionnelle__ puissante et réutilisable!

### 1. Pourquoi c'est important en React?
#### Les hooks personnalisés sont des factories.
Quand tu écris un __custom hook__, tu crées une fonction qui __renvoie__ un comportement encapsulé, souvent sous forme de valeurs et fonctions.
````js
function useCounter(initial = 0) {
	const [count, setCount] = useState(initial);
	const increment = () => setCount(c => c + 1);

	return {count, increment};
}
````
* Ici, ``useCounter`` est une factory de logique réutilisable.

#### Les composants React sont eux-même des fonctions qui retournent du JSX
````js
function Button({label}) {
	return <button>{label}</button>;
}
````
* Tu peux voir ça comme une factory de ``JSX``

#### Pattern "factory" dans des fonctions utilitaires ou config.
````js
function createLogger(prefix) {
	return function log(message) {
		console.log(`[${prefix}] ${message}`)
	}
}
````
* C'est super utile pour injecter une config, un style, un thème, etc.

## C. Currying (Fractionnement des arguments, transformer une fonction multi-argument en une suite de fonction)
Transformer une fonction avec plusieurs paramètres en une suite de fonctions à un seul paramètre.
````js
function add(a) {
  return function (b) {
    return a + b;
  };
}

add(2)(3); // 5
````
* Permet de créer des fonctions personnalisées plus facilement.

En React, ça permet de générer des callbacks personnalisés:
````js
function handleChange(field) {
  return function (event) {
    console.log(`${field} changed to ${event.target.value}`);
  };
}

<input onChange={handleChange("email")} />
<input onChange={handleChange("password")} />
````

````js
// Version currying
const add = a => b => a + b
add(2)(3); // 5

// Version factories
const add2 = add(2)
add2(3);
````

## D. Callback et Higher-Order Functions.
Une fonction peut prendre une autre fonction en argument, ou en retourner une autre.
````js
const sayHello = name => `Hello ${name}`;
const greet = (fn, name) => console.log(fn(name));

greet(sayHello, "Chen"); // "Hello Chen"
````

### 1. Async et Callback
Si on fait ``await`` dans une fonction callback non attendue par son appelant, le résultat n'est pas attendu, même si tu écris ``await`` dedans.
````js
const results = [1, 2, 3].map(async (id) => {
  const user = await getUser(id);
  return user;
});

// [Promise, Promise, Promise]
````
* map() ne sait pas que tu fais quelque chose d'async
* Il exécute les 3 fonctions immédiatement
* Tu obtiens un tableau de Promise, pas les valeurs 

Dans l'exemple ci-dessus, notre appelant est la fonction ``map()``, il ne sait pas ni ne se soucie que ton callback est ``async``, ``map()`` ne fait que collecter ce que ta fonction retourne => Un tableau de ``Promise``.

Règle d'or: L'``async`` rend ta fonction "retournée" asynchrone, mais l'appelant doit savoir quoi en faire !
````js
const results = await Promise.all(
  [1, 2, 3].map(async (id) => {
    const user = await getUser(id);
    return user;
  })
);
````

