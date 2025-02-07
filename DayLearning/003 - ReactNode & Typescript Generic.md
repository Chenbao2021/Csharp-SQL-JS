## I - ReactNode
Use ``ReactNode`` instead of ``JSX.Element | null | undefined | ... `` to keep your code more compact.

La différence entre passer des élémens comme props, et importer directement un composant dans le fichier, réside dans __qui contrôle le contenu de la structure__, et dans le __niveau de responsabilité__ des composants, par exemple:
````js
const Parent = () => {
    return <Child content={<span>Hello</span>} />;
};

const Child = ({ content }: { content: React.ReactNode }) => {
    return <div>{content}</div>;
};
````
* Le composant ``Parent`` décide du contenu (``<span>Hello</span>``).
* Le composant ``Child`` __ne fait qu'afficher__ ce qu'on lui passe en ``props``.
* __C'est donc le parent qui a le contrôle du contenu.__

Et

````js
import MyComponent from "./MyComponent";

const App = () => {
    return <MyComponent />;
};
````
* Ici, ``MyComponent`` ne reçoit pas son contenu via un prop, il définit lui-même ce qu'il affiche.
* __Le composant lui-même contrôle son contenu.__

__Modularité et Réutilisation__:
* Avec ``ReactNode`` : Plus générique et peut être réutilisé dans différents contextes.
* __Importé directement__ : Moins flexible car il est figé dans sa structure.

__Séparation des responsabilités__:
* Avec ``ReactNode`` en props: 
    * ``Parent`` se concentre sur le __layout__ ou la __structure__.
    * ``Fils`` se concentre sur le contenu.
* Importé directement: ``Parent`` gère à la fois la structure __et le contenu__, ce qui peut violer le principe de séparation des responsabilités.

__Maintenance et évolutivité__:
* Avec ``ReactNode`` en props: Si le contenu ou le comportement change, on n'a qu'à modifier les composants enfants.
* Importé directement: On doit modifier le composant père.

## II - Use TypeScript generics
Si tu n'a pas encore utilisé TypeScript generics, alors ça veut dire deux choses:
* Soit(either) tu fait des codes simples.
* Soit t'es complémentement perdu!

Les __generics__ en TypeScript permettent d'écrire du code réutilisable et flexible tout en conservant la sécurit de typage. Plutôt que de travailler avec le type ``any`` ou de créer plusieurs versions d'une fonction pour différents types, les generics vous permettent de définir une abstraction qui s'adapte à différents types.

##### A - Fonctions génériques.
La fonction ``identity`` retourne simplement ce qu'elle reçoit. Sans generics, vous pourriez être tenté d'utiliser ``any``:
* ````js
    function identity(arg: any): any {
        return arg;
    }
    const output = identity("Hello");
    ````

Avec les generics, on définit un paramètre de type qui sera précisé lors de l'appel ou inféré par TypeScript:
* ````js
    function identity<T = string>(arg: T): T {
        return arg;
    }
    const outputString = identity<string>("Hello");
    const outputNumber = identity(123);
    ````
    Ici, ``<T>`` est une __variable de type__. On peut lui donner n'importe quel nom, mais par convention, on utilise souvent ``T``.

##### B - Contraintes sur les generics
Parfois, on veut s'assurer que le type passé en paramètre possède certaines propriétés ou méthodes. On peut alors utiliser une __contrainte__ avec le mot-clé ``extends``.
* ````js
    interface HasLength {
        length: number;
    }
    function logLength<T extends HasLength>(arg: T): T {
        console.log(arg.length);
        return arg;
    }
    logLength("Hello");// Ok
    logLength([1, 2, 3]);// Ok
    logLength(123); // Erreur.
    ````

##### C - Classes génériques 
Revoir si besoin.

##### D - Interfaces génériques
On peut définir des interfaces qui utilisent des generics pour décrire des structures de données flexibles.
* ````js
    interface Pair<T = number, U = string> {
        first: T;
        second: U;
    }
    
    const pair: Pair<number, string> = {
        first: 1,
        second: "un",
    };
    ````

Imagine qu'on a une réponse d'API dont la structure varie en fonction des données:
* ````js
    interface ApiResponse<T> {
        data: T;
        error?: string;
    }
    interface User {
        id: number;
        name: string;
    }
    const responseUser: ApiResponse<User> = {
        data: { id: 1, name: "Alice" }
    }
    ````

##### E - Generics dans des fonctions asynchrones
Les generics sont particulièrement utiles avec les fonctions asynchrones, notamment lorsqu'on travaille avec des Promesses. Ils permettent de spécifier le type de la valeur résolue par la promesse.
* ````js
    async function fetchData<T>(url: string): Promise<T> {
        const response = await fetch(url);
        if(!reponse.ok) {
            throw new Error("Erreur lors de la récupération des données");
        }
        const data: T = await response.json();
        return data;
    }
    interface User {
        ...
    }
    // Appel de la fonction générique en précisant le type attendu.
    async function getUserData() {
        try {
            const user = await fetchData<User>("https://api...");
            console.log(user.name); // TS sait que "user" est de type User.
        } catch(error) {
            console.error("Erreur lors de la récupération des données: ", error);
        }
    }
    getUserData();
    ````

L'utilisation de generics dans des fonctions asynchrones avec ``async/await`` permet de :
* __Garantir la cohérence du type__ des données récupérées via des API ou d'autre sources asynchrones.
* __Adapter la fonction__ à différents types de données sans dupliquer le code.
* __Améliorer la sécurité de typage__ et la lisibilité du code, en rendant explicite le type des résultats attendus.



