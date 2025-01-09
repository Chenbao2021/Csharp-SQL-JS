## I. flex-basic, flex-shrink and flex-grow
Les propriétés ``flex-grow``, ``flex-shrink``, et ``flex-basis`` sont les trois composants fondamentaux de la propriété ``flex`` en CSS. 
Elles contrôlent comment les éléments flexibles s'adaptent à l'espace disponible ou débordent dans un conteneur flexbox.
### a. flex-grow: "grandir"
* __Rôle__: Définit combien un élément peut grandir pour occuper l'espace disponible dans le conteneur __en plus de sa taille__.
* Par exemple, si un élement a ``flex-grow: 2``, et les deux autres ont ``flex-grow: 1``, alors le première obtiendra deux fois plus d'espace que les autres.

### b. flex-shrink: "rétrécir"
* __Rôle__: Détermine si un élément peut __rétrécir sa taille initiale__ lorsqu'il n'y a pas assez de place dans le conteneur.
* __Remarque__: ``flex-shrink`` peut réduire la taille d'un composant à __0__, s'il n'y a pas de taille minial explicite et espace insuffisants dans le conteneur.
* Si les éléments ne peuvent pas tenir dans le conteneur, ceux avec une valeur ``flex-shrink > 0`` rétréciront proportionnellement à leur valeur.
    ````js
    .item {
        flex-shrink: 1; /* Les éléments peuvent rétrécir */
        width: 300px; /* Largeur de base */
    }
    ````
    * Si deux éléments de 300px sont dans un conteneur de 600px, tout va bien.
    * Si le conteneur mesure 500px, chaque élément rétrécira à 250px pour tenir dans le conteneur.
    * Si on met ``flex-shrink: 0`` sur un élément, il __ne rétrécira pas du tout__(Il gardera sa __largeur initiale__ et forcera les autres à rétrécir davantage ou provoquera un débordement.)

### c. flex-basis: "Taille de base"
* __Rôle__: Définit la taille initiale d'un élément avant l'application de ``flex-glow`` et ``flex-shrink``.
* C'est une valeur qui peut être fixe(comme ``200px``) ou dynamque (comme ``auto`` ou un pourcentage).
* __Remarque__: Si on définit explicitement ``flex-basis``, on a généralement pas besoin de dfinir ``width``, car ``flex-basis`` remplace la largeur(``width``) dans le calcul de la taille des éléments lorsqu'on utilise Flexbox.
* __Par défaut__: ``flex-basis`` est ``auto``, ce qui signifie que la taille d ebase est déterminée par le contenu ou par la proprieté ``width`` si elle est spécifiée.
    ````js
    .item {
        flex-basis: 200px; // Si c'est "auto", alors il récupère la valeur de width
        flex-grow: 1;
    }
    ````
    * Si chaque élément a ``flex-basis: 200px``, il commenceront avec cette largeur de base.
    * Puis, on ajoute les espaces supplémentaires grâce à ``flex-grow``.

### d. La propriété raccourcie: flex
Les trois propriétés ``flex-grow``, ``flex-shrink``, et ``flex-basis`` sont souvent combinées sous la forme de la propriété raccourcie ``flex``.
* Syntaxe: 
    ````js
    .item {
        flex: <grow> <shrink> <basis>;
    }
    ````
Les différents cas:
* cas1: ``flex-grow`` uniquement: ``flex: 1 0 auto`` (Peut grandir mais ne rétrécit pas.)
* cas2: ``flex-shrink`` uniquement: ``flex: 0 1 auto`` (Ne grandit pas mais peut rétrécir.)
* cas3: ``flex-basis`` uniquement ``flex: 0 0 150px`` (Taille fixe de 150px)