# I. ``Any`` et ``Unknown``.
En typescript avec React, il vaut mieux utiliser ``unknown`` plutôt que ``any`` si tu ne connais pas encore le type.

### Pourquoi utiliser ``unkown`` plutôt que ``any`` ?
* ``any`` désactive complètement la vérification de type. Tu peux faire n'importe quoi avec une variable de type ``any``, même des __choses dangereuses__.
* ``unknown`` t'oblige à vérifier le type avant de l'utiliser. C'est une manière plus "safe" de dire: "Je ne sais pas ce que c'est pour le moment."
	Exemple:
	````js
	function handleInput(value: unknown) {
		// Typescrpt ne te laissera pas faire ça.
		// console.log(value.toUpperCase());

		if(typeof value === 'string') {
			console.log(value.toUpperCase()); 
		}
	}
	````

### Quan utiliser ``any`` alors?
* Pour des prototypages rapides(tests, codes temporaires).
* Quand tu veux volontairement désactiver la vérification de type. (Ex: Pour intégrer un vieux code JS, ou une librairie non typée).
* En dernier recours, mais il faut l'assumer et le documenter.

# II. Vérification de type avec ``unknown``.
### A. Vérification simples avec ``typeof``, ``Array.isArray``, ``instanceOf``, etc.
QUand on veut juste savoir si c'est un ``string``, ``number``, ``boolean``, un tableau, ou un objet:
````js
function handle(value: unknown) {
	if(typeof value === "string) {
		...
	} else if(Array.isArray(value)) {
		// tableau.
	} ...
}
````

### B. Vérification personnalisées (Avec une ``interface`` ou un ``type``).
Quand tu attends à une structure d'objet bien précise, tu dois faire un check plus poussé(Souvent à la main).
````ts
interface User {
	id: number;
	name: string;
}

function isUser(obj: unkonwn): obj is User {
	return (
		typeof obj === 'object'
		&& obj !== null
		&& 'id' in obj && 'name' in obj
		&& typeof (obj as any).id === 'number' && typeof(obj as any).name === 'string'
	)
}

function handle(value: unknown) {
	if(isUser(value)) { .... }
}
````
* ``obj is User`` permet de dire à TypeScript: Si cette fonction retourne ``true``, alors tu peux considérer que l'objet est de type ``User`` à l'intérieur du ``if``.

### C. Cas où on a un objet énorme(100+ attribut retourné par API)
Quand on a un objet JSON massif retourné par une API avec 100+ attributs, il faut trouver un bon compromis entre sécurité, lisibilité et maintenabilité.

__Pour des cas complexes, on peut utiliser la librairie ``zod``, plus sûr, évolutif, maintenable et parfait pour les gros objets dynamiques.__

Sinon, on peut utiliser ``unknown`` + validation minimale + cast. C'est moins sûr mais acceptable si t'as pas besoin de tout valider. (Tu peux ne valider que ceux dont t'as besoin).
````ts
const data: unknown = await fetch("/api/endpoint").then(res => res.json());

// On a juste besoin d'utiliser l'attribut "name".
if(typeof data === "object" && data !== null && 'name' in data) {
	const user = data as { name: string };
	console.log(user.name);
}
````

### D. Bon reflexe avec ``unknown``
Si tu veux garder le typage strict mais __flexible__, sans tomber dans le piège du ``any``.
On peut toujours:
* Affiner plus tard avec des types personnalisés.
* Ajouter des vérifications spécifiques à ton besoin.
* Ou passer à ``zod``/ ``ìo-ts`` quand tu auras besoin de valider des objets plus complexes ou critiques.






