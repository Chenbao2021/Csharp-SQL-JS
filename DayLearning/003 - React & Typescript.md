# React & Typescript 
***
### I - ReactNode
Use ``ReactNode`` instead of ``JSX.Element | null | undefined | ... `` to keep your code more compact.
Par exemple:
````js
const CustomButton = <Button variant="outlined">Custom Button</Button>;
<MyComponent leftElement={CustomButton} rightElement={<p>Static Text</p>} />;
````
Avec
````js
const CustomButton = <Button variant="outlined">Custom Button</Button>;
<MyComponent leftElement={CustomButton} rightElement={<p>Static Text</p>} />;
````

La différence entre passer des éléments comme props, et importer directement un composant dans le fichier, réside dans __qui contrôle le contenu et la structure__, et dans le __niveau de responsabilité__ des composants.
1. Contrôle du contenu:
    * Avec ``ReactNode`` en props: Le composant parent contrôle le contenu.
    * Importé directement: Le composant qui a le contrôle.
2. Modularité et Réutilisation:
    * Avec ``ReactNode`` : Plus générique et peut être réutilisé dans différents contextes.
    * Importé directement : Moins flexible car il est figé dans sa structure.
3. Séparation des responsabilités:
    * Avec ``ReactNode`` en props: 
        * ``Parent`` se concentre sur le __layout__ ou la __structure__.
        * ``Fils`` se concentre sur le contenu.
    * Importé directement: ``Parent`` gère à la fois la structure __et le contenu__, ce qui peut violer le principe de séparation des responsabilités.
4. Maintenance et évolutivité:
    * Avec ``ReactNode`` en props: Si le contenu ou le comportement change, on n'a qu'à modifier les composants enfants.
    * Importé directement: On doit modifier le composant père.

On résume: Les responsabilités se divisent en deux, Layout et contenus.

### II - PropsWithChildren (à revoir)
* Différence entre passer un component par props, ou par children.

### III - Use TypeScript generics
If you are not using TypeScript generics, only two things could be happening:
* You are either writing very simple code or,
* You are missing out !!!

__Bad idea__: I create a single list component that accepts a union of items.
* Every time a new item is added, the functions/types must be updaded.
* The function is not entirely type-safe.
* This code depends on other files.
* Etc.

__Good idea__: Use a generic List.
Exemple dans la fonction ``addDoc``, qui peut prendre soit des fichiers de type file,soit des fichiers de type photo. Mais en générale ils sont presque identique.
Dans ce cas la on peut utiliser un type généric pour rendre les codes plus lisible et réutilisable.
Exemple: 
````js
const addDoc = useCallback(<T extends AttachedFile>(addedDocs: File[], type: fileType, attachedFiles: T[]) => 
{
    ...
}
````
