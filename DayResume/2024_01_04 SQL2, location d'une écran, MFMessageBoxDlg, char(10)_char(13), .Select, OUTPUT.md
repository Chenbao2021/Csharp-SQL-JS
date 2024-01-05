# SQL
#### 1 - STRING_SPLI
```
STRING_SPLIT(string, separator);
```
- Il génère 1 table d'une seul colonne.
- Il fait l'inverse de String_AGG sur une varchar.
- On ne peut pas faire une jointure avec une telle table.

#### 2 - Substring
```
substring(str, debut_index, nbr_wanted);
```
- On obtien un substsring qui commence par index "debut_index" et prend "nbr_wanted" caractères.
- Si un nbr_wanted trop grand, il s'arrête à la fin.

#### 3 - Replace
```
replace(str, target_string, replace_string)
```

#### 4 - SELECT VS RETURN
- Tous SQL indirectement retourne 1 int(par défaut '0')
- Donc on peut surcharger le return, by explicitement defined 'return x'
- return if often used in nested procedures.
- select - 1 => Un DataTable
- return -1 => Ce sera un valeur, où on a la moyen de la récupérer.

#### 5 - OUTPUT
- Utile pour des traitements récursives
- Une fonction ne renvoie qu'un seul chose, si on veut récupérer 4 valeurs, on doit faire 4 accès à la table, ce qui peut être coûteux. On peut utiliser le mot clé OUTPUT pour renvoyer plusieurs choses à la fois.
    ```
    CREATE PROCEDURE Sales.uspGetEmployeeSalesYTD
        @SalesPerson nvarchar(50)
        @SalesYTD money OUTPUT
    AS
        SET NOCOUNT ON;
        
        SELECT @SalesYTD = SalesYTD
        FROM Sales.SalesPerson AS sp
        JOIN ...
        
        RETURN;
    GO
    ```
    Et pour pour l'exécuter
    ```
    EXECUTE Sales.uspGetEmployeeSalesYTD  
        N'Blythe', @SalesYTD = @SalesYTDBySalesPerson OUTPUT;  
    ```
#### 6 - CURSOR et WHERE
- Parfois on ne peut pas tout remonter par selection, on doit faire des traitements ligne par ligne, WHILE et CURSOR sont les deux lignes qui peuvent servir dans ce cas la . Au niveau d'optimisabilité, les deux sont similiaires.

- CURSOR est long à écrire, mais il fait une seul SELECT
- WHERE a une syntaxe plus court, mais il faut que la table a une colonne d'Identity. Il est à éviter sur des gros tables car à chaque boucle il fait une SELECT.

# C#

#### 1 -  Chercher le dll de l'écran
On ouvre un écran dans le medyssis, on clique 'F5' du clavier,
dans l'écran qui apparaissent, la location de dll se trouve dans "Load Screen".

#### 2 - Au moins une fois par jour, CVER de Medissis_cl et Medissis_serv 

#### 3 - Medissis ne fait jamais des appels SQL directement, il donne des paramètres à 1 service, et il reçoit des réponses.

#### 4 - On ne throne pas d'exception, on évite de remonter les erreurs

#### 5 - On utilise MFMessageBoxDlg au lieu de MessageBox
- MFMessageBoxDlg et MessageBox sont tous les deux des classes, qui ont des méthodes pour afficher des alertes .
- On utilise ".ShowDialog()" pour afficher une alerte
    ```
    ShowDialog(Form frm, string sText, string sTitle, MFMessageBoxDlgButtons messageBoxButtons, MessageBoxIcon messageBoxIcon, MessageBoxDefaultButton defaultButton)
    ```
    - frm               : On met "this"
    - sText             : Message erreur
    - sTitle            : Le titre d'alerte , affiché en haut.
    - messageBoxButtons : Constant prédéfinie, ex : MFMessageBoxDlgButtons.OK
    - defaultButton     : Constant prédéfinie, ex : MessageBoxIcon.Error

#### 6 - 1 test if puis le traitement à la ligne.

#### 7 - Après ajout d'une ligne dans la table, la ligne reste selectionnée.
Quand la grid affiche un DataView, son MFGrid.Rows.Count est le nombre des lignes apparaissent du DataView.
et les indexes des lignes sont decidé par la méthode de sort, donc pour assurer que la dernière ligne ajouté se trouve à la fin, on choisit la colonne de SORT celui de ID(Et pas celui de annual_salary comme j'ai fait).
