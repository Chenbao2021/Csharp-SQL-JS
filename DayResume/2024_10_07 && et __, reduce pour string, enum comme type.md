# React
***
## I - && et ??
L'opérateur ``&&``(ET logique)
* Si la première expression est "falsy"(``false``, ``null``, ``undefined``, ``0``, ``NaN``, ou une chaîne vide ``""``), elle est retournée immédiatement.
* Si la première expression est "truthy", l'opérateur ``&&`` retourne la valeur de la deuxième expression.

L'opérateur ``??`` (Coalescence des nuls, principalement utilisé pour fournir une valeur par défaut) 
* Si la première valeur n'est pas ``null`` ou ``undefined``, elle est retournée.
* Si la première valeur est ``null`` ou ``undefined``, l'opérateur retourne la deuxième valeur.

## II - enum comme type
En Typescript, un **enum** est un type spécial qui permet de définir un ensemble de valeurs nommées, généralement utilisées pour représenter un ensemble fixe de constants.
````JS
enum Status {
  Active = "ACTIVE",
  Inactive = "INACTIVE",
  Suspended = "SUSPENDED"
}
let userStatus: Status;
````

## III - ``reduce`` pour fabriquer une chaîne de caractère.
``reduce`` est une méthode puissante qui permet de transformer un tableau en une seule valeur, et dans ce cas, une **chaîne de caractère**.
````
const items = [
    { name: 'Apple' },
    { name: 'Banana' },
    { name: 'Cherry' },
];
const result = items.reduce((accumulator, current) => {
    return accumulator + current.name + ', ';
}, '');

// Supprimer la virgule et l'espace à la fin (facultatif)
const finalResult = result.slice(0, -2);

console.log(finalResult); // "Apple, Banana, Cherry"
````
Mais on peut aussi utiliser ``join`` pour le faire:
``const result = items.map(item => item.name).join(', ');``