***
# precacheAndRoute
````js
import { precacheAndRoute } from 'workbox-precaching';
precacheAndRoute(self.__WB_MANIFEST);
self.addEventListener('fetch', (event) => {
    ...
})
````

##### À quoi sert ``precacheAndRoute``?
* __Pré-caching des ressources__: Il va prendre la liste des ressources générées lors de la build et les ajouter au cache du navigateur lors de l'installation du service worker.
* __Gestion des requêtes__ : Configure automatiquement un gestionnaire d'événements ``fetch`` qui intercepte les requêtes réseau pour les ressources pré-cachées et les sert depuis le cache.
* __precacheAndRoute__ gère aussi les étapes __install__ et __activate__

Donc, avec precacheAndRoute, on a plus besoin d'écrire les codes pour mettre les fichiers en cache manuellement!

##### ``precacheAndRoute`` et ``globPatterns``, qui s'occupe de mettre en cache?
``globPatterns``:
* Définit quels fichiers seront ajoutés au pré-cache lors du build.
* Produit(Crée) une liste de fichiers sous forme de ``self.__WB_MANIFEST``

``precacheAndRoute``:
* Consomme la liste générée par ``self.__WB_MANIFEST``.
* Télécharge et stocke ces fichiers en cache.
* Intercepte les requêtes pour ces fichiers et les sert depuis le cache.

Donc c'est ``globPattenrs`` qui prépare les donnée, puis ``precacheAndRoute`` qui la mise en cache réellement lors de l'installation du service worker.

# registerRoute
* __Objectif__: Permet d'ajouter des règles dynamiques de mise en cache ou de gestion des requêtes réseau __personnalisées__, comme celles qui ciblent un port spécifique.
* __Fonctionnement__: Vous définissez des règles avec une condition(comme un filtre par URL, port, méthode HTTP, etc.) et une stratégie(comme ``NetworkFirst``, ``CacheFirst``, etc.).
* __Utilisation__: Requis pour les cas où on doit gérer __manuellement__ des requêtes qui ne sont pas couvertes par le pré-caching. (Appels API, requêtes réseau spécifiques, etc.)

##### Mélanger ``fetch`` et ``registerRoute``
On peut combiner ces deux méthodes, selon les cas:
* Utiliser ``registerRoute`` pour les requêtes générales.(Sauvegarder dans cache storage)
* Utiliser ``fetch`` pour des besoins plus spécifiques(comme des logs ou des actons personnalisées)

