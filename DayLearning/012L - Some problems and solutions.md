#### I - Problème du premier chargement: Service worker n'a pas encore prit le contrôle.
* On peut utiliser setIntervalle pour retarder l'exécution de la première requête(Souvent celui qui est lancé par useEffect)

On peut tirer parti de l'événement ``controllerchange`` disponible sur ``navigator.serviceWorker``. 
Cet événement est déclenché lorsque le contrôleur de la page change, c'est-à-dire quand un service worker prend le contrôle.

* Vérifie si le navigateur support service worker et qu'il y a un service worker qui va déclencher
* .ready permet de vérifier si le service est déjà activé, mais n'assure pas qu'il prend déjà le contrôle.
    Si ce n'est pas le cas, sa valeur vaut ``null``.
    ````js
    useEffect(() => {
      if ('serviceWorker' in navigator) {
        navigator.serviceWorker.ready.then(() => {
            ...
        }
    } else 
        doRequest();
    ````

* Vérifier si le service worker prend déjà le contrôle. 
* Si oui, on peut faire le contrôle.
    ````js
    if (navigator.serviceWorker.controller) {
        doRequest();
     }
    ````

* On écoute l'événement controllerchange pour lancer la requête dès qu'il prend le contrôle.
* On ajoute un addEventListener sur l'événement 'controllerchange'
* Dè qu'il y a une action sur cet événement, on déclenche onControllerChange:
    * On fait l'action
    * On supprime notre propre ecouteur d'événement.
````js
 else {
        const onControllerChange = () => {
          navigator.serviceWorker.removeEventListener('controllerchange', onControllerChange);
          doRequest();
        };
        navigator.serviceWorker.addEventListener('controllerchange', onControllerChange);
    }
````
