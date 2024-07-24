# REACT
### I - homepage
Dans un projet React.js, le champ **homepage** dans le fichier **package.json** est utilisé pour spécifier l'URL de base du site ou de l'application web lorsque vous le déployez.

Ce champ est particulièrement utile pour les projets qui sont déployés sur des sous-chemins ou des répertoires spécifiques sur un serveur web.

Définit ce champ permet à React de générer(Compiler) les chemins corrects pour les ressources(Comme les fichiers Javascript, CSS et images).

Si votre application n'est pas hébérgée à la racine du domaine mais **dans un sous-répertoire**,le champ 'homepage' aide à générer les chemins relatifs appropriés pour toutes les ressources.

#### II - Effet du champ **'homepage'**
* Chemin des fichiers
    Lors de la construction du projet('npm run build'), React utilise le chemin spécifié dans **'homepage'** pour créer des chemins relatifs corrects pour les fichiers HTML, Javascript et CSS.
    Cela assure que les fichiers sont accessibles même si l'application n'est pas à la racine du domaine.
* Balise '<base>'
    La valeur de 'homepage' est également utilisée pour définir la balise '<base>' dans le fichier **'index.html** généré, ce qui permet de résoudre correctement les liens et les ressources.

#### III - Problème des chemins relatifs
Si votre application est déployée à la racine d'un domaine, les chemins relatifs pour les ressources ne posent généralement pas de problème.
Exemple:
* Domaine racine : https://www.example.com/
* Fichiers générés :
    * https://www.example.com/index.html
    * https://www.example.com/static/js/main.js
    * https://www.example.com/static/css/main.css

Mais si votre application est déployée dans un sous-répertoire, comme **https://www.example.com/my-app/**, les chemins relatifs doivent être ajustés pour inclure ce sous-répertoire. Sinon, les navigateurs ne trouveront pas les fichiers nécessaires.

**Comment 'homapage' résout ce problème?**
Le champ homepage dans package.json spécifie le chemin de base de votre application, ce qui permet à React de générer les chemins corrects lors de la construction.

Exemple:
* Avec : "homepage": "https://www.example.com/my-app"
* Il génère :
    ````html
    <!-- index.html -->
    <!DOCTYPE html>
    <html lang="en">
      <head>
        <meta charset="UTF-8">
        <link rel="stylesheet" href="/my-app/static/css/main.css">
      </head>
      <body>
        <div id="root"></div>
        <script src="/my-app/static/js/main.js"></script>
      </body>
    </html>
    ````

#### IV - Recommandation de mettre l'intégralité de l'URL dans le champ "homepage" dans le fichier 'package.json'
* **Compatibilité avec différents environnements de déploiement**
    En mettant l'URL complète, vous assurez que le chemin de base est défini corretement pour différents environnements de déploiement.  Cela élimine les incertitudes sur le chemin de base que l'application doit utiliser
    Exemple: "homepage": "https://www.example.com/my-app",
* **Résolution correcte des chemins relatifs**
    Lorsque vous spécifiez l'URL complète, Create React App peut générer des chemins absolus ou relatifs correctement dans les fichiers HTML, CSS, et Javascript.
    
    Par exemple: Vous avez une application React que vous souhaitez déployer dans un sous-répertoire 'my-app' sur un serveur web à l'adresse 'https://www.example.com/my-app'.
    Donc, en configurant "homepage": "https://www.example.com/my-app", tous les fichiers(.css, .html, .js) seront placé dans une sous-répertoire('/my-app') de du répertoire racine.

# SQL Server
### I - Transformer une table avec plusieurs lignes en une seule ligne d'une autre table.
* **Contexte** : J'ai une table A avec 4 colonnes(id, quantité_produit_X, avec X le nom du produit), et une table B avec 2 colonnes (Quantité, produit_X). Dans la table B j'ai 3 lignes qui correspondent à trois produits différents, et je veux transformer ces 3 lignes en juste une seule ligne de la table A.

* **Solution**
    1. D'abord on insère une ligne vide dans la table A.
    2. Puis on utilise un Cursor pour parcourir la table B ligne par ligne.
    3. Pour chaque ligne, on utilise IF sur la colonne produit_X de la table B pour décider quelle colonne de la table A à updater.
