# workbox et injectManifest
On ne peut pas utiliser les deux en même temps, c'est contradictoire. 

``ìnjectManifest`` et ``workbox`` des configurations du plugun ``vite-plugin-pwa``.
* ``workbox``: L'option qui utilise la méthode ``generateSW`` de ``workbox-build`` pour créer automatiquement le service worker.
    * Service worker est automatiquement geré par nos configurations.
    * Inclut des stratégies par défaut pour certaines fonctionnalités comme ``runtimeCaching``.
    * Moins de contrôle direct, beaucoup des configurations automatiques.
* ``injectManifest``: Avec cette option, on peut personnaliser notre propre fichier sw.js, une partie voire la totalité, selon nos besoins.

Exemples:
* ``workbox``:
    ````js
    workbox: {
		globPatterns: ['**/*.{js,css,html,svg,png,ico,ttf}'],
		cleanupOutdatedCaches: true,
		clientsClaim: true,
		maximumFileSizeToCacheInBytes: 5 * 1024 * 1024, // Limite de 5 MiB
		navigateFallback: '/index.html',
		runtimeCaching: [
			{
				urlPattern: /^https?:\/\/localhost:7239\/.*/,
				handler: 'NetworkFirst',
				options: {
					cacheName: 'inspectare-cache',
					expiration: {
						maxEntries: 500,
						maxAgeSeconds: 7 * 60 * 60 * 24,
					},
					// networkTimeoutSeconds: 10,
				},
			}
		],
	},
    ````
* ``injectManifest``:
    ````js
	strategies: 'injectManifest',
	srcDir: 'src',
	filename: 'sw.js',
	injectManifest: {
		// swSrc: './src/sw.js',
		swDest: 'dist/sw.js',
		maximumFileSizeToCacheInBytes: 5 * 1024 * 1024, // Limite de 5 MiB,
		globPatterns: ['**/*.{js,css,html,svg,png,ico,ttf}'],
	},
    ````
    * Toutes les proprietés sont importants!
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

Donc, avec precacheAndRoute, on a plus besoin d'écrire les codes pour mettre les fichiers en cache manuellement!