# Générale
***
####  I - Vocabulaire
***
- Amendement parasites : Des trackchanges qui se sont insérés même si on a rien changé

***
#### II - Bien communiquer avec ton N+1 : Faire un CR après chaque meeting
***
- Vaut mieux l'embêter avec trop des messages que de ne pas lui informer ce que tu as fait.
- Dès qu'une chose est faite, informez-le tout de suite.
- CR te sert aussi pour rappeller les points à traiter.


# SQL
***
#### I - Un changement de statut ne doit pas être considéré comme des Amendement.
***
Séparer le changement de statut à des amendement normales, par exemple, voici ce code:
````
-- GVY 29FEB24 vu en traitant US67341
	if (isnull(@old_status,'') = 'A' and @new_status = 'C') or (isnull(@old_status,'') = 'C' and @new_status = 'A')
	begin			
		update ct_line_cost_param
		set	
				status = tmp.status,
				date_upd_ut = getdate(),
				date_upd_local = tmp.date_upd_local,
				user_upd = tmp.user_upd
		from #CT_LINE_COST_PARAM tmp
		inner join ct_line_cost_param clcp on clcp.cost = tmp.cost
	end
	else -- GVY 29FEB24 fin
	...
````
