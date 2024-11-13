# The power of concise React Code
Sometimes, we find ourselves writing more code than necessary, that's where these one-liners come in handy.
__They are like the Swiss Army knives of the React world : Small, but oh so poweful!__

### 1. The Conditional Rendering Shortcut
``{condition && <Component />}``
If the condition is true, React moves on to evaluate what comes after the &&, which in this case is our component.
Use case:
* Show welcome message only for logged-in users.
* Display a special offer only during certain horus.
* etc.

### 2. The default Props Shortcut
``const {prop = defaultValue} = props;``
Exemple: ``const Button = ({size = 'medium', children}) => ...``

### 3. The State Update Shortcut
``setCount(prevCount => prevCount + 1);``
This approach ensures that you are always working with __the most up-to-date__ state value.
Use cases:
* Counters
* Toggling boolean values
* Any situation where the new state depends on the old one.

### 4. The array Manipulation Shortcut
``setItems(prevItems => [ ...prevItems, newItem ]);``
In React, __immutability is key for performance and predictability__. Insted of modifying the existing one, we create a new one.
Exemple:
``const addTask = (newTask) => {setTask(prevTasks => [...prevTasks, newTask]);}``

### 5. The Object Update Shortcut
Similar to arrays, we don't mutate the original object.
``setUser(prevUser => ({ ...prevUser, name: 'New Name' }));``
__Keep everything the same, except for these specific changes__.

### 6. The Ref Callback Shortcut
Refs in React are super useful for accessing DOM elements directly.
``<input ref={node => node && node.focus()} />``
This creates an input element that automatically focuses when it's rendered. It is great for creating accessible forms where you want to automatically focus on the first input field when the form loads.

### 7. The Memo Shortcut
``const MemoizedComponent = React.memo(({prop1}) => <Component prop1={prop1} />);``
React.memo is a higher-order component that skips rendering when props are the same.
This is great for pure functional component that are expensive to render or are deep in the component tree and receive the same props frequently.


