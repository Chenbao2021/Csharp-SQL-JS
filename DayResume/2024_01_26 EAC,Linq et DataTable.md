# C# 

***
#### I - Le compoent EAC 
***
EAC est l'abréviation du component: MFControl.MFEditAutoCompletion.
On peut utiliser en tant qu'un combo à l'aide d'une DataTable, et dans ce cas, il est beaucoup plus simple à utiliser qu'un MFCombo

Pour intialiser un EAC , on suit ces étapes:
1. D'abord on doit initialiser notre DataTable.
2. Puis on utilise la méthode .SetAutoCompletionSource()
    Par exemple:
    ```
    private void LoadEAC_PBIFamily()
    {
      m_dtPBIFamilly = RefDirector.RefManager.GetRefTableShp(IORefType.MED_REF_CODETABLE, 420);
      m_eac_PBIFamilly.SetAutoCompletionSource(m_dtPBIFamilly, RefDefines.full_name, RefDefines.id, null, null);
    }
    ```
    - Le troisième paramètre doit être une colonne de type Int, et c'est lui qu'on va utiliser pour faire la correspondance entre __.selectedValue__ et la deuxième paramètre (RefDefines.full_name).
    - Le deuxième paramètre sera la colonne qui contient des valeurs pour affichage niveau EAC.
    - Le première paramètre sera la DataTable qui sert pour fournir les données.
3. Si on affecte un entier qui ne correspondent pas à aucune valeur de la DataTable à .SelectedValue, alors le component EAC va prendre le valeur entière directement pour l'affichage, sinon il prend le valeur d'affichage du deuxième paramètre.


Pour nettoyer les valeurs:
- component_EAC.SelectedValue = null

***
#### II - Linq et DataTable
***
On peut utiliser Linq Methode pour faire une filtrage:
1. On utilise la méthode .AsEnumerable() pour transformer le DataTable en un type énumérable.
2. Puis on peut utiliser la méthode .Where(row => row.Field<>() ...) pour faire la filtrage
3. Puis on peut soit utiliser .CopyToTable() pour affecter la table après filtrage,
    Soit utiliser .FirstOrDefault() pour obtenir juste une DataRow.

