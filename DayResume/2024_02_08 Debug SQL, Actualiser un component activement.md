# Général
***
#### I - Toujours privilégier des modifications des valeurs en SQL qu'en Code C#
***
1. C'est plus facile quand il y a des modifications niveau d'affichage à effectuer, car les modifications en SQL s'applique tout de suite, or des modifications en C# faut attendre a la prochaine livraison.
2. Plusieurs écrans peuvent utiliser ce même code, alors une modification de valeur en SQL peut servir à tous les écrans, au lieu de modifier individuellement dans chaque écran.

***
#### II Quand une colonne qui ne doit pas être null devient null
***
C'est peut être le problème de ta requête SQL, que tu utilisé la clé de mauvaise table , quand tu fait des LEFT JOIN .

# SQL
***
#### I - CTR + G
***
- Find then Navigate
- G because G is the nearest key to F on standard "QWERTY".
***
#### II - Obtenir le JSON d'une procédure
***
Dans l'écran de warning, après le message :
__"Error while executing : ...Nom_procédure... @RETURN_VALUE = 6 @json_header ="__
Tu peux trouver le json que ton programme C# a formé, 
Tu peux la recopier l'intégralment, puis dans un fichier sql server ,  tu peux l'exécuter ainsi: 
MED_SAVE_UNIFIED_REF_COST3 @json_header='[...]', @debug=1

***
#### III - Exploiter le mode DEBUG
***
Quand on a bug niveau SQL,
On peut exécuter le procédure dans SQL Server management Studio , en précisant que @debug = 1 .

Et dans les codes de debug (Avec un IF pour les exécuter), on peut selectionner, vérifier des conditions, etc.
Par exemple: 
````
if @debug = 1
begin
	SELECT '' 'costm_ref_input_data', * FROM costm_ref_input_data WHERE idm_cost = @new_idm_cost
	SELECT '' 'nsh_cost_sub_family', * FROM nsh_cost_sub_family WHERE idm_cost = @new_idm_cost
	select ncsf.family, @new_family_text, ncsf.sub_family, @new_cost from nsh_cost_sub_family ncsf where ncsf.family = @new_family_text and ncsf.sub_family = @new_cost
	SELECT '' 'ct_line_cost_param', * FROM ct_line_cost_param WHERE idm_cost = @new_idm_cost
end
````

# C#
***
#### I - Bien aligner les components
***
En Visual studio code, quand tu clique un component, tu verras sa positionnement (x, y) dans l'écran. 
Dont on peut utiliser ça pour bien aligner les components.

***
#### II - Actualiser un component spécifique activement
***
On peut utiliser la méthode Invalidate pour redessiner un component activement.
Par exemple dans le cas de __MFMultiRefSelFast__, le fait de changer .SelectID ne déclenche pas une actualisation pour le couleur de bouton M .
Très utile quand on a des components qui possèdent des components fils.
````
public void Invalidate(bool invalidateChildren)
{
    ...
}
````
- Invalide une zone spécifique du contrôle et provoque l'envoi au contrôle d'un message relatif à la peinture.
- Invalide éventuellement les contrôles enfants assignés au contrôle.
    - _invalidateChildren : true pour invalider les contrôles enfants du contrôle; sinon false.


