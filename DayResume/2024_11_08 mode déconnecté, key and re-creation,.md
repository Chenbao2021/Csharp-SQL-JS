# Général
***
## I - Penser en mode déconnecté
Construire une application qui peut fonctionner en mode deconnecté, c'est de réaliser les logiques de calculation dans l'application. 
Contre exemple: Laisser tous les logiques en back-end, et en front-end on fait juste des appel APIs.

## II - key and re-creation
##### Pourquoi ``key={index}`` est une Erreur Courante
1. __Ajout d'un nouvel élément__: Si on a joute un élément au début de la liste, __tous les indices suivants changent__ (1 devient 2, 2 devient 3, etc.). Cela signifie que les clés de chaque élément de la liste sont modifiées, ce qui pousse React à recréer tous les composants de la liste, même si seuls certains ont réellement changé.
2. 