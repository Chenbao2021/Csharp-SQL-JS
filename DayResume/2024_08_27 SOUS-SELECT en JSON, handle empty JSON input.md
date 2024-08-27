# SQL Server
#### I - Traiter un Select en tant qu'un JSON
1. Générer du JSON à partir de SQL Server
    On peut utiliser la clause ``'FOR JSON'`` pour convertir les résultats d'une requête en format JSON. Par exemple:
    ````SQL
    SELECT 
        OrderID, 
        OrderDate, 
        CustomerID, 
        (SELECT ProductID, Quantity, UnitPrice 
         FROM OrderDetails 
         WHERE Orders.OrderID = OrderDetails.OrderID 
         FOR JSON PATH) AS OrderDetails
    FROM 
        Orders
    WHERE 
        CustomerID = 'ALFKI'
    FOR JSON PATH, ROOT('Orders');
    ````
    * ``FOR JSON PATH`` : Permet de structurer le JSON selon la hiérarchie que vous spécifiez dans votre requête.
    *  ``ROOT('Orders')`` : (Optionnel)Encapsule l'ensemble des résultats dans un objet JSON nommé "Orders"

2. Récupération des Résultats JSON en C#
    Un exemple pratique: 
    ````C
	public restriction[] restrictions
		{
			get {
				// Récupération de la chaîne JSON
				string jsonString = m_dr.Field<string>(VesselsDefines.restrictions);
				// Vérification si la chaîne est null ou vide
				if (string.IsNullOrEmpty(jsonString))
				{
					return new restriction[0]; // Retourne un tableau vide si la chaîne est null ou vide
				}
				// Désérialisation si la chaîne n'est pas null ou vide
				return JsonConvert.DeserializeObject<restriction[]>(jsonString);
		}
    ````
    * ``JsonConvert.DeserializeObject<T>()`` : Utilise Newtonsoft.Json( Ou une autre bibliothèque JSON) pour désérialiser le JSON en object C#.
    * Le IF permet de vérifier si la valeur est null , si c'est le cas, les traiter autrement.

#### II - handle empty JSON input
    ````
    CREATE PROCEDURE MED_WEB_VET_GET_VBRN_2 @json varchar(max) = ''
    ...
    ````
    Event though ``'@json varchar(max) = ''`` sets a default value of an empty string, SQL Server will still consider ``''`` as invalid JSON when you try to use it with JSON functions like 'JSON_VALUE', 'OPENJSON', etc. 
    
    So we need to check if the @json parameter is either null or an empty string before attempting to use it as JSON. Here is how we can do it:
    ````SQL
     IF ISJSON(@json) <> 1
      BEGIN
        PRINT 'Json is not valid'
        RETURN
      END
    ````
### III - Pour vérifier s'il y a des doublons sur une colonne 
    ````SQL
    select myColumn, COUNT(*)
    FROM myTable
    GROUP BY myColumn
    HAVING COUNT(*) > 1;
    ````

