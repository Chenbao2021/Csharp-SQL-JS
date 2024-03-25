***
#### Ceux qui sont à Metafactory exclusivement
***
##### 1. On va plutôt utiliser des components MF,que les composants Material UI, et AG Grid.
La site documentation de MF components n'est pas encore construit, pour voir la liste des components MF:
On fait "ctr + espace" dans l'accolade : 
````
import {  } from "@mf/react-mui-controls";
````

##### 2. Utiliser les snippet de Metafactory
- Snippet pour la création des différentes hooks
- Snippets pour la décomposition
- etc. (à découvrir)

***
#### On va privilégier l'utilisation de less.js que l'utilisation de sx.
***
- C'est plus facile de maintenir les codes avec less.js
- Quand on utilise sx de MUI, on introduit des objets, et à chaque render ces objets seront recrée, ce qui peut légérement nuire la performance de l'application.

***
#### React Node
***
Quand on parle de React Node, on parle de n'importe quoi qui donne 1 rendu visuel dans React.

***
#### Actualiser Visual Studio
***
Quand on a fait quelques choses d'important et qu'on doit rouvrir visual studio, on peut simplement l'actualiser au lieu de la fermer : 
Pour se faire :  "Ctr + Shift + P" ->  saisir "reload" -> Cliquer "Recharger la fênetre".

***
#### Les props sont(parfois) des fonctions qui prend des callbacks (Ex: onChange(fnCallback))
***
Quand tu as un component React qui a "onChange" (Ou quoi que ce soir) comme props,
mettes le souris sur la props, et tu peux voir des descriptions, surtout après les deux points ":", tu trouveras le type de props .
Grâce à ça, tu peux savoir quels seront les noms des variables que le props va retourner, et donc ta fonction qui est passé en callback peut les utiliser comme des callbacks.

Par exemple souvent on voit cette type des fonctions callbacks : 
````
<Component
    onChange = { (e, newValue) => {...}  }
>
````
Dans le code ci-dessus, d'abord on exécute "onChange", puis avec les returns de onChange on exécute la fonction callback.

***
#### MDI : Material design icon
***
Pour les Icons, on va utiliser les MDIs:
https://pictogrammers.com/library/mdi/

***
#### Shift + Clic gauche sur la flèche du sidebar des codes
***
Cela permet d'afficher les codes d'un component sous trois formes:
- Tous factoriser
- Tous dévélopper
- Dévélopper que les fils directes.

***
#### defaultColDef
***
````
const defaultColDef = useMemo(() => { 
	return {
        width: 150,
        cellStyle: { fontWeight: 'bold' },
    };
}, []);

<AgGridReact defaultColDef={defaultColDef} />
````

***
#### CellRendererFrameWork
***
Dans un colonne AG GRID, on peut personnaliser des affichage de chaque colonne ou ligne par utilisation du props "CellRendererFrameWork", par exemple : 
````
cellRendererFramework: function (row: any) {
        return (
          <Button
            danger
            onClick={() => {
              onDeleteClick(row.data);
            }}
          >
            حذف
          </Button>
        );
      },
````

***
#### Exemple de decomposition d'objet
***
````
interface props {
    objA : string,
    objB : {
        objB1: string,
        objB2: string
    }
}

...
const { objA, objB } = props;
const { objB1, objB2 } = objB;
...
````