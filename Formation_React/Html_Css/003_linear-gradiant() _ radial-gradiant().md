Source: https://developer.mozilla.org/en-US/docs/Web/CSS/gradient/linear-gradient
# linear-gradient()
Creates an image consisting of a progressive transition between two or more colors along a straight line.

default is from top to bottom.

#### Syntax
````
body {
  background: linear-gradient(
    to right,
    red 50px,
    orange 50px 150px,
    yellow 40% 60%,
    green 60% 80%,
    blue 80%
  );
}
````
- "to right top" : La direction des couleurs, dans ce cas la, le couleur de right top doi être "blue"
- "red 20%": La dégration de rouge commence à partir de 20%.
- "orange 20% 40%": Le couleur entre 20% et 40% est orange.
- Entre deux valeurs , il n'y a pas de dégradation mais le couleur fixe. et le couleur qui se trouve devant est prédominant.

#### repeating-linear-gradient()
It is similar to linear-gradient() and takes the same argument, but it repeats the color stops infinitely in all directions so as to cover its entire container.



# radial-gradient()
Create an image consisting of progressive transition between two or more colors that radiate from an origin.
Its shape may be a circle or an ellipse. 


