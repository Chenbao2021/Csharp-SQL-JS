#### I - Toujours ouvrir un projet avec __.workspace__ .

#### II - Les extensions qui augmente la productivité
- Traling Spaces: A VS Code extension that allows you to __highlight traling spaces and delete them in a flash!__
- Javascript Console utils : Easily insert and remove console.log statements.(ctr + shift + L / ctr + shift + D)
- VSCode Great Icons : Gives you better Icons.
- ESLint.

#### III - Typescript
- __"?:"__  : Désigne qu'une proprieté est optionnel.
- __enum__ : Si on met que des indexes et pas des valeurs, alors l'index dans la déclaration sera des valeurs pour les indexes.

#### IV - Export default / Export
- Pour un export default, le nom est peu important, donc on doit faire attention à ne pas faire des faute de frappe.
- Pour un export, le nom doit être identique.
- On peut import les deux exports en même temps, par exemple:
    ````
    Import default_export, { normal_export1, normal_export2 } from fichier.tsx
    ````

#### V - DEBUG
- Tabulaire Source : Chrome -> Clic droit -> Element : Ici on peut modifier le style de nos components directement pour observer les changements.   
    - En haut à gauche, on a l'icon pour "Select an element in the page to inspect it - ctr + shift + C", ça te permet d'afficher tous les styles concernant un élement.
- Tabulaire Network : Chrome -> Clic droit -> source -> ouvrir ton ficheir .js -> a gauche des codes: On peut debuguer nos codes ici.

#### VI - Snippet 
à revoir
#### VII - useCallback / useMemo
- On utilise toujours useCallback pour créer des fonctions, cela permet fasciliter le dévéloppement pour des gros projet.

- Une fonction déclaré avec useCallback nécessite définir ses dépéndances, sinon il ne va pas actualiser les variables de type simple ( number par exemple), exemple:
    ````
    const clickFive = useCallback(() => {
        setCount(count + 5);
    }, [count]);
    ````
- Si on ne met pas count dans sa dépénce, il ne va pas actualiser la valeur de count après chaque render, et du coup la valeur de count reste fixe (valeur d'origine + 5).
- On utilise useCallback pour éviter le max des render inutiles possibles.
- useCallback retourne la référence de la fonction ainsi crée . Mais useMemo ne le retourne pas la référence.
- Lorsqu'on utilise useMemo, on doit préciser explicitement un return , pour retourner quelques choses. useMemo permet de stocker un peu ce qu'on veut .
    par exemple : 
    ````
     const visibleTodos = useMemo(
        () => filterTodos(todos, tab),
        [todos, tab]
      );
    ````
    Attention : une fonction flèche à une ligne possède un return implicite.