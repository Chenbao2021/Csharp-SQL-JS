# Générale
### I - On développer d'abord le Back-end, avant de dévélopper le Front-end
* Cela permet d'utiliser directement les APIs quand on dévéloppe le front-end. 
* Éviter des ajustements inutiles

# React
### I - Effet visuel perceptible
````
const closeModale = useCallback(() => { setOpenModal(false), setActionName(undefined) }, [])
````
*  Si actionName est utilisé pour déclencher une animation ou une transition juste avant la fermeture du modal, il se peut que cette transition soit plus perceptible ou plus rapide que le changement d'état qui cache le modal.

Pour changer 'actionName' uniquement après que le modal est fermé, on peut utiliser useEffect : 
````
useEffect(() => {
  if (!openModal) {
    setActionName(undefined);
  }
}, [openModal]);
````


