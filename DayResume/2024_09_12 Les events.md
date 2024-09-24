# React
### I - Les events
1. __Origine des événements dans le web.__
    Dans les développement web traditionnel, les événements provienent du DOM. 
    Les DOM est une représentation structurée d'une page web sous forme d'arbre, où chaque élément(ou noeud) de la page, comme une balise HTML, est un objet que JavaScript peut manipuler.
    Lorsqu'un utilisateur interagit avec un élément de la page, un événement est déclenché.
    __Ces événements permettent aux développeurs d'ajouter des interactions à leurs pages.__ Par exemple, en JavaScript natif, on peut écouter un événement de clic et réagir comme ceci:
    ````
    const button = document.getElementById('myButton');
    button.addEventListener('click', function(event) {
      console.log('Button clicked!', event);
    });
    ````
2. __Pourquoi avons-nous des événements dans React?__
React utilise des événements pour répondre aux interactions de l'utilisateur avec les composants. 
Les événements React sont essentiellement une abstraction des événements du DOM.(Une couche de simplification et d'optimisation.)
    * __Gestion Simplifiée des évenements__
    Avec le DOM natif, chaque type d'événement doit être ajouté manuellement aux éléments.Ce qui n'est pas le cas en React(Tous les événements dans React sont normalisés)
    * __Système de Délégation des evenements(Event Delegation)__
    Au lieu d'attacher des gestionnaires d'événements à chaque élément du DOM individuellement, React attache un gestionnaire unique à la racine du DOM virtuel(Souvent l'élément ``<div id='root'>``) dans lequel React rend toute l'application.
3. __Les événements synthétiques(Synthetic Events) en React.__
4. __Différences entre événements React et événements natifs.__
    * __Propagation des événements__ : Les événements en React utilisent un mécanisme de délégation pour attacher un seul écouteur d'événement à la racine, tandis que dans les DOM natif, les événements sont attachés individuellement à chaque élément.
    
    * __Compatibilité Cross-Browser__ : Les événements synthétiques en React sont conçus pour fonctionner de manière uniforme sur tous les navigateurs, tandis que les événements DOM natifs peuvent parfois avoir des différences de comportement entre les navigateurs
    
    * __Évenements spécifiques à React __ : Comme ``onDoubleClick``, qui n'existent pas dans le DOM natif.