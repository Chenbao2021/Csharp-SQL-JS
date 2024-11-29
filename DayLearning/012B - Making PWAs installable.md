## Making PWAs installable
* Supporting browsers promote the PWA to the user for installation on the device
* The PWA can be installed like a platform-specific app, and can customize the install process.
* Once installed, the PWA gets an icon on the device, alongside platform-specific apps.
* Once installed, the PWA can be launched as a standalone app, rather than a website in a browser.
***

## Instalibility
The web app manifest
* This is a JSOn file that tells the browser how the PWA should appear and behave on the device.
* The manifest is included using a ``<link>`` element in the app's HTML:
    ``<link rel="manifest" href="manifest.json" />``
    If the PWA has more than one page, every page must reference the manifest in this way.
* A rather minimal manifest:
    ````js
    {
      "name": "My PWA",
      "icons": [
        {
          "src": "icons/512.png",
          "type": "image/png",
          "sizes": "512x512"
        }
      ]
    }
    ````
    * App identity: ``name``, ``short_name``, ``description``.
    * App presentation: 
        * ``start_url``: The start page when a user launches the PWA.
        * ``display``: ``fullscreen``, ``standalone``, ``browser``, etc.
    * Colors:
        * ``time_color``: The default color of OS and browser UI elements.
        * ``background_color``: ...
    * App iconography:
        * ``icons``: 
            ````js
            {
              "name": "MyApp",
              "icons": [
                {
                  "src": "icons/tiny.webp",
                  "sizes": "48x48"
                },
                {
                  "src": "icons/small.png",
                  "sizes": "72x72 96x96 128x128 256x256",
                  "purpose": "maskable"
                },
                {
                  "src": "icons/large.png",
                  "sizes": "512x512"
                },
                {
                  "src": "icons/scalable.svg",
                  "sizes": "any"
                }
              ]
            }
            ````
***

## Customizing the installation prompt
By default, the install prompt contains the name and icon for the PWA.
If you provide values for the ``description`` and ``screenshots`` manifest members, then on Android only, these values will be shown in the install prompt.
Ex: https://microsoftedge.github.io/Demos/pwamp/
***

## Launching the app
You can use the ``display`` manifest member to control the display mode:
* ``standalone``: Look like a platform-specific application
* ``browser``: Opened as a normal website.

