# Général
***
#### I - Ne pas faire du test à la fin de la journée
***
Ne pas faire tes tests à la fin de la journée dans un état fatigué, sinon on le fait chaque matinée en plein forme, et on le teste bien . 
Et une fois le test est fait, on peut donc partager l'avancement avec ton N+1.


# SQL 
***
#### I - Ne pas utiliser les identity column comme conditions de matching
***
En effet les identity column s'incrémente à chaque ajout, donc si on supprime une colonne, l'identity change.
Comme dans notre cas , on a deux tables 1195 et 1202 qui possèdent des données en relations, si la relation est maintenue par identity column,  alors si on supprime la donnée de 1195 par erreur puis on le rajoute, alors id n'est plus la même, l'erreur se prduit.


# C#
***
#### I - Window.ShowDialog() VS .ShowFrom(this)
***
- Window.ShowDialog(): In WPF, it creates a modal dialog, which means it will block interaction with other windows until it is closed.
- .ShowFrom(this): Open a window withi this method won't block interaction with other windows .
Exemples :
````
TrackChanges TrackWnd = new TrackChanges(this, "Track Changes :");
TrackWnd.SetDataTable(dtRetour);
//TrackWnd.ShowDialog();
TrackWnd.ShowFrom(this); // CYU 28FEB24 US67341
````
