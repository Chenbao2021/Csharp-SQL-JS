# React
***
### I - Render with key
* with useState
    1. `` const [configKey, setConfigKey] = React.useState(0);``
    2. ``useEffect(() => {setConfigKey((prevKey) => prevKey + 1);}, [config]);``
    3. ``<FroalaEditor key={configKey} config={config} />``
* with useRef
    1. ``const keyRef = useRef(0)``
    2. ``useEffect(() => {keyRef.current = Math.random()}, [config]);``
    3. ``<FroalaEditor key={keyRef.cuurent} ... />``

### II - La structure d'un component
Avec useRef, tu peux afficher la structure de n'importe quel component.
Par exemple avec Froala-text.
1. ``const editorRef = useRef<any>();``
2. ``<FroalaEditor ref={editorRef} ... />``
3. ``console.log(editorRef.current)``
ça te permet de modifier directement une proprieté, Méthode, etc.

### III - The ultime advantage of useCallback/useMemo
Dans React, les problèmes liés à des "anciennes références" d'une fonction ou d'une valeur peuvent survenir à cause des _re-renders__. Voici pourquoi cela se produit, et comment ``useCallback`` ou ``useMemo`` aident:
##### Problème: Fermeture(Closure)
1. __Rerenders et reconstructions__: À chaque re-render, React reconstruit les fonctions déclarées dans votre composant. À ce moment-là, elles capturent l'état actuel de leur environnement, mais elles ne "voient pas" les changements qui surviendront __après__ leur création.
    ````js
    function MyComponent() {
        const [count, setCount] = React.useState(0);
        const logCount = () => console.log(count);
    
        React.useEffect(() => {
            const id = setInterval(logCount, 1000);
            return () => clearInterval(id);
        }, []);
        return <button onClick={() => setCount(c => c + 1)}>Increment</button>;
    }
    ````
    * ``logCount`` est recréée à chaque re-render.
    * Mais l'effet(``useEffect``) n'est déclenché qu'au premier render, donc l'interval utilise la __version intiale__ de ``logCount`` qui contient une __référence obsolète__ de ``count`` et qui "se souvient" de la valeur de ``count`` au moment de sa création (Soit ``0``) grâce à la closure.
    * __Conséquence__ : Le ``console.log`` affiche toujours "Count actuel: 0", même si ``count`` a changé.


##### Autre exemples
Donc, lorsqu'il y a des contextes comme useEffet ci-dessus, on peut avoir des problèmes sur les données.
*  Problème avec ``setTimeout`` dans un ``useEffect``
*  Problème avec des gestionnaires d’événements dans un useEffect
*  Problème avec un composant enfant dépendant d'une fonction passée en prop(re-render inutile)
*   Problème avec des callbacks asynchrones

##### Resumé
Bien mettre useCallback,useMemo , et faire attentions aux dépendances!!!

Les références obsolètes peuvent apparaître dans plusieurs situations où des fonctions capturent des valeurs figées à cause de :
1. Hooks mal configurés (useEffect sans dépendances complètes).
2. Fermetures dans des callbacks (ex. : setTimeout, événements).
3. Re-renders inutiles dans des composants enfants (fonctions recréées passées en props).
4. Appels asynchrones ou calculs complexes.

Stabiliser les fonctions avec useCallback ou useMemo, ou utiliser useRef, permet de résoudre ces problèmes.
    