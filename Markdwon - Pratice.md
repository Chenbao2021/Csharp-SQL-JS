#### Afficher des blocs de code en précisant le langage de programmation
On peut donner des couleurs à des blocs des codes en précisant quelle langage de programmation s'agit après __```javascript__, par exemple: 
````javascript
 function reducer(state, action) {
      switch (action.type) {
        case 'updateField':
          return { ...state, [action.field]: action.value };
        default:
          return state;
      }
    }
````

#### Créer un bloc de code inline
En markdown, on entoure un mot avec ``` pour créer un bloc de cole inline.
Par exemple,  ``console.log(1)``.

#### Séparateur horizontal
On peut mettre un séparateur horizontal en utilisant troit tiret(-)


