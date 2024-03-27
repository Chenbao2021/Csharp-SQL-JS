Source: https://www.sitepoint.com/a-comprehensive-introduction-to-less-mixins/

### Introduction
Mixins : 
* Similar to functions in programming languages.
* Used to group CSS instructions in handy, reusable classes.
    * Allow you to embed all the properties of a class into another class by simply including the class name as one of its properties.

### Enter your arguments, Please
Mixins can be made parametric, meaning they can take arguments to enhance their utility.
A parametric mixin all by itself is not output when compiled(So a blank argument list make mixin dissapears).

Exemple: 
````
.round-borders (@radius: 5px) {
  border-radius: @radius;
}

header {
  .round-borders(4px);
}

.button {
  .round-borders; // Default 5px
}
````

Within a mixin, there is a special variable named @arguments that contain all the arguments passed to the mixin, the value of the variable has all the values separated by spaces.

### A Little Matchmaking
* __Matching on arity__ ( The number of arguments that the mixin takes).
    That means only the mixins that match the number of arguments passed in are used, __with the exception of zero-argument mixins__, which are always included.
    
* __Specifying a value in place of an argument name when declaring the mixin__.
    Exemple:
    ````
    .mixin (dark, @color) {
      color: darken(@color, 10%);
    }
    
    .mixin (light, @color) {
      color: lighten(@color, 10%);
    }
    
    .mixin (@_, @color) { // this is always included
      display: block;
    }
    
    @switch: light;
    
    .class {
      .mixin(@switch, #888);
    }
    ````

### Guards
A guard is a special expression that is associated with a mixin declaration that is evaluated during the mixin process.
It must evaluate to true before the mixin can be used.

List examples see: https://www.sitepoint.com/a-comprehensive-introduction-to-less-mixins/
or : ','
and: 'and'
not: 'not'


### Think Math is Bad? Think Again.
Check the source page.

### Color Alchemy
Less provides a variety of functions that transform colors.
Check the source page.

### Do you love hierarchy? Yes, I Do.
In CSS, we write out every ruleset separately, in Less you can simply nest selectors inside other selectors.
- CSS:
    ````
    header { color: black; }
    header nav {font-size: 12px;}
    header .logo {width: 300px;}
    header .logo:hover {text-decoration: none;}
    ````
- LESS:
    ````
    header    { color: black;
      nav     { font-size: 12px }
      .logo   { width: 300px;
        &:hover { text-decoration: none }
      }
    }
    ````

### Name it, Use it, and Re-use it. Is it that simple ?
What if you want to group mixins into separate bundles for later re-use, or for distributing? Less gives you the ability to do that by nesting mixins inside a ruleset with an ID, like #namespace.
````
    #fonts {
        .serif (@size: 14px) {
        font-family: Georgia, "Times New Roman", serif;
        font-size: @size;       
      }
      .sans-serif @size: 14pxx) { 
        font-family: Arial, Helvetica, sans-serif;
        font-size: @size;       
      }
    }
    
    body {
      #fonts > .sans-serif;
    }
````






