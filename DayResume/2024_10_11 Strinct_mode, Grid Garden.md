# React
***
## I - Strict mode
````JS
<React.StrictMode>
    ...
</React.StrictMode>
````
``React.StrictMode`` est un composant de dévéloppement dans React qui aide à identifier les problèmes potentiels dans un application.
Il est principalement utilisé pour faciliter le dévéloppement en fournissant des outils de débogage et en signalant les pratiques de codage problématiques:
1. ``Identification des comportements Obsolètes``: Il vérifie si l'application utilise des fonctionnalités obsolètes qui pourraient être supprimées dans une future version de React. Cela inclut des fonctionnalités qui ne sont plus recommandées ou qui ont un impact négatif sur les performances.
2. ``Vérification des Effets de type Side Effects``: ``StrictMode`` force l'exécution des hooks d'effet(``useEffect``, ``useLayoutEffect``) deux fois lors du développement(mais pas en production).Cela permet de détecter des effets secondaires inattendus et de rendre votre code plus prévisible.

## II - span pour grid
``grid-column-end: span 5`` : éteindre la gride sur 5 colonne.
``grid-column: 2/span 3``: commence par 2, et s'éteint sur 3 colonne.

