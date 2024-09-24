# React
****
## I - Fonction / Component
Les composants React peuvent ressembler similiaires à des fonctions JavaScript, ils jouent des rôles différents dans une application React.

1. **Fonction JavaScript ordinaire** :
    Une fonction JavaScript est simplement un bloc de code qui peut prendre des arguments, effectuer des calculs ou des opérations, et renvoyer une valeur. 
    Elle est **stateless** (Elle n'a pas d'état interne persistant, sauf si tu le définis explicitement en dehors de la fonction).
2. **Composant React** :
    Un **composant React** est également une fonction(Dans le cas des composants fonctionnels), mais il a un rôle particulier : **Il retourne un arbre de rendu JSX**, qui est utilisé pour générer une interface utilisateur(UI).
    Il est **stateful** (Il peut avoir un état local via les hooks comme ``useState``), il gère des événements, et peut être re-rendu à chaque changement d'état ou de props.
3. **useCallback** ne peut pas être utilisé pour un composant
    Un **composant React fonctionnel** est bien une fonction, mais il fait bien plus que simplement exécuter du code:
    * Il **retourne du JSX** qui est transformé en élément d'interface utilisateur.
    * Il **gère des hooks**(``useState``, ``useEffect``, etc.) qui sont essentiels pour son cycle de vie.
    * Il peut avoir des **props** qui changent à chaque re-rendu.
    
    Le **cycle de vie d'un composant React** implique plus que ce que ``useCallback`` peut gérer.Par exemple, les états, les effets, et les mises à jour du DOM ne sont pas des choses que ``useCalback`` est conçu pour manipuler.

## II - Cycle de vie d'un composant React.
Les hooks offrent une nouvelle façon plus simple et flexible de gérer les différents aspects du cycle de vie, mais ces aspects sont toujours présents, et il est crucial de les connaître pour bien comprendre comment et quand utiliser les hooks correctement.

**Pourquoi le cycle de vie reste important avec les hooks?**
1. **Les hooks remplacent les méthodes de cycle de vie des classes, mais les phases restent.**
    Dans les **composants de classe**, React offrait des méthodes explicites pour les différentes étapes du cycle de vie, comme ``componentDidMount``, ``componentDidUpdate``, et ``componentWillUnmount``. Avec les **hooks**, ces étapes sont toujours présentes, mais elles sont gérées différemment. Les hooks, comme ``useEffect``,``useState``, et ``useMemo``, correspondent à différentes parties du cycle de vie.
    
    Par exemple:
    * ``useEffect``: Remplace les méthodes de cycle de vie comme ``componentDidMount``; ``componentDidUpdate``, et ``componentWillUnMount``. Il permet d'exécuter du code à des moments spécifiques du cycle de vie.
    * ``useState``: Permet de gérer l'état interne du composant, à chaque re-rendu(qui fait partie du cycle de vie).
    * ``useMemo`` et ``useCallback``: Optimisent les calculs ou les références à des fonctions pendant le cycle de vie d'un composant en contrôlant la manière dont ils sont créés ou réutilisés.
2. **Comprendre le cycle de vie aide à bien utiliser ``useEffect``**
    Le **hook** ``useEffect`` est très puissant, mais pour l'utiliser correctement, tu dois comprendre quand et comment React monte, met à jour, et démonte un composant. ``useEffect`` peut être utilisé dans trois phases principales du cycle de vie:
    * __Montage__( = ``componentDidMount``): Excécuter une action lorsque le composant est monté.
        ``useEffect(() => {...}, [])`` : Pour exécuter un effet uniquement lors du montage.
    * __Mise à jour__ ( = ``componentDidUpdate``): Exécuter une action après que le composant a été mise à jour.
        ``useEffect(() => {...}, [dep])``: Pour exécuter un effet lorsque les dépendances changent.
    * __Démontage__ ( = ``componentWillUnmount``): Exécuter du code de nettoyage lorsque le composant est retiré du DOM(par exemple, annuler un abonnement ou un écouteur d'événement).
        ``useEffect(() => {...}, [])``: Avec une fonction de nettoyage pour exécuter une action lorsque le composant est démonté.
3. **Démontage** avec useEffect
    En React, le hook __useEffect__ peut être utilisé pour exécuter du code lorsque **un composant est démonté**, c'est-à-dire lorsqu'il est retiré du DOM. Cette fonctionnalité est utile pour effectuer du nettoyage ou des tâches spécifiques, comme:
    * Arrêter des timers ou des intervalles(``setInterval``, ``setTimeout``)
    * Désabonner des écouters d'événements.
    * Annuler des requêtes réseau en cours
    * Libérer des ressources ou nettoyer des effets de bord.
   
     Lorsque tu vois un ``return`` dans un ``useEffect``, cela signifie que tu utilises une **fonction de nettoyage**(ou cleanup). Cette fonction est appelée par React à un moment précis du cycle de vie du composant, soit lors du **démontage** du composant, soit avant que l'effet ne soit pas réexécuté à cause de la mise à jour des dépendances. Elle est utlisé pour nettoyer les effets secondaires que tu as configurés dans le ``useEffect``.
    
    **Quand la fonction de nettoyage est-elle appelée?**
    1. Lors du démontage du composant:
        La fonction retournée par ``useEffect`` est appelée lorsque le composant est retiré du DOM (démonté). Cela permet d'annuler ou de nettoyer tout effet secondaire qui ne doit plus persister une fois que le composant a été supprimé.
    2. Avant que l'effet ne soi réexécuté:
        Si un effet dépend de certaines valeurs(dépendances), et que l'une de ces valeurs change, React exécute d'abord la **fonction de nettoyage** de l'effet précédent avant d'appliquer l'effet suivant.
        Cela permet d'éviter d'accumuler plusieurs effets concurrents(Par exemple, plusieurs intervalles ou écouteurs d'événements).

4. **Exemples des démontages :**
    1. Nettoyage d'un timer
        ````JavaScript
          useEffect(() => {
            const interval = setInterval(() => {
              setCount((prevCount) => prevCount + 1);
            }, 1000);
        
            // Fonction de nettoyage pour effacer le timer
            return () => {
              clearInterval(interval);  // Nettoyage de l'intervalle
            };
          }, []);  // Le tableau vide signifie que l'effet s'exécute seulement au montage
        ````
    2. Nettoyage d'un écouteur d'événement
        ````JS
        import React, { useEffect } from 'react';
        function ResizeComponent() {
          useEffect(() => {
            const handleResize = () => {
              console.log('Window resized');
            };
        
            window.addEventListener('resize', handleResize);
        
            // Fonction de nettoyage pour retirer l'écouteur d'événements
            return () => {
              window.removeEventListener('resize', handleResize);
            };
          }, []);  // Le tableau vide signifie que l'effet s'exécute seulement au montage
        
          return <div>Regardez la console lorsque vous redimensionnez la fenêtre</div>;
        }
        ````
    


    


