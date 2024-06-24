La manipulation d'une image, telle que le zoom et le dézoom, dans une application React nécessite souvent l'utilisation de ``useRef`` pour accéder directement à l'élément DOM. Voici pourquoi useRef est crucial dans ce contexte :
### Pourquoi utiliser ``useRef`` pour manipuler une image
1. Accès direct à l'élément DOM:
    * ``useRef`` permet d'accéder à l'élément DOM de l'image directement. Cela est nécessaire pour appliquer des transformations(comme le zoom) qui ne peuvent pas être facilement gérées via l'état React seul.
2. Performance:

3. Utilisation d'API DOM:
    * Certaines API DOM, comme getBoundingClientRect ou les méthodes de transformation, nécessitent un accès direct à l'élément DOM.
    * Exemple:
        ````javascript
        const handleZoom = () => {
          const img = imgRef.current;
          img.style.transform = 'scale(1.5)'; // Zoom à 150%
        };
        ````

### useRef modifier directement le CSS.
useRef ne déclenche pas un re-render, mais il peut modifier les valeurs CSS directement, et les modifications de CSS change la mise en page des components, ce qui:
* Gagne beaucoup des performances 
* Mais augmente la difficulté de maintenance.

Et n'oublies pas d'utiliser des transitions CSS pour rendre les modifications de style plus fluides et visibles pour l'utilisateur!
Exemple: ``style={{transition: 'transform 0.3s ease'}}``
### Exemple concrète
1. Initialisation de ``useRef``:
    ````javascript
    const imgRef = useRef(null)
    ````
2. Fonction ``handleZoomIn``:
    ````javascript
    const handleZoomIn = () => {
        const img = imgRef.current;
        if(img) {
            img.style.transform = 'scale(1.5)'
        }
    }
    ````
3. Rendu du composant
    ````javascript
    return (
      <div>
        <img
          ref={imgRef}
          src="https://via.placeholder.com/300"
          alt="Zoomable"
          style={{ transition: 'transform 0.3s ease' }} // Transition douce pour l'effet de zoom
        />
        <div>
          <button onClick={handleZoomIn}>Zoom In</button>
        </div>
      </div>
    );
    ````
