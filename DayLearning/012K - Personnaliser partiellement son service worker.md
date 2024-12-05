# I - Les configurations
##### 1. Activer le mode ``injectManifest``
Par défaut, le service worker est en mode "generateSW".
Donc si on veut personnaliser notre service worker pour qu'il puisse sauvegarder des données dans indexedDB.
On doit mettre ceci dans le VitePWA:
````js
{
    ...
    strategies: 'injectManifest',
    ...
}
````
##### 2. Préciser où se trouve ton fichier sw.js.
On doit préciser où se trouve notre fichier sw.js personnalisé: 
````
// Le nom du repertoire depuis la racine.
srcDir: 'src'
filename: 'sw.js',
````
##### 3. Configurer ton proprieté ``injectManifest``.
C'est une proprietés qu'on a besoin de mettre en place qu'en mode ``injectManifest``.
injectManifest: {
    // Où est ce que tu veux mettre ton sw.js généré.
    swDest: 'dist/sw.js',
    // Taille max pour précacher un fichier (en octets)
    maximumFileSizeToCacheInBytes: 5 * 1024 * 1024
    // Motif pour les fichiers à précacher dans ``self.__WB_MANIFEST``
    globPatterns: ['**/*.{js,css,html,svg,png,ico,ttf}']
    // Motifs des fichiers à ignorer
    globIgnores: ['assets/ignore/**']
    // Modifier les chemins des fichiers précachés, permet de remplacer des préfixes d'URL dans le cache.
    modifyURLPrefix: {
        'dist/': '',
    }
}

# II - Créer ton sw.js personnalisé.
##### 1. ``precacheAndRoute``: Imports et Précache
````js
import { precacheAndRoute } from 'workbox-precaching'
import { registerRoute } from 'workbox-routing'
import { NetworkFirst } from 'workbox-strategies';
import { ExpirationPlugin } from 'workbox-expiration';

precacheAndRoute(self.__WB_MANIFEST)
````
* ``precacheAndRoute``: Fonction de Workbox pour gérer le pré-cache des ressources statiques générées lors de la construction de l'application.
* ``precacheAndRoute(self.__WB_MANIFEST)``:
    * Précache toutes les ressources listées dans ``self.__WB_MANIFEST``, qui est généré automatiquement par ``vite-plugin-pwa`` lors du build.
    * Configure également une route pour service ces ressources depuis le cache. (``install`` et ``activate``).

##### 2. ``registerRoute``: Enregistrement d'une Route personnalisée avec Workbox
````
registerRoute(
	// Filtrer uniquement les requêtes sur localhost et port 7239
	({ url }) => url.hostname === 'localhost' && url.port === '7239',
	new NetworkFirst({
		cacheName: 'inspectare-cache', // Nom du cache
		plugins: [
			new ExpirationPlugin({
				maxEntries: 500, // Nombre maximal d'entrées en cache
				maxAgeSeconds: 7 * 24 * 60 * 60, // Durée de vie des éléments : 7 jours
			}),
		],
	})
);
````
* ``registerRoute``:
    * __Filtre__: Sélectionne uniquement les requêtes dont le nom d'hôte est ``localhost`` et le port est ``7239``.
    * __Stratégie__: Utilise ``NetworkFirst``, ce qui signifie que le service worker tentera d'abord de récupérer la ressource depuis le réseau. Si cela échoue, il servira la ressource depuis la cache.
    * __Configuration du Cache__:
        * ``cacheName``: .
        * ``ExpirationPlugin``: .

##### 3. Analyse des Potentiels Conflits entre ``registerRoute`` et l'écouteur ``fetch`` personnalisé.
1. __Ordre de Traitement des Requêtes__
    * __Workbox__: Utilise l'événement ``fetch`` pour gérer les routes enregistrées via ``registerRoute``.
    * __Écouter__: Gestionnaire supplémentaire pour les même types de requêtes.
2. __Stratégies de Mise en Cache Différentes__
    * ``NetworkFirst`` avec le Cache API.
    * Manuellement la mise en cache en enregistrant les données dans IndexedDB.
    
    Deux mécanismes de mise en cache distincts pour les mêmes ressources, ce qui peut compliquer la gestion et l'inspection des données mises en cache.
3. __Performance et Complexité__
    Car on gére les deux fetchs en même temps.


