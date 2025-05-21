# I - Ressource dans cloud.
## A. C'est quoi une _ressource cloud_?
Une ressource dans un cloud(Azure, AWS, GCP, etc.) est tout ce que tu peux créer, gérer et utiliser dans le cloud.
Exemples:
* Une machine virtuelle(VM).
* Un compte de stockage(Blob, S3)
* Une base de données.
* Une application web déployée(Azure App Service, etc.)

Quand on écrit du __Terraform__, on dis simplement:
* "Hey cloud, crée-moi une base de données, une app web, une IP publique... selon ces paramètres !"

## B. Pourquoi faut-il créer ces ressources?
Parce que __le cloud ne fournit rien par défaut__.
Quand tu dis que tu veux déployer ton site React par exemple:
* Il te faut un __serveur web__ (Azure Static Web APp, App Service, etc.)
* Il te faut peut-être une __base de données__.
* Tu dois peut-être configurer un réseau, sécurité, accès, DNS...

Donc avant même de déployer ton code, tu dois:
* Proposer l'infrastructure.
* La provisionner(Manuellement ou via Terraform)

## C. Et pourquoi Azure a un "resource group" et pas AWS?
Tout ce que tu crées (VM, base de données, stockage ...) doit être rassemblé dans un "resource group", qui est comme un dossier logique.
* Un "resource group" te permet de gérer, taguer, supprimer ou déplacer plusieurs ressources ensemble.

Dans __AWS__, il n'y a pas de "resource group" obligatoire:
* AWS te permet de créer des ressources individuellement.
* Si tu veux regrouper logiquement, tu peux utiliser des __tags__ ou stacks (Dans CloudFormation).


# II - Utilisation de ces ressources
Une fois qu'on a __créé des ressources dans Azure__, on peut les utiliser de plusieurs façons, en fonction du type de ressource. Voici un aperçu clair:

## A. On les utilise directement via des interfaces:
### Exemple 1: Une Web App 
* Une fois créée, on peut accéder à notre app via son URL publique: ``https://...``
* On peut mettre à jour l'app avec CI/CD ou en pushant du code via FRP, Github, etc.

### Exemple 2: Un compte de Stockage (Blob Storage)
...

### Exemple 3: Une base de données(ex: Azure PostgreSQL)
* On peut y accéder depuis notre back-end en utilisant une __chaîne de connexion__ comme: 
	``postgres://admin:motdepasse@monserveur.postgres.database.azure.com:5432/nomdb``

## B. On les consommes dans notre code (backend/ frontend).
À revoir si besoin.

## C. On les gère via le portail ou les outils DevOps.
À revoir si besoin.
