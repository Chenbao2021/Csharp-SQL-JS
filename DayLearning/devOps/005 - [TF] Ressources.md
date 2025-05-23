# I - "Resource" dans Terraform
### A. Qu'est-ce qu'une "ressource" dans Terraform?
Dans Terraform, une ressource représente une composante de l'infrastructure qu'on veut créer sur une plateform cloud(Comm Azure, AWS, ...).

Par exemple: Une machine virtuelle, un groupe de ressources, un storage account, un App Service, etc.

### B. Structure de base d'une ressource Terraform.
````hcl
provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "rg" {
  name     = "demo-rg"
  location = "France Central"
}

resource "azurerm_storage_account" "storage" {
  name                     = "demostorage12345"
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}
````
* ``provider``: Bloc obligatoire pour indiquer le cloud utilisé(Ici: ``azurem``)
* ``resource`` : Mot-clé Terraform, utilisé pour créer ou modifier une resource.
* ``"azurerm_resource_group"`` : Type de ressource (Azure Resource Manager + type)
* ``"rg"`` : Nom logique (Utilisé dans le code Teraform, pas sur Azure)
* ``{ ... }`` : Bloc de configuration.

### C. Concepts importants à connaître.
|Concept|Description|
|--|--|
|Provider|Bloc obligatoire pour indiquer le cloud utilisé|
|Variables|Permettent de rendre le code plus réutilisable|
|State|fichier ``.tfstate`` qui garde une trace des ressources créées|
|Output|Permet de sortir des infos utiles après déploiement(Comme une URL)|
|Backend|Spécifie où est stocké le ``tfstate``(local, Azure Storage, etc.)|
|Plan/ Apply|Étaês pour présualiser puis appliquer les changements.|

### D. Structure de projet typique.
````bash
.
├── main.tf           # Déclaration des ressources
├── variables.tf      # Variables d'entrée
├── outputs.tf        # Variables de sortie
├── terraform.tfvars  # Valeurs concrètes des variables
├── provider.tf       # Fournisseur (Azure ici)
````

### F. Utilisation de variables (exemple).
variables.tf
````
variable "resource_group_name" {
  type    = string
  default = "my-default-rg"
}
````

main.tf
````
resource "azurerm_resource_group" "rg" {
  name     = var.resource_group_name
  location = "France Central"
}
````

terraform.tfvars
````
resource_group_name = "dev-rg-europe"
````

# II - Détailles du provider
### A. Intoduction
````bash
provider "azurerm" {
  client_id       = "..."
  client_secret   = "..."
  tenant_id       = "..."
  subscription_id = "..."
}
````
Lorsqu'on veut utiliser __Terraform__ s'authentifier auprès d'Azure, on doit fournir ces valeurs si on veut gérer l'authentification manuellement.

C'est comme si on fait:
````bash
az login --service-principal \
  --username <client_id> \
  --password <client_secret> \
  --tenant <tenant_id>
````

### B. À quoi sert chaque champ ?
|Champ|Description|
|--|--|
|``client_id``|Identifiant de ton App Registration|
|``client_secret``|Secret associé à cette App Registration(Comme un mot de passe). Absent si on utilise OICD|
|``tenant_id``|ID de ton tenant azure AD(Un cnteneur d'identité, contient les utilisateurs, groupes, rôles, app registrations, service principals, etc.)|
|``subscription_id``|ID de la subscription où on veux créer nos ressources. Rattachée à un tenant.|

### C. Est-ce obligatoire de les mettre __dans le fichier__ ``.tf``?
Non, c'est déconseillé en CI/CD!

Meilleur pratique: Passer ces valeurs via __variables d'environnement__, par exemple dans Github Actions.
````bash
env:
  ARM_CLIENT_ID: ${{ secrets.AZURE_CLIENT_ID }}
  ARM_CLIENT_SECRET: ${{ secrets.AZURE_CLIENT_SECRET }}
  ARM_TENANT_ID: ${{ secrets.AZURE_TENANT_ID }}
  ARM_SUBSCRIPTION_ID: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
````
Et dans le code ``.tf``, ne rien mettre:
````bash
provider "azurerm" {
  features {}
}
````


# III - App Service + Plan.
### A. C'est quoi un __App Service__ dans Azure?
UN __App Service__ est un service __PaaS__(Platform as a Service) d'Azure qui te permet __d'héberger des applications web__, __des API REST__, ou __même des backends(Node.js, Python, .NET, etc.)__, sans avoir à gérer de machine virtuelle toi-même.
* Par contre, __Key Vault__, __Storage__, et __Web API__, etc.) ne sont pas des App Services.

C'est à dire, on peut déployer:
* Une application Node.js, Python, .NET, Java, PHP.
* Une API REST.
* Une application React/Vue/Angular avec un backend.
* Un conteneur Docker.

### B. C'est qoi un __App Service Plan__ ?
Un __App Service Plan__ définit les ressources physiques (CPU, RAM, nombre d'instances, prix, etc.) utilisées pour exécuter un ou plusieurs App Services.

👉 C’est l’infrastructure sous-jacente, partagée entre plusieurs App Services si tu veux.

Et une __App Service__ ne peut pas exister sans __App Service Plan__:
* Le plan = La machine
* Le service = l'app que tu y installes.

### C. Exemple Terraform expliqué en détaillé: Créer un App Service + Plan.
````hcl
resource "azurerm_app_service_plan" "myplan" {
  name                = "demo-app-plan"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku {
    tier = "Basic"
    size = "B1"
  }
}
````
1. ``azurerm_app_service_plan``
	C'est la "machine" virtuelle managée qui exécute une ou plusieurs applcations web. 
	``resource "azurerm_app_service_plan" "myplan" {``: On déclare une ressource de type App Service Plan, nommée ``myplan`` __dans Terraform__(nom logique, utilisé pour lier à d'autre ressources).
2. ``name``: Nom réel du plan qui apparaîtra dans le portail Azure.
3. ``location``: Région géographique Azure(ex: "France Central"). Ici, on __réutilise__ celle du groupe de ressources.
4. ``resource_group_name``: Groupe de ressources dans lequel est placé ce plan. ça regroupe logiquement les ressources Azure.
5. ``sku``: Service Level - tarif et puissance.
	* ``tier``: Niveau de service(``Free``, ``Basic``, ``Standard``, ``Premium``, etc).
	* ``size``: Configuration CPU/ RAM(``b1`` = 1 CPU, 1.75Go RAM).

````hcl
resource "azurerm_app_service" "myapp" {
  name                = "demo-app"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.myplan.id

  site_config {
    always_on = true
  }

  app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = "1"
  }
}
````
1. ``azurerm_app_service``: C'est notre application, hébergée dans le plan défini plus haut.
	``resource "azurerm_app_service" "myapp"``: On déclare ne App Web, nommée ``myapp`` __dans Terraform__.
2. ``name``: Npm réel de notre App Service visible dans Azure.
3. ``location`` et ``resource_group_name``: Ils indiquent où créer cette ressource, exactement comme pour le plan.
4. ``app_service_plan_id``: Lien entre l'application et le plan. Elle tourne sur ce plan.
5. ``site_config``: ``always_on = true`` -> Garde l'app "réveillée" même sans trafic.


