# Le pouvoir des codes React concises.
Parfois, on se rendu compte qu'il est inutile d'écrire autant des codes, et pour répondre à cette situation, on a ce qu'on appelle "one-liners".
__Ils sont comme des couteux suisses: Petit, mais tout puissant__

### 1. Raccourci des rendus conditionnels
``{condition && <Component />}``
Si la condition est vrai, alors React va évaluer ce qui s'enchaîne dérrière &&.

Cas d'utilisation:
* Afficher une message de bienvenue juste pour les utilisateurs connectés.
* Afficher des offres spéciales pendant certaines heures.
* etc.

### 2. Raccourci pour les valeurs des props par défaut.
Exemple: ``const Button = ({size = 'medium', children}) => ...``

### 3. Raccourci pour actualiser un état.  
``setCount(prevCount => prevCount + 1);``
Cette approche assure que tu travailles toujours avec __les données les plus récentes__.
Cas d'utilisation:
* Compteurs
* Inverser(Tooggling) une valeur booléenne.
* Toutes les situations où la nouvelle valeur dépend de l'ancienne.

### 4. Raccourci pour manipuler des listes/ Objets. 
* List: ``setItems(prevItems => [ ...prevItems, newItem ]);``
* Objets: ``setUser(prevUser => ({ ...prevUser, name: 'New Name' }));``
En React, __l'immutabilité est le clé pour la performance et predictibilité(_predictability_)__. Au lieu de modifier celui qui existe déjà, on va créer une nouvelle.

### 5. Raccourci pour fonction de rappel de référence.
__Ref__ est très pratique pour accéder à des éléments DOM en React.
``<input ref={node => node && node.focus()} />``
* ``node`` représente __l'élément DOM correspondant__ (Ici, l'élément ``<input``).

Ceci nous permet de focaliser sur cet élément input lors d'un rendu. Et c'est idéale pour focaliser sur la première saisie d'une formulaire pour faciliter l'accessibilité lors des rendus.

### 7. Raccourci des memos.
``const MemoizedComponent = React.memo(({prop1}) => <Component prop1={prop1} />);``

React.memo est un composant d'ordre supérieur qui saute les rendus lorsque les props sont identiques.
C'est idéale pour une fonction pure et qui est lourde à se recharger ou un component qui reçoit fréquemment des props identiques.

