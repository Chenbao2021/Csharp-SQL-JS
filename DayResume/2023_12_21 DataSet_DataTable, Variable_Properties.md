# SQL
#### I - SET NOCOUNT ON
Généralement, quand on fait une requête SQL, SQL va nous dire combien des données sont impactés après la requête.
Ce qui peut être très lourd et coûteux dans des gros requêtes et procedures:
- Si des milliers des SELECT dans une procedure.

Pour éviter cela, on peut mettre ceci au début d'une Procedure:
```
SET NOCOUNT ON
```

#### II - Quand on veut executer une procedure SQL et qu'on est certain qu'il va simplement lire des données:
On ajoute ceci dans le début d'une procedure:
- SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

Pour signaler le system qu'il ne bloque pas l'accès a la table.

#### III - La méthode STRING_AGG(column_name)
Cette méthode est utilisé avec __GROUP BY__ , il concatere les résultats groupés, on peut préciser l'ordre d'apparition par un ORDER BY.
```
STRING_AGG(column_name, ',')
```

#### IV - COL_LENGTH 
On peut utiliser COL_LENGTH pour vérifier si une colonne existe ou pas : 
```
IF COL_LENGTH('tab_name', 'new_column_name') IS NULL
    ALTER TABLE tab_name ADD new_column_name varchar(255) NOT NULL DEFAULT 'A'
GO
```

# C#
#### I - DataSet et DataTable
DataSet = 1 collection of DataTable object that you can relate each other with dataRealation object. 
- A DataSet may work like a virtual database.
- A dataTable is the representation of one table in the DataSet.
- One dataTable can only be on DataSet.

To extract a dataTable from a dataSet:
```
DataTable tab1 = dataSetName.Tables["employee"];
```
            
#### II - Private variable and Properties
- The private variable can only be accessed within the same class.
- A property is like a combinaison of a variable and a method, and it can have two méthod: Get and Set
    ```
    private int m_iSelectedRow = -1;

    public int SelectedRow 
    {
        get { return m_iSelectedRow; }
        set { m_iSelectedRow = value; }
    }
    ```
    Then to access and modify the m_iSelectedRow, we use the .SelectedRow property.
#### III - Soigner tes codes, ça t'économise énormément des temps plus tard.

#### IV -  $ dans une datatable
Dans une DataTable, un colonne qui a le nom préfixe de "$" n'est pas affiché.

