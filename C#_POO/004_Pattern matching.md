***
#### I- Introduction par un exemple
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
#### II - Explication détaillée
***
- Introduite dans les versions plus récente du langage (C# 7.0) qui permet de vérifier et de traiter des types de manière plus expressive et concise.
- Cela facilite la manipulation de données en fonction de leur structure ou de leurs propriétés.

Il existe plusieurs formes de pattern matching en C#:
- __Pattern matching de type(Type Pattern)__:
    Permet de vérifier le type d'un objet et de le convertir de manière sécurisée:
    ````
    if (obj is MyClass myObj)
    {
        // Utilisation de myObj ici
    }
    ````
- __Pattern Matching de Propriété(Property Pattern)__:
    Permet de vérifier s'il s'agit d'un type d'objet et une propriété est vrai:
    ````
    if(shape is Circle {Radius: > 0} circle)
    {
        ...
        Console.WriteLine($"Cercle avec un rayon positif : {circle.Radius}");
    }
    ````
- __Pattern matching de Switch(Switch Expression)__:
    Exemple : 
    ````
    switch (shape)
    {
        case Circle c when c.Radius > 0:
            Console.WriteLine($"Cercle avec un rayon positif: {c.Radius}");
            break;
        case Rectangle r when r.Width == r.Height:
            Console.WriteLine("Carré");
            break;
        default:
            Console.WriteLine("Autre forme");
            break;
    }
    ````
    Dans cet exemple, le 'case Circle c when c.Radius > 0' est une expression de pattern matching de switch. Il vérifie si 'shape' est un cercle avec un rayon positif. Et le cas échéant, utilise 'c' comme référence à cet objet. 
    => Cela rend le code plus lisible, et évite la nécessité de cast explicite.

- __Pattern Matching de Tuple (Tuple Pattern)__:
    Le pattern matching de tuple permet de déstructurer un tuple lors de la vérification.
    Par exemple:
    ````
    var pair = (5, 10);
    switch (pair)
    {
        case (0, 0):
            Console.WriteLine("Origine");
            break;
        case (int x, 0):
            Console.WriteLine($"Sur l'axe x à la position {x}");
            break;
        case (0, int y):
            Console.WriteLine($"Sur l'axe y à la position {y}");
            break;
        case (int x, int y):
            Console.WriteLine($"À la position ({x}, {y})");
            break;
        default:
            Console.WriteLine("Autre position");
            break;
    }
    ````
     Ici, le 'case (int x, int y)' est un exemple de pattern matching de tuple. Il extrait les valeurs 'x' et 'y' du tuple et les utilise dans le code du cas. Cela simplifie la manipulation de tuples dans les switchs en évitant la nécessité de manipuler manuellement les éléments du tuple.