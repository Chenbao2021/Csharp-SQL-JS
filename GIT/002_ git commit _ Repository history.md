# I - Introduction
#### 1. __Préparation des modifications__
Avant de faire un commit, vous devez d'abord ajouter toutes vos modifications à la zone de staging avec 'git add [fichier]'. 

#### 2. __Création du commit__ 
Une fois que les fichiers sont dans la zone de staging, vous utilisez 'git commit' pour enregistrer ces changements dans l'historique de votre dépôt.

#### 3. __Description du commit__
Lors du commit, vous fournissez un message de commit qui décrit les modifications apportées, permettant ainsi de suivre plus facilement l'historique des modifications et de comprendre les raisons de changements.

# II - Annuler un commit
#### 1. __git revert__ :
Utilisez __'git revert [commit]'__ pour créer un nouveau commit qui annule les modifications faites par le [commit] spécifié. C'est une méthode sûre car elle ne modifie pas l'historique existant.
Exemples:
1. Annuler le dernier commit: __git revert HEAD__
2. Annuler une série de commits: __git revert HEAD~3..HEAD__
    Cela annule les trois derniers commits. Crée un nouveau commit pour chaque commit annulé dans l'ordre inverse. Assurez-vous de tester cette commande dans une branche séparée si vous n'êtes pas sûr des résultats.
3. Annuler un commit en modifiant le message de commit:
    ````
    git revert --no-commit HEAD
    git commit -m "Votre message explicatif pour l'annulation du commit"
    ````
    Cette utilisation de --no-commit permet de regrouper l'annulation avec d'autres changements que vous pourriez vouloir faire avant de commettre. Cela peut être utile pour ajuster quelque chose en même temps que l'annulation.

#### 2. __--no-commit__ :
    L'option --no-commit avec la commande git revert est utilisée pour préparer l'annulation d'un ou plusieurs commits sans créer immédiatement un nouveau commit pour chaque annulation.
    Exemple d'utilisation:
    ````
    git revert --no-commit HEAD~3..HEAD
    # Après cette commande, modifiez les fichiers si nécessaire
    git commit -m "Revert des trois derniers commits avec des ajustements"
    ````
    Cette commande annule les trois derniers commits et regroupe leurs effets dans le répertoire de travail sans créer de commits séparés. Vous pouvez ensuite vérifier les modifications, faire des ajustements, et commettre le tout en un seul commit.