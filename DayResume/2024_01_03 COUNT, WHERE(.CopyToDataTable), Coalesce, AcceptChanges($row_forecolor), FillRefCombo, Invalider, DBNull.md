# SQL
#### 1 - Coalesce
Evaluer les arguments dans l'ordre et retourner la valeur actuelle de la première expression qui ne prend pas initialement la valeur "NULL".
    ```
    SELECT COALESCE(NULL, NULL, '3', '4')
    ```
# C#
#### 1 - e.RowIndex , MFGrid.SelectedRows[0]
- e.RowIndex    : Index de la ligne contenant la cellule pour laquelle l'événement se produit.
    ```
    m_iSelectedRow = e.RowIndex;
    ```
- .SelectedRows : Retourne les lignes selectionné si le mode de selection est un mode de selection par ligne.
    ```
    DataRow drData = (mfGridEmployee.SelectedRows[0] as MFGridRow).DataRow;
    ```
e.RowIndex peut provoquer des erreurs lorsque on change le mode de "order by", ce qu'il vaut dire qu'il respecte toujours un ordre identique même si l'affichage change quand on change de 'order by'.

.SelectedRows évite ce problème, il affiche que le valeur de la ligne actuellement affiché.

#### 2 - .Count
- Don't use this in the if :
```
if (dsRetour != null && dsRetour.Tables[0] != null) {...}
```
- Instead , use this:
```
if (dsRetour != null && dsRetour.Tables.Count > 0) {...}
```

#### 3 - WHERE
- Quand on a une DataTable, on doit utiliser .AsEnumerable pour la transformer en type énumérable, et après les requêtes Linq, on doit la retransformer en DataTable par .CopyToDataTable.
    ```
    mfGridEmployee.DataSource =
    m_dtSourceEmployee.AsEnumerable()
      .Where(row => row.Field<string>("$status") != "C")
      .CopyToDataTable();
    ```

#### 4 - Quand $row_forecolor change, écran n'est pas up to date
- Faut utiliser DataTable.AcceptChanges() pour confirmer les modifications et mettre à jour l'écran.
    ```
    m_dtSourceEmployee.AcceptChanges();
    ```
#### 5 - .DefaultDataFilter() et DataVue
- DefaultDataFilter : Il fait un trie sur les données à afficher sur l'écran
- DataVue: Il fait un trie directement sur les données
    ```
    if (codeError == 1)
    {
        Clean();
        if(bDelete)
        {
          drData["$status"] = "C";
          drData["$row_forecolor"] = -65536;
        } else
        {
          drData["$status"] = 'A';
          drData["$row_forecolor"] = 0;
        }
        if (mf_btDelete.Text != "Logical Delete") mf_btDelete.Text = "Logical Delete";
    }
    m_dtSourceEmployee.AcceptChanges();
    ```
#### 6 - Utiliser "FillRefCombo" pour fasciliter la mise en place des combos
- Example de code:
    ```
    m_dtSourcePosition      = dsRetour.Tables[0];
    m_cbPosition.FillRefCombo(m_dtSourcePosition, "", "display_order", "ID", DISPLAY_MEMBER);
    ```
- La signature du code:
    ```
    public void FIllRefCombo(DataTable dataTable, string rowFilter, string sort, string valueMember, string displayMember)
    ```
    - dataTable: La DataTable qui contient les données
    - rowFilter: Le nom de la colonne servant à filtrer les éléments affichés
    - sort: Nom de la colonne de tri, si null tri selon le DisplayMember
    - valueMember: Nom de la colonne servant de ValueMember
    - displayMember: Nom de la colonne affichée aux utilisateurs

#### 7 - .Invalider
Cette méthode permet d'éffacer et redéssiner l'écran.

#### 8 - NUll et DBNull
On ne peut pas affecter à DateTime ou Double "NULL", car ce sont des valeurs.
On utilise NULL pour un object, et DBNull  pour des valeurs.

- DBNull : Represente une valeur qui n'existe pas.
    ```
    drData["end_date"]       = DBNull.Value;
    ```