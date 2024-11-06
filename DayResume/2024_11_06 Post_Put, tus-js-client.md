# General
***
## I - POST/PUT
* ``POST`` - Créer une nouvelle ressource 
    * La méthode __POST__ est utilisée pour créer une nouvelle ressource sur le serveur. Chaque requête POST ajoute généralement une ressource unique, avec un nouvel identifiant généré par le serveur.
    * ``POST`` est __non idempotente__, c'est à dire que si la même requête POST est envoyée plusieurs fois, elle peut créer plusieurs nouvelles ressources.
* ``PUT`` - Remplacer ou mettre à jour une ressource existante
    * La méthode ``PUT`` est utilisée pour remplacer ou mettre à jour une ressource existante avec des données nouvelles ou modifiées.
    * ``PUT`` est __idempotente__.

# React
***
## I - tus-js-client
La librairie ``tus-js-client`` est une bibliothèque JS qui implémente le protocole __tus__ pour des téléchargements de fichiers résumables.
Elle est utilisée pour gérer des envois de fichiers dans des environnements tels que le navigateur, Node.js, React Native, et Cordova.

#### Protocole tus
Le protocole tus est basé sur HTTP et conçu pour les téléchargements résumables. Ce type de transfert permet de reprendre un téléchargement après une interruption, que celle-ci soit due à un problème réseau ou à une fermeture accidentelle de l’application.

#### Fonctionnalités clés
* __Reprise de téléchargement__: Les téléchargements interrompus peuvent reprendre là où ils se sont arrêtés sans recharger les données précédentes.
* __Stockage des URLs de téléchargement __: Les URLs des sessions d’upload sont stockées (localStorage par exemple) pour permettre la reprise, même après la fermeture du navigateur​
* __Support des téléchargements parallèles __: Pour augmenter la vitesse, la bibliothèque peut fractionner un fichier en plusieurs parties et les envoyer en parallèle, rassemblant les fragments sur le serveur.

#### Incovénients
* __Dépendance au serveur tus __: Nécessite un serveur compatible avec le protocole tus pour la gestion des téléchargements.
* __Complexité pour les fichiers volumineux__: Bien que résumable, le processus peut être lent sur les connexions à faible débit.
* __Configuration requise pour les anciens navigateurs__:  Nécessite des polyfills pour le support des Promises dans certains navigateurs plus anciens.
* __Stockage local limité__ : Dans les environnements avec peu de mémoire persistante, la gestion des URLs de téléchargement pourrait être compromise.


#### Les différents proprietés
* ``endpoint``: C’est l'URL du serveur où le fichier sera téléchargé.
* ``retryDelays``:  Tableau de délais (en millisecondes) pour re-tenter un téléchargement en cas d'échec.
* ``metadata``: Ensemble de métadonnées sur le fichier à télécharger.
* __Fonctions de rappel(callback)__ : ``onError``, ``onProgress``, ``onSuccess``.
* ``chunkSize``: Taille des morceaux en octets pour les téléchargements segmentés(chunks).
* ``removeFingerprintOnSuccess``: Supprime l'empreinte du fichier de la mémoire locale une fois le téléchargement réussi. (Empêcher de relancer un téléchargement déjà effectué en supprimant les informations de reprise).
