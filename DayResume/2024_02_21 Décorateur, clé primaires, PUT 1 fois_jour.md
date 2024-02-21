# SQL
***
#### I - Bien vérifier les clés primaires pour éviter de faire des bêtises
***
Dans US69092, j'ai fait ce code pour obtenir id d'une préset spécifique:
````
select @id_preset = (select id_preset from dbo.nsh_user_preset where screen = 'surc_ret_claim_tb')
````
Dans la condition where, j'ai juste mit "screen = 'surc_ret_claim_tb'",  or il peut avoir plusieurs preset dans l'écran 'surc_ret_claim_tb'.
Ce qui peut entraîner des erreurs fatals.
D'où l'importance de bien vérifier quels sont les clés primaires d'une table.
La correction, dont j'ai ajouté le nom du préset pour éviter le problème :
````
select @id_preset = (select id_preset from dbo.nsh_user_preset where screen = 'surc_ret_claim_tb' and preset_name = 'Time Bar')
````

***
#### II - PUT au moins 1 fois par jour 
***
Il faut pas laisser tes codes SQL trop longtemps en local, cela risque d'être écrasé par les autres collègues, car tous les mondes partagent le même disque D/Totsa


# C#
***
#### I - éviter de modifier des codes ou de copie coller des codes existants
***
Utilises la principe du patron décorateur .

On peut inspirer de cette pensé sur des méthodes/Fonctions
Par exemple j'ai une méthode ClearWithoutFamily, qui nettoyer des valeurs à l'except de family.
Puis je veux qu'il néttoie aussi la family, dans ce cas la je crée une nouvelle méthode qui englobe ClearWithoutFamily :
````
private void ClearAll()
{
  m_eacFamily.SelectedId = null;
  ClearWithoutFamily();
}
````