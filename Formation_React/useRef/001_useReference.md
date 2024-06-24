# Qu'est-ce que ``useRef`` ?
##### Définition 
``useRef`` crée une référence mutable que l'on peut:
* Attacher à un élément DOM
* Utiliser pour stocker une valeur persistante qui ne nécessite pas de re-rendu lorsqu'elle est modifiée.

Syntaxe: ``const myRef = useRef(initialValue)``

##### Raisons d'utiliser 'useRef'
1. Accéder aux éléments DOM directement:
* ``useRef`` permet d'accéder à un élément DOM pour manipuler ses propriétés ou méthodes sans déclencher de re-rendu.
Exemple:
    ````javascript
    const MyComponent = () => {
        const inputRef = useRef(null);
        useEffect(() => {
            inputRef.current.focus();            
        }, []);
    
        return <input ref={inputRef} />;
    }
    ````
2. Stocker des valeurs mutables


