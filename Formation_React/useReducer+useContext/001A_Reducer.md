Source: https://react.dev/learn/extracting-state-logic-into-a-reducer
# Extracting State Logic into a Reducer
- Consolidate all the state update logic __outside__
- A __reducer__ is a single function.

## I - Consolidate state logic with a reducer
We can migrate from useState to useReducer in three steps:
1. __Move__ from setting state to dispatching actions.
2. __Write__ a reducer function
3. __Use__ the reducer from your component.

#### Step 1 : Move from setting state to dispatching actions
- Setting state: Telling React "what to do".
- Dispatching action: Specify "what the user just did" by dispatching "actions" from your event handlers.

    ````
    function handleAddTask(text) {
      dispatch({
        type: 'added',
        id: nextId++,
        text: text,
      });
    }
    ````
    - The object you pass to __dispatch__ is called an "action".
    - Generally the action should contain the minimal information about what happened.
    - It is common to give it a string __type__ that describes what happened, and pass any additional information in other fields.

#### Step2: Write a reducer function
A reducer function is where you will put your state logic. It takes two arguments, the current state and the action object, and it returns the next state.(React will set automatically what you return from the reducer)

````
function tasksReducer(tasks, action) {
  switch (action.type) {
    case 'added': {
      return [...];
    }
    default: {
      throw Error('Unknown action: ' + action.type);
    }
  }
}
````
- Because the reducer function takes state as an argument, you can declare it outside of your component. This decreases the indentation level and can make your code easier to read.
- On recommand wrapping each __case__ block into the {} curly breaces, so that variables declared inside of different __case__s don't clash with each other.
- A __case__ should usually end with a __return__ , or __break__.

#### Step3: Use the reducer from your component
````
const [tasks, dispatch] = useReducer(tasksReducer, initialTasks);
````
The useReducer Hook takes two arguments : 
- A reducer function
- An initial state (Will be the value returned)

And it returns :
- A stateful value
- A dispatch function(to "dispatch" user actions to the reducer)

## II - Writing reducers well
Keep these two tips in mind when writing reducers:
- Reducers must be pure: Same input always result in the same output. (No send request, no schedule timeout, no side effects, etc.)
- Each action describes a single user interaction, even if that leads to multiple changes in the data.(Like Reset)
