## I - The advantage of Reducer to State
-  Better management of state
-  Better performance
    State: Each time we use the spread opearation, we create a new object. If the object contains abundant datas, the creation may take a lot of times.
    Reducer: The new reducer is based on the Immer library, so that allow to modify a value of an object without create a new one.

## II - Demonter 1 component
Quand on dit démonter 1 component, ça veut dire :
````
{ 
    isHeaderDisplay && <Header ... />
}
````
- Ne pas mettre des appels des APIs ou des calcules complexes ans un demonte, car à chaque demonte il refait l'opération.
    Donc plutôt la mettre dans une couche supérieurs qui persiste.

## III - création des objets dans les composants, et la logique dans les reducers
On crée des objets dans des composants,
et on fait la logique dans les reducers, qui utilise les objets crée par les composants.

## IV - L'interface du props d'un composant se trouve dans le même fichier que le composant
On ne met pas l'interface du props d'un composant dans un fichier à part.

## V - Décompositiobn des props toujours en première ligne

## VI - On évite d'exporter par default

## VII - En générale , on fait 1 reducer par page.

***
# Exclusive à Metafactory
***

## VIII - ObjectUtils.setPathValue
````
(method) setPathValue(obj: any, path: string, value?: any): any
Affecte la valeur value à obj[path] où path est un chemin "root.path.subPath" et retourne la valeur précédente.
````

Cette méthode peut simplifier les codes, par exemple:
````
// Version 1

// if (action.field === "nbLines") {
// 	draft.criteria.nbLine = action.value
// } else if (action.field == "portType") {
// 	draft.criteria.portType = action.value
// }

// Version 2
ObjectUtils.setPathValue(draft.criteria, action.field, action.value);
````

## "OnChange" du component MfTextField prend plusieurs paramètres en entrés, notament "field".
En utilisant "Field", on peut écrire une méthode générique pour faire des opérations sur des attributs différentes.
Exemple : 
````
const onChangeGeneric = useCallback((value: any, field?: string) => {
	if (field)
		dispatch({ type: ActionEnum.updateSelectedData, value, field });
}, [dispatch]);
````
