[Origine](https://fr.react.dev/learn/you-might-not-need-an-effect)
# Intro
Les Effets sont une façon __d'échapper__ au paradigme de React.
Il nous permettent de __sortir__ de React et de synchroniser nos composants avec:
* Un système extérieur tel qu'un widget écrit sans React.
* Le réseau.
* Le DOM du navigateur.
* Etc.

Sinon (Par exemple si vous voulez juste mettre à jour l'état d'un composant lorsque ses props ou son état changent), on ne doit pas avoir besoin d'un effet !!

Retirer des __Effets superflus__ rendra votre code plus simple à comprendre, plus performant, et moins sujet aux erreurs.
***
# I - Comment retirer les Effets superflus.
Il y a deux scénarios principaux pour lesquels on a pas besoin d'Effets:
1. __Transformer des données utilisées par le rendu__: 
    * Si on souhait filtrer une liste avant de l'afficher. On peut tenter d'écrire un Effet qui mettre à jour une variable d'état lorsque la liste change. => __Inefficace !__ (Plusieurs rendu inutile à cause de setState)
    
2. __Gérer les événements utilisateurs__: Revoir.

En revanche, on a besoin d'Effets pour synchroniser les composants avec des __systèmes extérieurs__. Par exemple, un appel API, charger des données, synchroniser des résultats de recherche avec la requête à jour. 

### A - Mettre à jour un état sur base des props ou d'un autre état.
Soit deux props ``todos``, ``filer``, et  on veut une variable d'état ``visibleTodos`` qui partant de sa prop ``todos`` et en la filtrant selon sa prop ``filter``.  
* __Éviter de créer un Effet superflu__ comme:
    ````js
    ...
    function TodoList({todos, filter}) {
        const [visibleTodo, setVisibleTOdos] = useState([]);
        useEffect(() => {
            setVisibleTodos(getFilteredTodos(todos, filter));
        }, [todos, filter]);
    
        // ...
    }
    ````
    Ceci marche, mais ça :
    * Effets superflu: __Ajouter une variable d'état de plus à gérer !__
    * Inefficace: __SetFullName provoque un nouveau rendu!__.

* Mais utilise plutôt une __valeur calculée lors du rendu__.
    ````js
    function TodoList({todos, filter}) {
        const visibleTodos = getFilteredTodos(todos, filter);
    } 
    // ...
    ````
    * En général ça ira très bien, mais si ``getFilteredTodos()`` est un peu lente, alors on peut mémoriser ce calcule en le englobant dans un ``useMemo``.
__Quand quelque chose peut être calculé à partir des props et variables d'état existante, ne le mettez pas dans l'état mais calculez-le pendant le rendu.__, cela permet de :
* Plus performant (Pas de mises à jour en cascade).
* Plus simple(moins de code).
* Moins sujet à erreurs(On évite les bugs dus à la désynchronisation des variables d'état).

### B - Réinitialiser tout votre état quand une prop change.
Un beau jour, on remarque un problème dans notre code, c'est lorsqu'on passe d'un profil à l'autre, l'état ``comment`` n'est pas réinitialisé. Pour corriger cela, on a choisit de vider les variables avec un ``useEffect``:
* ````js
    export default function ProfilePage({userId}) {
        const [comment, setComment] = useState("");
        useEffect(() => {
            setComment("");
        }, [])
        // ...
    }
    ````
    * Mais comme tu sais, c'est un Effet superflu, et ça va faire un rendu basé sur une valeur obsolète, puis refaire un rendu.
    * Et c'est compliqué, car il faut le faire dans chaque composant qui utilise un état issu de ``ProfilePage``. Et souvent c'est source d'erreur.

La bonne alternative consiste à indiquer à React quue chaque composant de profil _représente un profil différent_, en leur fournissant une clé explicite. C'est à dire découpéez le composant en deux, et passer une prop ``key`` du composant externe au composant interne:
* ````js
    <Profil
        userId = {userId}
        key={userId}
    />
    ````
    * Toutes les variables d'état déclarées dans Profil seront réinitialisées automatiquement en cas de changement de clé.
* En temps normal, React _préserve l'état__ lorsqu'un même composant fait son rendu au même endroit. C'est le fait de passer ``userId`` comme ``key`` qui indique au React qu'il y a un changement de composant.

### C - Partager des traitements entre gestionnaires d'événements.
* Gestionnaire d'événements en React: Onclick, onChange, etc.
* Revoir si nécessaire.

### D - Envoyer une requête POST.
On ne devrait pas gérer une requête dans un Effet, sinon dans un gestionnaire d'événement correspondant.

Quand on décide si on doit placer un traitement dans un gestionnaire d'événement ou dans un Effet, la question principale doit être de quel type de traitement s'agit-il du point de vue utilisateur:
* Si ça fait suite à une __interaction spécifique__, gardez-le dans un gestionnaire d'événement.
* Si c'est dû au fait que l'utilisateur __voit le composant à l'écran__, gardez-le dans un Effet.

### E - Chaînes de calculs.
Pafois on peut chaîner des Effets pour que chacun ajuste une partie spécifique de l'état, sur base d'autres parties de l'état.
* ````
     // 🔴 Évitez : chaînes d’Effets pour ajuster des bouts d’état de façon interdépendante
      useEffect(() => {
        if (card !== null && card.gold) {
          setGoldCardCount(c => c + 1);
        }
      }, [card]);
      useEffect(() => {
        if (goldCardCount > 3) {
          setRound(r => r + 1)
          setGoldCardCount(0);
        }
      }, [goldCardCount]);
      useEffect(() => {
        if (round > 5) {
          setIsGameOver(true);
        }
      }, [round]);
      useEffect(() => {
        alert('Belle partie !');
      }, [isGameOver]);
    ````
    Ce code pose 2 problèmes:
    * Trop inefficae: Trop des rendus à cause de chaque appel ``set``.
    * Difficile à maintenir: S'il y a des nouvelles spécifications, on doit redéfinir la chaîne d'Effets.

Dans un tel cas, il vaut largement mieux calculer tout ce qu'on peut pendant le rendu, et ajuster l'état au sein d'un gestionnaire d'événement:
* ````js
    ...
     // ✅ Calculer tout ce qu’on peut au sein du rendu
    const isGameOve = round > 5;
    ````
    Et puis, mettre tous les logiques et sets dans la fonction ``handlePlaceCard`` afin de ne provoquer qu'un seul rendu.

