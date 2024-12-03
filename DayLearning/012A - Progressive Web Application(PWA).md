## Qu'est ce que PWA?
Un PWA est une application web qui manifeste comme des applications mobiles.
Ses intérêts:
* Mode deconnecté
* Pas besoin de "installation" (Mais ils sont vraiment installé en tant qu'un APP)
***

## Une visite en ligne est nécessaire.
Pour utiliser PWA en mode offline, il faut d'abord l'avoir visitée une fois en ligne pour permettre au __Service Worker__ d'enregistrer les ressources nécessaires pour une utilisation hors ligne.

##### I. Pourquoi ?
Lors de la première visite en ligne:
* Le __Service Worker__ est installé et activé.
* Puis le __Service Worker__(Ou __Workbox__, une bibliothèque surcouche simplifiant le travail avec les __Service Worker__) s'occupe de mettre les fichiers nécessaires dans le cache (_Voire III_)
* En utilisant ces fichiers, le navigateur pourrai afficher la page en deconnecté.

##### II. Que signifie "télécharger" une PWA?
* Une PWA n'a pas besoin d'être explicitement installée pour fonctionner en mode offline. Elle peut fonctionner hors ligne après une première visite, même sans être ajoutée à l'écran d'accueil.
* Mais l'installation améliore l'expérience utilisateur.

##### III. Ressources nécessaires pour le mode offline
Si je ne lui connais pas, je ne peux pas dire à quoi il ressemble.
Pour fonctionner en deconnecté, le __Service Worjer__ doit précacher ou mettre en cache dynamiquement:
1. __Fichiers critiques(précaching)__:
    * HTML, CSS, JS, polices, images, etc.
2. __Données dynamiques(runtime caching)__:
    * Les réponses d'API, pour permettre une navigation fluide hors ligne.

##### IV. Tester une PWA en offline
* Faut lancer en prod, sinon certaines fontionnalités ne marchent pas.
* Attention aux cross-origin.
* Chaque fois que tu as fait des modifications, faut ``Unregister`` l'ancien Service Worker, puis recharge la page.
* Les donnée en cache pour un port persiste, même si tu relances l'app par différentes façons (dev, preview, prod). Penser à les bien éliminer pour éviter des surprises.










