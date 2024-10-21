# Général
***
## I - Comprendre toutes les attributs d'un JSON retourné par API.
C'est primordial de comprendre touts les attributs.
Faire un tableau excel, et lister touts les attributs, avec leurs influences correspondantes(Il **ne s'agit pas** simplement de les traduire en français).
Par exemple: 
* ``isMandatory`` : 
    * Faux: Marqué si c'est obligatoire
    * Vrai: Si cet attribut a une valeur vrai, on va mettre un couleur de fond rouge pour.

## II - Mise à jour de useState
``useState`` ne met pas à jour la valeur initiale lors des re-rendets.
Le hook ``useState`` ne prend en compte l'argument initial que **lors du premier rendu du composant**. Si l'argument initial change après l'initialisation, l'état restera figé à la valeur qu'il avait lors du premier rendu.

On résume:
1. ``useState`` est appelé une seule fois lors du premier rendu pour initialiser l'état. Une fois initialisé, **il ne réévalue pas** l'argument d'initialisation sur les rendus suivants.
2. Si tu fais un ``setState`` pour un autre état B, **ça ne forcera pas** la réinitialisation d'état A basé sur son argument initial.



