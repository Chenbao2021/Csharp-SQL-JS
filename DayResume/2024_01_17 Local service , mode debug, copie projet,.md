
# SQL
***
#### 1 - Quand on veut dupliquer une colonne, c'est plus simple de le faire en SQL avec le SELECT 
***
````
SELECT
    ...
		r.trading as '$Trading',
		r.shipping as '$Shipping',
		case isnull(r.trading, 'No') when 'N' then 'No' when 'Y' then 'Yes' end as 'Trading',
		case isnull(r.shipping, 'No') when 'N' then 'No' when 'Y' then 'Yes' end as 'Shipping',
	 ...
````
# C#
***
#### 1 -Local Server
***
1. Telecharger le projet de service dans le répertoire : "D:\medissys\serveur\"
    depuis higgins (Comme celle de IOSREF), les projets de service se retrouve dans "medissys_srv".
2. .exe(Services launcher) pour lancer les serveurs se trouve dans : S:\tfe\med\exploit\Liens utiles\Medissys
3. On peut remplir ces informations pour pourvoir lancer le projet  en mode debug:
    | Title | String |
    | ------ | ------ |
    | Démarrer le programme externe     | D:\medissys\serveur\vc14d\IOCServicesLauncher.EXE |
    | Arguments de la ligne de commande | /M |
    | Repertoire de travail             | D:\medissys\serveur\vc14d\ |
4. Une fois l'application est lancé, on voit l'écran avec toutes les services proposés. Dans la colonne 'Nom' on cherche la service qu'on veut lancer, et clique droit, puis clique "(RE)Démarrer les services".
5. Pour pouvoir travailler sur service local,  quand tu lances l'application Medissys, faut bien activer l'option __"Local mode"__ dans l'écran de connexion.
6. Pour être sur que vous utilisez actuellement local service, clique "F5" dans l'écran Medissys, puis en bas , clique __"WCF SERVICE"__, puis clique droit, "connect to", puis choisir "localhost".

***
#### 2 - Comment lancer le mode debug pour un projet client téléchargé depuis higgins 
***
1. On fait d'abord un CVER pour les projets sur HIGGINS
2. On télécharge le projet dans le répertoire : "D:\medissys\client\"" ,
    pour éviter des bugs, on fait dans l'ordre:
    - Mettre à jour localement
    - Mettre à jour localement et ouvrir la solution.
    Normalement le visual studio ouvert avec le projet téléchargé.
3. Dans Explorateur de solutions (si c'est fermé CTR+ALT+L, ou dans le topbar "Affichage"),
    Clique droit le nom de projet(Icon C# du plus haut niveau), puis en bas, clique "propriétés", puis dans le sidebar, clique "Déboguer", et remplir ces informations:
    | Title | String |
    | ------ | ------ |
    | Démarrer le programme externe     | D:\medissys\Client\vc14d\MED.EXE |
    | Arguments de la ligne de commande | /Svs-tot-db03 /WN /Dtsigdev4 /C /L1 |
    | Repertoire de travail             | D:\medissys\Client\vc14d\ |
4. Et normalement, la tu peux lancer l'application avec Medissys.

***
#### 3 - Lorsqu'on copie collé un projet, faut enlever les espaces, sinon Visual a du mal de se lancer.
***
1. On utilise CTR+C+V pour cloner un proet, puis on enleve les espaces du répertoire copie collé, et enfin on peut travailler dessus.
2. Le but de cloner un projet est pour éviter que nos modifications soient écrasé lorsqu'on fait un CVER.
