# SQL
***
#### I - Traiter les valeurs nulls en SQL
***
On a trois façons de traiter les valeurs NULL en SQL :
- CASE : Peut traîter les nulls, mais générallement utilisé pour des choses plus sérieux
- COALESCE : Retourner la première valeur non nulle dans une liste d'expressions.
- ISNULL : Très puissant quand il n'y a que deux valeurs.

Exemple : 
````
dt.propulsion = ISNULL(vvme.meng_propulsion_type, vlme.meng_propulsion_type), 
dt.propulsion_type = COALESCE(vvme.meng_type, (select propulstion_type from #tmp_propulsion_type_value)),
dt.IMO_chemical = CASE ISNULL(vvs.sft_imo_chem_class_1 , ' ')
							WHEN ' ' THEN vls.sft_imo_chem_class_1 
							ELSE vvs.sft_imo_chem_class_1
							END
````
COALESCE peut être la méthode la plus efficace et lisible quand il y a plus de deux valeurs possibles.



