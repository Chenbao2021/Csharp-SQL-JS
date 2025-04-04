# I - Différence entre Hook personnalisé et fonction utilitaire
Ces deux notions peuvent être ressemblable, mais leur usage, leur comportement, et leur contexte sont bien différents.
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

Définition officielle : Une fonction JavaScript dont le nom commence par ``use``, et qui appelle un ou plusieurs autres hooks (hookd natifs et personnalisés).

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

### 3.Utiliser le contexte
Il est très recommandé de créer un hook personnalisé autour de ``useContext`` au lieu de l'appeler partout.
````js
// AuthContext.tsx
export const AuthContext = createContext<AuthContextType | undefined>(defaultValue | undefined);

// useAuth.ts
export const useAuth = () => {
	const context = useContxt(AuthContext); // Retourne l'attribut "value".
	if(!context) {
		throw new Error("useAuth must be used within an AuthProvider");
	}
	return context;
}; 

// AuthProvider.tsx
... 
return (
	<AuthContext.Provider value={...AuthContextType}>
		<App />
	</AuthContext.Provider>;
)
````
* Le ``defaultValue`` sera utilisé si aucun ``<Provider>`` n'est trouvé dans l'arbre React.(Comme "light" pour un thème).
* Un même contexte peut utiliser plusieurs fois, et useContext cherche celui qui est plus proche. (Pour ne pas dupliquer le code quand ce sont des même choses).


# III - Comprendre ce qu'est un Hook.
## A) Pourquoi les valeurs de ``useState`` n'est pas réinitialisé après re-rendu.
Chaque fois que le composant parent est re-rendu, la fonction ``useCounter()`` (Donc ton hook personnalisé) est ré-exécutée, ainsi que les fonctions à l'intérieur.

Mais attention, ça ne veut pas dire que le __state__ est perdu ou réinitialisé! Car React a un système interne de mémoire des hooks basé sur l'ordre d'appel des hooks.
(Un currentIndex qui réinitialisé à 0 à chaque re-render, et chaque appel useState l'incrémente, et puis mettre la valeur dans la case suivante du tableau).

Par exemple: 
````js
// App.tsx
function App() {
	const [count, setCount] = useState(0);
	const [name, setName] = useState("chen");
}

// React voit ça comme:
hookStates = [0, "chen"]
````
* À chaque re-render:
	* React refait le même appel dans le même ordre.
	* Donc on est certain que index ``0`` est toujours ``count``, ``1`` = ``name``.
	* Si tu changes l'ordre des hooks, React ne comprend plus. C'est pour ça qu'il ne faut jamais mettre un hook dans un bloc conditionnel.



## B) Versions simplifiées des hooks Reacts.
__Remarque__: Les hooks de base n'ont pas besoin d'appeler des hooks de base.
* Les hooks natifs sont comme des briques de LEGO de base.
* Les hooks personnalisés sont construits par des combinaisons des hooks natifs.

### 1. ``useState``
````ts
function useState<T>(initialValue: T): [T, (newValue:T) => void] {
	let value = initialValue;

	const setValue = (newValue: T) => {
		value = newValue;
		// Puis React déclenche un re-render avec des codes natifs.
	}

	return [value, setValue]
}
````
### 2. ``useRef``
````ts
function useRef<T>(initialValue: T): {current: T} {
	return {current: initialValue};
}
````
### 3. ``useContext``
````ts
function useContext<T>(context: React.Context<T>): T {
	// React trouve le provider le plus proche et retourne sa valeur.
	return context._currentValue; // En réalité beaucoup plus complexe.
}
````
### 4. ``useMemo``
````ts
function useMemo<T>(factory: () => T, deps: any[]): T {
	// React stocke le résultat et les deps.
	// Si les deps ne changent pas -> Retourner l'ancienne valeur.
	return factory();
}
````
### 5. ``useReducer``
````ts
function useReducer<T, A>(reducer: (state: T, action: A) => T, initialState: T): [T, (actuib: A) => void] {
	let state = initialState;

	function dispatch(action: A) {
		state = reducer(state, action);
	}

	return [state, dispatch];
}
````

# IV - Autres exemples
### A. ``useFetch``
Souvent on utilise des fetchs pour récupérer des données depuis le back, puis de la mettre dans un state ou plus. Ces opérations peuvent être très répétitives, et donc il est avantageux de le faire dans un hook personnalisé.(Ce n'est rien d'autre qu'une fonction, mais avec des hooks de base)

* Sans ``useFetch``:
	````ts
	const Users = () => {
		const [user, setUsers] = useState<User[]>([]);
		const [loading, setLoading] = useState(true);

		useEffect(() => {
			fetch("/api/users")
			.then((res) => res.json())
			.then((data) => {
				setUsers(data);
				setLoading(false);
			})
		}, []);
	
		return loading ? <p>Chargement...</p> : <UserList users={users} />;
	}
	````
* Avec ``useFetch``:
	````ts
	const user = () => {
		const {data: users, loading, error} = useFetch<User[]>('/api/users');

		if(loading) return <p>Chargement...</p>
		if(error) return <p>Erreur: {error.message}</p>

		return <UserList users={users}> />;
	}
	````
	Avec ton ``useFetch`` comme:
	````js
	const useFetch<T>(url: string) {
		const [data, setData] = useState<T|null>(null);
		const [loading, setLoading] = useState(true);
		const [error, setError] = useState<Error | null>(null);

		useEffetc(() => {
			setLoading(true);
			fetch(url)
			.then((res) => res.json())
			.then((json) => setData(json))
			.catch((err) => setError(err))
			.finally(() => setLoading(false))
		}, [url]);
		
		 return   {data, loading, error}
	}
	````
### B. ``useWindowSize``
Avoir un tell hook personnalisé permet de s'adapter l'UI en fonction de la taille de la fenêtre.
````js
import { useState, useEffect } from 'react';

function useWindowSize() {
	const [windowSize, setWindowSize] = useState({ width: window.innerWidth, height: window.innerHeight });

	useEffect(() => {
		function handleResize() {
			setWindowSize({width: window.innerWidth, height: window.innerHeight});
		}

		window.addEventListener('resize', handleResize);
	}, []);

	return windowSize;
}
````
Utilisation dans un component:
````js
function ResponsiveComponent() {
	const {width, height} = useWindowSize();

	return (
		<div>
			<p>Larger: {width}px</p>
			<p>Hauteur: {height} </p>
		</div>
	)
}
````

### C. ``useLocalStorage``
Ce hook permet de sauvegarder et restaurer l'état du composant dans le ``localStorage`` du navigateur.
````js
import {useState, useEffect} from 'react';

function useLocalStorage<T>(key: string, initialValue: T) {
	const [storedValue, setStoredValue] = useState<T>(() => {
		try {
			const item = window.localStorage.getItem(key);
			return item ? JSON.parse(item) as T: initialvaue;
		} catch (error) {
			return initialValue;
		}
	});

	const setValue = (value: T | ((val: T) => T)) => {
		try {
			const valueToStorage = value instanceof Function ? value(storedValue) : value;
			setStoredValue(valueToStore);
			window.localStorage.setItem(key, JSON.stringify(valueToStore));
		} catch(error) {
			console.error(error);
		}
	}

	return [storedValue, setValue] as const;
}

export default useLocalStorage;
````
Et son utilisation dans un composant:
````js
function Preferences() {
	const [theme, setTheme] = useLocalStorage('theme', 'light');

	return (
		<div>
			<p> Thème actuel: {theme}</p>
			<button onClick={() => setTheme(prev => prev === 'light' ? 'dark' : 'light')}>
				changer de thème.
			</button>
		</div>
	)
}
````
