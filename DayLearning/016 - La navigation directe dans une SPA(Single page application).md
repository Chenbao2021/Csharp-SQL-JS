Dans une SPA, par exemple React, les routes côté client sont gérés par React Router. 
Par exemple, si l'application est déployé en prod sur "exemple.com", alors avec React Router, on peut voir des urls comme "exemple.com/men/julien", ou "exemple.com/men/david". Mais sans configuration spécifique, on ne peut pas accéder directement "exemple.com/men/julien" par la barre de recherche directe, ce qui est parfois demandé.

***
# I - Principe derrière
Dans React, la gestion des URLs se fait côté __client__, c'est à dire que ces ressources n'existent pas vraiment en dur dans le serveur(Ce ne sont que des codes js/components react). Si on tente d'accéder directement via la barre d'adresse du navigateur, le __serveur__ retourne ``404``.`

##### Comment résoudre ce problème?
Pour que la navigation directe fonctionne, il faut mettre en place un système de fallback côté serveur.
L'idée est simple: Si le serveur ne trouve pas de fichier correspondant à l'URL demandée, il retourne systématiquement la page ``index.html``. Ensuite, une fois le bundle JS chargé, React Router peut interpréter l'URL et afficher la vue appropriée.

##### Pourquoi faut gérer ceci en côté serveur?
Malgré React Router s'occupe de la logique de navigation une fois l'application chargée dans le navigateur, le premier accès à une URL s'effectue toujours côté serveur. 
1. __Processus de requête intiiale__:
    Lorsque l'utilisateur saisit une URL dans la barre d'adresse et appuie sur Entrée, son navigateur envoie une requête HTTP au serveur pour cette route spécifique. À ce stade, le code JS du client (Votre application React) n'est pas encore chargé ni exécuté.
2. __Serveur statique__ vs __Route dynamique__  
    Dans une SPA, il n'existe généralement qu'un seul point d'entrée: le fichier ``index.html``.  Toutes les "routes" gérées par votre application sont en réalité gérées par du JavaScript exécuté __après__ le chargement du ``index.html``.
    Si le serveur n'est pas configuré pour renvoyer ``index.html`` pour les routes inconnues, il essaiera de trouver un fichier à l'URL demandée, qui n'existe pas dans le serveur.
3. __Faire intervenir la logique du routeur côté client__
    L'objectif est donc quelque soit la route demandé (excepté pour le fichiers statiques(CSS, images, JS, etc.)),on renvoie ``index.html``.
    Et une fois que le navigateur reçoit ``index.html`` et que le JS est chargé, React Router peut lire l'URL, la comparer aux définitions de routes de l'application, et afficher le composant correspondant. (Tout est fait automatiquement sans code à écrire si on a configuré avec ``BrowserRouter`` et ``Route`` de React Router).
***

# II - __PWA__ : Un ``serveur`` local qui retourner ``index.html``
Le Service Worker agit comme un proxy entre votre application et le réseau, donc il peut intercepter tes requêtes, y comprit les requêtes de navigate, c'est à dire de renvoyer le point entré sauvegardé en cache directement pour toutes les routes dynamiques afin d'éviter 404.

##### Mettre en place
Avec workbox, on peut configurer une stratégie dans le Service Worker pour renvoyer ``index.html`` depuis le cache lors de requêtes de navigation. L'idée est de transformer votre application en une SPA offline-first, où l'``index.html`` joue le rôle de shell de l'application.
* __Utilisez ``createHandlerBoundToUrl``__ de Workbox
    Workbox fournit l'utilitaire ``createHandlerBoundToURL`` qui retourne un handler renvoyant le fichier spécifié(ici ``index.html``) depuis le cache de précache.
    ````js
    // Pour toutes les requêtes de navigation (c'est-à-dire les requêtes qui aboutissent normalement
    // à un document HTML), on renvoie index.html depuis le cache, que l'utilisateur soit en ligne ou non.
    registerRoute(
      ({ request }) => request.mode === 'navigate',
      createHandlerBoundToURL('/index.html')
    );
    ````

##### Utilisation simultanné de ``registerRoute`` et ``addEventListener('fetch', ...)``
Faut être prudent
1. __Priorité et enchaînement des handlers__:
    Workbox, via ``registerRoute``, ajoute en interne un écouter sur l'événement ``fetch``. Lorsque vous utilisez ``registerRoute``, chaque requête interceptée par le Service Worker __passe d'abord par le routage Workbox__.Si une route correspond, Workbox appelle ``event.respondWidth()``.
    Si on ajoute un autre ``addEventListener('fetch', ...)`` séparé, il est appelés, mais __une seule réponse__ peut être fournie par requête(via ``event.respondWith()``). Si Workbox a déjà repondu à la requête, votre handler personnalisé n'a plus la main pour modifier cette réponse, et inversement.

***
# Autres points
##### I - ``injectManifest`` ne vous limite pas dans l'usage des outils Workbox.
L'option ``injectManifest`` offre simplement plus de contrôle sur le service worker en vous laissant écrire manuellement les codes, plutôt que de générer automatiquement un Service Worker complet avec ``generateSW``.

##### II - Configurations pour mettre en cache de ``index.html``: En prod et en dev
``vite-plugin-pwa`` utilise deux modes , soit ``generateSW``, soit ``injectManifest``.
* Lorsqu'on utilise ``generateSW``: On pass les options de configuration(dont ``globPatterns``) à la proprieté ``workbox`` dans la config du plugin ``vite-plugin-pwa``.
    ````js
    workbox: {
        // Ajoutez un pattern incluant .html
        globPatterns: ['**/*.{js,css,html,ico,png,svg}']
     }
    ````
* Lorsqu'on utilise ``injectManifest``: On pass nos options(dont ``globPatterns``) à la propriété ``injectManifest`` de la config du plugin.
    ````js
    injectManifest: {
		globPatterns: ['**/*.{js,css,html,svg,png,ico,ttf}'],
	},
    ````
    Et puis, dans votre Service Worker source, on doit avoir:
    ````js
    import { precacheAndRoute } from 'workbox-precaching';
    // self.__WB_MANIFEST sera remplacé lors du build par le tableau des fichiers à précacher
    precacheAndRoute(self.__WB_MANIFEST);
    ````

Aussi une différence subtile, c'est qu'il fonctionne différemment entre dev et prod:
* En __dev(vite dev)__, les fichiers ne sont pas réellement "buildés", ni précachés. Le Service Worker injecté par Workbox ne contient pas encore toutes les ressources finales car le build n'a pas été fait.
    Le serveur de développement fournit les fichiers __à la volée__ et n'exécute pas les étapes de précache.
* En __prod(vite build)__, Le build génére tous les fichiers optimisés dans ``dist``. À ce moment-là, ``injectManifest`` utilise ``globPatterns`` et ``globDirectory`` pour inclure ``index.html`` et les autres ressources dans le manifest. Le Service Worker résultant est complet et, lors du déploiement, les ressources sont précachées.










