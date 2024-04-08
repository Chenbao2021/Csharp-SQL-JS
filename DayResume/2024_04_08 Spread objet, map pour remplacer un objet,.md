***
# React
***
#### I - Spread un objet dans un objet
Exemple:
````
{
    ...selectedData,
    ...
    type: {
        ...selectedData.type,
        ...
    }
    ...
}
````

#### II - Utiliser Map pour remplacer un objet dans un Array
````
data?.map((obj) => {
    if (obj.id === selectedData?.id) {
    	return {
    	    ...New Object...
    	} as IRow
    } else {
    	return obj;
    }
})
````

