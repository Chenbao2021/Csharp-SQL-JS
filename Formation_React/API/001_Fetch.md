## I - Introduction
* Fetch = récupérer
* __'fetch'__ = Le nom de l'API native JavasSript pour effectuer des requêtes HTTP asynchrones, et remplace progressivement l'objet __'XMLHttpRequest'__ précédemment utilisé pour les requêtes HTTP.

Exemple simple d'utilisation de fetch pour un appel GET :
````
import React, { useEffect } from 'react';

function App() {
  useEffect(() => {
    fetch('https://api.exemple.com/data')
      .then(response => {
        ...
        return response.json();
      })
      .then(data => console.log(data))
      .catch(error => console.error('There was a problem with your fetch operation:', error));
  }, []);

  return <div>Regardez la console pour les données.</div>;
}
````
* __.then()__ : Attache une fonction de callback qui est exécutée lorsque la promesse est résolue. Elle peut aussi chaîner une autre promesse.
* __.catch()__ : Attache une fonction de callback qui est exécutée en cas d’erreur.

## II - Async/Await
* __async__ : Utilise pour déclarer une fonction comme asynchrone. Cela signifie que la fonction retournera toujours une promesse, sinon le retour sera emballé dans une promesse.
* __await__ : utilisé à l'intérieur d'une fonction 'async' pour attendre la résolution d'une promesse. Il suspend l'exécution de la fonction  __async__ jusqu'à ce que la promesse soit résolue ou rejetée.

Exemple: 
````
async function fetchData() {
  const response = await fetch('https://api.example.com/data');
  const data = await response.json();
  return data;
}
````
* le data retourné par la fonction fetchData est effectivement emballé dans une promesse, Même si data est le résultat de "await response.json()" . 
* Ainsi lorsque vous appelez fetchData(), vous __devez traiter le résultat comme une promesse__:
    * En utilisant await (__Dans une fonction async__) 
    * Ou les méthodes .then() et .catch()  


## III - Méthodes HTTP de base
Les méthodes HTTP définissent le type d'action que vous souhaitez exécuter sur une ressource :
* GET : Récupère des données. (Je viens récupérer mon colis)
* POST : Envoie des données pour créer une nouvelle ressource. (J'ai une nouvelle commande)
* PUT : Met à jour une ressource existante. (J'ai un erreur dans la commande, et je dois la modifier)
* DELETE : Supprime une ressource. (Finalement je ne la veux plus)

## IV - Manipulation des headers HTTP
#### Pourquoi manipuler les headers HTTP ?
Les headers HTTP permettent d'envoyer des informations supplémentaires avec les requêtes et les réponses. Par exemple:
* Indiquer le type de contenu attendu
* L'encodage des caractères
* Les informations d'autorisation
* Et bien d'autres aspects qui influencent la manière dont les données sont transmises et interprétées.

#### Problèmes de CORS (Cross-Origin Resource Sharing)
CORS est une politique de sécurité qui restreint les ressources HTTP demandées depuis un domaine différent de celui du serveur. Si les headers CORS ne sont pas correctement configurés, __les navigateurs bloquent__ les requêtes cross-origin.

__Lorsqu'une requête est faite à un domaine différent, le navigateur envoie automatiquement une requête préliminaire appelée "preflight" avant la requête principale__. Cette requête preflight utilise la méthode HTTP OPTIONS pour vérifier quelles sont les requêtes cross-origin autorisées par le serveur. Le serveur répond avec des __headers appropriés__ qui indiquent si la requête originale est permise.

Ces headers permettent au navigateur de savoir si la requête principale doit être autorisée à continuer

Exemple simple d'un header : __Access-Control-Allow-Origin: https://example.com__

Exemple complet de la configuration d'un serveur:
````
const express = require('express');
const cors = require('cors');
const app = express();

// Configuration CORS simple pour tous les domaines
app.use(cors());

// Configuration CORS plus spécifique
app.use(cors({
  origin: 'https://domaineA.com', // Autorise uniquement domaineA
  methods: ['GET', 'POST'], // Méthodes autorisées
  allowedHeaders: ['Content-Type', 'Authorization'],
  credentials: true
}));

app.get('/data', (req, res) => {
  res.json({ message: 'Ceci est une réponse protégée par CORS' });
});

app.listen(3000, () => console.log('Serveur démarré sur le port 3000'));
````

#### Petit plus: Back-end
CORS est destinées à la sécurité du navigateur pour protéger les utilisateurs des effets malveillants des __scripts de sites croisés__. Or les serveurs ne sont pas affectés par ce type de menace directement car ils ne sont pas sujets à des __actions initiées par l'utilisateur__ de la même manière que les navigateurs.
Voici un exemple de requête POST:
````
const fetch = require('node-fetch');
async function postExample() {
  try {
    const response = await fetch('https://jsonplaceholder.typicode.com/posts', {
      method: 'POST',
      body: JSON.stringify({
        title: 'foo',
        body: 'bar',
        userId: 1
      }),
      headers: { 'Content-Type': 'application/json' }
    });
    const data = await response.json();
    console.log(data);
  } catch (error) {
    console.error('Failed to post:', error);
  }
}
postExample();
````