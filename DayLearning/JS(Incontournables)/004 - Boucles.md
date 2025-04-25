# I - Boucles en JavaScript
## A. ``for`` classique -> Celui qui peut tout faire.
````js
for (let i = 0; i < 5; i++) {
	console.log(i);
}
````
* Contrôle total sur l'index.
* On peut utiliser ``break``, ``cotinue``, ``return``.
* Compaible avec ``await`` dans une boucle ``async``.

## B. ``for...of``(Sur tableaux, chaînes, objets itérables) -> CTrès utile
````js
const fruits = ["🍎", "🍌", "🍊"];
for (const fruit of fruits) {
	console.log(fruit);
}
````
* Simple, lisible, parfait pour les tableaux.
* Fonctionne avec ``async/ await``.
	````js
	for(const id of ids) {
		const user = await getUser(id);
	}
	````

## C. ``forEach``/ ``map`` (sur les tableaux)
````js
[1, 2, 3].forEach(x => {
	console.log(x);
})
````
* __Attention__: ``forEach`` ignore ``await``! On ne peut pas ``await`` une ``forEach``.

## D. ``for...in`` -> L'ancienne méthode.
````js
for(let key in obj) {
	console.log(key);
}
````
* Ne l'utilise jamais sur des tableaux.
* Il itère sur __les clés__, pas les valeurs, et peut inclure des propriétés héritées!
	Cela veut dire que s'il y a des propriétés dans le prototype de l'objet, elles peuvent aussi être parcourues, même si tu ne les vois pas dans ton objet directement.

### 1.Rappel sur prototype.
````js
const parent = { inherited: "👴" };
const child = Object.create(parent);
child.own = "🧒";

for (const key in child) {
  console.log(key); // "own", puis "inherited" qui vient de parent❗️
}
````

En effet, ``Object.create(...)`` crée un nouvel objet qui hérite d'un autre objet comme prototype.
* ``child`` a pour prototype l'objet ``parent``.
* Donc si ``child`` n'a pas une proprieté, JavaScript va la chercher dans ``parent``.
	``child.__proto__`` -> Qui est ``parent``.

Et ``for...in`` inclut les propriétés héritées -> Pas seulement celles que tu as définies toi-même.
Mais on a une solution sécurisée: ``Object.keys(obj)``
````js
for(const key of Object.keys(child)) {
	console.log(child[key])
}
````


## E. Pourquoi ``for...in`` existe alors qu'il est risqué?
### 1. ``for...in`` est "dangereuse".
Pas parce qu'elle est mauvaise en soi, mais parce qu'elle:
1. Inclut les propriétés héritées(prototype).
2. Peut parcourir des choses inattendues.
3. Est facile à mal utiliser, surtout avec des tableaux(où il ne faut jamais l'utiliser).
	````js
	Array.prototype.foo = "test";

	const arr = [1, 2, 3];

	for (const i in arr) {
		console.log(arr[i]); // 1, 2, 3, puis "test" 😱
	}
	````

### 2. Conçue pour des besoins précis.
Elle a été conçue pour un __besoin précis__, c'est un outil __ancien__ mais elle est toujours utile dans certains cas:
````js
const user = {
  name: "Chen",
  age: 27,
  role: "admin"
};

for (const key in user) {
  console.log(`${key}: ${user[key]}`);
}
````
* On ne sait pas à l'avance combien de clés il y a, ni comment elles s'appellent.

### 3. Mais aujourd'hui on a des alternatives plus sûres
|Tâche|Solution moderne|
|--|--|
|Parcourir un tableau|``for...of`` ou ``map``|
|Obtenir un tableau des clés|``Object.keys(obj)``|
|Obtenir les clés + valeurs|``Object.entries(obj)``|
|Accès direct à valeur|``obj[key]``|

## F. Différence entre ``for...of`` et ``map``
||``for...of``|``map()``|
|--|--|--|
|Type|Boucle|Fonction d'ordre supérieur(méthode)|
|Objectif|Exécuter une action|Créer un nouveau tableau transformé|
|Retour|Rien(``undefined``)|Nouveau tableau|
|Avait support|Oui|Non(``await`` dans callback ne bloque pas)|
|Break possible|Oui|Non|

### 1. Boucle ``async``
Un boucle ``async`` est simplement une boucle classique dans une fonction ``async``, où tu peux utiliser ``await`` pour __attendre les promesses une par une__ -> C'est super important pour exécuter des appels __séquentiels__:
````js
async function fetchUsers(ids) {
  for (const id of ids) {
    const user = await fetchUserById(id); // attend chaque appel
    console.log(user);
  }
}
````

### 2. Erreur fréquente: Utiliser ``map`` avec ``await``
````js
const results = ids.map(async id => {
  const user = await fetchUserById(id);
  return user;
});
console.log(results); // ❌ un tableau de Promises, pas les résultats
````
``map()`` est __parallèle__, donc il ne faut pas l'utiliser pour exécuter du code __séquentiel__. Correction:
````js
const results = await Promise.all(
  ids.map(id => fetchUserById(id))
);
````

### 3. Résumé
|Tu veux|Utilise|
|--|--|
|Séquentielle: Attendre chaque ``await`` une par une|``for..of`` dans une ``async function``|
|Parallèle: Exécuter tous les appels en parallèle|``await Promise.all(map(...))``|

