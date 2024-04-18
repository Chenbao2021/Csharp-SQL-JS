# SQL
#### Quand n différent que 1, on utilise varchar, sinon char(1)
Pour éviter des erreurs. Les char(n) sont des codes historiques , faut pas les refaire.

#### Ne jamais décommenter un morceau de code dans une procédure SQL !!!
Comme les editeurs SQl sont nuls, tu ne saurais jamais si tu as décommenté un "end" sans faire exprès.
ça va être catastrophe si tu fais un erreur , et ça prend beaucoup plus des temps pour corriger après.

#### N'utilise pas varchar(max) par défaut sans réflechir
- Performance : varchar ajuste dynamiquement sa taille pour stocker les données,  mais une colonne de varchar(max) ne peut pas avoir des indexation, et ne peuvent pas toujours recharger entièrement en mémoire, en plus SQL Server doit parfois gérer de manière plus coplexe l'allocation et les stockages des données pour 'varchar(max)'.
- Utilisation appropriée: Cela aide le serveur SQL à optimiser l'allocation de l'espace de stockage et le traitement des requêtes.
- Maintenance et clarté: Définir une longueur fixe quand c'est possible rend également le schéma de la base de données plus clair pour les autres dévéloppeurs et pour la maintenance future.

#### Requête dynamique
Exemple de requête dynamique:
1. Declaration
    ````
    declare @request_costm_ref varchar(max) = ''
    ````
2. Mise à jour
    ````
    ...
    SELECT @request_costm_ref = @request_costm_ref +
    case when @request_costm_ref = '' then '' else ',' end  +  
    'cr.id_pbi_family = ' + case when isnull(@new_cr_id_pbi_family,0) = 0 then 'null' else convert(varchar,isnull(@new_cr_id_pbi_family,0)) 
    end
    ...
    ````
3. Utilisation
    ````
    if @request_costm_ref <> ''
    begin
		select @request_costm_ref = 'update cr set ' + @request_costm_ref + ' from costm_ref cr WHERE cr.idm_cost =' + convert(varchar,@new_cr_idm_cost)
        exec (@request_costm_ref)
    end
    ````