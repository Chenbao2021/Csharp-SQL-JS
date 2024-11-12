# React
***
## I - camera && htmlFor
On peut utiliser ``<input type="file" capture="environment">`` pour ouvrir l'interface de la caméra du téléphone, permettant à l'utilisateur de prendre une photo ou de sélectionner un fichier depuis la galerie.
* ``type="file"``: Ouvre un sélecteur de fichiers.
* ``accept="image/*"``: Limite la sélection aux fichiers d'image seulement.
* ``capture="environment"``: Ouvre directement la caméra arrière pour prendre une photo.
* __On peut prévisualiser la photo sélectionnée. (Voir avec ChatGPT)__

La balise ``<input>`` est une balise __auto-fermante__ en HTML, ce qui signifie qu'elle ne peut pas avoir de contenu enfant.
On peut associer un input avec un icon, grâce à la balise ``<label>`` :
* En HTML, l'élément ``<label>`` peut être utilisé pour déclencher un ``<input type="file">`` en cliquant dessus. 
* On peut associer le ``<label>`` à l'input en utilisant l'attribut ``for/htmlFor``, ce qui permet d'utiliser une icône personnalisée comme déclencheur.
* Exemple: 
    ````js
      {/* Input de fichier caché */}
      <input
        type="file"
        accept="image/*"
        capture="environment"
        id="cameraInput"
        style={{ display: 'none' }}
        onChange={handleFileChange}
      />
      {/* Label avec l’icône pour activer la caméra */}
      <label htmlFor="cameraInput" style={{ cursor: 'pointer' }}>
        <img src="camera-icon.png" alt="Activer la caméra" width="100" />
      </label>
    ````
    Avec handleFileChange:
    ````
      const handleFileChange = (event) => {
        const file = event.target.files[0];
        if (file) {
          const reader = new FileReader();
          reader.onload = (e) => setPreviewSrc(e.target.result);
          reader.readAsDataURL(file);
        }
      };
    ````
    * Utilisation de ``htmlFor`` au lieu de ``for``: En React, ``htmlFor`` est utilisé pour associer le ``<label>`` à l'``input`` par son ``id``, comme dans ``<label htmlFor="cameraInput">``.


