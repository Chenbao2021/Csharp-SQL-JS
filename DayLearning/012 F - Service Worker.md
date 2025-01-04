Dans un projet __React avec vite__, l'utilisation de ``self.addEventListener`` pour des événements comme ``install``, ``activate``, ou ``fetch`` est toujours nécessaire __si vous écrivez un Service Worker personnalisé__,car ces événements sont spécifiques au comportement des Service Workers et non liés directement à React.

Mais avec ``vite-plugin-pwa``, une grande partie de cette gestion est automatisée, et on n'a pas toujours besoin de manipuler directemet les événements comme ``install`` ou ``activate``. 
***

# I - Qu'est ce qu'un Service Worker
Un __Service Worker__ est un script JavaScript qui agit comme un __intermédiaire programmable entre votre application web et le réseau__. 
Il est exécuté en arrière-plan par le navigateur, séparément de l'interface utilisateur, et permet d'améliorer l'expérience utilisateur en fournissant des fonctionnalités comme:
1. __Mise en cache des ressources__ (Pour rendre les applications accessibles hors ligne, manuellement ou dynamiquement).
2. __Intercepter les requêtes réseau__ (Via l'événement ``fetch``).
3. __Gestion avancée des requêtes réseau__ (Ex: Stratégie "Cache First" ou "Network First").
4. __Notifications Push__ (Envoyées par le serveur même lorsque l'application est fermée).
5. __Synchronisation en arrière-plan__ (Mise à jour des données lorsque le réseau est indisponible).
***

# II - Comment le Service Worker fonctionne (Cycle de vie)
1. __Enregistrement__(Automatique, fait si nécessaire):
    * Lorsqu'un Service Worker est enregistré via ``navigator.serviceWorker.register``, le navigateur télécharge le script et commence à l'installer.
        ````ts
        navigator.serviceWorker.register('/sw.js')
          .then((registration) => {
            console.log('Service Worker registered with scope:', registration.scope);
          })
          .catch((error) => {
            console.error('Service Worker registration failed:', error);
          });
        ````
    * Le fichier ``sw.js`` sera généré par l'option ``generateSW``(par défaut) dans le répertoire ``/dist`` lors de ``npm run build``.
2. __installation(``install``)__(Automatique par SW):
    * L'événement ``install`` est déclenché lorsque le Service Worker est téléchargé pour la première fois.
    * C'est le moment où les fichiers essentiels(HTML, CSS, JS, etc.) son précachés.
3. __Activation(``activate``)__(Automatique par SW):
    * Une fois installé, le Service Worker devient actif et prend le contrôle des pages dans son scope.
    * C'est le moment idéal pour nettoyer les anciens caches.
4. __Interception des requêtes(``fetch``)__:
    * Le Service Worker peut intercepter les requêtes réseau via l'événement ``fetch``.
    * Cela permet de servir des ressources depuis le cache ou de définir des stratégies de récupérationr réseau.
    ````ts
    self.addEventListener("fetch", (event) => {
        event.respondWith(
            caches.match(event.request).then((response) => {
              return response || fetch(event.request);  
            })
        )
    })
    ````

# III - L'événement ``fetch``
L'événement ``fetch`` du Service Worker intercepte __toutes les requêtes réseau__ effectuées par la page dans son scopr(Celles correspondant aux URLs gérées par le Service Worker).

Le Service Worker peut __distinguer les différents types de requêtes__ grâce à plusieurs techniques, notamment:
#### 1. Identifier les types de requêtes avec ``event.request``
L'objet ``event.request``, passé dans le gestionnaire ``fetch``, contient des informations détaillées sur la requête interceptée. Ces informations permettent de distinguer et de gérer différemment les appels API ou autre types de requêtes.
1. URL de la requête(``event.request.url``)
2. Méthode HTTP(``event.request.method``)
3. Destination(``event.request.destination``)
    Indique le type de ressource demandée.
4. Headers(``event.request.headers``)

# IV - Quelques proprietés à connaître pour VitePWA dans vite.config.ts
* ``navigateFallback``: En mode offline, pour un url qui n'est pas sauvegardé en cache, on réoriente vers l'url renseigné.
    Par exemple, pour tous les fichiers dans le répertoire public, on peut l'accéder par "/...."
* ``InjectRegister``: C'est l'attribut qui détermine si on démarre un service worker ou pas.
    * ``false``: Si on met à false, alors on doit démarrer le service worker manuellement.
    * ``auto``: Depend on whether you used the ``import {registerSW} from 'virtual:pwa-register'`` it will do nothing or use the ``script`` mode.
    * ``script``: Inject ``<script/>`` in ``<head>`` with src attribute to a generated script to register the service worker.
* ``includeAssets``: Fichiers à mettre dans cache.
* ``manifest``: Les metadatas de l'application.
* ``wokbox``: Utilisé pour personnaliser le comportement du __Service Worker__ généré par __Workbox__, qui est une bibliothèque puissante qui aide à gérer le caching, les strztgies réseau, et d'autres fonctionnalités pour les PWAs.
    * ``globalPatterns``: Workbox utilise ce pattern pour rechercher les fichiers dans votre répertoire de distribution: ``globPatterns: ['**/*.{js,css,html,svg,png,ico,ttf}'],``
    * ``runtimeCaching``: Définit des stratégies de mise en cache pour les requêtes réseau dynamiques(runtime).
        * ``urlPattern``: Spécifie un pattern d'URL(expression réulière) pour les requêtes réseau que Workbox doit intercepter.
        * ``handler``: Définit la stratégie de mise en cache pour ces requêtes:
            * ``NetworkFirst``
            * ``CacheFirst``
            * ``StaleWhileRevalidate``: Renvoie la ressource depuis la cache immédiatement, tout en actualisant le cache en arrière-plan.


