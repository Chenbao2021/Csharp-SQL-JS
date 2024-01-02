# SQL
#### I - UPDATES
1. Premiere façon d'écrire un UPDATE (Pas recommandé)
    ```
    UPDATE
        tab_name
    SET
        column_name = 'new value'
    WHERE
        id = 2
    ```
2. Deuxième façon d'écrire un UPDATE(recommandé, car on peut d'abord tester avec SELECT)
    ```
    // SELECT tab_name.column
    UPDATE tab_name SET tab_name.column = "new value"
    FROM tab_name
    WHERE tab_name.id = 2;
    ```

#### II - DELETE
1. Première façon d'écrire un DELETE(Pas recommandé)
```
DELETE 
    tab_name
WHERE
    column1_name BETWEEN 12.00 AND 14.00
    AND column2_name IS NULL;
```
2. Deuxième façon d'écrire un DELETE (recommandé)
```
// SELECT column1_name, column2_name
DELETE
FROM
    tab_name
WHERE
    column1_name BETWEEN 12.00 AND 14.00
    AND column2_name IS NULL;
```
# C#
#### I - On ne modifie pas directement les grids.
- Les grids ne contiennent pas des données , ils travaillent avec "DataTable".
- Dans le MAIN on appelle Constructeur, et puis le programme appel automatiquement la méthode Load.

#### II - BeginUpdate() et EndUpdate()
Entre ces deux méthodes, tous les changement des données ne provoque pas de re-render.

#### III - Toujours utiliser la méthode "InitGraphics()", puis utiliser la méthode "MFGridHelpers.InitGraphicsGrid"
```
    MFGridHelpers.InitGraphicsGrid(FormTheme.m_gridStyle, mfGridEmployee, true, GridStyle.BackColorMode.MAIN_BACKCOLOR);
```
- FormTheùe.m_gridStyle : Prédéfinie dans le système
- mfGridEmployee        : Nom du grid .
- true                  : bUseAlternatingFont, Indique si on utilise l'alternance des fontes
- GridStyle.BackColorMode.Main_BACKCOLOR : Indique quelle couleur utiliser pour le fond des cellules.

#### IV - Ajouter "i"/"s"/"d"/"b"/"cb"/"dt" etc. pour indiquer le type de variable.
#### V - On utilise .Value au lieu de .Text,  et on utilise .Lock au lieu de .Enable

