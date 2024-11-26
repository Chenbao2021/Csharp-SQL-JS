# React
***
# I - La différence entre les deux déclaration de undefined.
On a deux façon de donner le type "undefined" à une variable en Typescript.
1. id?: number
2. id: number | undefined

Il y a une différence subtile mais importante entre les deux déclarations en Typescript.
##### 1. ``id?: number``
* Le ``?`` signifie que la propriété est __optionnelle__.
* Elle peut être:
    * Non définie(``Undefined``)
    * Un nombre (``number``)
    * Ou explicitement ``null``
* Exemple
    ````JS
    type Example1= {
        id?: number
    }
    
    const example1a: Example1 = {}; // ok, car id est optionnel, même sa présence 
    const example1b: Example1 = { id: 42 }; // ok
    const example 1c: Example1 = { id: undefined } // Ok 
    ````

##### 2. ``id: number | undefined``
* La propriété est __obligatoire__, mais elle peut avoir deux valeurs possibles:
    * Un nombre(``number``).
    * Ou ``undefined``
    
    ````JS
    type Example2 = {
        id: number | undefined;
    }
    
    const example2a: Example2 = {}; // Erreur, car la présence de l'attribut id est obligatoire.
    const example2b: Example2 = { id:42 }; // Ok
    const example2c: Example2 = { id: undefined}; // Ok
    ````

##### 3. Pourquoi ces deux options existent-elles en Typescript?
1. __Gestion de l'optionalité et des valeurs manquantes__
    Typescript distingue entre:
    1. Une propriété __optionnelle__ qui peut être absente(``undefined`` implicite via ``?``)
    2. Une propriété __obligatoire__ qui peut avoir une valeur explicite comme ``undefined`` ou ``null``.
    
    Cela reflète les cas d'utilisation réels:
    * __Propriétés optionnelles(?)__ : Utilisées pour des objets où certaines informations peuvent ne pas exister.
    * __Union avec ``undefined``__: Représente explicitement une propriété toujours présente, mais pouvant contenir une valeur indéterminée.
    
2. __Flexibilité pour différents besoins__
    Ces deux mécanismes permettent de mieux modéliser des scénarios réels:
    1. __Propriété optionnelle(``?``)__:
        Représente des données incomplètes(par exemple, un formulaire où certaines réponses ne sont pas obligatoires).
    2. __Union avec ``undefined``:
        Montre que la propriété est toujours attendues dans l'objet, mais sa valeut peut être inconnue.

##### 4. Cas où on est obligé d'utiliser l'un mais pas l'autre
1. ``id``, on n'utilise pas une propriété optionnelles pour ``id``, parce que la clé d'un objet doit toujours être présente.
2. Certaines API ou structures de données en Back-end exigent explicitement que toutes les propriétés soient définies, même si leur valeur est indéterminée.
    ````TS
    type ApiRequest = {
      idAssessment: number | null | undefined; // La propriété doit toujours être présente.
      data: string;
    };
    
    // Exemple typique pour préparer un objet destiné à une API :
    const request: ApiRequest = {
      idAssessment: undefined, // Pas encore assigné, mais doit exister.
      data: "Some payload",
    };
    ````
3. Lorsque vous voulez assurer que certaines proprietés soient présents au moment de l'initialisation de l'objet, l'utilisation de propriété optionnelles est à éviter.
    ````
    type InitialState = {
      idAssessment: number | null | undefined; // Doit exister mais peut être indéfinie.
      isComplete: boolean;
    };
    
    const state: InitialState = {
      idAssessment: undefined, // Valeur non encore connue.
      isComplete: false,
    };
    ````