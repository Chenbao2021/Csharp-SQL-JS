# Télécharger un fichier depuis Web
Souvent dans une application web on a besoin de télécharger un fichier.
À la fin on aura un code complet pour inspirer.

### I - L'objectif général du code
1. Récupère un fichier depuis un API distance.
2. Convient les données Base64 du fichier en un format binaire exploitable(``Blob``).
3. Génère un lien temporaire pour que l'utilisateur puisse télécharger le fichier localement.
4. Automatiquement, simule un clic sur le lien pour déclencher le téléchargement.
5. Nettoie les ressources temporaire après le téléchargement.

### II - Explication ligne par ligne
__Conversion du fichier Base64 en ``Blob``__
``const blob = BlobUtil.b64toBlob(data.document, 'text/plain;base64');``
* L'API renvoie le contenu du fichier encodé en Base64. Ce format encore des données binaires sous forme de texte lisible.
    * __Données binaires__ : Toutes les informations stockées ou manipulées par un ordinateur sous forme de 0 et 1(bits). C'est le format de base pour représenter tous les types de données numériques dans les systèmes informatiques.
    Par exemple:
        * Texte(ASCII ou UTF-8)
        * Images(JPEG, PNG, GIF, etc.)
        * Vidéos(MP4, AVI, etc)
        * Documents(PDF, Word, etc.)
        * Exécutable(.exe, .dll)
        * Données audio(mp3, WAV)
* ``BlobUtil.b64toBlob``: Cette fonction de la bibliothèque ``blob-util`` convertit les données Base64 en un objet ``Blob``. Un ``Blob`` est un type binaire natif en JavaScript, utilisé pour sotcker des fichiers ou des données binaires.
    * 1er argument: Les données en Base64
    * 2eme: Le type de contenu du fichier.

__Création d'une URL temporaire pour le fichier__
``const blobUrl = URL.createObjectURL(blob);``
* Cette méthode crée une URL temporaire qui pointe vers le fichier contenu dans le ``Blob``. Cela permet d'utiliser le fichier comme s'il était hébergé sur un serveur.

__Création d'un lien HTML ``<a>`` pour le téléchargement__
````js
const element = document.createElement('a');
element.setAttribute('href', blobUrl);
element.setAttribute('download', ret.docFilename);
````
* Création d'un élément ``<a>``
    * Un élément HTML ``<a>`` est crée dynamiquement pour permettre le téléchargement.
* ``href`` et ``download``
    * ``href``: Pointe vers l'URL temporaire générée (``blobUrl``)
    * ``download``: Indique au navigateur qu'il doit télécharger le fichier et non pas afficher ou ouvrir dans le navigateur, en utilisant ``ret.docFilename`` comme nom.

__Simulation d'un clic pour déclencher le téléchargement__
````js
element.style.display = 'none';
document.body.appendChild(element);
element.click();
document.body.removeChild(element);
````
* Pourquoi un clic simulé?
    * Le navigateur n'autorise pas le téléchargement sans action explicite de l'utilisateur. En simulant un clic, le téléchargement se déclenche automatiquement.
* Étapes
        1. __Ajout au DOM__: L'élément ``<a>`` est ajouté au DOM(bien qu'il soit invisible grâce à ``display: none``).
        2. __Simulation de clic__: Le clic déclenche le téléchargement.
        3. __Nettoyage__: L'élément est retiré du DOM après utilisation.

__Nettoyage des ressources__
``URL.revokeObjectURL(blobUrl);```
* Il est recommandé d'ajouter cette ligne après le téléchargement, pour libèrer les ressources associées à l'URL temporaire pour éviter les fuites de mémoire.

Voici le code complet qui permet de télécharger un fichier en local avec une explication détaillé.
````js
reportDataService
	.getFile(idReport, idFile)
	.then((ret: any) => {
		const { data } = ret;

		// Conversion en Blob
		const blob = BlobUtil.b64toBlob(data.document, 'text/plain;base64');

		// Création de l'URL temporaire
		const blobUrl = URL.createObjectURL(blob);

		// Création et click sur l'élément <a>
		const element = document.createElement('a');
		element.href = blobUrl;
		element.download = data.docFilename || 'downloaded_file.txt';
		element.style.display = 'none';
		document.body.appendChild(element);
		element.click();

		// Nettoyage
		document.body.removeChild(element);
		URL.revokeObjectURL(blobUrl)
	})
````