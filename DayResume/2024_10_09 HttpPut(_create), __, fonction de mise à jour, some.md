# C#
***
## I - HttpPut("create")
Dans ce code: 
````C
[Route("api/[controller]")]
public class RetrieveController : ControllerBase
{
    [HttpPut("create")]
    public async Task<object> CreateReport([FromBody] System.Text.Json.JsonElement value)
    {
        // Votre logique ici
    }
}
````
Pour accéder à la fonction CreateReport, en Front on appelle l'url "/retrieve/create" en PUT méthode.

# React
***
## I - Utiliser || pour fournir une valeur par défaut
Quand on a cet attribut:
``let references: Reference[] | undefined;``, on doit vérifier si reference est une tableau ou undefined, et pour se faire.
L'une des méthodes est d'utiliser || pour fournir une valeur par défaut vide au tableau.
``for (let reference of references || []) { ... }``

## II - Fonction de mise à jour
Dans React, si tu veux t'assurer que ``prev`` n'est pas ``undefined`` dans une fonction de mise à jour d'état comme celle-ci:
``setSelectChapters((prev) => prev.concat(data.value));``

On peut utiliser || pour fournir une valeur par défaut en cas d'``undefined``:
``setSelectChapters((prev) => (prev ?? []).concat(data.value));``

## III - ``some``
Pour afficher les éléments de l'array ``A`` qui sont aussi présents dans ``B`` en se basant sur la même propriété ``value``:
````JS
const commonElements = A.filter(aItem => B.some(bItem => bItem.value === aItem.value)
````
