# Alléger les dépendances des Effets.
[Ce guide](https://fr.react.dev/learn/removing-effect-dependencies#dependencies-should-match-the-code) nous apprend à:
* Évaluer la pertinence des dépendances de nos Effets.
* Retirer celles qui s'acèrent superflus.

## Cycle de vie d'un Effet.
Chaque __composant React__ suit le même cycle de vie:
* __Monté__ lorsqu'il est ajouté à l'écran.
* __Met à jour__ lorsqu'il reçoit de nouvelles props ou variables d'état,
* __Démonté__ quand il est retiré de l'écran.

Mais pas aux __Effets__! Les Effets sont plutôt indépendant du cycle de vie des composants. 
Un Effet décrit la façon de __synchroniser un système exrérieur__ avec les props et états actuels:
* Le corps de Effet définit comment __démarrer la synchronisation__.
* La fonction de nettoyage(Return () => {...}) définit comment __stopper la synchronisation__.

Sans la fonction de nettoyage, __la synchronisation peut durer__ même si le component est démonté!

## Les dépendances devraient refléter notre code.
Les outils modernes peuvent nous aider à suggérer les dépendances appropriées, utilisez les STP! Par exemple:
* linter
* eslint

__Les Effets réagissent aux valeurs réactives__(Les valeurs qui peuvent être changé suite à un nouveau rendu).
Si ces valeurs réactives reçoivent une valeur différente, React resynchronise l'Effet.

## Pour retirer une dépendance, prouvez qu'elle n'en est pas une.
Notez qu'on ne peut pas __"choisir"__ les dépendances de notre Effet. Chaque __valeur réactive__ utilisée par le code de votre Effet doit être déclarée dans votre liste de dépendance.

On peut rencontrer ce code qui réduisent le linter au silence: ``// eslint-ignore-next-line react-hooks/exhaustive-deps.``
Mais, ceci a un risque très élevé de bugs!

## Pour changer les dépendances, changez le code.
__Si vous voulez changer les dépendances, changez d'abord le code environnant.__, c'est à dire votre façon de travailler doit être:
1. Pour commencer, vous __modifiez le code__ de votre Effet, ou la façon dont les valeurs réactives sont déclarées.
2. Ensuite, vous suivez les recommandations du _linter_ et ajustez les dépendances pour __correspondre à vos changements de code__.
3. Lorsque la liste des dépendances vous déplaît, vous __revenez à la première étape__(Et recommanceez à changer le code).

## Retirer les dépendances superflus.
Parfois, on a pas besoin de ré-exécuter chaque fois qu'une des dépendances change:
* Juste ré-exécuter des parties distinctes de notre Effet selon la situation.
* Seulement lire la valeur la plus à jour d'une dépendance plutôt que de réagir à chacun de ses changements.
* Une dépendance qui change trop souvent par inadvertance car il s'agit d'un objet ou d'une fonction.

#### A. Ce code devrait-il être dans un gestionnaire d'événement?
__On ne met pas une interaction spécifique dans un useEffet__, sinon dans le gestionnaire d'événement adéquat. Par exemple, un appel POST dans un Effet qui déclenche selon la valeur de "submitted", tel qu'il change de sa valeur lorsqu'on clique un bouton.
Dans Effet, on met juste les __interaction generale__. Par exemple, synchroniser un état ``cities`` lorsque la prop ``country`` change.

#### B. Votre Effet a-t-il trop de responsabilités?
Soit un Effet, dans son corp on a des codes qui:
* Synchroniser l'état ``cities`` avec le réseau en fonction de la prop ``country``.
* Synchroniser l'état ``area`` avec le réseau en fonction de l'état ``city``.

Alors, __découpez ces comportements en deux Effets !__, tel que chacun ne réagissent qu'à la donnée qui les concerne.
Désormais, deux données différentes sont synchronisées par deux Effets différents, afin de ne pas se déclencher l'un l'autre par inadvertance.

#### C. Lisez-vous un état pour calculer le prochain?
Utiliser une __fonction de mise à jour__ plutôt que la valeur du state pour mettre à jour le state.
*   ````js
    useEffect(() => {
        ...
        setMessages([...messages, receivedMessage])
    }, [messages])
    ````
    Dans ce cas, chaque fois qu'on reçoit un nouveau message, ``setMessages`` crée un nouveau rendu du composant avec u nouveau tableau ``message``, par conséquent il va aussi resynchroniser l'Effet.

Pour le corriger, onpeut utiliser une fonction de mise à jour:
*   ````js
    useEffect(() => {
        ...
        setMessages(msgs => [...msgs, receivedMessage]);
    }, [])
    ````
    __L'Effet ne lit désormais plus du tout la variable ``message``.

#### D. Voulez-vous lire une valeur sans réagir à ses changements?
* __useEffectEvent__(Outil complémentaire de useEffect):
    C'est un nouveau hook __expérimentale__ introduite dans React pour résoudre certains problèmes liés aux dépendances de ``useEffect``.
    Son objectif est de fournir un moyen plus sûr et plus efficace d'utiliser des fonctions dans ``useEffect`` sans provoquer de re-render inutile ou de stale closures(fermeture obsolète).
    
    Avec ``useEffect``, on a parfois les problèmes comme:
    * __Des re-renders inutiles__ lorsque la fonction utilisée à l'intérieur du ``useEffect`` change à chaque render.
    * __Le problème des fermetures obsolètes(stale closure)__: Si une fonction utilise une valeur d'état ou une prop qui change, elle peut se retrouver "coincée" avec une ancienne version de cette valeur.
    
    Avec ``useEffectEvent``, React fournit un moyen de capturer les valeurs les plus récentes sans dépendre du tableau de dépendances.
    
    __Limitations__:
    * C'est une __API expérimentale__, donc elle n'est pas encore stable, et pourrait être modifiée dans les versions futures de React.
    * __Disponible uniquement dans les version de React 18+.__
    * Ne pas confondre avec ``useEvent``.

Si on veut juste lire les valeurs d'une variable sans réagir à ces changements, on peut déplacer cette partie non-réactive du code dans un Événement d'Effet.
*   ````js
    const [isMuted, setIsMuted] = useState(false);
    const onMessage = useEffectEvent(receivedMessage => { 
        setMessage(msgs => [...msgs, receivedMessage]);
        if(!isMuted) {
            playSound();
        }
    } );
    useEffect(() => {
        ...
        onMessage(recevedMessage);
    }, [])
    ````
    useEffectEvent permet de découper ton Effet en parties réactive et non réactives. C'est à dire qu'on a plus besoin d'ajouter ``isMuted`` comme dépendence si on le lit au sein d'un Événement d'Effet.

Le ``useEffectEvent`` marche aussi pour les props.

#### E. Une valeur réactive change-t-elle par accident?
Regarder le doc original.

