Source1 : https://fr.memberstack.com/blog/react-usereducer
Source2 : https://www.frontendmag.com/tutorials/usereducer-vs-redux/

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




    
