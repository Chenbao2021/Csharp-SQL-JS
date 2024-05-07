# I - Introduction
__La sérialisation__ est __le processus de conversion__ d'un objet ou d'une structure de données en une séquence de bits ou en un format qui peut être __stocké__ dans un fichier, une base de données, ou transmis à travers un réseau.
Le format résultant doit être tel que l'objet peut être __recréé plus tard__ (désérialisation) dans l'état original.

### Pourquoi la sérialisation est-elle importante ?
* __Interopérabilité __: Elle permet aux applications écrites dans différents langages de programmation de s'échanger des données de manière standardisée. Par exemple, JSON est lisible par Javascript, ainsi par C#, Java et Python.
* __Persistance__ :  Les objets en mémoire peuvent être sauvegardés de manière persistante pour une utilisation ultérieure. 
* __Communication réseau__ : Dans les architectures client-serveur ou les systèmes distribués, la sérialisation permet de formater les messages pour la transmission à travers le réseau, facilitant ainsi l'échange de données complexes entre systèmes distants.

### Exemples de formats de sérialisation :
* JSON (JavaScript Object Notation) 
* XML (eXtensible Markup Language)
* Binaire 

# II - Exemple en Javascript: JSON.parse() et JSON.stringify()
### JSON.parse()
Cette fonction est utilisé pour la sérialisation.
 Ce processus prend un objet JavaScript (ou d'autres structures de données telles que des tableaux) et le convertit en une chaîne de caractères JSON.
 Ce processus est essentiel lorsque vous avez besoin de stocker des données dans des bases de données, des fichiers, ou de les envoyer à travers le réseau dans des requêtes API.
 
### JSON.parse()
Cette fonction est utilisé pour la désérialisation. Ce processus prend une chaîne de caractères formatée en JSON et la convertit en un objet JavaScript.

# III - Les principales raisons pour lesquelles la sérialisation est nécessaire
### A - Compatibilité entre les systèmes
__Des langages de programmation différentes__, si on envoie directement un objet JS vers un programme C#, le C# est incapable de traîter l'objet JS.
### B - Transmission de données en texte
Les protocoles de communication sur Internet, comme HTTP, sont principalement conçus pour __manipuler des données sous forme de chaînes de caractères__ ou de données binaires.
Or un objet JS peut contenir des fonctions, des références et d'autres types non-primitifs.

### C - Simplicité et standardisation
L'envoi de données sous forme sérialisée (comme JSON) simplifie la manipulation de ces données dans des environnements hétérogènes. __Tous les systèmes participants savent comment traiter ce format standardisé__ .

### D - Performance et efficacité
La sérialisation permet également de __compresser et d'optimiser__ les données pour la transmission, en ne conservant que les informations essentielles nécessaires à la reconstruction de l'objet sur un autre système.

