# workbox et injectmanifest
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
***
# ``injectManifest``:
[src](https://vite-pwa-org.netlify.app/guide/inject-manifest.html)
#### ``injectManifest``: What for
With this service worker ``strategy``, you can build your own service worker.

#### Location : "/public/sw.js"
By default, the plugin will assume the ``service worker`` source code is located at the ``Vite's public`` folder with the name ``sw.js``: ``/public/sw.js``.
If you want to change the location or the service worker name, you will need to change the following plugin options:
* ``srcDir``: __must__ be relative to the project root folder
* ``filename``: Including the fixe extension and must be relative to the ``srcDir`` folder.

#### Plugin Configuration
You __must__ configure ``strategies:'injectManifest'`` in ``vide-plugin-pwa`` plugin options in your ``vite.config.ts`` file:
``VitePWA({ strategies: 'injectManifest' })``

#### Service Worker Code
1. Your custom service worker shoudl have at least this code(``workbox-precaching`` as ``dev dependency``).
    ````js
    import {precacheAndRoute} from 'workbox-precaching'
    precacheAndRoute(self.__WB_MANIFEST)
    ````
    If you are not using ``precaching(self.__WB_MANIFEST), you need to disable ``injectionPoint`` to avoid compilation errors:
    ````js
    injectManifest: {
        injectionPoint: undefined
    }
    ````
2. You __must__ include in your service worker code at least this code(``workbox-core`` as ``dev dependency``)
    ````js
    import {clientsClaim} from 'workbox-core'
    self.skipWaiting();
    clientsClaim();
    ````
    * ``self.skipWaiting()``: Assure que le nouveau Service Worker s'active sans délai.
    * ``clientsClaim()``: Veille à ce que les pages actuellement ouvertes utilisent aussitôt ce nouveau Service Worker.

#### Cleanup Outdated Caches
When the user installs the new version of the application, we will have on the service worker cache all new assets and also the old ones. To delete old assets automatically, you will need to add the following code to your custom service worker:
````js
import { cleanupOutdatedCaches, precacheAndRoute } from 'workbox-precaching'
cleanupOutdatedCaches()

precacheAndRoute(self.__WB_MANIFEST)
````




