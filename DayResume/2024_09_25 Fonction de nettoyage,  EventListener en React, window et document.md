# React
****
## I - Fonction de nettoyage
Le ``useEffect`` retourne une fonction pour une raison précise: permettre d'effectuer des **tâches de nettoyage** lorsqu'un composant est démonté ou avant que l'effet ne soit réexécuté.
Cette fonction de nettoyage est cruciale pour éviter des effets secondaires indésirables:
* Fuites de mémoire
* Écouteurs d'événements non retirés
* Timers non arrêtés
* Des requêtes réseau en cours qui continuent après que le composant a été retiré du DOM.

**Règles à suivre pour un bon nettoyage**
1. S'assurer que chaque ressource utilisée dans l'effet est nettoyée :
    * Timers(``setTimeout``,``setInterval``): Arrêter avec ``clearTimeout`` ou ``clearInterval``.
    * Écouteur d'événements (``addEventListener``) : Supprimer avec ``removeEventListener``.
    * Requêtes réseau(``fetch``, WebSockets) : Annuler ou fermer les connexions.
    * Abonnements à des services externes: Se desabonner proprement.
2. Ne jamais oublier la fonction de nettoyage
3. Utiliser les dépendances correctement

## II - EventListener
Dans les projets React, l'utilisation direct de ``EventListener`` natifs du DOM peut être très utile dans certains cas, mais n'est pas recommandé.

1. **Avantages**
    * **Contrôle fin**: Donne un contrôle très précis sur les événements spécifiques du DOM et manipulations très personnalisées.
    * **Flexibilités**: On peut ajouter des événement sur n'importe quel élément du DOM, y compris ceux qui ne sont pas directement gérés par React, ou lorsque vous devez écouter des événements globaux (comme ```window`` ou ``document``).
2. **Inconvénients potnetiels**
    * **Gestion manuelle**: Assurer de le supprimer avec ``removeEventListener``.
    * **Interférance avec React's Event System** : React utilise un système d'événement synthétiques(``SyntheticEvent``), qui abstrait les événements du DOM pour fournir une API unifiée et plus performante. En utilisant des ``EventListener`` natifs, on contourne ce système.
3. **Quand utiliser des EventListener natifs dans React?**
    Il y a des situations spécifiques où l'utilisation directe d'``eVENTlISTENER`` natifs est justifiés:
    * Lorsque vous travaillez avec des événements globaux qui ne sont pas directement pris en charge par React(Par exemple, des événements de fenêtre comme ``resize``, ``scroll``, ou des événements personnalisés émis par d'autres bibliothèques)
    * Lorsque vous devez écouter des événements en dehors des éléments contrôlés par React, comme un élément DOM qui n'est pas directement rendu par React.
4. **Exemples**
    * ``Resize``
        ````JavaScript
        useEffect(() => {
        const handleResize = () => {
          setWindowWidth(window.innerWidth);
        };
    
        // Ajouter l'EventListener pour la fenêtre
        window.addEventListener('resize', handleResize);
    
        // Nettoyer l'EventListener lors du démontage du composant
        return () => {
          window.removeEventListener('resize', handleResize);
        };
      }, []); // Le tableau vide [] signifie que cet effet ne s'exécute qu'une fois, lors du montage du composant
        ````
    * ``Scroll``
        ````JavaScript
        seEffect(() => {
            const handleScroll = () => {
              setScrollY(window.scrollY); // La position verticale actuelle du scroll
            };
            // Ajouter l'écoute de l'événement scroll
            window.addEventListener('scroll', handleScroll);
            // Nettoyer l'EventListener lors du démontage du composant
            return () => {
              window.removeEventListener('scroll', handleScroll);
            };
          }, []); // Le tableau vide [] signifie que cet effet ne s'exécute qu'une fois lors du montage
        ````
    * ``mousemove``
        ````
         useEffect(() => {
            const handleMouseMove = (event) => {
              setMousePosition({ x: event.clientX, y: event.clientY });
            };
            // Ajouter l'écoute de l'événement mousemove
            window.addEventListener('mousemove', handleMouseMove);
            // Nettoyer l'EventListener lors du démontage du composant
            return () => {
              window.removeEventListener('mousemove', handleMouseMove);
            };
          }, []); // Le tableau vide [] signifie que cet effet ne s'exécute qu'une fois lors du montage

        ````
## III - window, document
**``window``**
L'objet ``window`` représente la fenêtre du navigateur dans laquelle la page web est affichée.
Il contient des informations et des fonctionnalités liées à la fenêtre et aux événements globaux(redimensionnement, de défilement, ou encore des interactions avec les popups).

Quelques propriétés et méthodes importantes de ``window`` :
* ``window.innerWidth`` : La largeur de la zone visible de la fenêtre du navigateur(viewport).
* ``window.innerHeight`` : La hauteur de la zone visible de la fenêtre du navigateur.
* ``window.scrollX`` : La position actuelle du défilement horizontal.
* ``window.scrollY`` : La position actuelle du défilement vertical.
* ``window.location`` : Contient l'URL actuelle de la page et permet d'accéder ou de modifier différentes parties de cette URL.
* ``window.alert()`` : Affiche une alerte(Une fenêtre popup avec un message.
* ``window.addEventListener()`` : Ajoute un écouteur d'événement sur la fenêtre.
* ``window.setTimeout()`` et ``window.setInterval()`` : Permettent de retarder ou de répéter une exécution de fonction après un certain temps.

**``document``**
L'objet ``document`` fait partie de l'objet ``window``, et représente la structure de la page HTML chargée dans la fenêtre. Il permet d'accéder et de manipuler le **DOM**(Document Object Model), qui est une représentation en arbre de la page HTML où chaque noeud est un élément HTML ou un morceau de texte.

Quelques propriétés et méthodes importantes de ``document``:
* ``document.getElementById()`` : Récupère un élément du DOM à partir de son identifiant unique (id).
* ``document.querySelector()`` : Sélectionne le premier élément qui correspond à un sélecteur CSS donné.
* ``document.createElement()`` : Crée un nouvel élément HTML dans le DOM.
* ``document.body`` : Représente l'élément <body> de la page HTML, où se trouve le contenu principal.
* ``document.title`` : Permet d'accéder ou de modifier le titre de la page HTML.
* ``document.addEventListener()`` : Ajoute un écouteur d'événements sur le document (par exemple, pour capturer des événements au niveau global).
* ``document.cookie`` : Permet d'accéder aux cookies du document.