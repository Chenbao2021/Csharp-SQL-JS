# React
***
#### I - "__return (__" or "__return <>__"
***
When return something, the something should in the same line of return.

***
#### II - Closure
***
JavaScript supports closures which means an inner function (e.g. handleClick) has access to variables and functions defined in a outer function (e.g. Board). 
The handleClick function can read the squares state and call the setSquares method because they are both defined inside of the Board function.

***
#### III - Immutability
***
Code:
````
function handleClick(i) {
    const nextSquares = squares.slice();
    nextSquares[i] = "X";
    setSquares(nextSquares);
}
````
We use .slice() to create a copy of the __squares__ array instead of modifting the existing array.
There are two approches to changing data: 
- To mutate the data by directly changing the data's values.
- To replace the data with a new copy which has the desired changes.

The second one is so called 'immutability', it has several benefits:
- Makes complex features much easier to implement.
- Allow you to skip re-rendering a part of the tree that clearly wasn't affected by it for performance reasons.
    Immutability makes it very cheap for components to compare whether their data has changed or not.
