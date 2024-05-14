# SQL
### I - JSON_QUERY
C'est une fonction utilisée dans SQL Server pour extraire un objet ou un tableau JSON à partir d'un document JSON.
Syntaxe: 
````
JSON_QUERY( expression, path )
````
* expression : C'est l'expression ou la colonne contenant le document JSON à partir duquel vous voulez extraire des données.
* path: C'est le chemin JSON spécifiant l'objet ou le tableau que vous voulez extraire.

Exemples:
* Exemple1 : Extraction d'un objet JSON
Supposons qu'on a une table "Products" avec une colonne "ProductInfo" contenant des données JSON.
    * Valeur de "ProductInfo":
        ````
        {
          "ProductID": 1,
          "ProductName": "Laptop",
          "Specifications": {
            "RAM": "16GB",
            "Storage": "512GB SSD"
          },
          "Price": 1200
        }
        ````
    Pour extraire l'objet "Specifications" : 
    ````
    SELECT JSON_QUERY(ProductInfo, '$.Specifications') AS Specifications
    FROM Products
    WHERE ProductID = 1
    ````

* Exemple 2 :  De même, on peut extraire un tableau. ou même un objet dans un tableau avec path '$.tableau.objet'

On peut ne pas spécifie le path pour obtenir toutes les données.

# React
### I -  React.MouseEventHandler
* Assure que les paramètres passés à la fonction de gestionnaire d'événements correspondent aux événements de souris de React.
* Il prend en charge le typage générique pour spécifier le type de l'élément sur lequel l'événement est écouté, ce qui permet d'accéder de manière sûre aux propriétés spécifiques de cet élément.

Exemple de code: 
````
const buttonOnClick: React.MouseEventHandler<HTMLButtonElement> = useCallback((event): void => {
	const buttonName = event.currentTarget.name;
    setActionName(buttonName)
    setOpenModal(true)
}, [])
````
* On a déclaré 'buttonOnClick' comme une fonction qui est un gestionnaire d'événements de souris pour un élément de type 'HTMKButtonElement'.
* On n'a pas besoin de spécifier les types des paramètres event car ils sont implicites grâce à la définition de React.MouseEventHandler<HTMLButtonElement>. Le compilateur TypeScript comprend automatiquement que event est un React.MouseEvent<HTMLButtonElement, MouseEvent>.

### II - target/eventTarget
__Exemple pratique__
````
<button onClick={handleClick}>
  <img src="icon.png" alt="Icon">
  Cliquez-moi
</button>
````
* 'event.target' sera l'élément <img> car c'est l'élément sur lequel l'utilisateur a cliqué
* 'event.currentTarger' sera l'élément '<button>' parce que c'est l'élément qui a le gestionnaire 'onClick'.

__event.target__
* Définition: C'est l'élément sur lequel l'événement a __réellement eu lieu__.  Cela signifie que si l'événement est déclenché par un élément spécifique, même si cet élément est un enfant(__<img>__) de l'élément(__<button>__) qui a le gestionnaire d'événement attaché, 'event.target' sera cet enfant(__<img>__).
* Usage: Utilises lorsque vous avez besoin de savoir précisément quel élément a été cliqué, touché, ou interacté de quelque manière que ce soir. C'est très utile dans le cas où plusieurs éléments enfant partagent le même gestionnaire d'événement via leur parent.

__event.currentTarget__
* Definition : Fait référence à l'élément auquel le gestionnaire d'événement est actuellement attaché lors de la propagation de l'événément.
* Usage: Quand vous avez besoin d'interagir ou de référencer l'élément qui a le gestionnaire d'événement. Cela est utile pour garantir que l'action effectuée par le gestionnaire d'événement est relative à l'élément qui l'a capté, indépendamment de l'endroit où l'événement a été initié.

__Pourquoi cette distinction est important?__
* __Comportement spécifique selon l'élément__: Dans certains cas, vous voudriez exécuter une action différente selon l'élément spécifique cliqué (utilisation de event.target).
* __Action généralisée sur l'élément avec le gestionnaire__: Dans d'autres cas, vous pourriez vouloir garantir que quelque chose se passe avec l'élément qui a le gestionnaire, peu importe quel enfant a été cliqué

__D'autre exemples__
````
<ul onClick={handleClick}>
  <li id="home">Accueil</li>
  <li id="about">À propos</li>
  <li id="contact">Contact</li>
</ul>
````
````
function handleClick(event) {
  console.log("target:", event.target.id); // Donne l'ID de l'élément li cliqué
  console.log("currentTarget:", event.currentTarget.tagName); // Toujours 'UL', peu importe l'élément cliqué
}
````

__D'autre exemples__
````
function ParentComponent() {
  return (
    <div onClick={handleParentClick}>
      Parent
      <ChildComponent />
    </div>
  );
}

function ChildComponent() {
  return (
    <div onClick={handleChildClick}>
      Child
    </div>
  );
}

function handleParentClick(event) {
  if (event.target === event.currentTarget) {
    console.log("Parent was clicked directly");
  }
}

function handleChildClick(event) {
  event.stopPropagation(); // Stoppe la propagation pour éviter que l'événement remonte au parent
  console.log("Child was clicked");
}
````

