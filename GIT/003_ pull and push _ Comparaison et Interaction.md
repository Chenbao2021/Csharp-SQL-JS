#### 1. Branche distante
Une branche distante dans Git est une référence à l'état d'une branche dans un dépôt distant.

On intéragit avec des branches distantes quand on effectue des opérations comme __'git fetch'__, __'git push'__, et __'git pull'__. 

#### 2. Commit local
Un commit local est une capture instantanée de l'état de votre projet dans votre dépôt local.

Les commits locaux sont les blocs de constructions de votre historique de développement. Ils permettent de suivre le progrès individuel de votre travail avant de partager ces changements avec d'autres via un dépôt distant. Chaque commit local possède un identifiant unique appelé un hash SHA-1, permettant de le référencer spécifiquement.

#### 3. Comparaison et Interaction
* Isolation vs Collaboration: 
    * Les commits locaux sont isolés jusqu'à ce qu'ils soient partagés.
    * Les branches distantes reflètent les changements qui ont été partagés et sont visible par tous.

* Synchronisation:
    * Pour intégrer les commits locaux dans une branche distantes: __"git push"__
    * Inversement: __"git pull"__ (Combinaison de git fetch et git merge).

* Bonne pratique pour travailler en collaboration
    Faire un __'git pull'__ avant un __'git push'__ est une bonne pratique dans les environnements collaboratifs pour éviter les erreurs de push et pour s'assurer que vos changements s'intègrent bien avec ceux des autres.