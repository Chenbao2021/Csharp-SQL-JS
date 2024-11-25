[Original text](https://dev.to/hkp22/javascript-shallow-copy-vs-deep-copy-examples-and-best-practices-3k0a)

# I - What Is a Shallow Copy?
* For properties that are primitives: The value itself is copied.
* For properties that are objects(Like array, or nested objects), only the reference is copied - Not the actual data. => The nested objects or arrays remain shared between the original and the copy.

To create a shallow copy:
* Using the Spread Operator(``...``)
    ``const shallowCopy = { ...originalObject }``
* Using ``Object.assign()``:
    ``const shallowCopy = Object.assign({}, originalObject);``

# II - What Is a Deep Copy?
* Duplicate every property(Primitives, Objects, etc.). This ensures that the copy is completely independent of the original.

To create a Deep Copy
* Using ``structuredClone``
    * This is a native methode, and an efficient way to deep clone JavaScript objects, introduces in __recent versions__ of modern browser.
    Exemple:
        ````js
        const obj = {
            arr: ["a", "b", "c"],
        };
        const obj_copy = structuredClone(obj);
        ````
* Using ``JSON.stringify()`` and ``JSON.parse()``
    * Converts the object into a JSON string and then parses it back into a new Object.
    * Limitations:
        * Cannot handle circular reference (Ex: une objet référence de lui même)
            ````js
            const node1 = { value: 1 };
            const node2 = { value: 2 };
            
            node1.next = node2;
            node2.next = node1; // Circular reference
            ````
        * Ignores properties like functions, ``undefined``, or ``symbole``.
            * Functions are not valid JSON data types, so they are omitted during JSON.stringify(). (Deleted)
            * ``Undefiend``: Omitted
            * ``Symbol``: Omitted too.
            * ``new Date``, ``new Set``, etc.
                Pour Date, il y a une méthode: .toString() qu'on peut l'utiliser.
* Using Librairie ( Add extra weight to yor bundle)
    * Par exemple ``Loadash``, provide robust deep cloning methods
        ````js
        const _ = require('loadash');
        const deepCopy = _.cloneDeep(originalObject);
        ````
        * Reconnaît les objets ``Date`` et les copie correctement.
        * Reconnaît aussi les ``Set`` et ``Map``.
        * Conserve les propriétés définies avec ``Undefined``.
        * Détectant les cycles et en évitant de copier un même objet plusieurs fois.

* Custom Recursive Function:
    * For full control, you can write a recursive function to clone nested objects.

# III - When to Use Deep Copy
* __Avoiding Side Effects__: When you need to ensure that changes in the copy don't affect the original.
* __State Management__: In frameworks like React or Redux, where immutability is critical.

Exemple:
````
const gameState = {
  level: 5,
  inventory: {
    weapons: ["sword", "shield"],
    potions: 3
  }
};

// Deep copy ensures no side effects
const savedState = JSON.parse(JSON.stringify(gameState));
````

# IV - Commont Mistakes and Pitfalls
1. __Assuming Shallow Copy Is Always Sufficient__:
    Developers often mistakenly use shallow copy methods for nested objects, leading to unintended changes in the original data.
2. __Overusing JSON methods__:
    While ``JSON.stringify``/``JSON.parse`` is simple, it doesn't work for all objects(eg., those containing methods or circular references).
3. __Neglecting Performance__:
    Deep copy methods can be slower, especially for large objects, so use them judiciously.




        

