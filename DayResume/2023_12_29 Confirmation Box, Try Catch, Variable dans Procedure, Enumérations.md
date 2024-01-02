# SQL
#### I - Procedures doivent renvoyer toujours les même résultats
- Cas 1 : On ne met pas un SELECT et un UPDATE dans une même procédure, cela est difficile à gérer dans les codes.
Mais on peut mettre UPDATE, DELETE, ADD dans une même requête, car ils renvoient tous des codeErreur et messageErreur similiaires.

- Cas 2 : Si dans un procedure, on a utilisé "IF", "ELSE IF", "ELSE" , on évite de retourner des résultats de format différents . Par example : "IF" retourne 1 table, et "ELSE IF" retourne 2.

#### II - On peut déclarer des variables dans des procedures
```
CREATE PROCEDURE cyu_formation
    @parametre1 type_parametre = default_value
AS
BEGIN
    declare @variable int = 0, @variable2 varchar(max), ...
END
```
    
#### III - BEGIN and END sont commes des parenthèses .

#### IV - On utilise SCOPE_IDENTITY pour obtenir le dernier ID utilisé par INSERT
```
...
SELECT @nex_id = SCORE_IDENTITY()
```

#### V - On peut utiliser "IF" et "ELSE" dans un procedure, vérifie si le nom et prénom existe déjà ?
- Cas1 - Vérifie si le nom et prénom existe déjà avant une insertion 
    ```
    IF exists( select 1 from formation_Employee_cyu where first_name = @first_name and last_name = @last_name and id <> @id )
        SELECT @codeError = -1, @messageError = "..."
    ELSE
    BEGIN
        ...QUERY....
        SELECT @codeEroor = 1
    END
    ```

- case 2 - Vérifie l'opération à faire et vérifie s'il existe dans la base de donnée
```
...
BEGIN
    if @status = 'L' and exist(select 1 from formation_Employee_cyu cyu WHERE id = @id and status = 'A')
    BEGIN ...
        select @codeEroor = 1
    END
    if @status = 'R' and exist(select 1 from ...)
```

#### VI - Un SELECT est utilisé comme RETURN en C# dans des procédures

# C#
#### I - Pour ouvrir une fenêtre de confirmation:
On peut utiliser la méthode MessageBox.Show() comme suit:
```
DialogResult res = MessageBox.Show($"Do you confirm the logical delete operation?", "Confirm", MessageBoxButtons.OKCancel);

if(res == DialogResult.OK) {
    ...
}
```
- MessageBoxButton.OKCancel est un constant prédéfinit dans le système.

#### II - Quand on déclare des variables en C#, on met toujours une valeur par défaut, parceque NULL peut souvent provoquer des problèmes (Par exemple CRASH).

#### III - Try {} Catch {}
Voici un example des codes qu'on peut utiliser pour capturer des erreurs:
```
try
{
...
}
catch (Exception ex)
{
    MFLog.LogException(ex);
    MFMessageBoxDlg.ShowDialog(this, "An error occurred during the operation.( InitDtEmployee() - FrmEmployes.cs)", "Error", MFMessageBoxDlgButtons.OK, MessageBoxIcon.Exclamation);
}
```

#### IV - Enumération
Utiliser enumération rend les codes plus facile à lire et plus facile à maintenir, 
Surtout quand il y a des modes fixes.
On évite d'utiliser des strings pour comparer des modes.

Voici les différents façons d'écrire une enum
```
enum Days {Staturday, Sunday, Monday, Tuesday, Wednesday, Thursday, Fryday}
enum Colors { Red = 1, Green = 2, Blue = 4, Yellow = 4 }
enum Country { None, UnitedStates = "United States", Canada }
```

Et en pratique, on définit une énumération puis on l'initialise :
```
public enum ScreenMode {
    VIEW = 0,
    AMEND = 1,
    CREATE = 2
}
private ScreenMode m_mode = ScreenMode.View;
```

Cas d'utilisation de enumération avec SWITCH:
```
private void Mf_btAmend_Click(object sender, EventArgs e)
{
  switch (m_mode)
  {
    case ScreenMode.VIEW  : SetMode(ScreenMode.AMEND);  break;
    case ScreenMode.AMEND : ConfirmUpdateInformation(); break;
    case ScreenMode.CREATE: ConfirmAddNewPersonal();    break;
    default:  break;
  }
}
```
#### V - Créer autant des booleans que nécéssaire
ça peut augmenter la lisibilité des codes et la maintenabilités
Example : 
```
void setMode(SreenMode mode) {
    bool bAmendOrCreate = mode == ScreenMode.Amend || mode == ScreenMode.CREATE
    bool bView = mode == ScreenMode.View
}
```

