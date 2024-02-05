# C# 
***
#### 1 - Trouver la procédure utilisé dans un écran
***
1. Lancer l'écran en dev.
2. Lancer MetaTrace
3. Exemple: "Amend", mettre des données to update dans l'écran, puis valider, et dans MetaTrace tu verras quelle méthode il a appelé.
4. Trouver cette méthode, __ctrl + clique droite__ jusqu'à la fin.
5. Puis mettre ton souris sur le nom de la méthode, et dans la description de la méthode tu trouves la classe/interface qui contient cette méthode, par exemple :
    ````
    EncapsuledDataSetTransport UpdateLineCostParam(DataSet dsData);
    ````
    Et la méthode UpdateLineCostParam se trouve dans ISvcContract.:
    ````
    ISvcContract.UpdateLineCostParam(DataSet dsData)
    ````
6. Clique __ISvcContract__ , puis regarder la description, et tu trouves le nom de la service qui contient la définition de la méthode.
7. Va dans HIGGINS, et dans la section __medissys_srv__, trouves la service correspondante et ouvre localement
8. Enfin, Recherches dans les fichiers, le nom de la méthode, et dans la définition de la méthode, tu trouveras quelle procédure il a appelé.
9. Vas dans SQL Server management Studio, et cherches le nom du procédure dans : D:\Git\Totsa\Med\SQL 

***
#### 2 - Pattern matching
***
Exemple de code:
````
private string SelectedIdToCharAsString(MFEditAutoCompletion mfEac)
{
  if (!(mfEac.SelectedId is int iSelectedId))
    return null;

  return ((char)iSelectedId).ToString();
}
````
- Ici, dans le __if__, on voit une déclaration conditionnelle.
    Elle vérifie si la propriété 'SelectedId' de l'objet 'mfEac' est de type 'int' et la convertit en 'iSelectedId'. Si ce n'est pas le cas, la méthode renvoie 'null'. 
__Cela sert à s'assurer que 'SelectedId' est un entier avant de le traiter comme tel.__

***
#### 3 - Params
***
````
RadioButtonToString(params MFRadioButton[] arrMfRb)
{
  foreach (MFRadioButton mfRb in arrMfRb)
  {
    if (mfRb.Checked)
      return mfRb.Text.Substring(0, 1);
  }
  return null;
}
````
- le fait d'utiliser __params__ dans la signature, suivi du type 'var_type[]' suggérè que la méthode accepte un nombre variable d'arguments de type 'var_type'.
- Lors de l'appel de la méthode, vous pouvez fournir un tableau d'objets ou simplement une liste d'objets séparés par des virgules. => Cela rend la méthode flexible en termes de nombre d'arguments qu'on peut passer.


***
#### 4 - Colonnes techniques
***
On peut mettre un __inv__ après la symbole $ d'une colonne technique.


