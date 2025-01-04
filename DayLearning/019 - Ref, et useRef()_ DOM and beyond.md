### I - Introduction
__Ref__ is just a ``{ current: initialValue }`` object. It's nothing special. Both ``useRef(initialValue)`` and ``createRef()`` give you that.
You can create your own ``useRef`` by this simple code:
````js
function useRef(initialValue) {
    return React.useState({current: initialValue})[0]
}
````
* That means, ``useRef`` takes the initial value as an argument for the returned value.

These return values would be persisted and you can also mutate them according to your need(_Without causing a re-rendere of a component_).
***

### II - Accessing the DOM with useRef
When we write jsx, it gets converted into React.createElement:
* ``<div>Hello World</div>`` which we write as jsx gets converted into ``React.createElement("div", null, "Hello World")
So you __dont' have any direct access to the DOM nodes__ from your returned jsx. 

So to get access to the DOM, you need to ask React to give you access to a particular DOM node when it renders your component. 

And the solution is using ``ref`` prop.
````js
function UploadButton({handleUpload}) {
    const inputRef = React.useRef();
    
    const handleClick = () => inputRef.current.click();
    
    return (
        <>
            <input type="file" hidden ref={inputRef} onInput={handleUpload} />
            <button onClick={handleClick}>Upload</button>
        </>
    )
}
````
In this example:
1. We are passing the ``ref`` prop ``inputRef``
2. When we click on a button that uses ``inputRef.current`` to get access to that __DOM element__ of that input and on that element we are calling ``click`` event.

Some other cases would be like getting a value from an input, changing focus, or selecting text.
***
### III - A stopwatch component with stop and resume functionality.
__A simple StopWatch component which updates time every 1 second__
````js
...
const [time, setTime] = useState(0);

useEffect(() => {
    const interval = setInterval(() => {
        setTime((s) => s + 1);
    }, 1000);
}, [])
...
````

__But now, we need a button which will make the ticking of time stop and resume__
We would like to add ticking state and update our useEffect, but we cannot to access the interval object, because ``interval`` is declared inside the ``if`` bloc, so not accessible from ``else``.

````js
...
useEffect(() => {
    if(ticking) {
        const interval = setInterval(() => {
            setTime((ms) => ms + 1);
        }, 1000)
    } else {
        clearInterval(interval);
    }
}, [ticking]);
...
````

If we put our variable ``interval`` outside ``useEffect`` , on every render all local variables would reset and it would become undefined again.

Right, as you guess, we need useRef here.
````
...
const interval = useRef();

useEffect(() => {
    if(ticking) {
        interval.current = setInterval(() => {
            setTime((ms) => ms + 1)
        }, 1000)
        
        return () => clearInterval(interval.current)
    } else {
        interval.current && clearInterval(interval.current)
    }
}, [ticking])
...
````

***
### IV - Qui a le prop ``ref``?
En React, toutes les balises DOM(par exemple ``div``, ``button``, etc.) ou __les composants React implémentant__ ``forwardRef`` peuvent recevoir une prop ``ref``.

C'est à dire:
* __Balise DOM__: React sait comment gérer la référence à l'élément DOM sous-jacent et donc la prop ``ref`` est utilisée pour récupérer le noeud DOM d'un ``<div>``.
* __Composant classe__: Les composants déclarés sous forme de classe acceptent ``ref``, car React sait créer une référence vers leur instance(this).
* __Composants fonctionnels simples__: Ils n'ont pas d'instance, donc la ref n'a pas de sens. pour rendre possible l'utilisation d'une ref sur un composant fonctionnel, il faut utiliser ``React.forwardRef``. Cela permet de "transférer" la prop ``ref`` à un  élément ou un autre composant interne.
    Exemple: 
    ````js
    const MyButton = React.forwardRef((props, ref) => {
      return <button ref={ref} {...props} />
    })
    ````








