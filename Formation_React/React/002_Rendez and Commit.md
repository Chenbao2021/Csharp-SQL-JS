Source: https://react.dev/learn/render-and-commit

# Introduction
React takes three steps to serving UI :
1. __Triggering__ a render (Delivering the guest's order to the kitchen)
2. __Rendering__ the component (Preparing the order in the kitchen)
3. __Committing__ to the DOM (placing the order on the table)

# Explication about three steps
### Step 1 : Trigger a render
1- It's the component's initial render: It's done by calling __createRoot__ with the target DOM node, and then calling its __render__ method with your component.
````
const root = createRoot(document.getElementById('root'))
root.render(<Image />);
````
2 - Re-render when state updates

### Step2 : React renders your component
After trigger a render, React calls your components to figure out what to display on screen.
- __On initial render__, React will call the root component
- __For subsequent renders__, React will call the function component whose state update triggered the render.

=> Recursive : If the updated component returns some other component, React will render that component next, 
=> Rendering must always be a pure calculation.

### Step3 : React commits changes to the DOM.
