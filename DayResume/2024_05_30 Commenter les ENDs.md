# SQL
#### I - Commenter les ENDs, quand ils sont nombreux.
Quand une procédure devient complexe, on peut avoir beaucoup des ENDs qui rend difficile à ajouter nouveau END. Dans ce cas il vaut mieux de commenter les ENDs pour éviter à mettre des ENDs à mauvaise place.
Exemple:
````sql
...
		-- END IF: retour
		end
	-- END ELSE : IF NOT EXISTS (SELECT 1 FROM nshm_propulsion_mode where id_propulsion_mode IN (select id_propulsion_mode from #tmp_nshm_propulsion_mode))
	END
-- END IF : IF @user_access In ('M', 'C', 'L', 'P')
END
````
