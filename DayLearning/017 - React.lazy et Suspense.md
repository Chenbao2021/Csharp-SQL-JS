__Lazy loading__, involves loading components or resources only when needed, either in response to a user action or when elements are about to be displayed on the screen:

To use lazy load from __React__ you will need to use __React.Suspense__, any component that needs to be lazy_loaded must be wrapped around with a Suspense.
* Suspense helps to provide a fallback for your lazy component.
* Can appear anywhere above the lazy component.
* Single suspense can be used for multiple lazy components.
* ````js
    const Quotes = lazy(() => import("./Quotes"))
    export default function() {
        return (
            <div>
                <Suspense fallback="Loading...">
                    <Quotes />
                </Suspense>
            </div>
        )
    }
    ````

### 1. Code Splitting Basé sur les Routes.
Imaginez une application de commerce électronique avec de nombreuses pages telles que __l'accueil__, __les produits__, __les détails du produit__, etc. Charger tous ces composants au démarrages augmenterait considérablement la taille du bundle initial + ralentir le temps de chargement de l'application.

=> On peut utiliser React.lazy pour charger les composants des routes uniquement lorsqu'un utilisateur navigue vers ces routes spécifiques.
* ``ErrorBoundary``: Caoturez les erreurs potentielles lors du chargement paresseux des composants pour éviter que l'application ne plante.

### 2. Chargement Paresseux de Modaux ou Fenêtres Contextuelles.
Supposons que votre application dispose de plusieurs modaux tels que __Ajouter au Panier__, __Connexion__, __Inscription__, __Aide__, etc.
Ces modaux ne sont pas visible par défaut et ne sont activés que suite à certaines interactions utilisateur.





***
En React, __Suspense__ est une fonctionnalité puissante pour simplifier:
* La gestion de l'attente et du chargement de données ou de ressources dans une application.
* Rendre l'interface utilisateur plus fluide en affichant des états de chargements de __manière déclarative__.


# Le principe de Suspense
Suspense agit comme un mécanisme qui suspend le redu d'un composant jusqu'à ce que des données ou des ressources soient prêtes.
Pendant que ces données ou ressources se chargent, React peut afficher un indicateur de chargement, tel qu'un spinner ou un message d'attente.

__Cela évite d'avoir à gérer manuellement des états de chargement dans vos composants__

structure basique: 
````js
import React, { Suspense } from 'react';

const LazyComponent = React.lazy(() => import('./LazyComponent'));

function App() {
  return (
    <Suspense fallback={<div>Chargement...</div>}>
      <LazyComponent />
    </Suspense>
  );
}
````
* ``React.lazy``: Permet de charger un composant de façon dynamique
* ``Suspense``: Permet d'attendre que ``LazyComponent`` soit prêt avant de l'afficher.
* ``fallback``: Définit ce qui doit être affiché pendant que le composant ou les données se chargent.

##### Limites
1. __Non conçu pour tout gérer__:
    * Suspense est parfait pour des __besoins simples__ comme lazy loading ou le chargement des données.
    * Mais il ne gère pas des états intermédiaires ou des dépendances multiples.
2. __Support limité avec des API personnalisées__:
    * Les promesses standards(``fetch``, ``axios``) ne sont pas directement compatibles avec Suspense.
    * On doit créer des abstractions supplémentaires pour permettre à Suspense de suspendre le rendu.
