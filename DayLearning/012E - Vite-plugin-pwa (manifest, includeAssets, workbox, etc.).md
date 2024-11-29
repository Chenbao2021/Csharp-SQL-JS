Le plugin ``vite-plugin-pwa`` offre plusieurs options de configuration pour personnaliser le comportement de Progressive Web Apps(PWA). Voici une explication détaillée des options courantes, avec des exemples et leur utilité
***
# I - Manifest
Le fichier ``manifest.webmanifest`` est un élémentdes Progression Web Apps(PWA). Il décrit les métadonnées de l'application(nom, icônes, apparence, comportement, etc.) pour les navigateurs, et permet une expérience d'installation native sur différents appareils.
***

### Propriétés principales et leur utilité
1. ``name`` et ``short_name``
    * ``name``: Nom complet de l'application, S'affiche sur l'écran d'accueil ou dans le menu d'installation.
    * ``short_name``: Nom court utilisé si l'espace est limité(ex: écran d'accueil sur mobile).

2. ``icons``
    * Décrit les icônes utilisées par l'application. Ces icônes appraissent sur l'écran d'accueil, dans le menu d'installation ou dans l'interface utilisateur du navigateur.
    * Chaque entrée doit inclure:
        * ``src``: Chemin de l'icône. (La repertoire public est considéré comme root.)
        * ``sizes``: Dimensions de l'image(Par exemple, ``192x192``)
        * ``type``: Type MIME de l'image (``image/png``) (Utiliser png si possible)
    
    Le navigateur utilise la proprieté ``sizes`` pour choisir la meilleur icône en fonction du contexte. ("192x192", ``512x512``), et il doit correspondre à la taille réelle de l'image.
3. ``start_url``(Valeur par défaut: ``/``)
    Permet de contrôler la page initiale après l'installation, la valeur sera utilisé au moment de démarrage de l'application.
4. ``display`` (Valeur par défaut: ``browser``)
    Determine l'apparence de l'application.
    * ``fullscreen``: Mode plein écran sans barre d'adresse
    * ``standalone``: Comportement similaire à une application native.
    * ``minimal-ui``: Interface minimaliste(Barre de navigation simplifiée).
    * ``browser``: Afficher l'application dans un navigateur classique.
5. ``background_color``: Couleur d'arrière-plan affichée pendant le chargement de l'application.
6. ``theme_color``: Idem.
7. ``screenshots``:
    Captures d'écran montrant des aperçus de l'application.
    La proprieté ``form_factor``:
    * ``wide``: Ordinateur de bureau, tablettes en mode paysage
    * ``narrow``: Smartphones ou tablettes en mode portrait.
8. ``orientation``:
    Orientation par défaut de l'application:
    * ``portrait-primary``
    * ``landscape``
9. ``scope``:
    Elle détermine les URL qui seront considérées comme faisant partie de l'application, limite les URLs accessibles via l'application.
    Toute URL en dehors de cette portée sera ouverte dans le navigateur normal au lieu d'être gérée par la PWA.
    * La valeur est une URL relative ou absolue.
    * Définit un chemin racine, et toutes les URL sous ce chemin seront incluses.
    * Par défaut, il prend la valeur de ``start_url``.

# II - ``registerType``
* __Description__: Définit comment le Service Worker est enregistré.
* __Valeur possibles__:
    * ``prompt``: L'utilisateur doit explicitement accepter l'installation du Service Worker.
    * ``autoUpdate``: Le Service Worker est automatiquement mis à jour en arrière-plan.

# III - "injectRegister"

# IV - "includeAssets"
* __Description__: Permet de spécifier les fichiers ou ressources à inclure dans le cache.
* __Valeurs possibles__: Tableau de chemins de fichiers ou de glob patterns.

Ces fichiers sont en dehors des fichiers déjà pris en charge automatiquement(Comme les fichiers générés par Vite ou ceux spécifiés dans le manifest).

Les fichiers mentionnés dans ``includeAssets`` doivent être placés dans le dossier ``public/`` pour être accessibles par le navigateur.

Une fois la PWA installée:
1. ces fichiers seront disponibles hors ligne via le cache.
2. On peut les référencer directement dans l'application.

#### Différence entre ``manifest`` et ``includeAssets``
1. Fichiers dans le ``manifest``:
    * Les fichiers spécifiés dans le champ ``icons``, ``screenshots``, ou d'autres champs du ``manifest`` sont automatiquement ajoutés au cache car ils sont essentiels au fonctionnement de la PWA.
    * Ces fichiers sont utilisés pour:
        * L'affichage de l'application sur l'écran(icônes).
        * La création d'une expérience enrichie lors de l'installation(screenshots).
        * Les propriétés graphiques définie pour le PWA.
2. Fichiers dans ``includeAssets``:
    * Les fichiers spécifiés dans ``includeAssets`` ne font pas partie des exigences du manifeste, mais ils sont explicitement ajoutés au cache selon vos besoins.
    * Exemple:
        ````
        VitePWA({
            includeAssets: ['robots.txt', 'favicon.ico', '/assets/images/logo.png']
        })
        ````
    