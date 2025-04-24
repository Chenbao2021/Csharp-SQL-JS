# I - Les objets(``{}``)
````js
const user = {
  name: "Chen",
  age: 27
};

console.log(user.name);     // "Chen"
console.log(user["age"]);   // 27
````

Erreur fréqunte: Utiliser un ``.key`` alors que la clé est une variable.
````js
const key = "name";
console.log(user.key);       // ❌ undefined
console.log(user[key]);      // ✅ "Chen"
````

On peut aussi créer notre objet avec ``new Object()``, mais c'est presque jamais utilisé aujoutd'hui, sauf dans du code très ancien ou bas niveau. En effet, le ``new`` est surtout utilisé avec une fonction constructeur ou une classe.

# II - Les tableaux(``[]``)
````js
const fruits = ["apple", "banana", "orange"];
console.log(fruits[1]); // "banana"
````
* Erreur classique: Accéder à un index hors du tableau -> ``undefined``.
* ``arr.push(x)`` est souvent considéré comme erreur dans beaucoup des contextes, notamment dans React. Car il modifie l'original (Mutation de référence). Donc si d'autres parties de ton code utilisent ce tableau, elles vont voir la modification même si tu ne voulais pas.
	* React ne voit pas les changements si la référence ne change pas.
		````js
		items.push(4);
		setItem(items); // React ne détecte pas de changement !
		
		setItem([...items, 4]); // Façon correcte!
		````



# III - Destructing
### A. Objet
````js
const user = {name: "Bao", age: 27}
const {name, age} = user;

console.log(name); // "Bao"
````
* Ce mécanisme est très utilisé dans React.

### B. Tableau
````js
const [first, second] = [10, 20, 30];
console.log(first);
````

# IV - Spread ``...`` - Pour copier/ combiner.
### A. Objet
````js
const user = { name: "Chen" };
const updated = { ...user, age:28 };
````
* Mais il ne fait pas de copie profonde.

### B. Tableau
````js
const arr1 = [1, 2];
const arr2 = [...arr1, 3, 4];
````

# V - Rest ``...`` - pour extraire le reste.
### A. Objet
````js
const {name, ...rest} = {name: "chen", age: 27, job: "dev", gender: "Male"}
console.log(rest); // {age: 27, job: "dev", gender: "Male"}
````

Si ``name`` n'existe pas dans l'objet, alors il vaut ``undefined``. Mais on peut donner une valeur par défaut à ``name``:
````js
const {name = "New person", ...others} = { ... }
````

### B. Tableau
````js
const [first, ...others] = [1, 2, 3]
console.log(others);
````
* En destruction, l'opérateur ``...`` doit être le dernier élément dans la liste.

