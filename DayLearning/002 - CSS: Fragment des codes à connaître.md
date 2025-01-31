# Css est à la fois une bénédiction(_blessing_) et une malédiction(_curse_).
## 1. Astuce(_trick_) pour centrer les éléments.
````css
.center-me {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
````
__Mais dans les projets récents, on utilise plus souvent le flebox:__
````css
.flex-center {
  display: flex;
  justify-content: center;
  align-items: center;
}
````

## 2. Défilement(_scrolling_) fluide(_smooth_) de la fênetre.
Lorsqu'on clique sur un bouton pour défiler vers une balise spécifique, le code ci-dessous peut donner un effet fluide tout simplement:
````
html {
    scroll-begavior: smooth;
}
````

Petit plus, on peut aussi le faire par JavaScript:
``document.documentElement.style.scrollBehavior = 'smooth';``

## 3. Tronquer(_truncate_) le texte avec des ellipses.
Parfois on doit afficher un text dans une espace restreinte, mais on ne veut pas des retour à la ligne(_wrap_) ou des débordement(_overflow_). Ce fragment(_snippet__) trunquera ton texte et ajouter des ellipses à la fin:
````css
.truncate {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
````
* ``white-space: nowrap;`` : Forcer tout le texte à rester sur une seule ligne. Si le texte est plus long que le conteneur, il débordera l'extérieur du conteneur.
* ``overflow: hidden;`` : Cache toute partie du texte qui dépasse les dimensions du conteneur.
* ``text-overflow: ellipsis;``: Lorsqu'il est utilisé avec les deux proprietés mentionnent ci-dessus, il remplace la partie cachée du texte par des points de suspension(...) pour indiquer qu'il a davantage de texte qui n'est pas affiché.

## 4. Fond dégradé(_gradient_)
Dégradation peut renforcer ta conception(_design_), ce fragment te permet de créer un fond linéairement dégradé :
````css
.gradient-bg {
  background: linear-gradient(to right, #ff7e5f, #feb47b);
}
````
Utiliser cette site pour créer le fragment CSS de dégradement faciment: https://cssgradient.io/

## 5. The lobotomized Owl Selector
Ce selecteur est incroyablement utile pour ajouter des espaces uniformes(_consistent_) entre les éléments:
````css
* + * {
  margin-top: 1.5em;
}
````
Ce selecteur vise(_target_) tous(_any_) les éléments qui suit directement un autre. Bonne façon(_way_) pour maintenir le rithme(_rhythm_) vertical dans ta disposition(_layout_) sans avoir besoin d'ajouter pour chaque élément.

## 6. Les varibles globales en CSS. 
````css
:root {
  --primary-color: #007bff;
  --secondary-color: #6c757d;
  --font-size: 16px;
}

.button {
  background-color: var(--primary-color);
  font-size: var(--font-size);
}
````
En(_by_) definissant les variables dans le pseudo-class ``:root``, on peut les réutiliser à tout moment par l'appel de ``var(...)``.
