# I - ``let``, ``const``, ``var`` et le scope.
* ``var``: Portée fonctionnelle, hoisting, __souvent à éviter__.
* ``let`` et ``const``: Portée bloc, modernes, préférés.
* ``const`` =/= immuable! C'est juste que la référence ne change pas.

### A. La portée
La portée est l'endroit où une variable existe et où tu peux y accéder.
Il y a deux types principaux de portée:
|Type de portée|Déclencheur|Exemple|
|--|--|--|
|Fonctionnelle|Toute une fonction|avec ``var``|
|Bloc|Il vit uniquement dans le ``{}`` à laquelle il a été déclaré|avec ``let`` ou ``const``|

#### 1. ``var`` -> Portée fonctionnelle
````js
function test() {
	if(true) {
		var x = 5; // Une variable juste pour if.
	}
	console.log(x); // Il va afficher 5, même si x est limité au bloc ``if``
}
````

#### 2. ``let`` et ``const`` -> Portée bloc
````js
function test() {
	if(true) {
		let x = 5;
		const y = 10;
	}
	console.log(x); // ReferenceError: x is not defined
}
````

#### 3. Hoisting: attention aux surprises!
````js
console.log(a); // undefined, a est remonté(hoisté), mais initialisé à ``undefined``
var a = 10

console.log(b); // ReferenceError: Cannot access 'b' before initialization.
let b = 20;
````

#### 4. Un piège classique dans les boucles:
````js
for (var i = 0; i < 3; i++) {
  setTimeout(() => console.log(i), 1000);
}
// Affiche : 3, 3, 3

for (let j = 0; j < 3; j++) {
  setTimeout(() => console.log(j), 1000);
}
// Affiche : 0, 1, 2 ✅
````
* Avec ``var``, la variable ``i`` est créé une seule fois et partagée entre toutes les itérations.
* Avec ``let``, la variable ``i`` est créé à chaque itération. (__Spécification JS__)

# II - Types primitifs vs Objets
### A. Types primitifs
* Ce sont des valeurs simples: non modifiables.
* Ils sont stockés directement en mémoire.
````
let a = 5;            // number
let b = "hello";      // string
let c = true;         // boolean
let d = null;         // type object (bug historique)
let e = undefined;    // undefined
let f = Symbol("id"); // unique
let g = 12345678901234567890n; // BigInt
````
* Attention: ``null`` est un type "object" selon ``typeof`` - bug du langage.
* Symbole est puissant, des clés avec ``Symbol`` n'aparaît pas dans ``for...in`` ni ``Object.keys()``, ce qui est utile pour cacher une info technique. Mais rarement utilisé en dev front.

# III - Comparaisons ``==`` vs ``===``
Toujours utiliser ``===`` sauf cas très spéciaux(ex: vérif ``== null`` pour null ou undefined).
### A. ``==``: comparaison lâche (coercition de type).
````js
0 == false // true
"1" == 1 // true
undefined == null // true
````

### B. ``===``: Comparaison stricte(type + valeur)
````js
0 === false; // false
"1" === 1; // false
undefined === null // false 
````

# IV - Opérateur utiles à maîtriser
### A. ``&&`` - ET logique, mais aussi court-circuitage.
````js
true && "bonjour" // "bonjour
false && "bonjour" // false

const user = null;
console.log(user && user.name); // null (éviter crash)
````

### B. ``||`` - OU logique, aussi avec retour de valeur utile.
````js
"" || "fallback" // "fallback"
0 || 100 // 100
true || "toto" // true

const theme = userTheme || "dark";
````

### C. ``??`` - nullish coalescing (=/= ``||``)`
Retourne le 2e uniquement si le 1er est ``null`` ou ``undefined``.(C'est à dire pas ``0`` ni ``""``).
````js
"" || "default"   // "default"
"" ?? "default"   // "" ✅

0 || 100    // 100 ❌
0 ?? 100    // 0 ✅
````

### D. ``?.`` - Optional chaining
Cette opération évite les crashes lorsqu'on tente d'accéder à un attribut d'un objet via une variable, mais que cette variable pointe vers une référence ``null`` ou ``undefined``.

Il s'applique à ce qu'il y a juste devant lui. C'est à dire il test si ce qui est à gauche est ``null`` ou ``undefined`` avant de tenter d'accéder à ce qui est à droite.

Par exemple: ``user?.name`` revient à dire: "si user est null ou undefined, retourne undefined sans erreur; sinon, accède à .name".

#### __Important__: Accéder à un attribut qui n'existe pas sur un objet NON null ne provoque PAS d'erreur en JavaScript. Ça retourne simplement ``undefined``.
* Il te dit juste: "Cet attribut n'existe pas, donc je te rends ``undefined``".

# V - ``!`` et ``!!`` - Négation logique
### La liste complète des 7 valeurs falsy en JavaScript:
* ``false``
* ``0``, ``-0``, ``0n``
* ``""``
* ``null``
* ``undefined``
* ``NaN``

Et tout le reste est __truthy__ (y compris des choses parfois surprenantes!, comme ``"  "``)
Avec ``!!``, on peut convertir une valeur en booléen "proprement".
````js
!true   // false
!0      // true

!!"abc" // true (force en booléen)
!!0     // false
````

# VI - ``typeof``, ``instanceof``

````js
typeof "abc"     // "string"
typeof {}        // "object"
typeof null      // "object" ❌ bug connu
typeof []        // "object"
typeof (() => {})  // "function"

[] instanceof Array     // true
{} instanceof Object    // true
(() => {}) instanceof Function // true
````
* [] et {} ne sont pas des types primitifs, mais ils sont bien de type ``object`` -> Ils sont des objets construits par le moteur JS à partir de constructuers intégrés(``Array``, ``Object``).
* ``instanceof`` est moins utilisé dans du code TypeScript "safe". (À utiliser avec précaution.).

### A.Utilisation avec ``unknown`` pour éviter ``any`` lorsqu'on ne veut pas préciser le contenu d'un objet.
````ts
function isObject(val: unknown): val is object {
	return typeof val === "object" && val !== null;
}

function handleInput(data: unknown) {
	if (isObject(data)) {
		console.log("C'est bien un objet!");
	}
}
````
* On ajoute ``val !== null`` car ``typeof nul === "object"`` (bug historique).

Cette pratique est totalement acceptable dans pas mal de cas:
* Tu ne veux pas manipuler le contenu, juste savoir si tu as bien affaire à un objet.
* Tu travailles avec des données dynamiques, externes ou typées ``unknown``.
* Tu veux narrower le type en TypeScript pour protéger ton code des crashes, sans le sur-typer.









