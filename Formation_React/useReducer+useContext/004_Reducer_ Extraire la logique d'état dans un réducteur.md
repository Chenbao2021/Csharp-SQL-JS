# Introduction

Les composants ayant de nombreuses mises à jour d'état réparties dans divers gestionnaires d'événements peuvent s'avérer complexes à gérer, notamment lorsque les appels à `setState` se situent dans plusieurs fonctions différentes au sein du même composant. Dans de telles situations, il est possible de consolider toute la logique de mise à jour d'état dans une fonction unique (idéalement située à l'extérieur du composant), connue sous le nom de réducteur.

# Les composants essentiels d'un réducteur

1. **Fonction de réduction** : définit comment l'état est mis à jour en réponse aux actions.
2. **Utilisation de `useReducer`** : s'effectue avec la fonction de réduction et l'état initial.
3. **(Optionnel) Une énumération des actions** : permet de définir clairement les actions possibles.
4. **(TypeScript) Les types des différentes actions** : pour une meilleure gestion des types dans les projets TypeScript.
5. **Exemple** :
    ```javascript
    function reducer(state, action) {
      switch (action.type) {
        case 'updateField':
          return { ...state, [action.field]: action.value };
        default:
          return state;
      }
    }

    function Formulaire() {
      const [state, dispatch] = useReducer(reducer, {
        nom: '',
        email: '',
        message: ''
      });
      // Logique du formulaire
    }
    ```

# Comparaison de `useState` et `useReducer`

- **Taille du code** :
    - `useState` : Moins de code requis initialement.
    - `useReducer` : Nécessite d'écrire à la fois la fonction de réduction et de dispatcher les actions, mais peut aider à réduire le code lorsque plusieurs gestionnaires d'événements modifient l'état de manière similaire.

- **Lisibilité** :
    - `useState` : Très lisible pour des mises à jour d'état simples.
    - `useReducer` : Facilite la séparation entre la logique de l'application et les actions initiées par les utilisateurs.

- **Débogage** :
    Avec `useReducer`, il est possible d'insérer des logs dans la console depuis le réducteur pour observer chaque mise à jour d'état et comprendre les raisons sous-jacentes, en lien avec les actions spécifiques. Si chaque action est correctement exécutée, cela indique que le problème réside dans la logique de réduction elle-même. En comparaison, `useState` requiert l'examen de davantage de code.

- **Tests** :
    Un réducteur est une fonction pure qui ne dépend pas du composant, ce qui signifie qu'il est possible de l'exporter et de le tester isolément.

- **Préférence personnelle** :
    Il est toujours possible de convertir l'usage de l'un en l'autre selon les besoins.

# Écrire les réducteurs correctement

- Les réducteurs doivent être purs, c'est-à-dire produire les mêmes résultats pour les mêmes entrées sans effets secondaires. Les résultats ne doivent pas dépendre de facteurs externes tels que le temps, des valeurs aléatoires, ou l'état d'autres objets.
- Chaque action dans un réducteur devrait représenter une interaction utilisateur unique, même si cela entraîne plusieurs changements dans l'état. Cette approche simplifie la gestion des états et facilite le débogage.

# Implémenter `useReducer` de zéro

Dispatcher une action appelle un réducteur avec l'état actuel et l'action, puis met à jour l'état avec le résultat. Voici un exemple de code :

```javascript
export function useReducer(reducer, initialState) {
    const [state, setState] = useState(initialState);
    
    function dispatch(action) {
        const nextState = reducer(state, action);
        setState(nextState);
    }
    
    return [state, dispatch];
}
```

---
