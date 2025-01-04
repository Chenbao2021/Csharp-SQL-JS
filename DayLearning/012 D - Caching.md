# Caching
***
There are two main benefits to caching resources locally: 
* __Offline operation__: Caching enables a PWA to function to a greater or lesser extent while the device does not have network connectivity.
* __Responsiveness__: Even if the device is online, a PWA will usually be much more responsive if its user interface is fetched from the cache, rather than the network.
***

# Caching technology overview
#### Fetch API
The ``fetch()`` function takes a ``request`` or a URL as an argument, and returns a ``Promise`` that resolves to a ``Response``

The ``fetch()`` function is available to service workers as zell as to the main app thread.
#### Service Worker API
A service worker is a part of a PWA: It's a separate script that runs in its own thread, separate from the app's main thread.
And it can intercepts the main app request and return a customized ``response``

#### Cache API
The ``Cache`` interface provides persistent storage for ``Request``/``Response`` pairs:
* Methods to add and delete pairs
* Look up a cached pairs

The cache is available in both the main app thread and the service worker.
***

## When to cache resources
À revoir

***

## Caching strategies
##### Cache first - Once a response is in the cache, it is never refreshed until a new version.
* For the precached resources, we will:
    * Look in the cache for the resource, and return the resource if it is found.
    * Otherwise, go to the network. If the network request succeeds, cache the resource for next time.
* For all other resources, we will always go to the network.

Precaching is an appropriate strategy for resources that the PWA is certain to need, that will not change for this version of the app:
* The basic user interface of the app.

If this is precached, the app's UI can be rendered on launch without needing any network requests.
````js
async function precache() {
    const cache = await caches.open("MyCache_1");
    return cache.addAll(["/", "/app.js", "/style.css"]);
}

self.addEventListener("install", (event) => {
    event.waitUntil(precache());
})
````

##### Cache first with cache refresh - Always send the request to the network, even cache hit.
For this solution, we get the responsiveness of "cache first", but get a fairly fresh response(as long as the request is made reasonably often).
````js
function isCacheable(request) {
    const url = new URL(request.url);
    return !url.pathname.endsWith(".json");
}

async function cacheFirstWithRefresh(request) {
    const fetchResponsePromise = fetch(request).then(async (networkResponse) => {
        if(networkResponse.ok) {
            const cache = await caches.open("MyCache_1");
            cache.put(request, networkResponse.clone());
        }
        return networkResponse;
    }
    
    return (await caches.match(request)) || (await fetchResponsePromise);
}

self.addEventListener("fresh", (event) => {
  if(isCacheable(event.request)) {
    event.respondWith(cacheFirstWithRefresh(event.request);
  }
})
````
* We update the cache asynchronously, so the app does not have to wait for the network response to be received before it can use the cached response.

##### Network first
We try to retrieve the resource from the network:
* Success: Return the response and update the cache.
* Fails: Try the cache.

````js
async function networkFirst(request) {
    try {
        const networkResponse = await fetch(request);
        if(networkResponse.ok) {
            const cache = await caches.open("MyCache_1");
            cache.put(request, networkResponse.clone());
        }
        return networkResponse;
    } catch(error) {
        const cachedResponse = await caches.match(request);
        return cachedResponse || Response.error();
    }
}

self.addEventListener("fresh", (event) => {
   const url = new URL(event.request.url);
   if(url.pathname.match(/^\/inbox/)) {
       event.respondWith(networkFirst(event.request));
   }
});
````


