#### 0 - un ptit plus: Prefer ``unknown`` Over ``any``
Unlike ``any``, ``unknown``requires type-checking before you canperform operations on it, enforcing safety
* ````
    function processInput(input: unknown) {
      if (typeof input === "string") {
        console.log(input.toUpperCase());
      }
    }
    ````

#### I - Don't utilize Enum for Meaningful Values
L'utilisation d'``enum`` en TS est souvent déconseillée pour des raisons liées à des problèmes pratiques et techniques:
* __N'est pas original__ à JavaScript.
* Permettent des __accès indésirés__ au moment de la compilation:
    ````ts
    export enum assessment {
    	cat1 = 1,
    	cat2 = 2,
    	cat3 = 3,
    	empty = -1000
    }
    console.log(assessment[5])// Pas d'erreur à la compilation alors qu'il n'existe pas !!!
    ````
* Enums ont __un comportement bidirectionnel__, ce qui peut être très confus.(Donner un key ou une valeur sont tous valables).
* Moins performant que ses alternatives modernes. Car il génère un __objet avec une fonction auto-invoquée__, ce qui alourdit(légérement) le code comparé à un simple objet constant.
    

Donc il est recommandé d'utiliser ses alternatives:
1. Literal Union Types :
    ````ts
    type Color = "Red" | "Green" | "Blue";
    const color: Color = "Red"; // Sécurité renforcée.
    ````
2. Const Object :
    ````ts
    const Color = {
      Red: '#FF0000',
      Green: '#00FF00',
      Blue: '#0000FF',
    } as const;
    type typeColor = typeof Color[keyof typeof Color];
    const color: typeColor = Color.Red;
    ````
    * ``as const`` transforme l'objet en littéraux immuables. (__Très important !!!__, sans cela, typeof retourne number | string au lieu de la valeur littéral)
    
    Mais comment fonctionne ``typeof Color[keyof typeof Color]``?
    1. ``typeof Color`` Donne le type de l'objet ``Color``
        ````ts
        {
          readonly Red: "#FF0000";
          readonly Green: "#00FF00";
          readonly Blue: "#0000FF";
        }
        ````
    2. ``keyof typeof Color``: Donne les clés de l'objet ``Color`` sous forme d'union:
        ``"Red" | "Green" | "Blue"``
    3. ``typeof Color[keyof typeof Color]``( 
        = ``typeof Color['Red' | 'Green' | 'Blue']`` 
        = ``typeof Color['Red'] | typeof Color['Green'] | typeof Color['Blue']``
        ): Donne les types de __valeurs__ associées à ces clés.
        ``"#FF0000" | "#00FF00" | "#0000FF"``

