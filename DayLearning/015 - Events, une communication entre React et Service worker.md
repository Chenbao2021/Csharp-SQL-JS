Les __Service Workers__ et __React__ nécessitent des mécanismes spécifiques comme des signaux ou des canaux de communication pour dialoguer entre eux, car ils fonctionnent dans des environnements distincts et isolés.
***
# Pourquoi un Signal ou un Mécanisme de Communication est Nécessaire?
#### 1. Contexte d'exécution distinct
* __Service Worker__ s'exécute en arrière-plan, indépendamment des pages web ou composants React actifs. Il est conçu pour gérer des fonctionnalités comme la mise en cache, les notifications, ou la synchronisation en arrière-plan.
* __React__, en revanche, s'exécute dans le contexte de la page web (Dans le thread principal du navigateur).
* __Conséquence__: Ces deux environnements ne partagent pas d'accès direct à la mémoire ou aux variables, donc un mécanisme de communication explicite est requis.

#### 2. Architecture orientée événements
* Les Service Workers utilisent une architecture basée sur des événements(comme ``fetch``, ``push``, ou ``sync``), alors que React gère les composants via un cycle de rendu.
* React n'a pas une API native ou intégrée pour écouter directement les événements d'un Service Worker. Un signal explicite ou une gestion événementielle(``postMessage``, BroadcastChannel, etc.) est nécessaire.

#### 3. Sécurité du navigateur
* Le navigateur isole strictement les Service Workers pour éviter les accès directs et non sécurisés aux données critiques(Comme les cookis ou le DOM).
* Les signaux offrent un mécanisme contrôlé et sécurisé pour partager des données uniquement lorsque c'est explicitement nécessaire.
***
# Les Signaux Sont-Ils Peu Performants?
En générale, ces mécanismes sont __performants et bien adaptés aux besoins des PWA modernes.
#### 1. Faible surcharge des signaux comme ``postMessage``
* ``postMessage`` est optimisé par le navigateur et introduit une surcharge minimale. Cela fonctionne via la messagerie inter-thread, conçue pour être légère.

#### 2. BroadcastChannel est conçu pour le multitâche
* Si plusieurs onglets ou instances de l'application doivent communiquer avec un Service Worker, __BroadcastChannel__ est une solution performante.
* La charge est négligeable dans la plupart des cas, sauf si des données volumineuses sont échangées fréquemment.

#### 3. La communication est événementielle, non continue

#### 4. Les limites de performances potentielles
Les problèmes de performance ne proviennent pas des signaux eux-mêmes, mais plutôt:
* __De la taille des données transmises__: Envoyer de grandes quantités de données peut ralentir la communication.
* __De la fréquence des signaux__: Une communication excessive(100+ messages par secondes) peut introduire des délais.
* __De la gestion des états dans React__: Si chaque signal provoque un re-rendu coûteux dans React, cela peut affecter la fluidité.
***

# Exemple pratique
#### 1. Une fonction qui s'exécute côté SW lorsque un appel échoue, et signale l'échec à l'application React.
````JS
function notifyReactApp(data) {
    self.clients.matchAll({ includeUncontrolled: true }).then( (clients) => {
        clients.forEach( (client) => {
                    client.postMessage(data);
                }
            )
        }
    )
}
````
#### 2. Configuration dans React pour écouter le Service Worker
Dans l'application React, ajoutez un listener pour recevoir les messages du SW et mettre à jour le state ``isOnline``.
````JS
useEffetc(() => {
        if('serviceWorker' in navigator) {
            navigator.serviceWorker.ready.then( (registration) => {
                    navigator.serviceWorker.addEventListener('message', (event) => {
                            const data = event.data;
                            if(data && data.isOnline !== undefined) 
                                setIsOnline(data.isOnline);
                        }
                    )
                }
            )
        }
    } 
)
````
* ``navigator.serviceWorker.addEventListener``: __Écouter les messages provenant du Service Worker__

#### 3. ``navigator.serviceWorker.addEventListener`` et ``self``
1. Les messages envoyés depuis le Service Worker doivent être reçus par la page principale via ``navigator.serviceWorker.addEventListener``.
2. Inversement, l'application principale peut envoyer des messages au Service Worker via ``navigator.serviceWorker.controller.postMessage``.



