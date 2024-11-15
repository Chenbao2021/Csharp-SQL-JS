__The CSS can be both a blessing and a curse.__

## 1. The Magical Centering Trick
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

## 2. Smooth Scrolling(滚动) for the Win
When we click a button to scroll to a specific balise, use this can give a smooth fashion effect 
````
html {
    scroll-begavior: smooth;
}
````

and we can even do this in Javascript:
``document.documentElement.style.scrollBehavior = 'smooth';``

## 3. Truncate Text with Ellipsis
Sometimes you need to display text in a confined space, but you don't want it to wrap or overflow. This snippet will truncate your text and add an ellipsis (...) at the end:
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

## 4. Simple CSS Gradient Background
Gradients can add depth and interest to your design, here is how to create a simple linear gradient background:
````css
.gradient-bg {
  background: linear-gradient(to right, #ff7e5f, #feb47b);
}
````
use this website to create your gradient code: https://cssgradient.io/

## 5. The lobotomized Owl Selector
This selector is incredibly useful for adding consistent spacing between elements:
````css
* + * {
  margin-top: 1.5em;
}
````
This selector targets any element that directly follows another element. Great way to maintain vertical rhythm in your layouts without having to add margin classes to every element.

## 6. CSS Variable for Easy Theming
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
By defining variables in the :root pseudo-class, you can reuse these values throughout your stylesheet.
