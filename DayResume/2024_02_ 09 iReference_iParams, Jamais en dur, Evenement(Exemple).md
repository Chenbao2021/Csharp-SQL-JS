# SQL
***
#### 1 - Éviter de mettre une valeur en dur, utilisez des requêtes SQL pour obtenir la valeur la plus récente .
***
Au lieu de faire : 
````
, ISNULL(@short_name, "Default")
````
Fait ça : 
````
...
select @default_name = short_name from codetable where number = 185 and id = 8574
...
, ISNULL(@short_name, @default_name)
...
````

# C#
***
#### I - Qu'est ce que c'est iReference et iParams?
***
Exemple : 
```
LoadEAC(IORefType.MED_REF_CODETABLE,        420, m_eac_PBIFamilly);
LoadEAC(IORefType.MED_REF_COST_FAMILY,       -1, m_eacFamily);
LoadEAC(IORefType.MED_REF_CODETABLE,        186, m_eac_TSFinanceFamily);
LoadEAC(IORefType.MED_REF_CODETABLE,        372, m_eac_BunkerFam);
```
Et dans __LoadEAC__ on a ce morceau de code : 
````
...
dt = RefDirector.RefManager.GetRefTableShp(iReference, iParams);
````
Plusieurs cas possible:
- Si iParams = -1, alors iReference est le nom de procédure qui est stocké dans la base de donnée
- si iParams != -1, alors iReference est un nom de la service, qui pointe vers la table :
    ````
    select * from codetable where number = iParams
    ````

***
#### II - Exemple pratique de Evenement
***
But: On veut locker une partie des componentes lorsque un checkbox est cliqué, et delocker lorsque le checkbox est décliqué. 
    Cela est assez difficile à mettre en place , car :
    - On a des différents opérations comme Add, Amend, Delete, et on doit faire attention à chaque cas.
    - On doit faire attention aux cas de lock et de délock...
    - etc.

Donc on va mettre un event au lieu de faire tous cela, et avec un event, ces opérations devient extrêmement simple:
1. D'abord on définit notre méthode, qui va changer le booléan de nos components:
    ````
    private void LockTradingComponents(bool setBool)
    {
        m_gb_Trading.Enabled = setBool;
    
        m_ck_gs_TRD.Enabled = setBool;
        m_ck_gs_SHP.Enabled = setBool;
        ...
    }
    ````
2. Puis on definit notre événement :
    ````
    private void checkTradingChecked(object sender, EventArgs e)
    {
      if(m_ckTrading.Checked)
      {
        LockTradingComponents(true);
      } else
      {
        LockTradingComponents(false);
      }
    } 
    ````
3. Enfin, on peut ajouter cet événement à notre bouton:
    ````
    m_ckTrading.CheckedChanged += checkTradingChecked;
    ````

