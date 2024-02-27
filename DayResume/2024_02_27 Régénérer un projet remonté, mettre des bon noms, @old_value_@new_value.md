# C#
***
#### I - Régénérer un projet après avoir remonter depuis HIGGINS
***
- Avant de régénérér, il faut que tous les Medissys soient fermés.
- Certains écran d'un projet nécéssitent de régénérer pour pouvoir lancer correctement
- Parfois la personne qui a PUT le projet oublie de régénérér le projet avant de PUT, dans ce cas les codes ne sont pas synchronisés. (Car "régénérer" génère un .DLL avec les codes actuels pour Medissys en release).
- En outre, un autre avantage est que si on a un echec lors de régénération,  alors ça confirme que le dernièr PUT contient des erreurs .

***
#### II - L'importance des noms 
***
Utilises des noms des variables correctes, cela fascilite les debugs et extension plus tard.
Des noms incompréhensibles rendent la lecture très inéfficaces.
Mieux des noms de variables longs mais comprenhensible que des noms des variables courtes mais incompréhensible.

***
#### III - @old_value, @new_value dans un track changes.
***
Quand on fait des choses similiaires dans SQL, essaies de trouver un point commun entre tous les codes à réaliser.
Comme le cas des @old_values , @new_values .
ça rend la correction des codes beaucoup plus facile.


