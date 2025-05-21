# I - Qu'est-ce que __Terraform__ ?
## A. Définition
__Terraform__ est un outil d'Infrastructure as Code (IaC) qui permet de __créer__, __modifier__, __supprimer__ et __versionner une infrastructure informatique__ en la décrivant avec du code déclaratif ( Principalement en HCL: HashiCorp Configuration Language).
* Infrastructure = C'est tout ce qui permet à tes applications de fonctionner: 
	* Les machines.
	* Les réseaux.
	* Les bases de données.
	* Les systèmes.
	* etc.
* IaC = C'est une __pratique DevOps__ qui consiste à définir et gérer l'infrastructure informatique à l'aide de fichiers de configuraton écrits en code, pluôt que de tout faire à la main dans une interface graphique.

## B. Exemple
Tu veux créer un serveur dans AWS:
### Sans IaC:
1. Tu vas dans la console web AWS.
2. Tu cliques sur EC2.
3. Tu choisis un type de machine.
4. Tu cliques sur "Lancer".

Problème: C'est manuel, lent, non réplicable, et tu risques de faire des erreurs!

### Avec IaC, on écrit un fichier ``main.tf`` comme:
````hcl
provider "aws" {
	region = "eu-west-1"
}

resource "aws_instance" "web" {
	ami = "ami-12345678"
	instance_type = "t2.micro"

	tags = {
		Name = "ServeurWeb"
	}
}
````
* ``provider "aws"``: On dit à Terraform qu'on veut utiliser le cloud AWS.
* ``region = "eu-west-1"``: On choisit la région dans laquelle on veut créer notre serveur.
* ``ressource``: On déclare une ressource que Terraform doit créer.
* ``"aws_instance"``: C'est le type de ressource, ici une machine virtuelle EC2 dans AWS
* ``"web"``: C'est le nom interne que tu donnes à ta machine. Tu peux l'utiliser plus tard pour faire référence à elle.
* ``ami = "ami-12345678"``: (Amazon Machine Image), C'est l'ID d'une image à installer sur la machine(Comme Ubuntu ou Amazon Linux). Et c'est un identifiant d'image préexistante dans AWS.
	* Une image système, c'est un peu comme quand tu achète un nouvel ordinateur:
		* Soit ut le reçois "vide", et tu dois tout installer.
		* Soit tu le reçois avec __Windows déjà installé__, prêt à l'emploie => C'est une image système.

## C. Pourquoi a-t-on besoin d'une AMI dans Terraform?
Parce qu'une machine virtuelle a besoin d'un sytème d'exploitation pour démarrer.

Quand on écris ça dans Terraform:
````h
ami = "ami-0fc61db8544a617ed"
````
C'est comme si on dit: Quand tu créeras cette machine virtuelle, utilise l'image système "ami-0fc61db8544a617ed" (Le Système d'exploitation Ubuntu 22.04 LTS).


# II - Commandes clés
En effet, Terraform est un logiciel en ligne de commande. C'est un programme qu'on installe sur notre PC.
C'est un logiciel __générique__, il ne sait pas créer des machines dans AWS ou Azure ou tout seul. Il a besoin qu'on lui spécialise en appelant "provider xxxx".

Voici les étapes typiques
* ``terraform init``: Initialise le Térraform (Télécharge les plugins nécessaires).
* ``terraform plan``: Affcher ce que Terraform va faire(Sans rien appliquer).
* ``terraform apply``: Appliquer réellement les changements (crée/ modifie/ supprime les ressources).
* ``terraform destroy``: Supprime toutes les ressources créées par Terraform.

# III - Exemple d'un projet IaC
## A. Objectif:
* Un groupe de ressources(ressource groupd)
* Un réseau virtuel (VNet) avec un sous-réseau.
* Une machine virtuel Linux
* Un compte Azure Storage(comme S3)

## B. Arborescence du projet Terraform
````bash
/azure-iac
│
├── main.tf              # Configuration principale
├── variables.tf         # Variables du projet
├── outputs.tf           # Sorties utiles (ex : IP publique)
└── terraform.tfvars     # Valeurs concrètes des variables
````

## C. ``main.tf``
````hcl

````


## D. Voici un projet qui illustre bien IaC:
* Fichier clair, modulaire et versionnable
* Pas de clics manuels: Tout est décrit dans le code.
* Réutilisable pour différents environnements (test/prod)
* Automatisation complète avec une seule commande.


