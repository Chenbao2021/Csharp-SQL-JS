# SQL
#### I - System variable in SQL Server
- @@ROWCOUNT        : Is a system variable that returns the number of rows read by the previous statement.
- SCOPE_IDENTITY()  : Retourner le dernièr ID utilisé/Crée. 

#### II - Select d'affectation et Select de remontée.
- SELECT d'affectation  : S'il y a "=",  la SELECT est comme un SET.
    ```
    SELECT @codeError = -1, @messageError = 'Commande unknown'
    ```
- SELECT de remontée    : S'il n'y a pas "=", la SELECT est comme un RETURN dans une fonction.
    ```
    SELECT @codeError 'codeError', @messageError 'messageError', @new_id 'new_id'
    ```

- SET ne peut qu'affecter une ligne à la fois, mais SELECT peut affecter plusieurs lignes à la fois.

# C#
#### I - Pour raison de sécurité , après this.close(), on ajoute return pour éviter qu'il y a des codes qui sont resté dans la cache.

#### II - Ne pas encapsuler 1 méthode dans 1 autre méthode

#### III - C# est toujours synchrone, il n'y a pas la notion de Async/Await comme dans JS

#### IV - Pour filtration 
- Soit on fait avec LINQ sur une table globale, et chaque fois on filtre ce qu'on veut.
    ```
            mfGridEmployee.DataSource =
              m_dtSourceEmployee.AsEnumerable()
              .Where(row => row.Field<string>("$status") != "C")
              .CopyToDataTable();
    ```

- Soit on créer un VUE(DataView) puis on l'utilise comme un DataTable
    ```
          DataView dv = new DataView(m_dtSourceEmployee, string.Format("{0} <> 'C'", "$status"), m_sEmp_startDate, DataViewRowState.CurrentRows);
    ```

#### V - AccepteChanged, RejectChanged
- RejectChanges annule tous les changements jusqu'à le dernière appel a la méthode AccepteChanged.
