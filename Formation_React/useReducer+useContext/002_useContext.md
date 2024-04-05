Source1: https://fr.react.dev/reference/react/useContext
Source2: https://medium.com/@msgold/using-usecontext-in-react-a-comprehensive-guide-8a9f5271f7a8
Source3: https://grafikart.fr/tutoriels/react-contextes-1335

# Introduction
__useContext__ est un Hook React qui vous permet de lire et de vous abonner à un contexte depuis votre composant.
Les étapes sont:
1. Créer un context contextName, avec son valeur par défaut
2. Créer une couche Provider, utilise useContext(contextName) pour pointer une context crée. déclare les satates ou réducer, puis utiliser .Provider.
3. Dans les components enfants, quand on appelle "useContext(contextName)":
    1. il récupère le contexte spécifié en argument depuis l'arborescence des composants React. Il recherche le Provider le plus proche englobant ce composant qui fournit ce contexte 
    2. Une fois que le Provider du contexte est trouvé, 'useContext' retourne la valeur actuelle du contexte fournie par ce Provider. Cette valeur est celle qui a été passée au Provider via sa prop 'value'.
    3. En résumé, quand on appelle useContexte dans le component enfant, il va chercher les valeurs dans __value__ de la ou on a utilisé :
        ````
        <MyContext.Provider value="Valeur partagée">
        ````


````
// Create a context
const someContext = React.createContext<{ gridData: IState, dispatch: React.Dispatch<Action> } | null>(null)
// Call useContext Hook
const GridDataContext = React.useContext(someContext);
// Use Provider.
const [gridData, dispatch] = useReducer(gridReducer, undefined, getInitialState);
return (
	<GridDataContext.Provider value={{ gridData, dispatch }}>
		{children}
	</GridDataContext.Provider>
)
````
- someContext = Le contexte qu'on a préalablement créé avec __createContext__. Le contexte lui-même ne contient pas d'information, il représente seulement le type d'information qu'on peut fournir ou lire depuis des composants.

# Why useContext is Important
- Avoiding Prop Drilling
- Sharing Data Across Components

# Using the useContext Hook
- Accessing Context Values
The __useContext__ hook allow us to access the value of a context within a functional component.
By passing the context object to the useContext hook, we can retrieve the current value of the context.
    ````
    const GridDataContext = React.createContext<{ gridData: IState, dispatch: React.Dispatch<Action> } | null>(null)
    export function useGridDataContext() {
    	return React.useContext(GridDataContext);
    }
    ````
- Updating Context Value 
We use the context provider. The provider allows us to define the value that should be made available to all components that consume the context.
````
const [gridData, dispatch] = useReducer(gridReducer, undefined, getInitialState);
return (
	<GridDataContext.Provider value={{ gridData, dispatch }}>
		{children}
	</GridDataContext.Provider>
````

# Pratical example
See the source2 for details
- Un contexte peut être créé via la méthode __createContexte()__ qui prendra en paramètre une valeur de base qui sera utilisé si aucun contexte n'est fournit plus tard.
    ````
    const TodoContext = createContext({todos: []})
    ````
- Ensuite, n'importe quel composant peut récupérer la valeur dans le contexte à l'aide du hook __useContext()__
    ````
    function Footer () {
        const {todos} = useContext(TodoContext)
        return <footer>
            Il y a {todos.length} tâches
        </footer>
    }
    ````
- Il est ensuite possible de définir un contexte pour toute une partie de l'application à l'aide d'un provider
    ````
    <TodoContext.Provider value={{todos: ['Tâche 1', 'Tâche 2']}}>
        <TaskList/>
        <Footer/>
    </TodoContext.Provider>
    ````
- Tous les composant enfant d'un provider, lorsqu'ils utilisent __useContext()__, récupèrerons alors la valeur fournie par le provider. 


# Frequently Asked Questions(FAQs)
- 

