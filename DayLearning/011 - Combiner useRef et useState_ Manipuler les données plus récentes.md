# Intro
Lorsqu'on a besoin d'accéder à la __version la plus récente__ d'un objet, pour une raison ou l'autre, comme:
* Ne pas déclencher un re-rendu
* Éviter des fonctions asynchrones utilisent des anciens références avec des valeurs non à jour

Il est parfois très recommandé d'utiliser une combinaison de  ``useRef``(Accès rapide) et ``useState``(Rendu) pour stocker et modifier l'objet.
***
## Pourquoi utiliser ``useRef`` pour un objet mutable?
* __Pas de déclenchement de re-rendu__
* __Stocker une référence stable__
* __Accès toujours à jour__ : Les modifications faites à un objet dans ``useRef`` sont immédiatement accessibles dans toutes les fonctions et effets sans avoir à dépendre d'un tableau de dépendance! 
__On peut l'utiliser en coopération avec un useState pour avoir toujours les valeurs les plus récentes__.
***

## Quand est-ce une bonne pratique?
* __Données non liées au rendu__ : Des données qui ne sont pas directement utilisé pour l'affichage.
* __Amélioration des performances__: Éviter les re-rendus inutiles.
* __Partage entre différentes fonctions__: Si plusieurs parties de ton application doivent accéder ou modifier l'objet sans déclencher de re-rendu, en plus chaque partie peut toujours avoir des valeurs les plus récentes!
***

## Combiner ``useRef`` et ``useState`` pour manipuler et stocker la version la plus récente.
Pourquoi combiner ``useRef`` et ``useState`` ?
1. ``useRef``:
    * Garder une référence stable et instantanée à une valeur sans déclencher de re-render.
    * Parfait pour stocker et accéder à une valeur __la plus récente__ rapidement.
    * __Limitation__: Ne déclenche pas de re-render lorsque la valeur change.
2. ``useState``:
    * Déclenche un re-render du composant lorsque la valaeur change.
    * Parfait pour les valeurs utilisées dans le rendu de l'interface utilisateur
    * __Limitation__: Lors des mises à jour successives rapides, l'état peut ne pas refléter immédiatement la version la plus récente.
3. Combiner les deux
    * ``useRef`` pour conserver l'état le plus récent en temps réel.
    * ``useState`` pour déclencher un re-render quand c'est nécessaire.
4. Exemple
    ````JS
    const RefWithStateExample = () => {
      const [stateValue, setStateValue] = useState(0); // Pour déclencher un re-render
      const refValue = useRef(0); // Pour stocker la valeur la plus récente
    
      const updateValue = () => {
        const newValue = refValue.current + 1; // Calcule la nouvelle valeur
        refValue.current = newValue; // Met à jour la référence
        setStateValue(newValue); // Déclenche le re-render
      };
    ...
    ````

