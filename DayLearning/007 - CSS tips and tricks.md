[Original text](https://dev.to/devsyedmohsin/css-tips-and-tricks-you-will-add-to-cart-163p)
# The ``gradient text``
````css
h1 {
    background-image: linear-gradient(to right, #C6FFDD, #FBD786, #f7797d);
    background-clip: text;
    color: transparent;
}
````

# The ``Image filled text``
````css
h1 {
    background-image: url('illustration.webp')
    background-clip: text;
    color: transparent;
}
````


# Improve ``media defaults``
When writing css reset add these properties to improve media defaults
````css
img, picture, video, svg {
  max-width: 100%;
  object-fit: contain;  /* preserve a nice aspect-ratio */
}
````

# The ``smooth scrolling``
````css
html {
    scroll-behavior: smooth;
}
````

# The ``hyphens``
Set hyphens property on your text content to make it hyphenated(Trat d'union) when text wraps across multiple lines.
````css
hyphens: manual
````

# The ``first letter``
Avoid unnecessary spans and use pseudo elements to style your content likewise first letter pseudo element we also have first-line pseudo element
````css
h1::first-letter {
    color: #ff8A00;
}
````

# The ``selection pseudo element``
````css
::selection {
    color: coral;
}
````

# The ``text indent``
````css
p {
    text-indent: 5.275rem;
}
````




