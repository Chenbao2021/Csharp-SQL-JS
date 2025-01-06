Dans ce premièr leçon, vous allez apprendre:
* Comment utiliser le canvas pour dessiner des formes dynamiques
* Comment gérer les interactions utilisateurs(souris)
* Comment limiter la vitesse des animations pour un rendu contrôlé.
* Comment organiser des animations complexes dans une seule boucle.

***
#### 1. Comprendre le canvas et dessiner un damier
__Qu'est-ce que le canvas ?__
Le ``canvas`` est un élément HTML5 qui permet de dessiner des graphiques dynamiques directement dans le navigateur. Avec JavaScript, on peut contrôler chaque pixel pour créer des dessins, animations ou interactions.

__Avec un exemple pratique__
Créer un damier 8*8 où chaque case a une couleur aléatoire.
1. __Initialiser le canvas__: On récupère le contexte du canvas pour dessiner dessus.
    ````js
    const canvas = canvasRef.current;
    const ctx = canvas.getContext("2d");
    ````
2. __Dessiner des rectangles__: Chaque case du damier est un rectangle. La méthode ``fillRect`` permet de dessiner un rectangle coloré.
    ````js
    ctx.fillStyle = 'red';
    ctx.fillRect(x, y, width, height);
    ````
3. __Ajouter des couleurs aléatoires__: Une fonction génère des couleurs au format hexadécimal.
    ````js
    function randomColor() {
        return `#${Matj.floor(Math.random() * 16777215).toString(16)}`;
    }
    ````
4. __Dessiner tout le damier__: On utilise deux boucles ``for`` pour parcourir chaque case.
    ````js
    for (let i = 0; i < 8; i++) {
        for (let j = 0; j < 8; j++) {
            ctx.fillStyle = randomColor();
            ctx.fillRect(i * cellWidth, j * cellHeight, cellWidth, cellHeight);
        }
    }
    ````
__Concept clés__
* __Canvas et contexte(ctx)__: Le contexte est l'outil principal pour dessiner sur le canvas.
* ``fillStyle`` et ``fillRect``: Permettent de remplir des formes avec des couleurs.



#### 2. Ajouter des interactions: Transformer une case en noir au survol.
__Objectif__
Quand la souris passe au-dessus d'une case, celle-ci devient noire.
__Étapes à suivre__
1. __Détecter le mouvement de la souris :__ On utilise l'événement ``mousemove`` pour savoir où se trouve la souris.
    ``canvas.addEventListener('mouvemove', handleMouseMove)``
2. __Calculer la case survolée__: À partir des coordonnées de la souris, on détremine quelle case est survolée. On utilise la largeur de la hauteur des cases.
    ````js
    const hoveredCol = Math.floor(event.offsetX / cellWidth);
    const hoveredRow = Math.floor(event.offsetY / cellHeight);
    ````
#### 3. __Mémoriser les cases noires__: Pour éviter que les cases noires disparaissent, il est essentiel de mémoriser l'état du damier, y comprit les cases noires avec un eventLister sur mousemove.
    ````js
    // Initialisation
    canvas.addEventListener('mousemove', handleMouseMove);
    animate();
    return () => {
        canvas.removeEventListener('mousemove', handleMouseMove);
    };
    ````
#### 4. __Faire disparaître les cases noires progressivement(FIFO)__: Les cases noires ne restent pas éternellement; elle disparaissent progressivement dans l'ordre où elles ont été survolées.
    1. Créer une file d'attente.
        ````js
        const blackQueue = [];
        ````
    2. Ajouter des cases à la file.
        ````js
        blackQueue.push({ col: hoveredCol, row: hoveredRow });
        ````
    3. Limiter la taille de fille.
        ````js
        if(blackQueue.length > maxBlackCells) {
            const oldCell = blackQueue.shift();
            colors[oldCell.row][oldCell.col] = randomColor();
        }
        ````
#### 5. __Limiter la vitesse des animations__: Certaines animations, comme un effet d'éclair('lightning'), doivent être ralenties pour ne pas se déclencher à chaque frame.
    1. Utiliser ``deltaTime`` : ``requestAnimationFrame`` fournit un timestamp(``currentTime``) pour chaque frame. On utilise ce timestamp pour calculer le temps écoulé entre deux frames.(En effet, ``requestAnimationFrame`` fournit par défaut un argument "currentTime" pour la fonction passé en tant qu'argument)
    ``const deltaTime = currentTime - lastRenderTime``

    2. Condition pour ralentir l'animation: On vérifie si un certain intervalle de temps s'est écoulé avant d'exécuter l'animation:
    ````js
    if (deltaTime > 500) {
        generateRandomLightning();
        lastRenderTime = currentTime;
    }
    ````
    
#### 6. __Combiner toutes les animations avec une fonction centrale__
    1. __Créer une fonction ``animate``__: Cette fonction est appelée à chaque frame par ``requestAnimationFrame``.
    L'ordre est très important, celle qui dessine après vont superposer sur celle qui a été dessiné en première.
    ````js
    function animate(currentTime) {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        drawDamier();
        drawBlackCells();
        if(deltaTime > 500) {
            generateRandomLightning();
            lastRenderTime = currentTime;
        }
        requestAnimationFrame(animate);
    }
    ````
    2. __Séparer les responsabilités__: Chaque fonction(dessin du damier, gestion des cases noires, effet d'éclair) est appelée indépendamment dans ``animate``.
    3. __Lancer l'animation :__
    ``requestAnimationFrame(animate)``
