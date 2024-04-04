Source1 : https://fr.memberstack.com/blog/react-usereducer
Source2 : https://www.frontendmag.com/tutorials/usereducer-vs-redux/

# I - How does useReducer work ?
React useReducer works similiar to JavaScript's Array.prototype.reducer()
- It accepts a reducer function and an initialState as parameters.
- Its reducer function accepts a state and an action as parameters.
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
- The useReducer returns an array containing the current state returned by the reducer function and a dispatch for passing values to the action parameter.(The reason why we can't put an HTML element inside, because infinite iterate cause error with reducer mecanism)

- The useReducer hooks take two arguments (a reducer function and a initial state):
    -  The reducer function is responsible for updating the state based on the actions dispatched by the application.
    -  When an action is dispatched, the reducer function receives the current state and the action as arguments and returns the updated state.
- We can provide help function in the Provider, and using them by import its in the value attribut, like:
    ````
    <CartContext.Provider value={{ state, addItem, removeItem, clearCart }}>
    ````
    Here , addItem is a function like:
    ````
    const addItem = (item) => {
    dispatch({ type: 'ADD_ITEM', payload: item })
    }
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




    
