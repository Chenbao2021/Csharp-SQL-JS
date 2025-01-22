## I. Nature des Promesses dans JavaScript:
* Une __Promesse__ est un objet représentant une opération asynchrone.
* Lorsqu'une promesse est créée, elle s'exécute immédiatement et retourne un objet ``Promise``. Donc le fait d'ajouter un ``await`` dans async n'empêche pas le retour de l'objet ``Promise``. 
* Si vous imbriquez des appels asynchrones dans des ``.then()`` sans attendre leur résolution, le __contexte d'appel principal__ continue son exécution sans se soucier de ce qui se passe dans les sous-promesses.  
    Par exemple:
    * ````js
        onst promises = [1, 2, 3].map((id) => {
            return asyncOperation(id).then((result) => {
                console.log(`Première étape pour ${result}`);
                // Sous-promesse non retournée
                asyncOperation(result + 10).then((res) => {
                    console.log(`Sous-promesse pour ${res}`);
                });
            });
        });
        ````
        Ici, la sous-promesse ``asyncOperation()`` n'est pas retourné! donc elle ne sera pas incluse dans la chaîne de promesses principale. le __contexte d'appel principal__ sera terminé une fois que les codes terminé d'exécuter. 
        * Solution: Ajouter un ``return`` devant ``asyncOperation(result + 10)``
* Lors de l'exécution d'un programme, lorsqu'une promesse est rencontrée:
    1. Le programme(Déclenché par thread principal, mais selon les cas, les traitements peuvent être délégué) commence imméiatement l'opération asynchrone associée à la promesse.
    2. Le programme principal __ne s'arrête pas__ et continue d'exécuter les instructions suivantes.
    3. Une fois que la promesse est résolue ou rejetée, le gestionnaire associé(``.then``, ``.catch``) ou la partie ``await`` est __mis dans la file d'attent des microtâches__.
    4. Ce gestionnaire ou cette partie ``await`` est exécuté uniquement après que toutes les tâches synchrones en cours soient terminées.

    Par exemple:
    * ````js
        const main = async () => {
            const promise = new Promise((resolve) => {
                resolve("Résolu");
            });
            promise.then((result) => {
                console.log("Première étape :", result);
        
                // Sous-promesse non retournée
                new Promise((resolve) => {
                    setTimeout(() => {
                        console.log("Sous-promesse terminée");
                        resolve();
                    }, 1000);
                });
            });
    
            console.log("Programme principal terminé");
        };
        main();
        ````
        * Ici, le premier qui va afficher sera "Programme principal terminé", le programme principal considère que tout est terminé alors que la sous-promesse est encore en cours!
        * Solutions possibles: ``return`` + Enchaîner des ``.then``, utiliser ``return``+``await``, ``Promise.all``, etc.

## II. Solutions
#### 1. Utiliser ``async/await`` au lieu de ``.then()``
En réécrivant le code avec ``async/await``, chaque sous-promesse est explicitement __attendue__ avant de continuer. Cela crée un __flux linéaire__ où chaque étape dépend de la résolution de la précédente.
* Exemple
    ````js
    const response = await reportDataService.getHeader(...)
    addItem(response);
    const chapterResult = await reportDataService.getChapterList(...)
    addItem(chapterResult);
    const waitChapList = ChapList.map(async (chap) => {
        const result = await reportDataService.getQuestionList(...);
        addItem(result);
    }
    await Promise.all(waitChapList);
    ````
    * Ici, Aucune étape ne commence avant la résolution de la précédente.
    * Dans des situations similiaires, utiliser async/await rendre les codes  plus lisible et maintenable

#### 2. Le rôle de ``return`` et ``await`` dans une fonction ``async``
1. Das une fonction ``async``, même s'il n'y a pas de retour explicite, ``undefined`` sera retourné à la fin d'exécution du programme principale.
2. ``await`` : Quand on doit effectuer des opérations après la résolution d'une promesse.
    * Atteindre la résolution de la promesse pour lancer les codes suivants, souvent parce que les codes suivants nécessitent le résultat de la promesse.
3. ``return``: Quand on veut juste la résolution de la promesse, on peut la retourner pour que d'autre partie de la programme la gère.(Par exemple, "Promise.all")
    * On ne fait plus d'opérations supplémentaire après la résolution de la promesse.

## Problème avec ``Promise.all``
``Promise.all`` attend un tableau plat contenant des promesses, et __non des tableaux imbriqués__ !!! 
Avec un tableau imbriqué des promesses: Certaines promesses restent non résolues, ou l'exécution se termine prématurément.

* Code avec d'erreur:
    ````js
    ...
    const questionListPromises = data.questionList.map((question) => {
        const promises: Promise<any>[] = [];
        if (setToCheckIfQuestionModified.has(String(question.idReportElem))) {
            const uploadPromisesAttachedFiles = question?.attachedFiles?.map(async (file) => {
                const fileData = keysOffline.find((key) => key.id == file.idFile);
                if (fileData) {
                    const result = await uploadFileLight(...);
                    return reportDataService.putFile(...);
                }
            });
    
            if (uploadPromisesAttachedFiles) {
                promises.push(Promise.all(uploadPromisesAttachedFiles));
            }
            return promises;
        }
        return true;
    });
    ....
    `````
* La version corrigée:
    ````js
    const questionListPromises = data.questionList.flatMap((question) => {
        if (setToCheckIfQuestionModified.has(String(question.idReportElem))) {
            return question?.attachedFiles?.map(async (file) => {
                const fileData = keysOffline.find((key) => key.id == file.idFile);
                if (fileData) {
                    const result = await uploadFileLight(...);
                    return reportDataService.putFile(...);
                }
            });
        }
        return []; // Retourne un tableau vide si aucune promesse n'est à ajouter
    });
    ````

## Retour des promesses dans ``map(async ...)``
Si une promesse asynchrone n'est pas explicitement retournée dans un ``map(async ...)``,  elle ne sera pas prise en compte dans le tableau final. Donc utilisez bien les ``await``, ou retournes le.
Exemples:
1. Chaque fonction de rappel retourne implicitement une promesse, car elle est marquée comme ``async``. Et chaque promesse dans la fonction de rappel sont précédée d'un await qui les gèrent. Cela est suffisante pour que ``Promise.all`` fonctionne correctement.
    ````js
    const promises = array.map(async (item) => {
        await new Promise((resolve) => setTimeout(resolve, 1000));
        console.log(item);
    });
    
    await Promise.all(promises); // Attendre la résolution de toutes les promesses
    ````
2. Absence de retour de la promesse: Promise.all se résolvent immédiatement car la promesse n'est pas retourné.
    ````
    const promises = array.map(async (item) => {
        new Promise((resolve) => setTimeout(resolve, 1000)); // La promesse est créée mais pas retournée
        console.log(item); // Cette ligne est exécutée immédiatement
    });
    ````
    


