[Original text](https://fr.react.dev/learn/choosing-the-state-structure)

# Principes de structuration d'état.
Voici quelques principes qui peuvent nous guider sur la structure des états:
1. __Regrouper les états associés.__: Si des états se mettrent à jour toujours simultannément, alors crée un état objet pour les gérer toujours ensemble.
2. __Éviter les états redondants.__: Créer le minimum des états possibles, calculer ceux qui peuvent être calculé et les stocker en tant que variables. C'est négligeable au niveau de performance mais ça évite des journées des debugs.
3. __Évitez les états fortement imbriqués.__: Un état fortement hiérarchisé peuvent devenir très compliqué à mettre à jours lors que le projet devient complexe (oublie, codes trop compliqué, etc.). Quand c'est possible, priorisez une __structure d'état plate__. Mais un éat objet est bien sûr acceptable.

C'est un peu comme __normaliser la structure de la base de données__.
Dans la suite on verra comment ces principes s'appliquent concrètement.
***
## I - Regrouper les états associés.
__Si deux variables d'état changent toujours ensemble, ce serait une bonne idée de les réunir en une seule variable d'état.__
Comme le cas des coordonnées d'un point.
* ````
    const [position, setPosition] = useState({ x: 0, y: 0 })
    ````

## II - Éviter les contradictions dans l'état.
Voici deux variables d'état basé sur l'état d'envoie des données:
* ````js
     const [isSending, setIsSending] = useState(false);
    const [isSent, setIsSent] = useState(false);
    ````

Si on a oublié de les appeler ensemble, oups ! Et plus le projet devient complexe, plus il est dur de comprendre ce qu'il s'est passé.

Donc, il est préférable de les remplacer par une variable d'état ``status`` qui peut prendre l'un des trois états valides: ``typing``, ``sending``, et ``sent``.
* ````js
    const [status, setStatus] = useState<statusType>('typing');
    const isSending = status === 'sending';
    const isSent = status === 'sent';
    if(isSent) 
        return ...
    return ( ... )
    ````
    * isSending et isSent ne sont pas des variables d'état, donc on n'a pas à soucier de leur désynchronisation.

## III - Éviter les états redondants.
Si on peut calculer certaines informations depuis les props d'un composants ou une de ses variables d'état existantes pendant le rendu, on __ne doit pas__ mettre ces informations dans l'état du composant!
Par exemple si on a trois variables d'état: ``firstName``, ``lastName`` et ``fullName``, on voit que ``fullName`` est redondant, car on peut toujours le calculer depuis ``firstName`` et ``lastName`` pendant le rendu.
* ````js
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const fullName = firstName + ' ' + lastName;
    ````
    * Ici, fullName n'est pas une variable d'état. Elle est plutôt évaluée pendant le rendu.
        Par conséquent, les gestionnaires de changement n'auront rien à faire pour le mettre à jour, c'est calculé automatiquement.

## IV - Évitez la duplication d'états.
Supposons qu'on a une liste d'options des voyages, et on doit choisir un parmit tous.
Voici une proposition des variables d'états:
* ````js
    const initialListItems =  [{ id: 0, title: 'bretzels'}, ...]
    const [listItems, setListItems] = useState(initialListItems);
    const [selectedItem, setSelectedItem] = useState(items[0]);
    ````
    * On voit qu'il y a une duplication des informations ici, et quand le projet devient complexe, on peut changer la variable listItems mais oublier de synchroniser la variable selectedItem.

Même si vous pensez que vous allez toujours synchroniser les deux variables d'états, mais on peut faire plus simplement, au lieu d'un objet ``selectedItem, on garde le ``selectedId`` dans l'état, puis obtenons le ``selectedItem`` en cherchant dans la liste ``items`` un élément avec cet ID.
* ````js
    const [items, setItems] = useState(initialItems);
    const [selectedId, setSelectedId] = useState(0);
    const selectedItem = items.find(item => item.id === selectedId);
    ````

## V - Éviter les états fortement imbriqués
Imaginez un plan de voyage composé de planètes, de continents et de pays.
On pourrait être tenté(e) de strucuturer son état à l'aide de listes et d'objets imbriqués très complexe.

__La mise à jour d'un état imbriqué__ implique de faire des copies des objets en remontant depuis la partie qui a changé! Supprimer un lieu profondément imbriqué consisterait à copier tous les niveaux supérieurs.

__Si l'état est trop imbriqué pour être mis à jour facilement, envisagez de l'aplatir__. (Revoir [Original text](https://fr.react.dev/learn/choosing-the-state-structure))





