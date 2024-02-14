# Générale
***
#### 1 - Tous les documents conernant un US se trouvent dans:   _S:\tfe\med\Dossiers\_
***

# C#
***
#### 1 - Toujours regénérer en tâche de fond avant de PUT dans le HIGGINS
***
Certains erreurs en DEV sont considérés comme des Exceptions en Production, donc chaque fois faut vérifier qu'on peut bien générer en tâche de fond quelque soit en dev ou release.
***
#### 2 - Utilise Locked au lieu de Enabled pour les MFMultiRefSelFast
***
Car un problème d'affichage lorsqu'on disabled le component.

***
#### 3 - On évite de remplacer des données viennent de SQL
***
On privilège toujours de modifier les données dans SQL au lieu de les remonter puis modifier en local(i.e. En code C#).

***
#### 4 - Respecter ce règle : 1 fonction fait 1 seule chose.
***
Ça paraît évident, mais soit vigilant,  toujours respecter cette règle : 1 fonction fait 1 seule chose.
Et c'est pour ça, choisir un bon nom est primordial pour que les gens comprennent ton code.

L'erreur que j'ai faite: J'ai appelé "PopulateAllFields" dans 'SetScreenMode'. 
Or 'SetScreenMode' est censé effectuer seulement le changement d'écran, et les effets secondaires, il n'est pas censé  faire 'PopulateAllFields', qui est une action compliquée et ne fait pas partie __obligatoire__ de 'SetScreenMode'.

***
#### 5 - Faut tester beaucoup beaucoup beaucoup plus !
***