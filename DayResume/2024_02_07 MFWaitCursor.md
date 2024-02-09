# C#
***
#### 1 - Un wait cursor pour rendre l'application plus utilisable
***
Parfois ça peut être lent de charger certains applications, et pour informer les utilisateurs que l'application est entrain de se charger, on peut utiliser MFWaitCursor pour transformer le sourir en une cercle tournante.
Exemple : 
````
using (MFWaitCursor wait = new MFWaitCursor())
{
GetDataCostFamily(false, null);
FillGridCostFamily();
SetScreenMode(ScreenMode.View);
m_bInit = false;
LoadAll(); // CYU 24JAN24 US67341

m_ckTrading.CheckedChanged += checkTradingChecked;
}
````
- Pendant l'exécution de tous les méthodes entre croché, on aura notre souris en forme de cercle .

