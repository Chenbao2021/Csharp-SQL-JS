# Introduction
Les quatre principales caractéristiques de la POO sont:
- Abstraction
- Encapsulation
- Héritage
- Polymorphisme

Abstraction:
- Modélisation des attributs et interactions pertinents des entités en tant que classes
    Afin de définir une représentation abstraite d'un système.

Encapsulation: 
- Masquage de l'état interne et des fonctionnalitées d'un objet
- Autorisation d'accès uniquement par le biais d'un ensemble public de fonctions.

Héritage:
- Possibilité de créer des abstractions basées sur des abstractions existantes.

Polymorphisme: 
- Possibilité d'implémenter des propriétés ou des méthodes héritées de différentes manières parmi plusieurs abstractions.


***
# Abstraction
***


***
# Encapsulation
***
- Membre : 
    C'est l'ensemble des méthodes, champs, constantes, propriétés et événements.
    En C#, il n'y a pas des variables ou méthodes globales.
- Encapsulation : 
    On peut spécifier le degré d'accessiobilité de chacun de ses membres au code situé en dehors de la classe ou de struct.
- Accessibilité :
    __public__: Accessible partout
    __protected__ : Accessible dans la classe, et par les instances de la classe dérivée.
    __Internal__ : Accessible uniquement dans les fichiers d'un même assembleur. Frequemment utilisé lors du développement basé sur les composants, car il permet à un groupe de composants de collaborer de façon privée sans être exposés au reste du code de l'application.
    __protected internal__ : Accessible depuis l'assembly actif ou depuis des types dérivés de la classe conteneur.
    __private protected__ : Accessible dans la classe, et  uniquement par les instances de la classe dérivée qui sont dans le même assembleur.



***
# Héritage
***
La classe dont les membres sont hérités : Classe de base.
La classe qui hérite de ces membres     : Classe dérivée.

D'un point de vue conceptuel : Une classe dérivée est une spécialisation de la classe de base.

Quelques mot-clés à connaître:
- Pour une classe:
    - abstract : Signifie que la classe est abstraite , ne peut pas être instanciée telle quelle. Elle doit être dérivée.
    - Sealed : La classe ne peut pas être dérivée.
- Pour une fonction/Propriété:
    - abstract : Se trouve dans une classe abstraite, signifie que la fonction doit être définie dans les classes dérivées non abstract.
    - virtual : Dans une classe instanciable ou abstraite, signifie que la fonction/propriété peut être redéfinie dans les classes dérivées.
    - sealed : dernière redéfinition possible de la fonction ou de la propriété
    - new : Remplace la définition de la fonction dans la classe enfant.
    - override : Redéfintiion d'une fonction __abstraite ou virtuelle__ dans la classe enfant.
    - sealed override : Dernière redéfinition d'une fonction abstraite ou virtuelle dans la classe enfant.
Une classe abstraite dont toutes les fonctions ou propriétées sont abstraites peut être remplacée par une interface.

Méthode abstraites et virtuelles:
- Si une méthode est déclaré comme virtual, alors ses classes dérivé peut remplacer la méthode avec sa propre implémentation avec le mot-clé (override).
- Si une méthode est déclaré comme abstract, la méthode doit être remplacée dans toutes les classes non abstratites qui héritent directement de cette classe.

Classes de base abstraites:
- Une classe abstraite peut être utilisée seulement si une nouvelle classe en est dérivée:
- Une classe abstraite peut contenir une ou plusieurs signatures de méthode qui sont également déclarées comme abstraites.
    - Ces signatures spécifient les paramètres et la valeur de retour, mais n'ont aucune implémentation.
    - Les classes dérivées qui ne sont pas abstraites doivent founir l'implémentation pour toutes les méthodes abstraites d'une classe de base abstraite.

Interface:
- C'est un type référence qui définit un ensemble de membres.
- Une interface peut définir une implémentation par défaut pour tout ou partie de ces membres.
- Les interfaces sont utilisées pour définir des fonctions spécifiques pour les classes qui n'ont pas nécessairement de relation de type __<est un>__.  Par exemple, IEquatable<T> n'implique pas le même type de relation <est un> qui existe entre une classe de base et une classe dérivée (Ex: Mammifère est un Animal).

Masquage des membres de la classe de base par une classe dérivée: 
- Une classe dérivée peut masquer des membres de la classe de base en déclarant les membres à l'aide du même nom et la même signature.
- Le modificateur __new__ peut être utilisé pour indiquer explicitement que ce membre n'est pas censé substituer le membre de base . (__new__ n'est pas obligatoire), mais il présent par défaut si rien n'est indiqué.
- La méthode de la classe de base peut être appelée à partir de la classe dérivée à l'aide du mot clé __base__ . 
- Substituer(Override) : On change la méthode de la classe de base . 
- Masquage(New) : On crée une méthode de même signature dans la classe dérivée, et on ne touche pas celle de classe de base.
- + : Une instance de classe dérivée mais référencé par une référence de classe de base, alors il va utiliser la méthode de la classe de base si on a utilisé __new__ au lieu de __override__ .
 

***
# Polymorphisme
***
Polymorphisme = plusieurs formes . 
Il prend 2 aspects distincts :
- Au moment de l'exécution, les objets d'une classe dérivée peuvent être traités comme des objets d'une classe de base dans les paramètres de méthode et les collections ou les tableaux.  => Le type déclaré de l'objet n'est plus identique à son type au moment de l'exécution.

- Les classes de base peuvent définir et implémenter des méthodes virtuelles, et les classes dérivées peuvent les substituer => Les classes dérivées fournissent leur propre définition et implémentation.
    - Au moment de l'exécution, quand le code client appelle la méthode, le CLR recherche le type au moment de l'exécution et appelle cette substitution de la méthode virtuelle. 

#### Membres virtuels:
- La classe dérivée peut remplacer les membres virtuels dans la classe de base, en définissant un nouveau comportement.
- La classe dérivée peut hériter de la méthode de classe de base la plus proche sans la remplacer, en conservant le comportement existant, mais en permettant à d'autres classes dérivées de remplacer la méthode.
- La classe dérivée peut définir une nouvelle implémentation non virtuelle de ces membres qui masque les implémentations de la classe de base.

Quand une classe dérivée est substituée à un membre virtuel, ce membre est appelé lors de l'accès à une instance de cette classe en tant qu'instance de la classe de base.

On utilise le terme "etendre", car quand on substitue une méthode, on peut utiliser le mot-clé "base" pour garder les codes d'origine. 

#### Masquer des membres de classe de base par de nouveaux membres:
Si on souhait que la classe dérivée ait un membre portant le même nom qu'un membre de classe de base, on peut utiliser le mot-clé __new__ pour masquer le membre de classe de base.
Dans ce cas, les membres de classe de base masqués sont accessible à partir du code client en effectuant un cast de l'instance de la classe dérivée vers une instance de la classe de base.
````
DerivedClass B = new DerivedClass();
B.DoWork(); // Calls the new method.

BaseClass A = (BaseClass)B;
A.DoWork(); // Calls the old method.
````

#### Accéder aux membres virtuels de la classe de base à partir de classes dérivées.
Une classe dérivée qui a remplacé ou écrasé une méthode ou une propriété peut encore accéder à la méthode ou à la propriété dans la classe de base avec le mot clé __base__.



