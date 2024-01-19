# Général
*** 
#### 1 - les commandes git : fetch/ pull/ push/ sync
***
- __Récupérer__ (Fetch)
- __Tirage__ (pull)
- __Envoi__ (push)
- __Synchroniser__ (Tirer puis pousser)

__Récupérer__
- On doit récupérer et tirer avant de pousser.
- Récupération vérifie s'il y a des validations distantes à incorporer dans vos changements lovaux.

__Extraction__
- Tirez toujours avant de pousser. Quand vous tirez en premier, vous pouvez empêcher les conflits de fusion en amont.

__Envoi de données__
- On commit nos changements locaux
- On pousse nos commits sur Github.


# SQL
***
#### 1 - Difference entre cast et convert
***
CAST:
```
CAST (expression AS data_type)
```
- expression can be a column or a value , for example We have a column 'salary' stored as varchar in our table 'employee' and we want to convert it into an integer:
    ````
    SELECT CAST(salary AS INT) FROM employees;
    ````
Convert:
````
CONVERT(data_type(length), expression, style)
````
- sytle : optional parameter, determines how the result should be formatted

- flexibility   : 'CONVERT' has an additional style parameter.
- Universality  : 'CAST' is universal support across different SQL dialects, making it more portable. 
# C#

***
#### 1 - Track change of specific user
***
1. D'abord , on définie les deux screens : 
    - SA = Screen qui est déjà ouvert.
    - SB = Screen à ouvrir.
2. D'abord, côté SA, on prépare l'appel à l'écran SB:
    2.1. On recupère les données depuis la base de donnée:
    
        ````
        dicProcsParams["id"] = drcurrent_row.Field<int>("$id");
        DataSet dsRetour = DB.DataSetLoadWithParams("CYU_FORMATION_GET_TRACK_LOGS", dicProcsParams);
        ````
    2.2. On crée une nouvelle instance d'écran SB, on l'initialise avec dsRetour, enfin on l'affiche
      
        ````
        if(dsRetour.Tables.Count > 0) 
        {
            DataTable dt = dsRetour.Tables[0];
            FrmTrackLogs TrackLogs = new FrmTrackLogs(this, "title") // Constructeur de SB
            TrackLogs.SetDataTable(dt); // Une méthode public de SB
            TrackLogs.ShowDialog(); // Sert à l'affichage.
        }
        ````
3. Puis, on explique ce qu'on a fait côté SB:
    3.1. D'abord, on personalise notre constructeur: 
       
    -  ````
        public FrmTrackLogs(MFForm parentForm, string title)
        {
          InitializeComponent();
          m_parentForm = parentForm;
          this.Text = title;
        }
        ````
    3.2 On implémente notre fonction __public void SetDataTable(DataTable dt)__
    - Binding source (Renseignes toi la différence entre bindingsource et dataview ).
        ````
        BindingSource bdSource      = new BindingSource { DataSource = dt; }
        mfGridTrackLogs.DataSource  = bdSource;
        ````
    - AutoResize/ HeaderText/ .Sort
        ````
        mfGridTrackLogs.AutoResizeColumns();
        mfGridTrackLogs.Columns["table_name"] = "Table name";
        ...
        mfGridTrackLogs.Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
        ````
    - CellFormatting
        ````
        mfGridTrackLogs.CellFormatting += TrackCelFormatting
        ````
***
#### 2 - Utilisation d'une classe private pour englober les valeurs de même type
***
C'est pas obligatoire, mais c'est une très bonne pratice, ça peut facilite l'utilisation, et la lisibilité de tes codes.
C'est peut être très utiles quand il y a beaucoup des variables constants.
Par exemple:
```
private class ColumnDefines
{
	public static readonly string old_value = "old_value";
	public static readonly string new_value = "new_value";
}

ColumnDefines.old_value
```

***
#### 3 - Difference constant et readonly
***
Il existe deux manière pour déclarer une constant en C#:
- static const
- static readonly

Bien qu'à priori similaires, ces deux approches ont des comportements qui peuvent être très différents.

Utilisation de __const__ :
- Doît être initialisée lors de sa déclaration.
- Nécessite de l'initialiser au moment de la compilation, donc ne pas l'initiaiser par un appel depuis une base de données.
- Il n'est pas véritablement une variable, il remplace directement la valeur de la constante
    <=> Une variable __const__ n'a pas d'espace réservé en mémoire.
- On peut la rapprocher au #define de C/C++

Utilisation de __readonly__ :
- readonly n'est en lecture seule qu'en dehors d'un constructeur. 
- Il existe en tant qu'une variable, donc on peut la manipuler par référence, ce qui n'est pas le cas avec const. (Ce qui permet une modification éventuelle avec readonly)

En cas de doute, utilise la variante avec __readonly__ , ça prend des places mais c'est complètement négligeable avec ton 16GB de ram !

***
#### 4 - Fixer la ligne selectionné quand on click "amend"
***
We can do it by calling the function __.AcceptChanges()__ on the dataTable.
```
...
... Tous les autre codes...
m_dtSourceEmployee.AcceptChanges();
```

***
#### 5 - CellFormatting
***
On a pas besoin de utiliser foreach ou quoique soit dans CellFormatting, car CellFormatting l'a fait lui même !!! mettre un foreach avec CellFormating peut rendre l'application inutilisabe au terme de complexité (exponentielle).
Chaque fois qu'on apporte une modification sur CellFormatting, CellFormating lance un double for pour envoyer des notifications à chaque fonction subscriber. 
Dans le deuxième argument __e__, on reçoit des informations telque rowIndex et ColumnIndex de la cellule qui est entrain de subir des traitements.
Exemple1 pour changer des couleurs: 
```
private void EmployeeCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
{
  if (e.RowIndex == 0)
    return;
  DataRow drCurrent = (mfGridEmployee.Rows[e.RowIndex] as MFGridRow)?.DataRow;
  if (drCurrent == null) 
    return;
  if(!string.IsNullOrEmpty(drCurrent.Field<string>(m_sEmp_status)) && drCurrent.Field<string>(m_sEmp_status) == "C")
  {
    mfGridEmployee.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
  }
}
```
Exemple2 Pour changer des mots d'affichage:
````
private void TrackCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
{
  //if (e.RowIndex == 0)
  //  return;
  DataRow drCurrent = (mfGridTrackLogs.Rows[e.RowIndex] as MFGridRow)?.DataRow;
  if(drCurrent == null) 
    return;
  string action = drCurrent.Field<string>(DefineTrackLog.m_sTra_action);

  if (action == "U")
    action = "Update";
  else if (action == "A")
    action = "Add";
  else if (action == "L")
    action = "Logical Delete";
  else if (action == "R")
    action = "Undelete";

  mfGridTrackLogs.Rows[e.RowIndex].Cells[DefineTrackLog.m_sTra_action].Value = action;
}
````




