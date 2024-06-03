# SQL
#### I - Procédure et incompatible.
Quand on modifie une procédure SQL, on doit augmenter le numéro de version si la procédure devietn incompatible.
Elle est incompatible si:
1. Elle prend plus de paramètres
2. Elle retourne plus de select
3. Elle retourne plus de colonne.

# C#
# I - Accesseur ``get`` 
Un accesseur ``get`` est une méthode spéciale associée à une propriété qui permet de récupérer la valeur de cette propriété. 
__Fonctionnement de l'Accesseur__
````C
public List<VesselsModel> vessels
{
  get
  {
    return m_listVessels;
  }
}
````
1. __Accès à la Propriété :__
    Quand on écrit ``var vesselsList = instance.vessels;``, le runtime C# reconnaît qu'il faut appeler l'accesseur ``get`` de la propriété vessels.
2. __Exécution de l'Accesseur :__
    Le runtime appelle l'accesseur ``get``, c'est-à-dire le bloc de code à l'intérieur de get { ... }.
3. __Retour de la valeur :__
    L'accesseur exécute le code ``return m_listVessels;``, renvoyant ainsi la valeur actuelle de ``m_listVessels``.
4. __Assignation de la Valeur:__
    La valeur retournée par l'accesseur ``get`` est assignée à la variable vesselsList.

