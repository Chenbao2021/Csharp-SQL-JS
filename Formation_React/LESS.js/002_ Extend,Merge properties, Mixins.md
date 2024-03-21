Source: https://lesscss.org/features/#features-overview-feature

- Extend : 
    -Extend is a feature in Less that allows you to share styles between selectors. 
    -It generates CSS rules with comma-separated selectors, which can sometimes lead to more concise and efficient CSS output.
- Mixins : 
    -Mixins do not generate additional CSS selectors like extend does; instead, they insert the styles directly into the selectors where they are included.

# Extend
Extend is a Less pseudo-class which merges the selector it is put on with ones that march what it references.
Exemple:
````
nav ul {
  &:extend(.inline);
  background: blue;
}
````
Similiar to :
````
nav ul {
  background: blue;
}
.inline,
nav ul {
  color: red;
}
````
- all: extends all instances .
- It can contain one or more classes to extend, separated by commas.
- All extends mut be at the end of the selector
- If a ruleset contains multiple selectors, any of them can have the extend keyword:
    ````
    .big-division,
    .big-bag:extend(.bag),
    .big-bucket:extend(.bucket) {
      // body
    }
    ````
    - Placing extend into a body is a shortcut for placing it into every single selector of that ruleset.
- Extend is able to match nested selectors.
- Extend is not able to match selectors with variables, but it can attached to an interpolated selector.

Use cases for Extend
- Avoid adding a base class.
    ````
    .animal {
      background-color: black;
      color: white;
    }
    .bear {
      &:extend(.animal);
      background-color: brown;
    }
    ````
- Reducing CSS Size : It will produce ruselet css classes when they contain same codes.

# Merge properties
- Comma: +
- Space: +_
To avoid any unintentional joins, __merge__ requires an explicit + or +_ flag on each join pending declaration.

# Mixins
You can mix-in class selectors and id selectors:
````
.a, #b {
  color: red;
}
.mixin-class {
  .a();
}
.mixin-id {
  #b();
}
````
Similiar to
````
.a, #b {
  color: red;
}
.mixin-class {
  color: red;
}
.mixin-id {
  color: red;
}
````
- The parentheses is required

If you want to create a mixin but you don't want that mixin to be in your CSS output, then put parentheses after the mixin definition.
````
.my-mixin {
  color: black;
}
.my-other-mixin() {
  background: white;
}
.class {
  .my-mixin();
  .my-other-mixin();
}
````

Mixins can contain more than just properties, they can contain selectors too.
If you want to mixin properties inside a more complicated selector, you can stack up multiple ids or classes:
````
#outer() {
  .inner {
    color: red;
  }
}

.c {
  #outer.inner();
}
````

