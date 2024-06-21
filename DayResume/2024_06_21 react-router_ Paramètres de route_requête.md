# React
#### I - Différence entre query et parameter dans React-router-dom
En ``react-router-dom``, il y a une distinction entre les paramètres de route(route parameters) et les paramètres de requête(query parameters). 
##### Paramètres de Route (Route parameters)
Les paramètres de route sont inclus dans le chemin de l'URL et définis par des segments dynamiques. Ils font partie de l'URL proprement dite et sont utilisés pour indiquer des ressources spécifiques ou des sous-sections de votre application.
Syntaxe : ``<Route path="/user/:userId/profile component={UserProfile} />"``
Accès: ``const { userId } = useParams()``

##### Paramètres de Requête (Query Parameters)
Les paramètres de requête sont inclus après un point d'interrogation ('?') dans l'URL et utilisés pour transmettre des données supplémentaires qui ne font pas partie de la hiérarchie de l'URL.
Syntaxe: ``/search?query=reacr&sort=asc``
Définition de la route : ``<Route path="/search" component={SearchResults} />``
Accès: 
````js
const location = useLocation();
queryParams = new URLSearchParams(location.search);

const query = queryParams.get('query');
const sort = queryParams.get('sort');
````

#### II - Avantages et désavantages de paramètres de route/requête
##### Paramètres de Route(Route Parameters)
Avantages
* Sémantique claire: Les paramètres de route sont utiles pour les routes RESTful et donnent une structure claire à l'URL.
* SEO Friendly: Les moteurs de recherche comprennent mieux les URL structurées, ce qui peut améliorer le référencement.
* Accès direct: Les paramètres de route sont directement accessibles via le hook ``useParams``,ce qui simplifie l'accès aux données dans les composants.

Desavantages
* Rigidité: Moins flexible pour des changements dynamiques dans la requêtes
* Complexité: Quand il y a plusieurs paramètres, ça devient compliqués.

##### Paramètres de requêtes
L'inverse des paramètres de route.


