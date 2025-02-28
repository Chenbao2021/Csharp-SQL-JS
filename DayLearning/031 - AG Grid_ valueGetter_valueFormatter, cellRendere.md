# Introduction
Pour mieux developper avec __AG Grid__, il est important de connaître et comprendre ces proprietés:
* valueGetter
* valueFormatter
* cellRenderer/ CellRendererParameters

# I - Le lien entre ``field`` et ``rowData``
* ``rowData``:
    Ce sont vos __données sources__ passées à la grille. Chaque objet représente une ligne de données.
    ````js
    const rowData = [
      { firstName: "Jean", lastName: "Dupont", price: 45.5 },
      { firstName: "Marie", lastName: "Durand", price: 78.2 }
    ];
    ````
* ``field``:
    Il permet de liér directement une colonne à une __propriété spécifique__ de vos données(``rowData``). Si on utilise ``field: 'price'``, AG Grid ira automatiquement chercher la valeur dans les données.
    ````
    {
      headerName: "Prix brut",
      field: "price" // AG Grid affichera directement la valeur de la propriété price
    }
    ````

* Mais lorsqu'on utilise valueGetter, on a plus besoin de définir ``field``.
    AG Grid __ignore__ complètement le ``field`` puisque la valeur à afficher est déjà fournie par la fonction.
    => __On n'utilise jamais les deux ensemble.__

# II - valueGetter(_columnsDefs_): Récupérer ou calculer une valeur.
* __Rôle__: Il permet de définir ou de calculer dynamiquement la valeur à afficher dans une cellule.
* __Quand l'utiliser?__: Lorsque la valeur affichée ne vient pas directement des données de la ligne ou nécessite un calcul personnalisé.
* __Exemple d'utilisation__: Imaginons que nos données contiennent les champs ``firstName`` et ``lastName``, mais vous voulez afficher le nom complet dans une seule cellule.
    ````js
    {
      headerName: "Nom complet",
      valueGetter: (params) => `${params.data.firstName} ${params.data.lastName}`
    }
    ````

### Paramètre de valueGetter
Les paramètres de ``valueGetter`` contiennent des infos sur la cellule, la ligne, les colonnes, etc. On va s'intéresser exclusivement sur le paramètre ``data`` de ``valueGetter``:
* Quand on passe un tableau d'objets à AG Grid avec la propriété ``rowData``, chaque ligne de la grille représente __un objet unique__ de ce tableau.
* Lorsque ``valueGetter`` est appelé, le paramètre ``params.data`` fait référence à __cet objet spécifique__ de ``rowData`` correspondant à la ligne actuelle.

#### Exemple concret
Supposons qu'on a ce tableau de données(``rowData``):
````js
const rowData = [
  { id: 1, firstName: "Jean", lastName: "Dupont", age: 30 },
  { id: 2, firstName: "Marie", lastName: "Durand", age: 25 },
  { id: 3, firstName: "Pierre", lastName: "Martin", age: 40 }
];
````

Utilisation de ``valueGetter``:
````js
{
  headerName: "Nom complet",
  valueGetter: (params) => {
    console.log(params.data); // Affiche l'objet de rowData correspondant à la ligne actuelle
    return `${params.data.firstName} ${params.data.lastName}`;
  }
}
````

Lors du rendu de la grille, pour chaque ligne:
* Sur la première ligne, ``params.data`` sera: ``{ id: 1, firstName: "Jean", lastName: "Dupont", age: 30 }``
* Sur la deuxième ligne, ce sera: ``{ id: 2, firstName: "Marie", lastName: "Durand", age: 25 }``

# III - ValueFormatter:  Formater l'affichage de la valeur
* __Rôle__: Il sert uniquement à transformer la valeur existante en une chaîne formatée avant de l'afficher, sans modifier la valeur réelle de la cellule.
* __Quand l'utiliser__: Lorsqu'on souhait modifier l'apparence d'une valeur sans toucher aux données sous-jacentes(ex: format de date, nombre arrondi, affichage de devise).
* __Exemple d'utilisation__:

## Paramètre de valueFormatter.
Comme dans ``valueGetter``, le paramètre ``params`` de ``valueFormatter`` contient également un champ ``data``, qui représente __l'objet de ``rowData`` pour la ligne actuelle``.

__Mais avec une différence important__: Dans ``valueFormatter``, on reçoit aussi un paramètre supplémentaire ``params.value``, qui contient __la valeur brute__ que AG Grid va formater avant de l'afficher.
* Si on utilise ``field`` : ``params.value`` contiendra automatiquement la valeur de ce champ.
* Si on utilise un ``valueGetter``: ``params.value`` contiendra le résultat retourné par ce ``valueGetter``.
* Si on utilise un ``cellRenderer``: ``valueFormatter`` n'est généralement pas utilisé car on contrôle déjà l'affichage.

# IV - CellRenderer/CellRendererParams
* ``cellRenderer`` - __Personnaliser entièrement le contenu de la cellule__
    Permet de retourner du contenu HTML personnalisé ou un composant React. Idéal pour des contenus interactifs(icônes, boutons, images).
    __Exemple: Bouton dans une cellule__
    ````js
    {
      headerName: "Action",
      field: "action",
      cellRenderer: (params) => {
        return `<button onclick="alert('Action sur ${params.data.firstName}')">Cliquez-moi</button>`;
      }
    }
    ````
* ``cellRendererParams`` - __Passer des paramètres au renderer__.
    Permet de personnaliser davantage le comportement d'un ``cellRenderer``.
    __Exemple: Passer un message personnalisé__
    ````js
    {
      headerName: "Action",
      cellRenderer: (params) => {
        return `<button>${params.message}</button>`;
      },
      cellRendererParams: {
        message: "Cliquez ici"
      }
    }
    ````

# V - Avec typescript
Les colonnes de AG Grid ont pour type: <TData = any, TValue = any>.
* ``TData`` correspond au type d'une ligne (``rowData``, qui serait utilisé aussi comme paramètre de ``valueGetter`` et ``valueFormatter``).
* ``TValue`` correspond au type de la valeur retournée par ``valueGetter``, ``valueFormatter``, etc.
* Par défaut,ces valeurs sont ``any``, ce qui désactive la vérification de type. Pour renforcer la sécurité TypeScript, on remplace ``any`` par une __interface bien définie__

### 1. ``TData`` définit ``params.data``
Lorsque tu spécifies un type ``TData`` dans ``ColDef<TData>``, __AG Grid applique ce type à__ ``params.data`` dans ``valueGetter``, ``valueFormatter``n ``cellRenderer``, etc.
* Exemple 1:
    ````ts
    interface RowData {
      id: number;
      firstName: string;
      lastName: string;
      age: number;
      price: number;
    }
    ````
    Dans ``valueGetter``, ``params.data`` est automatiquement reconnu comme ``rowData``:
    ````ts
    const columnDefs: ColDef<RowData>[] = [
      {
        headerName: "Nom complet",
        valueGetter: (params) => {
          // ✅ TypeScript sait que params.data est un RowData
          return `${params.data.firstName} ${params.data.lastName}`;
        }
      }
    ];
    ````

* Exemple 2 (Pour un tableau de childrens:
    ````ts
    const bunkerConsumptionMtChildren: ColDef<IRoutingLineView, number>[] = [];
	estimatorData?.routingView?.qualities?.forEach((quality: IQualityConso, idx) => {
		bunkerConsumptionMtChildren.push({
		    ...
			valueGetter: p => p.data?.qualitiesConso ? p.data.qualitiesConso.find(qc => qc.quality?.id === quality?.quality?.id)?.consoGlobalCalculated : undefined,
			valueFormatter: p => p.value ? NumberUtils.format(p.value, Tools.createNumberParsingOptions(2)) : '',
		});
	});
    ````
    Dans ce cas, on a pas besoin de définir le type de ``p``, car on l'a déjà défini dans ``ColDef`` à l'initialisation de bunkerConsumptionMtChildren.
    