# SQL
### I - Utiliser ISNULL si les valeurs peuvent être null
Exemple:
````SQL
select ...
FROM VET_REF_INTRANET_MAJOR
WHERE isnull(status, 'A') <> 'C'
````
``status`` peut avoir la valeur ``null``. Si on n'utilise pas ``isnull``,  la condition:
`` null <> 'C' ``  renvoie  false, à cause de la configuration de SQL Server.


# C# 
###0 I - Accesseur pour accéder les proprietés d'un DataRow protected
En C#, si vous avez une ``DataRow`` protégée dans votre classe, il est souvent utile de fournir des accesseurs (getters) pour accéder aux valeurs des colonnes de cette ligne de données de manière plus propre et encapsulée. 

Avec ces accesseurs, vous pouvez accéder aux valeurs des colonnes de la DataRow en utilisant des propriétés fortement typées, ce qui rend le code plus lisible et plus maintenable :
````C
DataRow row = /* obtenir la DataRow à partir d'une source de données */;
MaClasse instance = new MaClasse(row);

Console.WriteLine("Nom: " + instance.Nom);
Console.WriteLine("Age: " + instance.Age);
Console.WriteLine("Date de naissance: " + instance.DateDeNaissance.ToShortDateString());
````



