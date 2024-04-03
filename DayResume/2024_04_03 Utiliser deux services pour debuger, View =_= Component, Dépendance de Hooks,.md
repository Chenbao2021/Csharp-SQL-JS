# React
##### Dépendance des hooks
Quand on définit une méthode avec useCallback, et que dans la méthode on utilise des states directement sans passer par les paramètres, alors il est impérativement de la mettre dans criteria.
Sinon il ne va pas s'actualiser.

#### View =/= Component
En React repertoire, un View deisgn tous ceux qui affichent en permanent et totalement dépéndants,
or les components sont des parties dépéndants, ou des outils

# C#
# Utiliser Deux services pour debuguer 
1. UPDS une service, par exemple IOSNotification, puis la régénérer
2. Lancer ton service en debug, puis chercher les codes de IOSNotification dans le même visual studio code, et mettre un point d'arrête dans les fichiers de IOSNotification, et donc tu peux debuger les deux services
