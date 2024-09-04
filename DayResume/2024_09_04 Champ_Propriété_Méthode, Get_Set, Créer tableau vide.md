# C#
#### I - Champs VS Propriétés
En C#, __une propriété__ et un __champ__ sont deux concepts distincts bien que souvent utilisés ensemble pour gérer des données dans une classe.
1. __Champ(field)__:
    * Un champ est une variable membre directement déclarée dans une classe ou une structure.
    * Il peut être public, privé, ou avoir d'autres niveaux d'accessibilité, mais son accès est généralement direct(Pas d'encapsulation).
    * Ne permet pas d'ajouter des validations ( des calculs ou des transformations lorsque les données sont lues ou modifiées ).
    * Exemple:
        ````C
        public class Example
        {
            // Champ privé
            private int age;
            
            // Champ public (ce qui n'est pas recommandé pour encapsulation)
            public string name;
        }
        ````
2. __Propriété(property)__:
    * Une __propriété__ est un mécanisme qui encapsule un champ, en fournissant un accès indirect avec des accesseurs __get__ et __set__.
    * Les propriétés permettent de contrôler la lecture et l'écriture d'un champ privé ou de l'encapsuler avec des logiques additionnelles comme des validations ou des calculs avant de retourner ou de modofier une valeur.
    * Les propriétés peuvent avoir des accesseurs de différents niveaux d'accessibilité(Par exemple public get et private set).
    * Exemple:
        ````
        public class Example
        {
            private int _age;
            // Propriété avec un accesseur public et une validation dans le set
            public int Age
            {
                get { return _age; }  // Lire la valeur
                set 
                { 
                    if (value >= 0) // Validation
                    {
                        _age = value;  // Modifier la valeur
                    }
                }
            }
        }
        ````

#### II - Propriétés VS Méthodes
* Une propriété en C# est une façon plus propre et concise d'exposer un champ privé tout en maintenant le contrôle. (On peut définir ``get`` pour la lecture, et ``set`` pour l'écriture).
* Une méthode pourrait également faire ce travail, mais l'utilisation d'une propriété rend le code plus lisible et expressif, car cela ressemble à l'accès direct à un champ, alors qu'en fait il s'agit d'un accès contrôlé.

* Exemple:
    ````SQL
    public class ListPortModal
    {
        protected List<PortModel> m_listPorts = new List<PortModel>();
    
        // Propriété publique pour accéder à la liste des ports
        public List<PortModel> ports
        {
            get
            {
                return m_listPorts; // Retourne la liste protégée
            }
        }
    }
    ````
    Puis on peut accéder à la propriété comme suit:
    ````
    public ListPortModal Get()
    {
        ListPortModal listPort = PortsDAL.GetAll();
    
        // Vous pouvez ensuite accéder à la propriété "ports" comme ceci :
        List<PortModel> portsList = listPort.ports; // Accès via la propriété
    
        return listPort;
    }
    ````
* __Nommage des propriétés en majuscule__
    En C#, il est __recommandé, mais pas obligatoire__, de nommer les propriétés en PascalCase(première lettre de chaque mot en majuscule) pour suivre les conventions de nommage.
    Cela améliore la lisibilité du code et facilite la distinction entre les propriétés et les champs(qui sont souvent en camelCase ou précédés d'un underscore).

* __Propriétés auto-implémentées__
    On peut ne pas définir explicitement un champ privé. Le compilateur génère automatiquement un champ privé en arrière-plan pour stocker la valeur.
    Cela simplifie le code lorsque vous n'avez pas besoin de logique personnalisée pour le ``get`` ou le ```set``.
    Exemple: ``public string Name { get; set; }``
    Mais si on a besoin de logique spécifique dans le ``get`` ou le ``set`` de la propriété alors on doit définir les champs correspondants.
    Exemple: 
    ````C
    public string Name
    {
        get { return _name; }
        set
        {
            if (!string.IsNullOrEmpty(value)) // Logique personnalisée
            {
                _name = value;
            }
        }
    }
    ````

#### III - Créer une table vide
* Code:
    ````
    if (string.IsNullOrEmpty(jsonString))
    {
        return new Terminal[0]; // Retourne un tableau vide si la chaîne est null ou vide
    }
    ````
* Détails:
    ``return new Terminal[0]``: "Crée un tableau de type ``Terminal`` avec une taille de 0", donc c'est un tableau qui n'a pas d'éléments.