# React
***
#### I - valueSetter in AG-GRID(ColDef)
Le **"valueSetter"** dans AG Grid est une fonction qui permet de contrôler comment les nouvelles valeurs sont définies pour les cellules dans la grille.
Il est utilisé pour effectuer des opérations ou des validations spécifiques avant que la valeur ne soit effectivement enregistrée dans le modèle de données sous-jacent.

**Fonctionnalité de 'valueSetter'**
1. **Validation et Transformation** : Vous pouvez utiliser 'valueSetter' pour valider ou transformer les dopnnées avant de les définir dans la grille.
2. **Contrôle Granulaire** (Contrôle en détail) :  Il offre un contrôle en détail sur la mise à jour des valeurs, permettant des actions complexes comme la mise à jour de plusieurs champs.
3. **Interception des Modifications** : Le ``valueSetter`` vous permet d'intercepter les modifications des valeurs. Vous pouvez déclencher des actions supplémentaires comme l'envoi de notifications, la mise à jour de l'état de l'application, etc.
4. **Retourner une Confirmation**: La fonction doit retourner ``true`` si la valeur a été mise à jour avec succès, sinon ``false`` . Cela permet à AG Grid de savoir si la modification a été acceptée.

**Exemple1**
````javascript
{
    ...
    valueSetter: (params) => {
        // Exemple de validation: Vérifier si la nouvelle valeur est un nombre
        if (typeof params.newValue === 'number' && !isNaN(params.newValue)) {
            // Mettre à jour la valeur dans les données
            params.data[params.colDef.field] = params.newValue;
            // Déclencher une action, par exemple mettre à jour l'état dans Redux
            dispatch({ type: ..., ... });
            return true
        } 
        // Retourner false si la validation échoue
        return false;
    }
    ...
}
````

**Exemple2**
````javascript
{
    field: "name",
    headerName: "Name",
    editable: true,
    valueSetter: (params) => {
        if(typeof params.newValue == 'string') {
            params.data[params.colDef.field] = params.newValue.toUpperCase();
            return true;
        }
        return false;
    }
}
````

#### II - useImperativeHandle
``useImperativeHandle`` est un hook dans React qui permet de personnaliser la valeur de l'objet ref exposé à un parent lorsque ce dernier utilise une référence pour interagir avec un composant enfant.
Cela permet de contrôler les méthodes et propriétés accessibles via la ref, offrant un moyen plus contrôlé d'exposer certaines parties de l'API d'un composant enfant.

``useImperativeHandle(ref, createHandle, [deps])``
* ref: La référence créee par 'React.forwardRef' dans le composant parent.
* CreateHandle: Une fonction qui retourne un objet avec les propriétés et méthodes que vous souhaitez exposer.

**Exemple d'utilisation**
````javascript
...
useImperativeHandle(ref, () => (
    {
        // La méthode getValue sera appelée par AG Grid pour obtenir la valeur de l'éditeur
        getValue() {
            return parseFloat(value);
        }
    }
)
...
````
1. ``forwardRef``: Permet de passer une ref du parent au composant enfant. C'est une condition préalable pour utiliser ``useImperativeHandle``.
2. ``useImperativeHandle``: 
    * Le hook est utilisé pour exposer la méthode ``getValue`` à AG Grid
    * ``ref``: La ref passée par le parent(AG Grid dans ce cas)
    * ``() => ({getValue: () => parseFloat(value)})`` : Un objet retourné qui expose la méthode ``getValue``, qui retourne la valeur actuelle de l'input convertie en nombre.

#### III - GetValue
La méthode ``getValue`` est utilisé pour récupérer la valeur que l'utilisateur a entrée dans l'éditeur de cellule.
Lorsque l'édition est terminée, AG Grid appelle ``getValue`` pour obtenir la valeur final que l'éditeur doit enregistrer dans la grille.


