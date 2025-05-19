# I - Le fichier ``.yml``/ ``.yaml``
Ces fichiers sont au coeur de __l'automatisation moderne__, surtout avec GitHub Actions, qui est souvent utilisé dans les projets d'infrastructure.


## A. Qu'est-ce qu'un fichier ``.yml``/ ``.yaml``?
Un fichier ``.yaml`` est un __fichier de configuration écrit en YAML__, c'est un format simple et lisible utilisé pour :
* Décrire des __actions automatisées(CI/CD).
* Définir des paramètres.
* Structurer des __données hiérarchiques__.

## B. En particulier dans ``.github/workflow/*.yml``, ce sont des __Github Actions Workflows__.
Ils permettent d'exécuter automatiquement des actions comme:
* Vérifier ton code(``terraform validate``)
* Formater(``terraform fmt``)
* Déployer automatiquement ton infrastructure(``terraform apply``)
* Faire des tests, publier, etc.

## C. Structure typique d'un fichier ``.yml`` Github Actions
Voici un exemple simple:
````
name: Déploiement Terraform

on: 
  push:
    branches:
      - main

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - uses: hashicorp/setup-terraform@v2
        with:
          terraform_version: 1.5.0
      - run: terraform init
      - run: terraform plan
      - run: terraform apply -auto-approve
````
* ``name:``: Nom du workflow(Visible dans l'onglet Actions sur Github).
* ``on:`` : Ce qui déclenche le workflow: Ici un __push sur main__.
* ``jobs:``: Un ou plusieurs __jobs__ à exécuter.
* ``deploy:``: Nom du job.
* ``runs-on:``: Le système d'exécution (Ici, un serveur Ubuntu "propre").
* ``steps::`` : Les étapes à exécuter une par une.
* ``uses:`` : Utilisation d'une __action Github préexistante__(Comme un plugin).
* ``run:`` : Commandes à exécuter dans le terminal (Comme si tu les tapais).

## D. Détailles des commandes qu'on trouve dans ``.yml``


# II - Pourquoi parle-t-on de __commandes Terraform__ dans les fichiers ``.github/workflows/*.yml``?
Parce que dans ton projet(Qui est un projet IaC(Infrastructure as Code sur Azure), le code que tu écris en ``.tf`` ne suffit pas à lui seul).
Il faut aussi une __manière de l'exécuter automatiquement__, et c'est là qu'intervient __Github Actions__, avec ses fichiers ``.yml``.

## A. Github Actions + Terraform = automatisation de l'infrastructure.
Github Actions te permet d'écrire des __scripts d'automatisation(dans les ``.yml``) pour faire tourner des commandes comme:
|Commande Terraform| Rôle|
|--|--|
|``terraform init``|Initialiser le dossier|
|``terraform validate``|Vérifier que ton code est valide|
|``terraform fmt``|Formater automatiquement ton code|
|``terraform plan``|Afficher ce que Terraform va faire (prévisualisation)|
|``terraform apply``|Déployer réellement l’infrastructure dans le cloud|

En résume:
|Élément|Rôle|
|--|--|
|``.tf``|Décrit ton infra(machines, réseuax, bases)|
|``.yml``|Automatise l'exécution de ton infra|
|``terraform apply`` dans ``.yml``|Exécute le déploiement automatiquement|

