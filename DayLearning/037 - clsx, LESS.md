# __clsx__
On peut utiliser ``clsx`` pour gérer proprement nos classes selon des conditions.
### A. Syntaxe de base.
````js
import clsx from 'clsx'
<div className={clsx('btn', 'btn-primary')}>
// Résultat: class="btn btn-primary"
````

### B. Classes conditionnelles(ternaire ou booléen).
````js
<div className={clsx('btn', isActive && 'active')}>
````
* Résultat: Si ``isActive`` true, alors 'bnt active', sinon 'active'.

### C. Utiliser la syntaxe objet, qui est plus lisible.
````js
clsx('class1', 'class2', {
	'primary': isPrimary,
	'secondary': isSecondary,
	'disabled': isDisabled
})
````
* Dans l'objet, on peut mettre autant des classes, et seulement celles avec une valeur ``true`` seront ajoutés.

Sans ``clsx``, on aurait écrire comme :
````js
let className = 'class1 class2';
if(isPrimary) className += ' ' + 'primary' 
````
* C'est faisable, mais moins lisible et moins propre.

### D. Combiner avec des tableaux.
````js
clsx(['btn', isActive && 'active', isLarge ? 'lg':'sm'])
````

# LESS, combo perfect avec clsx.
LESS est un préprocesseur CSS, c'est un outil qui nous permet d'écrire avec une syntaxe améliorée, et les transforme en vrai __CSS__ par "compilation".

### A. Les variables(``@``)
Très utilisées pour:
* Couleurs.
* Tailles.
* Fonts.
* Marges/ paddings.

Par exemple, on peut déclarer toutes les variables dans un fichier "const.less", puis l'importer dans un autre fichier less pour l'utiliser:

````js
// const.less
@primary-color: #000000;
@font-stack: 'Segoe UI', sans-serif;

// autreFicheirs.less
@import '../const.less';
.class {
	color: @black-color;
}
...
````
### B. L'imbrication et l'opérateur ``&``
L'opérateur ``&`` représente le sélecteur parent.
````css
.card {
	border: 1px solid #ddd;
	&:hover {
		box-shadow: 0 0 5px rgba(0, 0, 0, 0.2);
	}
}
````
* ``&:hover`` devient ``.card:hover``.

### C. Les Mixins(réutilisation de blocs)
Mixin simple:
````css
.centered {
	display: flex;
	justify-content: center;
	align-items: center;
}

.box {
	.centered;
	maxWidth: 200px;
}
````

Mixin avec paramètres:
````css
.button-style(@bg-color, @text-color: white) {
	padding: 10px 20px;
	border-radius: 6px;
	background-color: @bg-color;
	color: @text-color;
	border: none;
	cursor: pointer;
}

.btn-primary {
	.button-style(#2196F3);
}

.btn-danger {
	.button-style(#e74c3c);
}
````

Mixin: Gérer les responsive breakpoints proprement.
````css
.responsive(@property, @sm, @md, @lg) {
	@{property}: @sm;
	@media (min-width: 768px) {
		@{property}: @md;
	}
	@media (min-width: 1024px) {
		@{property}: @lg;
	}
}

.container {
	.responsive(width, 100%, 750px, 1000px);
}
````

### D. Les fonctions utiles de LESS(Et comment les exploiter)
Fonctions couleurs:
* ``lighten(@c, 10%)``: Éclaircit une couleur.
* ``darken(@c, 15%)``: Assombrit une couleur.
* ``fade(@c, 50%)``: Donne une transparence.
* ``mix(@c1, @c2, 30%)``: Mélange deux couleurs.
* ``contrast(@c)``: Noir ou blanc, selon la lisibilité.

````css
@bg: #3498db;

.btn {
	background-color: @bg;
	border-color: darken(@bg, 10%);
	color: contrast(@bg);
}
````




