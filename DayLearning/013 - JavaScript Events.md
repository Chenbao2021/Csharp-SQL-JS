[src](https://dev.to/vineethsagar/js-events-explained-with-examples-bfh)
# What is an Event?
Events are actions that happen in a system.
The system fires a signal when an event occurs and provides a mechanism to perform a task or activity automatically.

Exemple:
* Hovering over an element
* Clicking
* Scrolling
* Zoom
* ETC

Each event has an ``event handler`` that executes a block of code when an event is fired.

There are two types of events, __Browser events__(built-in, predetermined), and __Synthetic events__(Created and dispatched by the programmer).

# Add and remove an event listener
For an element in order to respond to various action you need to add an event listener to that element.
__addEventListener()__ method is used to add an event listener.
__removeEventListener()__ method is used to remove an event listener.
Both the above methods have the same syntax: It takes two parameters . First is the event name and the second is the callbak function that neet to be excuted.

# Événement personnalisé et émettre un __signal__.
#### 1. Création et déclenchement d'un événement personnalisé
En JS natif, les événements personnalisés sont créés avec le constructeur ``CustomEvent`` et déclenchés avec la méthode ``dispatchEvent``.

Création d'un événement personnalisé
````js
const myEvent = new CustomEvent('myCustomEvent', {
    detail: {message: 'Ceci est un événement personnalisé'}
})
````
Selection d'un élément du DOM
````js
const element = document.querySelector("#myElement")
````
Ajout d'un écouteur pour l'événement personnalisé
````
element.addEventListener('myCustomEvent', (event) => {
   console.log("Evenement personnalisé détecté: ", event.detail); 
});
```` 
Puis, on peut déclenchement de l'événement pour __element__ selectionné:
````js
element.dispatchEvent(myEvent)
````

#### 2. Autres
* Plusieurs écouteurs sur le même événement, pour un même élément:
    * Tous les écouteurs enregistrés pour cet événement seront appelés, dans l'ordre où ils ont été ajoutés avec ``addEventListener``.
    * Si un des écouteurs utilise ``event.stopImmediatePropagation()``, les écouteurs suivants ne seront pas appelés.

* Plusieurs objets DOM attachés au même événement:
    1. L'événement suit les __phases de propagation__ (L'événement vont exécuter qu'une seul fois):
        * __Capture__ : Traverse l'arbre DOM de la racine(``document``) vers l'élément cible.(faut préciser avec le troisième paramètre ``{capture: true}``)
        * __Cible__: Les écouteurs sur l'élément qui a déclenché l'evenement sont exécutés.
        * __Bubbling__: L'événement remonte de l'élément cible vers la racine(Action par défault).
    2. Si plusieurs éléments ont des écouteurs pour cet événement :
        * Les écouteurs s'exécutent en fonction de la phase de propagation.(capture ou bubbling)
    3. Par exemple, des images dans une galeries, toutes(galerie aussi) ont un écouteur sur "click". 
        1. Quand on clique l'image, le clic est d'abord traité par l'image(phase cible)
        2. Il __bulle_ vers ses parents, y compris le conteneur de la galerie. (Si le parent n'écoute pas sur cet événement, alors ce __bulle_ ne lui concerne pas)
        3. Si un parent a un écouteur pour cet événement, il sera déclenché.
* Pourquoi ce mécanisme?
    1. __Capture__: Permet aux ancêtres d'un élément de "voir" les événements qui descendent vers un élément cible.
    2. __Cible__: Permet de gérer précisement les événements sur l'élément qui les a déclenchés.
    3. __Bubbling__: Permet aux parents de réagir après l'élément cible, sans interférer directement avec lui.

* La méthode ``event.stopPropagation()``
* La méthode ``event.preventDefault()``
    Utilisée pour __empêcher le comportement par défaut__ associé à un événement.
    Par exemple:
    * __Un clic sur un lien__: Redirige vers l'URL spécifiéé dans ``href``.
    * __Un click sur un bouton de soumission__: Envoie le formulaire.
    * __Faire glisser un élément__: Déclencher le comportement de glisser-déposer.
    * __Empêcher la sélection de texte ou le clic droit__
