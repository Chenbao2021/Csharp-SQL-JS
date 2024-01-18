# SQL 
***
#### 1. Bonne vérification si une codeError = -1 dans une table
***
La mauvaise vérification:
```
IF (SELECT codeError from #tab_information) <> -1
```
La bonne vérification
```
IF NOT EXISTS (SELECT 1 FROM #tab_information where codeError = -1)
```
***
#### 2 - Officialiser SQL 
***
1. D'abord on la pousse dans GIT
    1. Ouvrir visual studio, ouvrir "sans code"
    2. Cliquer "GIT", "dépôt local"
    3. En bas , cliquer "crayon"
    4. Commit tes modifications
    5. En haut, cliquer "Affichage", "Dépot local"
    6. "Récupérer", "Tirer", "Envoyer".

2. Enfin on la pousse dans ScriptSender
    - Attention, l'ordre est important:
        - D'abord, pousser des procédures indépendants (n'est pas utilisé par les autres procédures)
        - Enfin, pousser les procédures qui utilisent les procédures déjà poussés.

***
#### 3 - Sécuriser les variables
***
Faut éviter des variables traînent avec des  valeurs __null__ ,
Une comparaison avec un valeur null peuvent causer des erreurs.
Utiliser ISNULL() ou valeur par défaut pour tous les variables qui risquent d'avoir __null__ comme valeur.

# C#
***
#### 1 - Events, delegate, publisher/subscriber pattern
***
- Events allow a class or object to notify other classes or objects when something occurs.
- Events are used in a publisher/subscriber pattern in which the publisher raises an event and the subscribers respond to the event.
- Use __EventHandler__ as the delegate type to declare an event
- Extend the __EventArgs__ class to pass data from the publisher to subscribers.

Pour l'utilisation de .CellFormating, les méthodes subscrit seront appelé chaque fois qu'il y a une modification sur la grille.

For more information and examples , check : https://www.csharptutorial.net/csharp-tutorial/csharp-events/

***
#### 2 - BindingData
***
1. Two-way Data Binding.
    ```
    // Assuming you have a Datatable as a DataSource
    DataTable dataTable = GetFruitsData();
    
    // Create a BindingSource and set its DataSource to the DataTable
    BindingSource bindingSource = new BindingSource();
    bindingSource.DataSource = dataTable();
    
    // Bind a TextBox to a specific column in the DataTable through the BindingSource.
    // 'Text' property of the textBox1 , "FruitName" column in bidingSource.
    textBox1.DataBindings.Add("Text", bindingSource, "FruitName");
    
    // Now, any changes in the TextBox will automatically update the DataTable and vice versa.
    ```

2. Applying Filter to DataSource:
    ```
    // Apply a filter to show only fruits with Quantity greater than 10
    bindingSource.Filter = "Quantity > 10";
    ```
3. One DataSource to Many Controls:
    ```
    // Bind two DataGridViews to the same BindingSource
    dataGridViewApples.DataSource = bindingSource;
    dataGridViewPeaches.DataSource = bindingSource;
    
    // Set filters to the dataSource
    bindingSource.Filter = "FruitType = 'Apple'";
    
    // Set Sorting
    bindingSource.Sort = "FruitName ASC";
    
    // Searching for a specific fruit
    int index = bindingSource.Find("FruitName", "Banane");
    ```

***
#### 3 - Chaque modification sur une variable static readonly string, doit relancer le processus pour que la modification soit prise en compte.
***

***
#### 4 - Après avoir testé en debug, on doit aussi la tester en release
***
Quand l'application a réussit ses tests en mode debug:
1. On va faire un CVER sur HIGGINS et on télécharge la version la plus récènte du projet
2. On va faire merger le projet local et le projet HIGGINS par l'application __WinMerge.exe__, qui se trouve dans "S/tfe/med/exploit/lien utile".
3. Puis on la crée en mode release, et on commence nos tests.
4. Une fois que le projet reussit tout ses tests, on peut officialiser le projet .

Bonne habitude: On ne pousse pas DLL avant de pousser les SQLs 
