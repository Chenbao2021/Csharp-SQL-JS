# I - Concepts Terraforms
### 1. ``resource``: Le coeur du déploiement.
* Une ressource est une déclaration dans Terraform disant: ``Je veux que cette chose existe dans le cloud(ou autre provider).``

* Syntaxe:
	````dash
	resource "<provider>_<type>" "<nom_logique>" {
  		...propriétés...
	}
	````

* Comment Terraform sait quoi faire avec ça ?
	1. Lors du ``terraform init``, Terraform __installe le <provider>__(ex: ``azurerm``) depuis le Terraform Registry.
	2. Ce provider contient du code(plugin GO) qui définit ce que veut dire "<provider>_<type>"(ex: "azurerm_virtual_network").
	3. Lors du ``terraform apply``:
		* Terraform lit ce bloc ``resource``.
		* Il dit : “je dois appeler la méthode ``create()`` du plugin ``azurerm`` pour un réseau virtuel”
		* Il convertit ça en un __appel d'API vers Azure__ (Par ex, une requête REST).

	

### 2. ``data``: Lecture seule d'un élément existant.
* Usage:
	````
	data "azurerm_subnet" "existing" {
		name                 = "subnet-existant"
		virtual_network_name = "vnet-existant"
		resource_group_name  = "rg-existant"
	}
	````
	* Contrairement à ``resource``, un ``data`` ne crée rien.
	* Terraform utilise les API du provider pour lire des infos, par exemple pour réutiliser un subnet déjà existant.


* Exemple: 
	````
		# In an Azure landing zone, there should be only one vnet and one udr.
		data "azurerm_resources" "vnet" {
			type = "Microsoft.Network/virtualNetworks"
		}
		data "azurerm_resources" "udr" {
			type = "Microsoft.Network/routeTables"
		}
	````
	Ces deux blocs utilisent une __data source spéciale__ qui permet à Terraform de rechercher dynamiquement des ressources existantes dans Azure, __sans connaître leurs noms exacts à l'avance__.
	* ``azurerm_resources``: C'est une __data source générique__ qui permet de chercher toutes les ressources Azure d'un certain type, dans un __groupe de ressources__, un __abonnement__, ou même une __souscription entière__. On peut lui ajouter:
		* Un filtre.
		* Un ``type``, comme ici.

	Ce que fait ces codes:
	1. ``data "azurerm_resources" "vnet"`` :
		* Recherche toutes les ressources de type ``Microsoft.Network/VirtualNetworks``(VNet)
		* Le but est d'obtenir la liste des VNets existants dans le scope(groupe de ressources/ Abonnements).
		* Si on est dans un contexte de __landing zone__, on part du principe qu'il ne devrait y en avoir qu'un seul, donc on fait probablement: ``data.azurerm_resources.vnet.resources[0].id``
	2. ``data "azurerm_resources" "udr"`` :
		* UDR = User defined route, c'est une table de routage personnalisée, qu'on peut associer à un subnet pour contrôler comment le trafic est dirigé.
		
### 3. ``output``
Les ``output`` permettent d'exposer des valeurs à l'extérieur d'un module, ou de les afficher après ``terraform apply``.
````hcl
output "ip_vm" {
	value = azurerm_public_ip.my_ip_address
}
````
* Très utile pour: Afficher une URL, une IP, une ID de ressource après déploiement.


### 4. ``provider``
* Définit quel cloud ou service Terraform doit utiliser (Azure, AWS, Github, etc.)
* Il faut le configurer en début de fichier (``main.tf``) pour autoriser Terraform à parler au bon service.
* Chaque provider peut avoir des options (spécifique) d'authentification, de région, etc.
````hcl
provider "azurerm" {
	features {} # Obligatoire même vide.
}
````

### 5. ``variable``
* Permet de rendre ton code flexible. Tu peux passer des paramètres sans changer le ``.tf`` à chaque fois.
* Imaginons qu'on a ce code dans notre fichier ``main.tf``:
	````hcl
	resource "azurerm_resource_group" "example" {
		name = var.rg_name
		location = var.location
	}
	````
	Et dans ``variables.tf``:
	````hcl
	variable "rg_name" {
		description = "Nom du groupe de ressources"
		type        = string
	}

	variable "location" {
		description = "Région Azure"
		type        = string
		default     = "westeurope"
	}
	````
	Maintenant, si on veut déployer ce même code avec un autre nom de groupe de ressources ou dans une autre région, on n'a pas besoin de modifier ``main.tf``, mais juste en ajoutant un nouveau fichier ``.tfvars``:
	````yml
	terraform apply -var-file="prod.tfvars"
	````

### 6. ``locals``
* Pour définir des __valeurs intermédiaires__ ou des calculs dans le code.
	````hcl
	locals {
		full_name = "${var.prefix}-${var.name}"
	}
	````

### 7. ``module``: Regroupement de ressources réutilisable.
* Un module, c'est un __ensemble de ressources encapsulées__ dans un dossier séparé(Ou téléchargées depuis un registre). Il permet de réutiliser un groupe de ressources comme un composant.
* Exemple:
	````hcl
	module "network" {
		source              = "./modules/network"
		resource_group_name = var.rg_name
		location            = var.location
	}
	````
	Et dans ``./modules/network/main.tf``, on a par exemple:
	````hcl
	resource "azurerm_virtual_network" "vnet" {
		name                = "mon-vnet"
		address_space       = ["10.0.0.0/16"]
		location            = var.location
		resource_group_name = var.resource_group_name
	}
	````


### 8. ``terraform``
* Bloc spécial pour __la configuration globale du projet__(backend, version required, etc.)
	````hcl
	terraform {
		required_providers {
			azurerm = {
				source  = "hashicorp/azurerm"
				version = ">= 3.0.0"
			}
		}
	}
	````


# II - L'origine de ``var``

### 1. ```var`` dans une ``resource``
Quand on écrit quelque chose comme: 
````hcl
resource "azurerm_resource_group" "network" {
  name     = var.rg_name
  location = var.location
}
````
Ici, ``var.rg_name`` fait référence à une variable définie globalement dans le fichier ``variables.tf`` (ou ailleurs dans le dossier principal).
Ce sont des variables du projet principal (root module).

### 2. ``var`` dans un ``module``
Par contre, si on utilise une resource dans un module, alors les ``var.`` seront des variables locales au module.
````hcl
module "network" {
  source              = "./modules/network"
  vnet_name           = "vnet-principale"
  location            = "westeurope"
  resource_group_name = "rg-principale"
}
````
