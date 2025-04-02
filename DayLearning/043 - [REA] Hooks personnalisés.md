# I - Différence entre Hook personnalisé et fonction utilitaire
Pour un débutant React, ces deux notions peuvent être ressemblable, mais leur usage, leur comportement, et leur contexte sont bien différents.
## A) Fonction utilitaire
C'est une fontion JavaScript/TypeScript simple. Elle est utilisé pour encapsuler de la logique pure, c'est-à-dire sans accès au système de React(Comme le state, les effets, le contexte, etc.).
* Manipuler de chaînes, de dates, de nombres.
* Fonctions de formatage, validation, calculs.
* Ne dépend pas du cycle de vie React.

## B) Hook personnalisé
Un hook personnalisé est un __mini-component React sans interface graphique__. Son nom commence par ``use_``(Convention), appelle d'autres hooks React, et qui retourne des données(valeur, objet, fonction...), mais pas du JSX. 
* Gérer du state réutilisable.
* Regrouper des effets secondaires.
* Accéder au contexte ou gérer un comportement UI.

# II - Différence entre Hooks et components.
## A) Ce qu'est un hook.
* Un __composant React__ retourne du JSX -> Ce qui s'affiche à l'écran.
* Un __hook React(personnalisé ou natif)__ retourne des données, des fonctions, ou des états, utilisé dans un component.

## B) Exemples simples des hooks personnalisés
### 1. Compteur
````js
import {useState} from 'react'

export const useCounter = (initial = 0) => {
	const  [counter, setCount] = useState(0);

	const increment = () => setCount(c => c + 1);
	const decrement = () => setCount(c => c - 1);
	return {count, increment, decrement};
} 
````
* Si on doit passer increment/decrement à un component enfant, alors englobent ils dans ``useCallback``, sinon ils vont être recréés à chaque rerender.
* Si vous posez la question: Pourquoi useState n'est pas réinitialisé? Regardez la dernière chapitre.

### 2. Largeur d'écran
````js
import {useState, useEffect} from "react";

export const useWindowWidth = () => {
	const [width, setWidth] = useState(window.innerWidth);
	useEffect(() => {
		const handleResize = () => setWidth(window.innerWidth);
		window.addEventListener("resize", handleResize);
		return () => window.removeEventListener("resize", handleResize);
	})

	return width;
}
````

## C) Versions simplifiées des hooks Reacts.
### 1. ``useState``
### 2. ``useEffect``
### 3. ``useRef``
### 4. ``useContext``
### 5. ``useMemo``
### 6. ``useCallback``
### 7. ``useReducer``
