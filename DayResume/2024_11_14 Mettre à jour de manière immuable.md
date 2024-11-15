# React
***
## I - Mettre à jour de manière immuable
Pour modifier un attribut d'un objet du objet dans une application React de manière optimal, il est important de respecter l'__immutabilité__.
Cela signifie que nous devons créer une __copie de l'état__ au lieu de le modifier directement.

Voici un exemple:
````js
const updatePhotoComment = useCallback((photoName: string, newComment: string) => {
    if(stateQuestion && stateQuestion.photos) {
        let isModified = false;
        const updatePhotos = stateQuestion.photos.map(photo => {
            if(photo.documentFilename === photoName) {
                if(photo.comment !== newComment) {
                    isModified = true;
                    // Retourne une nouvelle copie de 'photo' avec le commentaire mise à jour
                    return {...photo, comment: newComment}
                }
            }
            // Retourne la photo inchangée si ce n'est pas la cible
            return photo;
        })
    }
    
    // Met à jour 'stateQuestion' avec les 'photos' modifiés
    if(isModified) {
        setStateQuestion(prevState => ({ 
            ...prevState,
            photos: updatePhotos,
        })
    }
})
````
Pourquoi cette méthode est optimale?
* __Immutabilité__: En créant une copie du tableau et des objets imbriqués, React détecte corretement les changements.
* __Rerender correct__: __setStateQuestion__ avec une nouvelle référence pour __photos__ déclenche un rerender uniquement si nécessaire.
* __Sécurité__: Cette approche réduit le risque de bugs liés à la modification directe des objets d'état.
