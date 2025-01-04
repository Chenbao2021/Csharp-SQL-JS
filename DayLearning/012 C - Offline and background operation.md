## Offline and background operation
Usually without network connectivety, most websites are just unusable.
But PWA enable to :
* Provide a good user experience even when the device has intermitten network connectivity.
* Update its state when the app is not running
* Notify the user about important event that have happened while the app was not running.
***

## Websites and workers
* A __service worker__ run in a separate thread to the main in the background.
* The worker and the main code can't directly access each other's state, but can communicate by sending each other messages.
* So a PWA always has a high level architecture split between:
    * The _main app_, with the HTML, CSS, and the part of the JavaScript that implements the app's UI(by handling user events, for example).
    * The _service worker_, which handles offline and background tasks.
***

## Offline operation
When the __service worker__ is installed, it can fetch the resources from the server for the pages it controls(Including pages, styles, scripts, and images, for example) and add them to a local cache.(``WorkerGlobalScope.caches``)

One way a service worker can handle requests is a __"cache-first"__ strategy. In this strategy:
* If the requested resource exists in the cache, get the resource from the cache and return the resource to the app.
* I the requested resource does not exist in the cache, try to fetch the resource from the network.
    * If the resource could be fetched, add the resource to the cache for next time, and return the resource to the App.
    * If the resource could not be fetched, return some default fallback resource.

The following code sample shows an implementation fo this:
````JS
const putInCache = async (request, response) => {
    const cache = await caches.open("v1") // v1 est le nom de cache, créer si non existe.
    await cache.put(request, response); // request serviras comme clé.
}

const cacheFirst = async ({request, fallbackUrl}) => {
    // First try to get the resource from the cache.
    const responseFromCache = await caches.match(request);
    if(responseFromCache)
        return responseFromCache;
    
    // Secondly, try to get the resource from the networl
    Try {
        const responseFromNetwork = await fetch(request);
        // If the network request succeeded, clone the response:
        // - Put one copy in the cache, for the next time
        // - return the original to the app.
        // Cloning is needed because a response can only be consumed once.
        putInCache(request, responseFromNetwork.clone())
        return responseFromNetwork;
    } catch(error) {
        // If the network request failed,
        // get the fallback response from the cache
        const fallbackResponse = await caches.match(fallbackUrl);
        if(fallbackResponse) 
            return fallbackResponse
        // Si fallback a aussi echoué, retourne un erreur
    }
}

self.addEventListener("fetch", (event) => {
  event.respondWith(
    cacheFirst({
        request: event.request,
        fallbackUrl: "/fallback.html"
    })
  )
})
````

## Background sync(Use for short operations)
Suppose a user compose an email and presses "Send". In a traditional website, they must keep the tab open until the app has sent the email.
Background sync enables the PWA to ask its service worker to perform a task on its behalf. As soon as the device has network connectivity, the browser will restart the service worker.(Retry a limited number of times)

* Registering a sync event
    ````js
    async function registerSync() {
        const swRegistration = await navigator.serviceWorker.ready;
        swRegistration.sync.register("send-message");
    }
    ````
* Handling a sync event
    As soon as the device has network connectivity, the ``sync`` event fires in the service worker scope. The service worker checks the name of the task and run the appropriate function, ``sendMessage()`` in this case:
    ````js
    self.addEventListener("sync", (event) => {
        if(event.tag === "send-message") {
            event.waitUntil(sendMessage());
        }
    })
    ````
    * The ``waitUntil()`` method takes a ``Promise`` as a parameter and asks the browser not to stop the service worker until the promise has settled. If the operation take too long, the service worker will be stopped anyway.

## Backgroudn fetch (Use for long operations)
With background fetch:
* The request is initiated from the main app UI.
* Whether or not the main app is open, the browser displays a persistent UI element that notifies the user about the ongoing request, and enables them to cancel it or check its progress.
* When the request is completed with success or failure, or the user has asked to check the request's progress, then the browser starts the service worker(if necessary) and fires the appropriate event in the service worker's scope.

Making a background fetch request
* A background fetch request is initiated in the main app code, by calling ``backgroundFetch.fetch()`` on the ``ServiceWorkerRegistration`` object, like this:
    ````js
    const swRegistration = await navigator.serviceWorker.ready;
    const fetchRegistration = await swRegistration.backgroundFetch.fetch(
        "download-movie",
        ["/my-movie-part-1.webm", "/my-movie-part-2.webm"],
        {
          icons: movieIcons,
          title: "Downloading my movie",
          downloadTotal: 60 * 1024 * 1024,
        },
    );
    ````
    * ``download-movie``: An identifier for this fetch request
    * An array of ``Request`` objects or URLs. A single background fetch request can include multiple network requests.
    * An object containing data for the UI that the browser uses to show the existence and progress of the request. (``icons`` and ``title`` will be used)

Handling request outcomes
* ``backgroundfetchsuccess``: All requests were successful
* ``backgroundfetchfail``: At least one request failed
* ``backgroundfetchabort``: The fetch was canceled by the user or by the main app.
* ``backgroundfetchclick``: The user clicked on the progress UI element that the browser is showing.

Retrieving response data
* In the handlers for the ``backgroundfetchsuccess``, ``backgroundfetchfail``, and ``backgroundfetchabort`` events, the service worker can retrieve the request and response data.
    ````
    self.addEventListener("backgroundfetchsuccess", (event) => { 
        const registration = event.registration;
        event.waitUntil(async () => {
            const registration = event.registration;
            const record = await registration.matchAll();
            const responsePromise = records.map(
                async (record) => await record.responseReady;
            )
        })
    })
    const response = Promise.all(responsePromises);
    ````
    * ``match()`` or ``matchAll()`` return BackgroundFetchRecord objects, and each of them has a ``responseReady`` property that is a ``Promise`` which resolve with the ``response``, once the resonse is available.
***

## Push
The push API enables a PWA to receive messages pushed from the server, whether the app is running or not.
A common use case : A chat app, when the user receives a message from on of their contacts, it is delivered as a push message and the app shows a notification.

#### Subscribing to push messages
À revoir : https://developer.mozilla.org/en-US/docs/Web/Progressive_web_apps/Guides/Offline_and_background_operation
## Permissions and restrictions
Aussi. certains APIs nécesittent activer les paramètres du navigateur.




