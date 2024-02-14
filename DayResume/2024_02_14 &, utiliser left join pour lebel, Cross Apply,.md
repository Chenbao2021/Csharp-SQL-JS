#SQL
***
#### 1 - & opérateur en pratique
***
On peut utiliser l'opérateur & pour vérifier le valeur d'une variable.
Exemple, prenons la fonction SQL suivante:
````
CREATE FUNCTION dbo.F_MED_UNIFIED_REF_COST_FORMAT_CHECK2 (@requirement int)
    returns varchar(max)
AS
BEGIN
    DECLARE @retour varchar(max) = ''
    IF ISNULL(@requirement, 0) > 0
    BEGIN
        if @requirement & 1 = 1
            select @retour = 'Display'
        if @requirement & 2 = 2
        BEGIN
            if @retour <> ''
                select @retour = @retour + ','
            select @retour = @retour + 'Expand'
        END
    END
END
GO
````
- Si on a @requirement = 3, on a alors 3 & 1 = 1, et 3 & 2 = 2, ce qui simplifie baucoup les codes.

Puis, prenons un exemple C#: 
````
// V1
if (iDrField == 1)      { ckDisplay.Checked = true; }
else if (iDrField == 2) { ckEditable.Checked = true; }
else if (iDrField == 3) { ckDisplay.Checked = true; ckEditable.Checked = true; }
else if (iDrField == 4) { ckMandatory.Checked = true; }
else if (iDrField == 5) { ckDisplay.Checked = true; ckMandatory.Checked = true; }
else if (iDrField == 6) { ckEditable.Checked = true; ckMandatory.Checked = true; }
else if (iDrField == 7) { ckDisplay.Checked = true; ckEditable.Checked = true; ckMandatory.Checked = true; }

// V2
if((iDrField & 1) == 1) { ckDisplay.Checked = true; }
if((iDrField & 2) == 2) { ckEditable.Checked = true; }
if( (iDrField & 4) == 4 ) { ckMandatory.Checked = true; }
````
- On voir que v2 est beaucoup plus simple a implémenter, même qu'avec seulement 7 valeurs différentes,et si on aura plus de 7 ? La simplicité séra diminuée exponentiellement.
 
***
#### 2 - On peut utiliser un Left join pour ajouter des lebels
***
On a :
- Tab_A(id, id_nourriture)
- Tab_B(id_nourriture, nom_nourriture)

Alors on peut faire un Left Join pour afficher le nom_nourriture avec l'id de A :
````
SELECT a.id, b.nom_nourriture
FROM Tab_A a
LEFT JOIN Tab_B ON a.id_nourriture = b.id_nourriture
````

***
#### 3 - Cross Apply
***
Code: 
````
WITH TMP_CONTEXT(idm_cost, all_count_full_name) AS (
    SELECT r.[$inv_idm_cost], STRING_AGG(c.code, ',')
    FROM #tmp_costm_ref r
    CROSS APPLY string_split(r.[$inv_id_context_list], ';') as cx
    INNER JOIN phm_ds_context c on c.id_context = cx.value
    GROUP BY r.[$inv_idm_cost]
)
UPDATE r
SET r.Context = cx.all_cont_full_name
FROM #tmp_costm_ref r
INNER JOIN TMP_CONTEXT cx ON cx.idm_cost = r.[$inv_idm_cost]
````
- cross apply va faire l'action pour chaque ligne en question
- string_split will separe la ligne en deux
- On fait d'abord CROSS APPLy, puis avec INNER JOIN , on appliquera inner join avec chaque valeurs (la ligne avec value = 1, puis la ligne avec value = 2)


