Source1 : https://fr.memberstack.com/blog/react-usereducer
Source2 : https://www.frontendmag.com/tutorials/usereducer-vs-redux/

# Introduction
__useReducer__ est un hook de gestion d'état de React qui est souvent utilisé comme alternative à __useState__ lorsque l'état de votre composant devient complexe et nécessite une logique de mise à jour avancée.
On utilise useReducer lorsque:
- L'état de votre composant est complexe: Si votre état contient plusieurs sous-ensembles de données ou sis a logique de mise à jour est compliquée, __useReducer__ peut rendre le code plus clair en définissant des actions spécifiques pour mettre à jour l'état.
- La mise à jour de l'état dépend de l'état précédent ou d'actions asynchrones: 'useReducer' permet de définir une fonction de réduction qui prend en compte l'état actuel et une action pour détérminer le nouvel état, ce qui est utile lorsque la mise à jour de l'état dépend de son état précédent ou d'autres facteurs complexe.
    C'est réalisable avec useState, mais ça peut devenir très compliqué facilement. Et avec beaucoup plus de re-render
- Vous avez besoin de passer des fonctions de mise à jour de l'état comme des callbacks:
    Parfois, vous pouvez avoir besoin de passer des fonctions de mise à jour de l'état en tant que callbacks pour d'autre fonctions. 'useReducer' facilite cela en vous permettant de passer la fonction de dispatch elle-même, ce qui est plus propre que de passer plusieurs fonctions de mise à jour d'état.

# I - How does useReducer work ?
React useReducer works similiar to JavaScript's Array.prototype.reducer()
- First, we need to create a __reducer function__, that accepts a state and an action as parameters.
    In the exemple we use Immer to simplify the code.
    Exemple: 
    ````
    export const gridReducer = produce((draft: IState, action: Action) => {
    	switch (action.type) {
    		case ActionEnum.setSelectedData:
    			draft.selectedData = action.value
    			break;
    	}
    	return draft;
    });
    ````
- The useReducer hooks
    - Take two(or three) arguments, a reducer function and a initial state:
        -  The reducer function is responsible for updating the state based on the actions dispatched by the application.
        -  When an action is dispatched, the reducer function receives the current state and the action as arguments and returns the updated state.
    - __Returns an array__ containing(The reason why not [] but {} when we do destruction):
        - The current state returned by the reducer function 
        - A dispatch for passing values to the action parameter.

- We can provide help function in the Provider, and using them by import its in the value attribut, like:
    ````
    ...
    const addItem = (item) => {
    dispatch({ type: 'ADD_ITEM', payload: item })
    }
    <CartContext.Provider value={{ state, addItem, removeItem, clearCart }}>
    ````

# II - The reducer function
- Accepts state and action as parameters and returns an updated state
    - state : Les données actuelles
    - action : Les données reçoivent depuis dispatch( type action, value to update)
    Exemple : 
        ````
        const reducer = (state, action) => {
          // logic to update state with value from action
          return updatedState
        }
        ````
    action can be a single value or an object with a label(type) and some data to update the state(payload).

# Exemples
Check the source

# III - When not to use the useReducer Hook
- Managing simple state
It would be much quicker to set up useState for cases where we need to handle a very simple state.

- Central state management
In a large application with many components depending on the same state, it would be better to use a third party state management like Redux.




    
