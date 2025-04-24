# I - Les fonctions
### A. Les types de déclarations de fonctions.
````js
// Hoisting: Elle est disponible avant sa déclaration.
// A sont propre ``this`` si appelée comme méthode d'un objet.
function classique() {
  return "Je suis une fonction classique";
}

// Le nom n'est pas obligatoire.
// Pas hoistée.
// this dépend du contexte d'appel.
const anonyme = function () {
  return "Je suis une fonction anonyme";
};

// Pas d'argument: Le pseudo-variable disponible uniquement dans les fonctions classiques.
// Pas de this.
// Pas de new possible.
const fléchée = () => {
  return "Je suis une fonction fléchée";
};
````
* En React, on utilise majoritairement des fonctions fléchées, mais attention au comportement du ``this``

### B. Fonctions fléchées(``=>``)
Les fonctions fléchées:
* Sont plus courtes.
* __Ne créent pas leur propre ``this``__.
* Ne peuvent pas être utilisées comme constructeur(``new``).

#### 1. Mais qu'est ce que veut dire "créer son propre ``this`` ?
````js
const obj = {
  name: "Chen",
  classique: function () {
    console.log("classique", this.name); // ✅ "Chen"
  },
  flechee: () => {
    console.log("flechee", this.name); // ❌ undefined
  }
};

obj.classique(); // classique Chen
obj.flechee();   // flechee undefined
````
* ``classique()`` crée son propre ``this``, et cette variable est déterminée au moment de l'appel, selon qui appelle la fonction.
* ``flechee()`` ne crée pas de ``this``, donc elle prend celui où elle a été définie, c'est à dire tel qu'il est dans l'endroit du code où elle est écrite.
	````js
	const obj = {
		name: "Chen",
		outer: function () {
			const inner = () => {
				console.log(this.name); // 🔥 ici, "this" vient de "outer"
			};
			inner();
		}
	};

	obj.outer(); // ✅ "Chen"
	````

### C. ``this`` expliqué simplement.
Le mot-clé ``this`` désigne le contexte d'exécution, qui change selon comment la fonction est appelée, pas où elle est déclarée.
````js
function test() {
  console.log(this);
}
test(); // contexte global (window en navigateur)

const obj = { f: test };
obj.f(); // contexte = obj ✅
````
* En React, le ``this`` est rarement utilisé, car les fonctions composants n'ont pas de ``this``.

#### 1. ``this`` est une valeur contextuelle disponible à l'intérieur d'une fonction classique, qui dépend de la façon dont la fonction est appelée.
````js
const obj = {
  name: "Chen",
  hello() {
    console.log("Hello", this.name);
  }
};

const obj2 = {
  name: "Bao",
  hello2: obj.hello
};

obj2.hello2(); // ? (bao, car le contexte est obj2, même si la fonction vient de ``obj``).
````

#### 2. En react, les fonctions components n'ont pas de ``this``
Regardons comment React appelle les fonctions component: 
````js
MyComponent(props); // Pas des objets contextuelle pour this(undefined).
````
Et non pas:
````js
React.MyComponent() // N'existe pas.
````

Rappel:
* ``MyComponent(props)``: On cuisine notre plat, on a le résultat, mais c'est à nous de décider si on va le servir sur la table(Le DOM).
* ``<MyComponent props={props} />``: On passe la commande au serveur(React). Il cuisine et __sert le plat directement__ sur la table(Le DOM).

#### 3. ``bind``, ``call``, ``apply``
````js
function greet() {
  console.log("Bonjour", this.name);
}

const person = { name: "Chen" };
const bound = greet.bind(person); // crée une nouvelle fonction avec `this` fixé
bound(); // Bonjour Chen
````

### D. Closures (Fermetures)
Une closure est une fonction qui "se souvient" de la portée où elle a été créée, même après l'exécution de cette portée.
````js
function compteur() {
  let count = 0;
  return function () {
    count++;
    console.log("Count:", count);
  };
}

const c = compteur();
c(); // Count: 1
c(); // Count: 2
````
* En React, c'est exactement ce qui se passe dans un ``useState``, ou un ``useRef`` , etc.

Exemple pratique:
````js
function makeTimer() {
  let start = Date.now();
  return function () {
    const now = Date.now();
    console.log("Elapsed:", now - start, "ms");
  };
}

const timer = makeTimer();
setTimeout(timer, 1000); // Elapsed: ~1000ms
setTimeout(timer, 2000); // Elapsed: ~2000ms
````
* Dans cet exemple, la fonction referencé par la variable ``timer`` a toujours accés à la variable ``start``.

