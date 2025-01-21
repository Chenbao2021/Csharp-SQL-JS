## I. Nature des Promesses dans JavaScript:
* Une __Promesse__ est un objet représentant une opération asynchrone.
* Lorsqu'une promesse est créée, elle s'exécute immédiatement et retourne un objet ``Promise``. Ce n'est que lorsque la promesse est __résolue__ ou __rejetée__ que le code inscrit dans ``.then()`` ou ``.catch()`` est exécuté.
* Si vous imbriquez des appels asynchrones dans des ``.then()`` sans attendre leur résolution, le __contexte d'appel principal__ continue son exécution sans se soucier de ce qui se passe dans les sous-promesses.

## II. Pourquoi ``refreshAllReport`` ne bloquait pas?
Quand on utilise plusieurs appels avec ``.then()``, on ne doit pas oublier que ces ``.then()`` sont eux aussi des promesses, et qu'on doit assurer qu'ils sont correctement reliées au cycle principale.
* Exemple des ``.then()`` qui ne sont pas reliées au cycle principale.
    ````js
    const allPromise = keysStr.map(async (id) => {
        if(id !== idReport) {
            reportDataService.getHeader(+(id ?? 0), ...)
                .then(() => {
                    // Sous-promesse non attendue
                    ...
                    reportDataService.getChapterList(...)
                        .then((result) => {
                            // Encore une sous-promesse non propagée!
                        });
                }
        }
    }
    await Promise.all(allPromise);// Attente partielle, pas de propagation.
    ````
    Dans ce cas:
    * ``await Promise.all(allPromise)`` ne garantit pas que les __sous-promesses imbriquées__ sont terminées, car elles sont définies dans des ``.then()`` qui ne sont pas explicitement chaînés avec ``await``.
    * Chaque ``then`` retourne une nouvelle promesse, donc rassureer que tous les then retourne une promesse.

## Solution et Pourquoi Elle Fonctionne.
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

#### 2. Différence entre ``return`` et ``await``
1. Avec ``await`` dans le ``map``:Quand on doit effectuer des opérations après la résolution de chaque promesse.
    * L'exécution du code dans chaque itération attent que la promesse retournée par ``reportDataService.getQuestionList`` soit résolue avant de continuer à l'étape suivante.
    * Comme map récoit une fonction ``async``, du coup chaque objet de la liste ChapList renvoie une promesse __indépendante__ et ``Promise.all`` attend la résolution de toutes ces promesses.

2. Avec ``return`` dans le ``map`` (sans ``await``): Quand on ne doit pas effectuer d'opérations supplémentaires après la résolution de la promesse.
    * Lorsque vous faire simplement un ``return ...``, la promesse retournée par ``getquestionList`` est ajoutée directement dans le tableau généré par ``.map``
    * On ne fait plus d'opérations supplémentaire après la résolution de la promesse, comme ``addItem``.

## Problème avec ``Promise.all``
``Promise.all`` attend un tableau plat contenant des promesses, et non des tableaux imbriqués. 
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
* 1) Chaque fonction de rappel retourne implicitement une promesse, car elle est marquée comme ``async``. Et chaque promesse dans la fonction de rappel sont précédée d'un await qui les gèrent. Cela est suffisante pour que ``Promise.all`` fonctionne correctement.
    ````js
    const promises = array.map(async (item) => {
        await new Promise((resolve) => setTimeout(resolve, 1000));
        console.log(item);
    });
    
    await Promise.all(promises); // Attendre la résolution de toutes les promesses
    ````
* 2) Absence de retour de la promesse: Promise.all se résolvent immédiatement car la promesse n'est pas retourné.
    ````
    const promises = array.map(async (item) => {
        new Promise((resolve) => setTimeout(resolve, 1000)); // La promesse est créée mais pas retournée
        console.log(item); // Cette ligne est exécutée immédiatement
    });
    ````
    


