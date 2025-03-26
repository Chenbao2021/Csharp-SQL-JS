# ``useContext`` + ``useReducer``: Parfaite combo.
### A. Contexte
|Hook| Utilité principale|
|---|---|
|``useContext``| Partager des données globales dans l'arbre de composants(sans prop drilling)|
|``useReducer``| Gérer un état complexe avec des actions (comme Redux, mais natif)|

Pourquoi les combiner?
* ``useReducer`` = Pour gérer l'état, et logique de changement.
* ``useContext`` = Pour rendre cet état(et le dispatch) accessible partout.

Résultat: Tu peux modifier et lire l'état __de n'importe où dans ton app__, comme un mini store Redux.

### B. createContext
``createContexte()`` est appelé à l'extérieur du component(Souvent dans une ficheir à part, puis on l'importe), et il fonctionne comme un "tunnel de communication" qui travers différents composants sans respecter le principe de "props drilling":
* Création: ``export const MyContext = createContext()``
* "En haut", Root ( App, Main, etc.): via ``<MonContexte.Provider value={...}> </MonContexte.provider>``
* "En bas", Components(Les childrens): via ``useContexte(MonContexte)``

### C. Pattern classique
1 - Dans un fichier destinée pour les contextes:
````js
export const StateContext = createContext();
export const DispatchContext = createContext():

export const useStore = useCallback(() => {
	const context = useContext(stateContext);
	if(!context) throw new Error("useStore must be used within Provider!");
	return context;
}, [])
export const useDispatch = useCallback(() => {
	const context = useContext(DispatchContext);
	if(!context) throw new Error("useStore must be used within Provider!");
	return context;
}, [])
````
* On crée deux context, et on va voir pourquoi.

2 - Puis dans le fichier Root, on va englober les enfants dans un __Provider__:
````js
function App() {
	const [state, dispatch] = useReducer(reducer, initialState);

	return (
		<StateContext.Provider value={state}>
			<DispatchContext.Provider value={dispatch}>
				<MyComponent />
			</DispatchCOntext.Provider>
		</StateContext.Provider>
	)
}
````
* Un Provider ne peut prendre qu'une seul valeur, mais cette valeur peut être un objet/tableau, si c'est un tableau, pensez de le définir avec ``useMemo``.
	````js
	const value = useMemo(() => { dispatch: dispatch, store: store }, [store]);
	````
	* Ici, ``store`` est un objet stable venant du ``useReducer``. on peut le passer comme value sans ``useMemo``.
* On peut empiler autant des providers qu'on a besoin, et chaque provider gère une donnée différente. Et ça n'a pas vraiment d'impact sur la performance.
* L'ordre d'empilement entre les providers n'a pas d'importance.
3 - Enfin, on va l'utiliser dans les components enfants.
````js
function MyComponent() {
	const state = useStore();
	const dispatch = useDispatch();

	const increment = useCallback(() => {
		dispatch({type: 'increment'})
	}, [])
	return (
		<button onClick={increment}>
			{state.count}
		</button>
	)
}
````

4 - Pourquoi deux contextes(``StateContext`` + ``DispatchContext``)?
* Un ``Context.Provider`` ne déclenche un re-render de ses enfants que si la ``value`` change.
	Utiliser ``useMemo`` si besoin.
* Les composants qui n'utilisent que ``dispatch`` ne seront pas re-rendus quand le ``state`` change.

### D. Utiliser ``useReducer`` sans ``useContext``
Sans ``Context``, on doit passer le ``state`` et le ``dispatch`` manuellement via les props(prop drilling). Si on a que 1 ou 2 niveau de composants, c'est pratique mais:
* Si ``ChildrenComponent`` est profondément imbriqué dans 4 ou 5 niveaux de composants, tu dois propager ``state`` et ``dispatch`` à chaque niveau.
* On alourdit les composants intermédiaires et rendre la maintenance plus difficile.



