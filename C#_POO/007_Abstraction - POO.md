# Abstraction - POO
En POO, l'abstraction fait référence au processus de séparation des aspects essentiels d'un objet ou d'un système logiciel de ses détails d'implémentation spécifiques.

abstraite = Sans se préoccuper des détails spécifiques d'implémentation.
- Peinture abstraite: Elle représente des idées, des émotions ou des formes, mais elle ne tente pas de reproduire la réalité de manière détaillée.

L'abstraction est importante pour plusieurs raisons:
- __Simplification de la complexité__ : En cachant les détails d'implémentation, l'abstraction permet aux développeurs de se concentrer sur les aspects importants d'un système sans être submergés par des détails techniques.
- __Facilitation de la gestion du code__ : En divisant le code en modules abstaits, il devient plus facile de le maintenir, de le réutiliser et de le modifier sans affecter le reste du système.
- __Encapsulation__ : L'abstraction est souvent associée à l'encapsulation, qui consiste à regrouper les données et les opérations qui les manipulent dans une même entité. => Limiter l'accès direct aux données et de garantir une meilleure modularité et sécurité du code.
- __Flexibilité et extensibilité__ : En utilisant des abstractions bien conçues, les développeurs peuvent créer des systèmes plus flexibles et extensibles, capable de s'adapter plus facilement aux changements de besoins ou d'environnements.

L'utilisation des interfaces est l'une des principales techniques pour implémenter de l'abstraction, mais ce n'est pas la seule. Les interfaces sont en effet un moyen courant de définir des contrats abstraits entre les différentes parties d'un programme.
Cependant, l'abstraction en C# peut également être réalisée à l'aide d'autres mécanismes, tels que les classes abstraites, les méthodes virtuelles, les déléguées, les événements, etc.

Prenons un exemple en C# avec abstraction et sans abstraction : 
- Avec abstraction 
    ````
    using System;
    
    // Abstraction : Définition d'une interface
    public interface IShape
    {
        double CalculateArea();
    }
    
    // Implémentation concrète : classe Rectangle
    public class Rectangle : IShape
    {
        ...
    }
    ````
- sans abstraction
    ````
    using System;
    // Pas d'abstraction, seulement une classe spécifique
    public class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }
    
        public double CalculateArea()
        {
            return Width * Height;
        }
    }
    ````