# React
### I - Try/Catch pour gérer le format d'une date
En, React. J'ai un input qui va consommer une Date, mais on si le saisit n'est pas une Date alors une exception est lancé puis le site crash.
Dans ce cas la:
* Soit on vérifie les saisits ont un bon format
* Soit on gérè l'exception à l'endroit précis.

Cas pratique où j'utilise la deuxième méthode:
````javascript
try {
	const year = date.getFullYear();
	const month = pad(date.getMonth() + 1); // Les mois sont de 0 à 11
	const day = pad(date.getDate());
	const hours = pad(date.getHours());
	const minutes = pad(date.getMinutes());
	return `${year}-${month}-${day}T${hours}:${minutes}`;
} catch (error) {
	_mfPopup.displayMessage({ content: ["Le format de Date n'est pas correct!"], state: "error" });
	return -1
}
````
* Dans le bloc try, j'essaie d'extraire les données de date, puis retourner dans un format que je demande.
* Dans le bloc catch, s'il y a une exception provoqué dans le bloc try, alors j'affiche une message d'erreur, puis je retourne -1 a la fonction appelante pour qu'il puisse continuer l'opération.

### II - useMemo et useCallback
``useMemo`` utilise la référence de la fonction fournie par ``ùseCallback`` au moment de son dernier calcul et contiuera à utiliser cette référence jusqu'à ce que ses propres dépendances changent. 
Cette référence est simplement une variable dans le contexte de React qui peut pointer vers la version "ancienne" de la fonction tant que ``useMemo`` ne se rrecalculera pas.

Pour clarifier davantage : 
1. Initialisation
    Au premier rendu, ``useCallback`` crée une fonction et ``useMemo`` utilise cette fonction pour mémoriser une valeur (La référence de la fonction) .
2. Changement des dépendances de ``ùseCallback``
    Si les dépendances de ``useCallback`` changent, une nouvelle fonction est créée. L'ancienne fonction est remplacée dans le contexte React, mais si ``useMemo`` ne dépend pas directement de ``useCallback``, il ne sera pas informé de cette nouvelle fonction.
3. Référence de la fonction
    La référence de la fonction créée par ``useCallback`` reste la même pour ``useMemo`` jusqu'à ce que ``useMemo`` se recalculera en raison d'un changement dans ses propres dépendances.

Exemple:
````javascript
import React, { useState, useCallback, useMemo } from 'react';

const MyComponent = () => {
  const [count, setCount] = useState(0);
  const [multiplier, setMultiplier] = useState(2);

  // useCallback crée une fonction dépendant de `count`
  const memoizedCallback = useCallback(() => {
    return count * multiplier;
  }, [count]);

  // useMemo dépend seulement de `multiplier`
  const memoizedValue = useMemo(() => {
    return memoizedCallback();
  }, [multiplier]);

  return (
    <div>
      <p>Count: {count}</p>
      <p>Multiplier: {multiplier}</p>
      <p>Memoized Value: {memoizedValue}</p>
      <button onClick={() => setCount(count + 1)}>Increment Count</button>
      <button onClick={() => setMultiplier(multiplier + 1)}>Increment Multiplier</button>
    </div>
  );
};

export default MyComponent;
````
Explication détaillée:
1. Premier rendu:
    * ``memoizedCallback`` est créé avec ``count = 0`` et ``multiplier = 2``
    * ``memoizedValue`` est calculé en appelant ``memoizedCallback``, ce qui donne ``0 * 2 = 0``
2. Changement de ``count``:
    * Lorsque ``count`` change(par exemple, devient ``1``), ``memoizedCallback`` est recréé avec une nouvelle fonction. Cependant, ``memoizedValue`` ne se recalculera pas car ``multiplier`` n'a pas changé. Ainsi, ``useMemo`` continue d'utiliser l'ancienne fonction de ``memoizedCallback`` (Celle avec ``count = 0`` et ``multiplier = 2``)
3. Changement de ``multiplier``:
    * Lorsque ``multiplier`` change, ``useMemo`` se recalculera, et à ce moment-là, il utilisera la version actuelle de ``memoizedCallback``.

### III - Stockage et Cycle de Vie des Fonctions
1. **Création de la Fonction** :
    Lorsque vous utilisez ``useCallback``, une nouvelle fonction est créée et sa référence est stockée dans une variable liée à l'instance de votre composant.
2. **Références et Fermetures** :
    Cette fonction fait partie de la fermeture du composant, ce qui signifie qu'elle peut capturer et conserver l'état et les variables locales du composant au moment de sa création.
3. **Remplacement de la Référence** :
    Lorsque les dépendances de ``useCallback`` changent, une nouvelle fonction est créée et la référence de cette nouvelle fonction remplace l'ancienne référence.
4. **Utilisation par ``useMemo``** :
    ``useMemo`` utilise la référence de la fonction fournie au moment de son dernier calcul. Tant que ``useMemo`` ne se recalculera pas(parce que ses propres dépendances n'ont pas changé), Il continuera à utiliser l'ancienne référence de la fonction
5. **Garbage Collection**:
    L'ancienne fonction devient éligible pour le garbage collection(collecte de déchets) une fois qu'aucune référence ne la pointe.
    Cela signifie qu'une fois que ``useMemo`` se recalculera et utilisera la nouvelle fonction, l'ancienne fonction pourra être supprimée de la mémoire par le moteur JavaScript.