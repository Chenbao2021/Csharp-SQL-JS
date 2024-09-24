# General
### I - Spécialiser
Spécialiser dans une domaine, être mieux que les autres dans un et un seul domaine.

### II - API(Application programming interface)
Chercher à utiliser une service sans connaître ses implémentations.

# React
## I - Règles des Hooks
* __Only call Hooks at the top level__
* __Only call Hooks from React functions__

#### Only call Hooks at the top level
__You can only call Hooks while React is rendering a function component__.
It's not supported to call Hooks(function starting with __use__) in any other cases:
* Do not call Hooks inside conditions or loops.
* Do not call Hooks after a conditional __return__ statement
    ````javascript
    function Bad({ cond }) {
      if (cond) {
        return;
      }
      // 🔴 Bad: after a conditional return (to fix, move it before the return!)
      const theme = useContext(ThemeContext);
      // ...
    }
    ````
* Do not call Hooks in event handlers.
* Do not call Hooks in class components.
* Do not call Hooks inside functions passed to __useMemo__, __useReducer__, or __useEffect__.
* Do not call Hooks inside try/catch/finally blocks.

## II - Event handler/ Functions
1. __Event Handler( Gestionnaire d'événements ) : __
    * Un event handler est une fonction associée à un événement particulier(Comme un clic, un changement de champ de texte, etc.)
    * Il est souvent utilisé dans les composants React pour réagir aux actions de l'utilisateur.
    * Un event handler n'est pas appelé directement dans le code. Il est passé en référence à un événement (Comme onClick, onChange, onSubmit), et c'est React qui l'appelle automatiquement en réponse à une interaction utilisateur.
    * Par défaut, un event handler reçoit un argument event, qui contient des informations sur l'événement utilisateur (Comme les détails du clic, la touche pressé, etc.)
2. __Fonctions : __
    * Une fonction en JavaScript est un bloc de code qui peut être réutilisé et appelé à n'importe quel moment.
    * Elle peut être utilisée pour encapsuler une logique réutilisable, effectuer des calculs ou retourner un rendu JSX dans un composant.


