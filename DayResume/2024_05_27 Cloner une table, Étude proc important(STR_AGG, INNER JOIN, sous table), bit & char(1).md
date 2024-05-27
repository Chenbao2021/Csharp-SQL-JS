# SQL
#### I - Cloner une table
Si on veut clonner la structure d'une table, on peut utiliser Select.
Par exemple si on a une table A, on peut faire:
````SQL
Select *
INTO #new_table
FROM A
WHERE 1 = 0
````
Et la table #new_table contient donc la structure de la table A.

#### II - Étude de codes (Sous-table, select distinc, string_agg, group by, )
````SQL
SELECT
	m.id_ph_meta_stock_mvt,
	STRING_AGG(d.quality, ', ') as aggregated_quality
INTO #aggregated_quality
FROM #msm m
INNER JOIN (
	select distinct d.id_ph_meta_stock_mvt, h.quality
	FROM #details d 
	INNER JOIN ph_ds_deal pdd on pdd.id_ph_deal = d.id_ph_deal
	INNER JOIN header h on h.contract_number = pdd.contract_number 
) d ON m.id_ph_meta_stock_mvt = d.id_ph_meta_stock_mvt
GROUP BY m.id_ph_meta_stock_mvt
	
UPDATE m
SET
	m.quality = aq.aggregated_quality
FROM #msm m
INNER JOIN #aggregated_quality aq ON m.id_ph_meta_stock_mvt = aq.id_ph_meta_stock_mvt
````

Ce code SQL mérite d'être relire prochainement.

#### III - **If else** VS **CASE WHEN**
A exemple of "CASE WHEN" that works better than IF ELSE:
````SQL
SELECT
	@can_view = CASE WHEN @user_access IN ('V', 'M', 'C', 'L', 'P') THEN 1 ELSE 0 END,
	@can_amend = CASE WHEN @user_access IN ('M', 'C', 'L', 'P') THEN 1 ELSE 0 END,
	@can_create = CASE WHEN @user_access IN ('C', 'L', 'P') THEN 1 ELSE 0 END,
	@can_logical_delete = CASE WHEN @user_access IN ('L', 'P') THEN 1 ELSE 0 END,
	@can_physical_delete = CASE WHEN @user_access IN ('P') THEN 1 ELSE 0 END
````

#### IV - Différence Bit et Char(1)
bit est un type de donnée, utilise 1 bit d'espace de stockage. ses valeurs peuvent être 0, 1 ou null.
Et char(1) utilise 1 octet pour stocker une valeurs (8 bits).