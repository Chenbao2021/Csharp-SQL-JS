# C#

***
#### 1 - Trouver le procédure appelé
***
Quand on a une .dll est lancé, mais on ne connaît pas le nom de procédure a travers les codes clients.
1. D'abord dans l'écran .dll ouvert,  on clique __F5__, puis __WCF SERVICE__.
2. Vérifier quelle service il est entrain d'utiliser(Comment on sait quelle service parmit tout ?)
3. Par exemple : IOS.REF
4. Télécharger la service en local depui HIGGINS, puis le lancer en mode release avec IOCServicesLauncher.EXE.
5. Dans l'application, aller dans __WCF SERVICE__, choisit la service local qu'on vient de lancer.
6. Dans IOCServicesLauncher.EXE, en bas à gauche , dans la colonne message on trouve les noms des procédures que la service a appelé.
7. Copie collé cette message la , tu peux la copier depuis __open logs__ , et va dans Microsoft SQL Server Management Studio, 
8. Pour trouver la détaille du procédure:
    8.1. Icon(Haut milieu): Rechercher dans les fichiers
    8.2. Dans "regarder dans:", on met :  D:\Git\Totsa\Med\SQL (C'est une repertoire qui est partagé par tous les mondes.)
    8.3. Dans "Rechercher", on met le nom du procédure
    8.4. Dans l'écran de Microsoft SQL Server, on trouve les résultats.

***
#### 2 - Toujorus travailler sur un repertoire de la copie, pour ne pas être ecrasé quand on prend la main.
***

***
#### 3 - Quand on fait régénérer , les fichiers regeneré se trouvera dans l'endroit ou on va put au higgins.
***





