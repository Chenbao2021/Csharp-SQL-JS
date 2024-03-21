# Parent Selectors
Referencing parent selectors with __&__:
````
a {
  color: blue;
  &:hover {
    color: green;
  }
}
````
means:
````
a {
  color: blue;
}

a:hover {
  color: green;
}
````

Without __&__, the aboce example would result in __a :hover__ (a descendant selector that matches hovered elements inside of <a> tag).

Use cases:
- Produce repetitive class names:
    ````
    .button {
      &-ok {
        background-image: url("ok.png");
      }
    }
    ````
    Output:
    ````
    .button-ok {
      background-image: url("ok.png");
    }
    ````
- We can also apply multiple &
- Note & represent all parent selector(not just the nearest ancestor) !

# Rappel sur les selecteurs CSS
- .a + .b : Tous les b suivi de a.
- .a .b : Tous les b qui se retrouvent dans a.
- .a > .b : Tous les b qui sont des enfants directs de a
- .a.b : Tout élement qui a simultanément la classe a, et la classe b
- .a, .b; Tous les classes a et classe b.


# Variable Interpolation
- control values from a single location : @link-color = #428bca
- Selector names, property names, URLs and @import statement
    ````
    // Variables
    @my-selector: banner;
    
    // Usage
    .@{my-selector} {
      font-weight: bold;
      line-height: 40px;
      margin: 0 auto;
    }
    ````
    - Always use the variable by englobe it within __@{variable_name}__
    - variable is using reference,  my-selector = reference point to the variable, @my-selector = value in this reference.

- Lazy evaluation : Variable do not have to be declared before being used.
    - When defining a variable twie, the last definition of the variable is used, searching from the current scope upwards.


