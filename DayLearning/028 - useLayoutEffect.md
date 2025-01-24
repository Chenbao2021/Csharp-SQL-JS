``useLayoutEffect``  peut être important dans certains cas spécifique, mais la majorité des besoins sont bien couverts par ``useEffect``.
__Règle générale__: Si vous n'êtes pas sûr, commencez avec ``useEffect``. Passez à ``useLayoutEffect`` uniquement si vous rencontrez un problème spécifique lié au DOM ou à des états intermédiaires visibles.

## 1. Qu'est-ce que ``useLayoutEffect`` ?  
* ``useLayoutEffect`` fonctionne de manière similaire à ``useEffect``, mais il est exécuté à un moment différent dans le cycle de vie du rendu.
    En effet, __le cycle de rendu__ de React se déroule en plusieurs étapes:
    1. __Évaluer les états et les props nécessaires__ pour calculer le Virtual DOM 
    2. __Phase de rendu virtuel__: React génère le DOM virtuel.
    3. __Mise à jour du DOM réel__: React applique les changements au DOM réel.
        * À la fin, exécution de ``useLayoutEffect`` synchronement. 
    4. __Affichage(paint)__: Le navigateur affiche visuellement les changements à l'écran.
        * À la fin, exécution de ``useEffect``: manière asynchrone, après l'affichage.

    Et notre ``useLayoutEffect`` s'exécute après la mise à jour du DOM réel(étape 2) mais __avant l'affichage__. Cela vous permet d'appliquer des ajustements immédiatement si nécessaire.
    
* Il est déclenché __synchroniquement après que le DOM a été mis à jour, mais avant que le navigateur ne "pein"(affiche) à l'écran__. Cela permet de modifier directement le DOM ou d'effectuer des calculs précis liés à sa disposition avant que l'utilisateur ne voie quoi que ce soit.(Pour éviter des vacillement pour des chargements lourds).
    * Mais comme c'est __Synchroniquemet__, alors il  peut rendre l'interface trop "lent" pour les utilisateurs.

## 2. Quand utiliser ``useLayoutEffect``?
Souvent on utilise ``useLayoutEffect`` avec ``useRef``, car cela permet de modifier directement le style de DOM sans déclencher un re-rendu de l'application. Et cette modification directe du style de DOM s'affichera directement pour les utilisateurs. 
1. __Mésurer le DOM avant affichage__: Par exemple, déterminer la taille ou la position d'un élément après un rendu mais avant que l'utilisateur ne voie la mise à jour.
    Exemple: Taille de la police.
    * Avec ``useEffect``:
        1. Au premier rendu, le __texte s'affiche avec la taille de police initiale(16px)__.
        2. Une fois le ``useEffect`` exécuté, la taille de police est mise à jour selon la largeur du conteneur.
        3. Cela peut entraîner en __vacillement visuel__, car l'utilisateur voit d'abord le texte avec la taille initiale, puis avec la taille recalculée.
    * Avec ``useLayoutEffect``(Pas de vacillement):
        1. Le ``useLayoutEffect`` est exécuté __avant que l'écran ne soit affiché__.
        2. La taille de police est ajustée __avant l'affichage__, donc l'utilisateur voit directement le texte avec la bonne taille.
2. __Appliquer des modifications synchrones au DOM__: Car ``useEffect`` est une opération asynchrone, c'est à dire qu'il ne bloque pas la processus principale. Par contre ``useLayoutEffect``, il bloque l'étape 2 et 3 jusqu'il termine ses codes.
    * Remarque : On ne peut pas donner une callback async à useEffect.

__Pourquoi ne pas toujours utiliser?__
* __Performance__: ``useLayoutEffect`` bloque le navigateur jusqu'à ce que l'effet soit terminé. Cela peut ralentir l'affichage si mal utilisé.
* __Complexité inutile__: Dans la plupart des cas, ``useEffect`` est suffisant, car les ajustements visuels mineurs après l'affichage ne sont pas critique (Dans même pas 10ms, c'est imperceptible). Donc l'utiliser que si ce "saut"(Affichage intermédiaire) est perceptible et dérangeant.   

## 3. Exemples pratiques
1. Éviter les états intermédiaires
    ````js
    const [questionList, setQuestionList] = useState([]);
    useLayoutEffect(() => {
        setQuestionList(["question1", "question2", "question3", ...])
    }, []);
    ...
    ````
    Ce qui se passe:
    1. __Avant le rendu initial__, ``useLayoutEffect`` met à jour ``questionList``.
    2. L'utilisateur voit directement la liste mise à jour sans état intermédiaire.




    

