# React
***
## I - useRef pour vérifie si useCallback est appelé
On peut stocker une version précédente de cette fonction en utilisant un ``useRef``. Ensuite, à chaque rendu, on compare la référence de la fonction actuelle avec celle stockée précédemment.
Exemple:
````
// Utilise useCallback pour créer une fonction stable
const updateQuestion = useCallback(() => {
console.log('Mise à jour de la question');
}, []);
````
````
// Utilise useRef pour stocker la référence de la fonction précédente
const previousFunctionRef = useRef(updateQuestion);
````
````
useEffect(() => {
    if (previousFunctionRef.current !== updateQuestion) {
      console.log('La fonction a été recréée');
    } else {
      console.log('La fonction est stable');
    }
    
    // Met à jour la référence avec la version actuelle de la fonction
    previousFunctionRef.current = updateQuestion;
}, [updateQuestion]);
````

## II - Fonctions du composant parent.
En React, il est tout à fait acceptable de passer une fonction du composant parent vers le composant enfant pour modifier des données ou interagir avec l'état du composant parent.
* #### Avantages
    * **Gestion centralisée**: Cela permet d'éviter la duplication des données et facilite le suivi des changements.
    * **Facilité de communication parent-enfant**: Permet au composant enfant de déclencher des mises à jour sans avoir à gérer l'état lui-même.
    * **Contrôle accru du flux de donées**: Garantit que les composants enfants n'ont pas de contrôle direct sur l'état du parent. Ils peuvent uniquement envoyer des événements au parent qui décide ensuite comment traiter ces événements.
* #### Précautions et bonnes pratiques
    * **Éviter les fonctions redéfinies à chaque rendu**: Utiliser ``useCallback``.
    * **Respecter les principes de "prop drilling"**: Si tu passes une fonction à travers plusieurs niveau de composant, cela peut devenir fastidieux et rendre le code plus difficile à maintenir.=> Utiliser un ``context`` ou un ``state manager`` comme Redux.
    * **Utiliser des callbacks spécifiques aux besoins de l'enfant"**: Éviter des fonctions très génériques.

## III - Différence entre ?? et ||
La différence réside principalement en comment ils évaluent les valeurs dites ``falsy``.
1. Opérateur ``||`` (OR logique):
    * Ses valeurs ``falsy`` incluent: ``null``, ``undefined``, ``0``, ``''``, ``NaN`` et ``false``.
2. Opérateur ``??`` (Nullish coalescing):
    * Ses valeurs ``falsy`` incluent: ``null`` et ``undefined``.
    * 
