Source: https://react.dev/learn/state-as-a-snapshot

# Introduction
- Setting a state variable trigger a re-render

# Rendering takes a snapshot in time
- __Rendering__ : React is calling your component function.And the function return a JSX snapshot. (It includes logic)
    => Its state, its props(Les props qui sont utilisé dans JSX), event handlers(Les fonctions qui se trouvent dans onCLick, onChange, etc.), and local variables(Les variables qui sont utilisé dans JSX) were all calculated using its state __at the time of the render__.
- When React re-renders a component:
    1. React calls your function again.
    2. Your function returns a new JSX snapshot(With the new value of state).
    3. React then updates the screen to match the snapshot your function returned.

- Setting state only changes it for the __next render__.(same for dispatch, etc.), that means a state variable's value never changes within a render.
- Very useful to avoid timing mistakes.

# QA

### 1. Pouvez-vous expliquer ce qui se passe lors du processus de rendu d'un composant React ?
Pendant le processus de rendu, React appelle la fonction de votre composant qui retourne un JSX snapshot.
Cet instantané inclut les props, les gestionnaires d'événements et les variables locales, tous calculés à partir de l'état du composant au moment du rendu.

### 2. À quoi fait référence le terme "instantané" dans le contexte du rendu React ?
L'instantané se réfère à la sortie JSX de la fonction du composant, qui représente l'état visuel du composant à un moment donné, incluant les props, les événements et les variables locales.

