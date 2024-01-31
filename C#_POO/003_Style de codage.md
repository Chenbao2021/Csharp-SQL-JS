# Introduction
1. Noms d'identificateurs C#
2. Convention de codage C#
***
# I - Règles et conventions de nommage des identificateurs C#
***
Un __identificateur__ est le nom que vous assignez à un type(classe, interface, struct, délégué ou enum), un membre, une variable ou un espace de noms.

#### Règle de nommage
- Les identificateurs doivent commencer par une lettre ou un trait de soulignement(_)

On peut déclarer des identificateurs qui correspondent à des mots clé C# en utilisant le préfixe __@__ sur l'identificateur. ex: @if déclare un identificateur nommé if.

#### Conventions d'attribution d'un nom
Par convention, C# utilise:
- PascalCase pour les noms de types, les espaces de noms et tous les membres publics
- L'équipe dotnet/docs utilise les conventions suivantes:
    - Les noms d'interfaces commencent par un __I__ majuscule
    - Préférez la clarté à la concision
    - Les champs d'instance privées commencent par un trait de soulignement(_)
    - Les champs statiques commencent par s_ .
    - Évitez d'utiliser des abréviations ou des acronymes dans des noms.
***
# II - Conventions de code C# courantes
***

- Utilisez des requêtes et des méthodes LINQ pour la manipulation de collection afin d'améliorer la lisibilité du code.
- Utilisez __var__ uniquement lorsqu'un lecteur peut déduire le type de l'expression.
- Écrivez du code avec clarté et simplicité.
- Évitez les logiques de code trop complexes.

#### Donnnées de chaîne
- Utilisez une __interpolation de chaîne__ pour concaténer les chaînes courtes :
    ````
    string displayName = $"Mr. {vFirstName}, {vLastName}"
    ````
- Lorsqu'on travaillez avec une quantité de texte importante, utilisez __System.Text.StringBuilder__

#### Tableau
- Utilisez la syntaxe concise lorsque vous intialisez des tableaux sur la ligne de déclaration.
    ````
    string[] vowels1 = {"a", "b", "c", "d" };
    ````

#### Délégués
Les délégués sont des types de données qui permettent de __stocker des références vers des méthodes__.
Les délégues apportent:
- Une abstraction supplémentaire
- Permettant une plus grande flexibilité dans la manière dont les méthodes sont référencées et appelées.
- Particulièrement utiles dans les situations où vous souhaitez passer des méthodes en tant qu'argument, utiliser des méthodes anonymes ou travailler avec des événements.

Action<> et Func<> sont des types de délégués génériques très utiles en C# pour représenter des méthodes anonymes ou des expressions lambda.
Action ne retourne pas des valeurs , tandis que Func retourne des valeurs.

- Utilisez Func<> et Action<> au lieu de définir des types délégués. Dans une classe, définissez la méthode délégué:
    ````
    Action<string> actionExample1 = x => Console.WriteLine($"x is: {x}");
    Action<string, string> actionExample2 = (x, y) =>
        Console.WriteLine($"x is: {x}, y is {y}");
    Func<string, int> funcExample1 = x => Convert.ToInt32(x);
    Func<int, int, int> funcExample2 = (x, y) => x + y;
    ````

#### Instructions __try-catch__ et __using__ dans la gestion des exceptions
- Utilisez une instruction __try-catch__ pour la plus grande part de la gestion des exceptions
- Simplifiez votre code à l'aide de l'instruction __using__. Si vous avez une instruction try-finally dans laquelle le seul code du bloc __finally__ est un appel à la méthode __Dispose__, utilisez à la place une instruction using.
    Version 1 : Finally only call Dispose.
    ````
    Font bodyStyle = new Font("Arial", 10.0f);
    try
    {
        byte charset = bodyStyle.GdiCharSet;
    }
    finally
    {
        if (bodyStyle != null)
        {
            ((IDisposable)bodyStyle).Dispose();
        }
    }
    ````
    Version 2 : Faire la même avec une instruction __using__
    ````
    using(Font arial = new Font("Arial", 10.0f))
    {
     BYTE charset2 = arial.GdiCharSet;   
    }
    ````

#### Opérateur && et ||
- Utilisez __&&__ >>  __&__ et __||__ >>  __|__ lorsque vous effectuez des comparaisons:
    - && : Lorsque la première expression a la valeur false, il n'évalue pas la suivante.
    - & : Évalue les deux expressions, même si le première est false.
    - || : Si condition1 est évalue vrai, alors la condition2 ne sera pas évalué.
    - | : Les deux conditions sont toujours évalués.
    Engénéral, & et | sont utilisés pour des opérations binaires.

#### new opérateur
- Utiliser une forme concises d'instanciation d'objet:
    ````
    ExampleClass instance2 = new();
    ````
- Initialiseur et constructeur C# :
  On peut utiliser des initialiseurs d'objets pour initialiser des objets de type de façon déclarative sans appeler explicitement un constructeur pour le type :
    - Le compilateur traite les initialiseurs d'objets en accédant d'abord au constructeur d'instance sans paramètre, puis en traitant les initialisations des membres. 
    - Si le constrcuteur sans paramètre est déclaré comme __private__ dans la classe, les intialiseurs d'objets qui nécessitent un accès public échoueront.
- Utilisez des initialiseurs d'objets pour simplifier la création d'objets:
    ````
    ExampleClass thirdExample = new ExampleClass { Name = "Desktop", ID = 37414, Location = "Redmond", Age = 2.3 }
    ````

#### Gestion des événements
- Utilisez une expression lambda pour définir un gestionnaire d'événements que vous n'aurez pas besoin de supprimer ultérieurement:
    ````
    public Form2()
    {
         this.Click += (s, e) => 
        {   
            MessageBox.Show(
                ((MouseEventArgs)e).Location.ToString()
            );
        }
    }
    ````
- La définition traditionnelle d'un gestionnaire d'événement ressemble à ça: 
    ````
    public Form1() 
    {
        this.Click += new EventHandler(Form1_Click);    
    }
    void Form1_Click(object? sender, EventArgs e){
        MessageBox.Show(...)
    }
    ````
- Exemple 1 : Dans les projets que tu fait, tu peux trouver l'utilisation des événements partout , dans le fichier "_.Designer.cs":
    1. this.m_btAddOk.Click += new System.EventHandler(this.m_btAdd_Click);
    2. this.m_grRes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_grRes_CellDoubleClick);
	3. this.m_grRes.SelectionChanged += new System.EventHandler(this.m_grRes_SelectionChanged);

#### Membres static
- Appelez les membres static en utilisant le nom de la classe : Nom_classe.Membre_statique.
- Ne qualifiez pas un membre statique défini dans une classe de base avec le nom d'une classe dérivée.

#### Placer les directives using en dehors de la déclaration d'espace de noms
- C'est mieux de placer les déclarations __using__ en dehors de l'espace de noms.

#### Recommandations sur le style:
- Limitez les lignes à 65 caractères pour améliorer la lisibilité du code sur les documents.
- Décomposez les instructions longues en plusieurs lignes pour améliorer la clarté.
- Utilisez le style <Allman> pour les accolades: les accolades ouvrantes et fermantes ouvrent et ferment leur propre nouvelle ligne. 

#### Conventions de disposition
- Écrivez une seule instruction par ligne.
- Écrivez une seule déclaration par ligne.
- Utilisez des parenthèses pour rendre apparentes les clauses d'une expression:
    ````
    if ((startX > endX) && (startX > previousX))
    {
        // Take appropriate action.
    }
    ````
