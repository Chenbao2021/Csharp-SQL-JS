Source : https://css-tricks.com/snippets/css/a-guide-to-flexbox/

****
# Table of contents
1. Background
2. Basic and terminology
3. Flexbox properties
4. Prefixing flexbox
5. Examples
6. Flexbox tricks
7. Browser support
8. Bugs
9. Related properties
10. More information
11. More sources
****

#### I - Background
- Flex: Give the container the ability to alter its items' width/height to best fill the available space.
- Flexbox layout is most appropriate to the components of an application, and small-scale layouts, while the __Grid__ layout is intended for larger scale layouts.

#### II - Basics and terminology
flexbox is a whole module and not a single property, it involves a lot of things including its whole set of properties.
- Parent element : "flex container"
- Children : "flex items"

The flex layout is based on : "flex-flow directions".
- main axis or cross axis : Depends on the "flex-direction" property.

#### III - Flexbox properties
#####  Properties for the Parent (flex container)
- __display__ : flex;  // It enables a flex content for all its direct children.
- __flex-direction__ : row(default) | row-reverse | column | column-reverse
    Flexbox is a single-direction layout concept.
- __flew-wrap__ : nowrap(default) | wrap | wrap-reverse;
    - nowrap : flex items will all try to fit onto one line.
    - wrap : flex items will wrap onto multiple lines, from top to bottom
    - wrap-reverse: from bottom to top.
- __flex-flow__ : A shorthand for the flex-direction and flex-wrap , the default value is _row nowrap_
- __justify-content__: flex-start | flex-end | center | space-between | space-around | space-evenly | start | end | left | right ...
    The alignment along the main axis.
    - center : items are centered along the line
    - space-between : items are evenly distributed in the line; __first item is on the start line, last item on the end line__.
    - space-around : __equal space around them__ (the first item will have one unit of space left, and 2 unit of space right(1+1))
    - space-evenly : The spacing between any two items is equal (including the edges))

- __align-items__ : stretch | flex-start | flex-end | center | baseline ... 
    Defines the default behavior for how flex items are laid out along the cross axis on the current line.
    Like justify-content version for the cross-axis.

- __align-content__: flex-start | flex-end | center | space-between | space-around | space-evenly | strech ...
    __Hey!__ : A single-line (flex-wrap : wrap) will not reflect align-content.
    - This aligns a flex container's lines within when there is extra space in the cross-axis, similar to how justify-content aligns individual items within the main-axis.
    -strech : lines trech to take up the remaining space.
    -  align-items gère l'alignement des éléments sur l'axe secondaire (justify-content sur l'axe principal), alors que align-content gère l'espacement des éléments sur l'axe secondaire.
- __gap__ : row-gap column-gap, row-gap, column-gap
    - Explicitely controls the space between flex items.
    - It applies that spacing _only between items_ not on the outer edges.
    - The behavior could be thought of as a minimum gutter, as if the gutter is bigger somehow(like justify-content: space-between;) then __the gap will only take effect if that space would end up smaller__.
    - Gap works in grid and multi-column layout as well.

##### Properties for the Children (flex items)
- __order__ : 5 // Default is 0
    - The order property controls the order in which they appear in the flex container.
    - Items with the same order revert to source order (by default).
- __flex-grow__ : 4; // default 0
    - This defines the ability for a flex item to grow if necessary.
- __flex-shrink__ : 3; // default 1
    - This defines the ability for a flex item to shrink if necessary.
    - If the size of all flex items is larger than the flex container, items shrink to fit according to flex-shrink.
- __flex-basis__ : [number] | auto 
    - The default size of an element before the remaining space is distributed.
    - auto : Look at my width or height property
- __flex__ : shorthand for flex-grow, flex-shrink and flex-basis combined.
- __align-self__ : This allow the default alignment(or the one specified by align-items) to be overridden for individua flex items.

#### Prefixing Flexbox

#### Examples
1. Perfect centering
    ````
    .parent {
      display: flex;
      height: 300px; /* Or whatever */
    }
    
    .child {
      width: 100px;  /* Or whatever */
      height: 100px; /* Or whatever */
      margin: auto;  /* Magic! */
    }
    ````
    - A margin set to auto in a flex container absorb extra space.
2. Perfect centering of various elements
    ````
    .flex-container {
      /* We first create a flex layout context */
      display: flex;
    
      /* Then we define the flow direction 
         and if we allow the items to wrap 
       * Remember this is the same as:
       * flex-direction: row;
       * flex-wrap: wrap;
       */
      flex-flow: row wrap;
    
      /* Then we define how is distributed the remaining space */
      justify-content: space-around;
    }
    ````
3. we have a right-aligned navigation element on the very top of our website, but we want it to be centered on medium-sized screens and single-columned on small devices. Easy enough.
    ````
    /* Large */
    .navigation {
      display: flex;
      flex-flow: row wrap;
      /* This aligns items to the end line on main-axis */
      justify-content: flex-end;
    }
    
    /* Medium screens */
    @media all and (max-width: 800px) {
      .navigation {
        /* When on medium sized screens, we center it by evenly distributing empty space around items */
        justify-content: space-around;
      }
    }
    
    /* Small screens */
    @media all and (max-width: 500px) {
      .navigation {
        /* On small screens, we are no longer using row direction but column */
        flex-direction: column;
      }
    }
    ````



