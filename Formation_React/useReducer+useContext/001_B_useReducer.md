Source: https://react.dev/reference/react/useReducer

# useReducer
__useReducer__ is a React Hook that lets you add a _reducer_ to your component.
````
const [state, dispatch] = useReducer(reducer, initialArg, init?)
````
Parameters
- __reducer__ : The reducer function that specifies how the state gets updated. It must be pure, should take the state and action as argument, and should return the next state.
- __initialArg__ : The value from which the initial state is calculated.
- optional __init__: The initializer function that should return the initial state. That means the initial state is set to the result of calling __init(initialArg)__ .

Returns
- The __current state__: During the first render, it's set to __init(initialArg) or __initialArg__ (if there's no __init__).
- The __dispatch function__ : That let you update the state to a different value and trigger a re-render.

# __dispatch__ function
This function lets you update the state to a different value and trigger a re-render.
You need to pass the action as the only argument to the __dispatch__ function.
- The dispatch function only updates the state variable __for the next render__.
- useReducer is very similar to useState, but it lets you move the state update logic from event handlers into a single function outside of your component.

Action can have any shape, but at least a __type__ property.

# Avoiding recreating the initial state
React saves the initial state once and ignores it on the next renders.
use __init__ parameter if you want create a initial state:
````
function TodoList({ username }) {
  const [state, dispatch] = useReducer(reducer, username, createInitialState);
  // ...
````

# Troubleshootings
### I - I’ve dispatched an action, but logging gives me the old state value 
=> Calling the __dispatch__ does not change state in the running code.
````
function handleClick() {
  console.log(state.age);  // 42

  dispatch({ type: 'incremented_age' }); // Request a re-render with 43
  console.log(state.age);  // Still 42!

  setTimeout(() => {
    console.log(state.age); // Also 42!
  }, 5000);
}
````
If you need to guess the next state value, you can calculate it manually by calling the reducer function(passing state and action).

### II - My entire reducer state becomes undefined after dispatching 
If your state unexpectedly becomes __undefined__, you are likely:
1. forgetting to __return__ state in one of the cases.
2. your action type doesn't match any of the __case__ statements. To find why, throw an error outside the __switch__:
    ````
    function reducer(state, action) {
      switch (action.type) {
        case 'incremented_age': {
          // ...
        }
        case 'edited_name': {
          // ...
        }
      }
      throw Error('Unknown action: ' + action.type);
    }
    ````

### III - My reducer or initializer function runs twice 
In Strict Mode, React will call your reducer and initializer functions __twice__. This shouldn’t break your code.
This development-only behavior helps you keep components pure.

Details: https://react.dev/reference/react/useReducer#my-reducer-or-initializer-function-runs-twice
