***
# Vocabulaire
- Disposer = Utilisé pour indiquer qu'un objet ou une ressource est libérée ou détruite
- project / module : Un projet est une application indépendante, et il est constitué par des modules
    - Les modules peuvent être utilisé dans différents projects.
***
# SQL
***
#### I - CASE WHEN END : Attention !
***
Attention à l'utilisation de CASE en SQL, car il s'arrête au première condition qu'il rencontre.
Exemple US67328 .
````
-- cas 3
WHEN delivery_at = 'D' AND DATEDIFF(DAY, dcd.actual_bl_date, @today) = @delay_DIS_S_BL_N + @delay_DIS_S_BL_N_S AND ISNULL(dcd.det_no_dem, 'N') = 'N' AND ISNULL(dem_estimate, 0) <> 0 THEN 33
-- #CYU 11MAR24 US67328 CASE peut s'arreter ici , au lieu de continuer pour les WHEN de cas3
WHEN delivery_at = 'D' AND DATEDIFF(DAY, dcd.actual_bl_date, @today) >= @delay_DIS_S_BL AND ISNULL(dcd.dem_det_incurred, '') = '' AND ISNULL(dcd.det_no_dem, 'N') = 'N' THEN -23
WHEN delivery_at = 'D' AND DATEDIFF(DAY, dcd.actual_bl_date, @today) >= @delay_DIS_S_BL_N AND ISNULL(dcd.det_no_dem, 'N') = 'N' AND ISNULL(dem_estimate, 0) <> 0 THEN -33

-- cas 2
WHEN delivery_at = 'D' AND dcd.cod_date IS NOT NULL AND DATEDIFF(DAY, dcd.cod_date, @today) = @delay_DIS_S_COD AND ISNULL(dcd.dem_det_incurred, '') = '' AND ISNULL(dcd.det_no_dem, 'N') = 'N' THEN 2
...
			
````

# C#
***
#### I - Quand on a pas la main sur un projet, il est en lecture seul, donc on ne peut pas régénérér le projet.
***
Il faut aller dans le repertoire en local, et désactiver manullement "read only"

