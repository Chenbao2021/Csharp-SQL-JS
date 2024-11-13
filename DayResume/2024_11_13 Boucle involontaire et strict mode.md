# React
***
## I - Boucle involontaire
Code initial avec bug : 
````js
const handleAddPhotos = useCallback((event) => {
  setStateQuestion(prevState => ({
    ...prevState,
    photos: addDoc([event.target.files[0]], fileType.photo, prevState.photos ?? [])
  }));
  event.target.value = null; // Réinitialise la valeur de l'input
}, []);
````

#### Problème initial
Dans votre code initial, ``addDoc`` était appelé à l'intérieur de la fonction de mise à jour de ``setStateQuestion``. Cela créait une boucle involontaire en fonction de la façon dont React gère les mises à jours d'état:
1. ``addDoc`` __dépendait__ de ``prevState.photos``, ce qui faisait que chaque fois que ``prevState.photos`` changeair, React pouvait déclencher ``handleAddPhotos`` et rappeler ``addDoc``.
2. __Boucle involontaire__ : Comme ``setState`` déclenche un re-rendu du composant, et ``addDoc`` dépendait indirectement de cet état, React pouvait déclencher ``setStateQuestion`` plusieurs fois, causant un double appel à ``addDoc``.

### Mais pourquoi il est appelé deux fois ici?
À cause du mode __Mode Strict de React__ en développement.
Lorsque ce mode est activé, React appelle certaines fonctions de composants(comme les effets et les mises à jour d'état) __deux fois__ pour aider les développeurs à détecter des effets secondaires non désirés ou des erreurs subtiles dans leur code.

Cela signifie qu'en mode développement:
1. React exécute votre composant, déclenchant ``handleAddPhotos`` et ``setStateQuestion``.
2. Ensuite, React __démonte et remonte__ le composant immédiatement pour vérifier s'il y a des efefts de bord. (D'ou notre double ajout de la photo)
3. Cette conséquence entraîne double appel de notre fonction ``addDoc`` car il est appelé dans setStateQuestion, qui est appelé deux fois à cause de strict mode
4. Donc on a l'erreur de double ajout de photo dans le code initial.

