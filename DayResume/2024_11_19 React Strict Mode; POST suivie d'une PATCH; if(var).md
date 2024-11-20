# React
***
## I - React Strict Mode
__Souvent, l'erreur ne vient pas du Stric mode mais d'autre part, verifies bien!!!__
En Strict Mode,  React rend chaque composant __deux fois__(Cela signifie que tout hook ou fonction de callback attaché à un composant peut être déclenché deux fois).
* Vérifie si les ``useEffect`` nettoient correctement leurs effets en démontant/remontant les composants.
* Vérifier si les dépendances de ``useCallback`` ou ``useMemo`` sont correctes

Pour éviter que certains codes dans un hook soit appelé deux fois pendant la phase de test, on peut faire ainsi:
````
let isHandled = false;
...
(Un morceau de code dans un hook) :
    if(!isHandled) {
        ...
        isHandled = true;
    }
````
La méthode ci-dessus évite qu'une partie des codes du hook soit appelé deux fois.

__Les fonctions englobé dans un useCallback/useMemo ne sera pas recrée lors de montage/démontage.__

## II - ``POST`` suivie d'une ``PATCH``
C'est un comportement typique de la bibliothèque ``tus-js-client`` ou des protocoles similiaires utilisés pour les téléchargements de fichiers résumable.
Ce problème est lié au protocole utilisé pour gérer les téléchargements.

Pourquoi ``POST`` et ``PATCH`` sont utilisés dans ``tus`` ?
* ``POST``: Création de la ressource
    * Notifier le serveur qu'un nouveau fichier va être téléchargé.
    * Recevoir un identifiant unique pour ce téléchargement.(Dans l'ên-tete, avec res.getHeader(...))
    * À la fin de ce stade, la ressource est prête sur le serveur, mais aucun contenu n'a encore été envoyé.
* ``PATCH``: Envoi des données
    * Envoie un segment du fichier à l'URL fournie par le serveur(``Location``).
    * Peut être répétée plusieurs fois pour les téléchargements segmentés ou en cas de reprise après interruption.
    * Pour des fichiers volumineux, ``tus-js-client`` segmente le fichier en morceaux et effectue plusieurs requêtes ``PATCH```
    * Si le téléchargement est interrompu, ``tus-js-client`` envoie une requête ``HEAD`` pour vérifier l'état de la ressource avant de reprendre avec une requête ``PATCH``.

## III - If(var)
Si tu n'est pas sur que ton variable peut être undefined ou null, utilises simplement 
``if(var)`` au lieu de ``if(var === undefined)`` ou ``if(var === null)``.
