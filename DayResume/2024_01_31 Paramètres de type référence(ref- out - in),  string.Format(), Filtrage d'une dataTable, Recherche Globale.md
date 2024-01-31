# C#
***
#### 1- using string.Format to create a filter 
***
On peut utiliser strig.Format pour construire notre where condition:
````
string sFilter1 = string.Format("ref_name = 'cost_amount_type' and value2_char = 'y'")
string sFilter2 = string.Format("({0}={1} and {2}={3}) or ({0}={3} and {2}={1})",
        Columns.id_entity_leader, iId1, Columns.id_entity, iId2);
````
Dans la question suivant, pn verra comment les utiliser 

***
#### 2- Filter a DataTable by .GetFilteredRefTable or Linq method
***
Chez MetaFactory, on peut utiliser des méthodes de  RefDirector.RefManager pour récupérer des tables.
On a deux façons( ou plus, mais je ne connais pas) :
1. Avec la méthode .GetrefTableShp et LINQ methods:
    ````
     DataTable dt = RefDirector.RefManager.GetRefTableShp(IORefType.PH_REF_PHM_DS_REFERENCE);
    
     DataTable dtFilter = dt.AsEnumerable().Where(row => row.Field<string>("ref_name") == "cost_amount_type").Where(row => row.Field<string>("value_char") == "Y").CopyToDataTable();
    ````
2. Avec la méthode .GetFilteredRefTable et string.Format()
    ````
    string sFilter = string.Format("ref_name = 'cost_amount_type' and value2_char = 'Y'");
    DataTable dtFilter = RefDirector.RefManager.GetFilteredRefTable(IORefType.PH_REF_PHM_DS_REFERENCE, sFilter);
    ````

La première façon de faire est plus simple, mais limité avec les outils et services de MetaFactory, et la deuxième façon est plus générale.

***
#### 3 - paramètres de type référence
***
- En C#, on a le droit de modifier les paramètres de type référence lorsqu'ils sont passés avec le mot-clé 'ref' ou 'out'. 

- Sans indiquer le mot-clé __ref__ ou __out__, toutes les modifications effectuent à l'intérieur de la méthode ne sera pas affectée à la variable d'extérieur.

- Utilisation de __ref__ ou __out__ doit être cohérente entre la signature de la méthode et les appels de la méthode. Si la méthode utilise __ref__, tous les appels de cette méthode doivent également utiliser __ref__, idem pour __out__ .

- ref est à la fois un __out__ et un __in__.

Voici une brève explication des deux mots-clés:
- __ref__:
    - Utilisé pour passer une variable par référence à une méthode.
    - La variable doit être initialisée avant d'être passée à la méthode.
    - La méthode peut modifier la variable, et ces modifications seront reflétées à l'extérieur de la méthode.
    - Exemple:
        ````
        void MyMethod(ref int x) 
        {
         x = x * 2;   
        }
    
        int myValue = 5;
        MyMethod(ref myValue); // myValue = 10
        ````
- __out__ :
    - Utilisé pour indiquer qu'un paramètre est destiné à être initialisé dans la méthode.
    - La variable n'est pas nécessairement initialisée avant d'être passée à la méthode.
    - La méthode __doit garantir que toutes les branches de code initialisent la variable__ avant qu'elle ne quitte la méthode.
    - Exemple 1: 
        ````
        void MyMethod(out int y)
        {
            y = 42;
        }
    
        int myResult;
        MyMethod(out myResult);
        ````
    - Exemple 2 : 
        `````
        string s = Console.ReadLine();
        if(int.TryParse(s, out int nb))
        {
            Console.WriteLine($"Entier lu: {nb}");
        }
        ````
        Puis une implémentation très naïf de TryParse:
        ````
        static bool TryParseNaïf(string s, out int nb)
        {
            try
            {
                nb = int.Parse(s);
                return true;
            }
            catch(Exception)
            {
                nb = 0;
                return false;
            }
        }
        ````

À noter que l'utilisation de 'ref' et 'out' doit être faite avec précaution, car elle peut __rendre le code plus difficile à comprendre et à maintenir__.
Généralement il est préférable de retourner une valeur à partir de la méthode plutôt que de modifier les paramètres par référence.

Bonus: On a aussi le mot-clé __in__ :
- La distinction entre passage par valeur et passe __in__ :
    - Le passage par valeur provoque une copie, que l'appelé peut modifier à loisir.
    - Le passage __in__ est un passage par référence mais qui ne permet pas à l'appelé de modifier le paramètre.

***
#### 4 - Pour faire une recherche globale(Exemple dans le dll serveur)
***
1. Cliquer sur __Rechercher dans les fichiers(Ctrl + Maj + F)__
2. Dans l'Edit __Regarder dans__ : n:\medissys\serveur
3. Dans l'Edit __Types de fichier__ : *.cs
4. Enfin, dans l'Edit de recherche, mettre le nom de la méthode que tu as besoin de savoir.

