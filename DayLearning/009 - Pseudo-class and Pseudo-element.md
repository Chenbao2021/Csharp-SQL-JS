[Original text](https://dev.to/fynbarr/understanding-css-selectors-pseudo-class-selectors-e6g)
# I - Introduction
A __pseudo-class__ is a selector that selects elements in a __specific state__, like a class added to the HTML.
0. Changer les couleurs d'un input quand il est empty:
    ````css
	.ui.search.dropdown>.text:empty{
		color: rgba(191,191,191,.87) !important
	}
	.ui.search.dropdown>.text:not(empty){
		color: red !important
	}
    ````
1. Changer le style au survole d'un élément.(``:hover``).
    ````css
    button:hover {
        background-color: blue;
        color: white;
    }
    ````
2. Styliser le premier ou dernier enfant d'un conteneur(``:first-child``, ``:last-child``).
    ````css
    ul li:first-child {
        font-weight: bold;
    }
    ````
3. Appliquer un style aux entrées invalides (``:invalid``).
    ````css
    input:invalid {
        border: 2px solid red;
    }
    ````
4. Styliser les éléments actifs ou ciblés (``:active``, ``:focus``).
    ````css
    a:focus {
        outline: 2px solid orange;
    }
    ````
5. Sélectio d'éléments spécifiques avec ``:nth-child``
    ````css
   tr:nth-child(even) {
        background-color: #f2f2f2;
    }
    ````


A __pseudo-element__ is used to style __specified parts__ of an element. It can be used to style the first letter, or line, of an element. Insert content before, or after, the content of an element.
1. Ajouter du contenu avec ``::before`` et ``::after``
    ````css
    h1::after {
        content: " 🌟" /*Nécessaire pour afficher un pseudo-élément */
    }
    ````
2. Styler la première lettre d'un paragraphe
    ````css
    p::first-letter {
        font-size: 2rem;
        color: red;
    }
    ````

3. Mettre en forme la première ligne d'un texte
    ````css
    p::first-line {
        font-weight: bold;
    }
    ````

4. Masquer des textes tout en affichant un symbole avec ``::before``
    ````css
    .hidden-text::before {
        content: ' 🔒'; 
        display: inline-block;
        color: black;
        position: absolute;
        top: 0;
        left: 0;
    }
    ````


__Rappel__: un selector = les p, h1, a, div, etc.

````css

````

