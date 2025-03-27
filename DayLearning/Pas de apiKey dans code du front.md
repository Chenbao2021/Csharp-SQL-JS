# I - Fichier ``.env``.
C'est un fichier super utile et __très courant__ dans les projets React, Node.js, etc.
### Qu'est-ce qu'un fichier ``.env`` ?
Un fichier ``.env``(abréviation de environment) sert à stocker des variables d'environnement, c'est-à-dire __des informations qu'on ne veut pas coder en dur__ dans notre code source.

Exemple typeque:
````ts
VITE_FIREBASE_API_KEY=AIzaSyD...
VITE_FIREBASE_AUTH_DOMAIN=mon-projet.firebaseapp.com
VITE_BACKEND_URL=https://mon-api.com
````
* Ces variables peuvent ensuite être utilisées dans ton code comme des __valeurs dynamiques__ selon l'environnement(dev, prod, etc.)

### Pourquoi c'est utile?
1. __Séparer la configuration du code__.
Tu changes l'URL d'une API ou une clé API sans modifier ton code, juste le fichier ``.env``.

2. __Changer selon l'environnement__.
Tu peux avoir plusieurs fichiers:
	* ``.env``: Variables communes à tous les moodes.
	* ``.env.development``: Variables spécifiques au mode dev.
	* ``.env.production``: Pour la production.
	* ``env.local``, ``env.staging``, etc.

Et vte s'occupe de charger tous les variables selon l'environnement, et puis tu peux les accéder tous par ``import.meta.env.VITE_``.

3. __Ne pas versionner certaines infos__.
Tu peux ajouter ``.env`` à ton ``.gitignore`` si tu ne veux pas que certaines infos soient poussées sur Github.

### Comment utiliser un ``.env`` dans un projet Vite + React?
1. On crée notre fichier ``.env`` à la racine de notre projet.
2. On ajoute des variables __avec le préfixe ``VITE_``(Important pour que Vite les expose au front-end).
	````js
	VITE_FIREBASE_API_KEY=AIzaSyD...
	VITE_API_URL=https://api.mon-site.com
	````
	* "exposer au front-end" veut dire, au moment de la compilation, Vite va lire les fichiers ``.end`` et va remplacer dans le code toutes les exprissions comme ``import.meta.env.Vite_`` par leur valeur réelle. Sinon ils seront disponible que pour Back-end(Node.js).
3. Tu y accède dans ton code comme ça:
	````js
	const apiKey = import.meta.env.VITE_FIREBASE_API_KEY
	const apiUrl = import.meta.env.VITE_API_URL
	````
	* ``import.meta.env``: C'est une interface spéciale fournie par certains outils de build modernes comme Vite. C'est une manière __standardisée et sécurisée__ d'accéder à des variables d'environnement dans une application __JavaScript/TypeScript__ côté client(Navigateur).
	* Par contre, dans un outils autre que Vite, comme webpack ou Node.js, on utlise ``process.env`` pour accéder à ces valeurs.

4. ``.env`` n'est pas un coffre-fort! __Tout ce qui est accessible dans le front peut être vu par les utilisateurs__(Dans le navigateur).

### Exemple complet
``.env``:
````js
VITE_FIREBASE_API_KEY = AIzaSy...
VITE_FIREBASE_PROJECT_ID = mon-project-id.
````

``.firebase.ts``:
````js
const firebaseConfig = {
	apiKey: import.meta.env.Vite_FIREBASE_API_KEY,
	projectId: import.meta.env.VITE_FIREBASE_PROJECT_ID
}
````

# II - ``apiKey`` Firebase.
Faut-il exposer notre ``apiKey`` Firebase dans le côté Front-end?
C'est une préocuppation très courante quand on utilise Firebase dans une app React(ou toute app front-end).

### Est-ce une bonne idée de mettre la ``apiKey`` Firebase côté front?
Oui, c'est normal et prévu pour.
Firebase est conçu pour être utilisé __dans des apps front_end__, donc:
* Les clés API Firebase ne sont pas des __secrets__.

### Ce que ça signifie vraiment:
* La ``apiKey`` Firebase __ne donne pas un accès illimité à ton projet__.
* Elle est utilisé uniquement pour identifier ton projet Firebase quand tu fais appel aux services publics(Comme Authentification, Firestore, etc.)
* Les règles de sécurité sont gérées côté Firebase(ex: Firestore Security Rules, Auth Rules, etc.)

Donc, ce n'est pas ta ``apiKey`` qui protège tes données, mais __les règles de sécurité dans la console Firebase__.

### Exemple typique dans une app React + Firebase:
````js
// firebaseConfig.ts
import { initializeApp } from 'firebase/app'

const firebaseConfig = {
  apiKey: "AIza...xyz",
  authDomain: "mon-projet.firebaseapp.com",
  projectId: "mon-projet",
  storageBucket: "mon-projet.appspot.com",
  messagingSenderId: "1234567890",
  appId: "1:1234567890:web:abcdef123456"
}

export const app = initializeApp(firebaseConfig)
````

Et c'es totalement OK que ces infos soient visibles dans le code.

### Ce qu'il ne faut jamais faire:
* Mettre une __clé d'admin Firebase dans ton front__ (Comme celle d'un service account).
* Mettre des infos sensibles(Comme un mdp, une API secrète vers un autre service!)

__Tous ceux que t'utilise dans le côté Front-end est visible par tous!__

### Ce qu'il faut faire:
1. Mettre ta ``apiKey`` et autres infos Firebase dans un fichier ``.env``(Juste pour la maintenabilité, pas pour la sécurité).
2. Configurer des règles de sécurité solides dans Firebase:
	* Exemple: Seulement les utilisateurs authentifiés peuvent lire/écrire, vérification de ``request.auth.uid``, etc.

# III - Créer un back-end minimal pour échanger ``apiKey``.
On ne mets jamais notre ``apiKey`` dans les codes de front-end, sauf cas spécial comme Firebase.
Car elle sera visible __dans le navigateur__, donc __toute personne pourrait l'utiliser gratuitement à tes frais__.

### 1. Créer un petit backend(Même minimal)
Tu peux créer un __très simple__ pour faire relais entre ton front et OpenAI.
Exemple: 
* Vercel/ Netlify serveless functions.
* Cloud Functions for Firebase(Tu les as déjà avec Firebase !).
* Cloudflare Workers.

Exemple:
````js
exports.askChatGPT = functions.https.onCall(async (data, context) => {
  const prompt = data.prompt
  const response = await axios.post("https://api.openai.com/v1/chat/completions", {
    model: "gpt-3.5-turbo",
    messages: [{ role: "user", content: prompt }]
  }, {
    headers: {
      Authorization: `Bearer ${functions.config().openai.key}`
    }
  })

  return response.data
})
````
* ``export.askChatGpt``: On exporte une fonction nommée ``askChatGPT`` pour que Firebase puisse la reconnaître.
* ``functions.https.onCall(...)``: C'est un type spécial de fonction Firebase qu'on peut appeler directement depuis le front-end via ``firebase.functions().httpsCallable('askChatGPT)``.
* ``data``: Ce sont les données envoyées par le front(ex: ``{promp: "Bonjour"}``)
* ``context``: Contient les infos sur l'utilisateur connecté(auth), utile si tu veux vérifier qu'il est loggé.
* ``return response.data``: On renvoie la réponse d'OpenAI directement au front.

