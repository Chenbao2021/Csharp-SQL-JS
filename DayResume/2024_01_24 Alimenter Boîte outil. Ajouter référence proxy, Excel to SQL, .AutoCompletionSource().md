#Vocabulaire
- Remonter = charger = Select from
- Bouton déroulant = Drop down button
# SQL
***
#### 1 - Transform Excel into SQL
***
(à compléter plus tard avec des images)
En principe, on a une fichier d'Excel, où on a tous les données à ajouter dans la base de donnée.
Chaque ligne correspond à une ligne dans la database.
L'idée est donc :
- Ecrire une formule Excel, pour transformer tous les colonnes d'une ligne en une string, qui se termine avec ",".

   Par exemple
      ````
      = A3 & "= vb." & C3&","
      ````
- appliquer cette formule à tous les lignes d'Excel.
- Copie coller tous ces strs .
- Puis en SQL, on écrit la requête corresponde, et on colle ce qu'on a fait en Excel, par exemple pour Insert:
    ```
    INSERT INTO tab(...col_names)
    VALUE [copie coller tous]
    ```
- Comme ça, on peut très facilement insérer des données Excel dans des database.

# C#
***
#### 0 - Base en C#
***
- Attribuer des valeurs en même temps
    ```
    m_btAmend.Visible = m_btAdd.Visible = m_btDelete.Visible = false;
    ```
- Un char peut se mettre dans un Dictionary<int, ..> 
- EAC n'a pas le bouton déroulant par défaut , il faut l'activer par l'attribut "ShowDropDownButton".

***
#### 1 - Alimenter la boîte outil.
***
1. Dans le topbar , clique __Outils_
2. Clique le septième option : __Choisir des éléments dans la boîte à outils__
3. Parmit quatre choix dans la fênetre qui apparait, choisir : __Composants .NET Framework__
4. Cliquer __Parcourir__, puis aller dans __D/medissys/client/vc14__
5. Tous les .dll que tu as besoin se trouve ici.

***
#### 2 - Ajouter une référence proxy
***
Quand tu vois cet erreur: _...(vous manque-t-il une directive using ou une référence d'assembly)_ , 
c'est parce que la référence Proxy n'est pas encore ajouté dans ton projet.

Pour se faire :
1. Ouvrir l'explorateur de solutions
2. Clique droite sur __Références__ , qui se trouve en troisième position du projet dans mon cas. / Où se trouve dans Ajouter-> Référence
3. Puis choisir __Ajouter une solution__
4. Clique __Parcourir__, puis aller dans __D/medissys/client/vc14__
5. Trouves le nom du .dll et l'ajouter
6. Les outils MF se trouve dans le .dll "MFCtrl.dll"
***
#### 3 - MFEditAutoCompletion.SetAutoCompletionSource
***
Le component MFEditAutoCompletion possède la méthode : 
```
public void SetAutoCompletionSource(DataTable dtAll, string sDisplayCol, string sValueCol, string sColForeColor, string sColBackColor)
```
et l'attribut __ShowDropDownButton__ qui lui permet de se comporter comme un Combo en utilisant simplement une datatable.
- dtAll: La source des données pour autocompletion
- sDisplayCol : Colonne contenant les valeurs à afficher.
- sValueCol: Colonne contenant les ids pour SelectId.
- Les deux autres on peut mettre null,  car ils ne servent plus maintenant.

Un exemple d'utilisation de la méthode(avec dictionnaire , création de datatable, et utilisation de la méthode):
```
private void SetCustomValues(MFEditAutoCompletion mfEAC, Dictionary<int, string> dict)
{
    DataTable dt = new DataTable();
    dt.Columns.Add("id", typeof(int));
    dt.Columns.Add("name", typeof(string));
    
    foreach(KeyValuePair<int, string> kvpIdName in dict) 
    {
        DataRow dr = dt.NewRow();
        dr.SetField<int>("id", kvpIdName.Key);
        dr.SetField<string>("name", kvpIdName.Value);
        dt.Rows.Add(dr);
    }
    mfEAC.SetAutoCompletionSource(dt, Name, ID, null, null);
} 
```
