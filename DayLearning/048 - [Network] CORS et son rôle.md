# I - Rappels du réseau
### 1. Client/ Serveur: Le modèle fondamental du Web.
Client:
* C'est ton navigateur(chrome, firefox, etc)
* Il envoie des requêtes au serveur pour obtenir des ressources(pages HTML, fichiers js/css, images, etc.)

Serveur:
* C'est la machine à distance qui répond à ces requêtes.
* Il peut envoyer une page HTML complète, ou juste une réponse JSON(API REST)
### 2. Requête HTTP: Aller-retour entre client et serveur.
Une requête HTTP se compose:
* Méthode: ``GET``, ``POST``, ``PUT``, ``DELETE`` ...
* URL: L'adresse de la ressource demandée.
* Headers: Infos supplémentaires(Comme le type de contenu souhaité)
* (optionnel) Corps/ Body: Pour envoyer des données(ex: ``POST`` d'une information).

### 3. Protocoles essentiels.
HTTP/ HTTPS: 
* HTTP = HyperText Transfert Protocol: Langage de communication entre client et serveur.
* HTTPS = HTTP Sécurisé, via TLS(chiffrement). Permet de protéger les données.

TCP/IP:
* TCP = Transmission Control Protocol (Garantit que les paquets arrivent dans l'ordre).
* IP = Internet Protocol(adresse du serveur).
* Pour toi: Retient juste que TCP/IP est la base qui permet à HTTP de fonctionner.

DNS:
* Le Domain Name System transforme un __nom de domaine__(ex: google.com) en adresse IP.
* C'est comme un annuaire du web.
* Sans DNS? ton navigateur ne saurait pas où aller.

### 4. Cycle de vie d'une requête web(en résumé simplifié)
Quand tu tapes ``https://mon-site.com`` dans chrome:
1. __DNS lookup__: Trouve l'adresse IP de ``mon-site.com``.
2. __TCP Handshake__: Le client et le serveur établissent une connexion fiable.
3. __TLS Handhsake__: Si HTTPS, chiffrement des échanges.
4. __HTTP Request__: Le navigateur envoie une requête GET.
5. __HTTP Response__: Le serveur répond avec le contenu (HTML/ CSS/ JS/ images)
6. __Rendu__: Le navigateur interprète le HTML, télécharge les ressources, exécute le JS.

# II - Sécurité & CORS
### 1. Origine et même origine(Same-Origin Policy)
La Same-Origin Policy est une règle de sécurité du navigateur qui empêche le JS de faire des requête vers un __autre domaine__ que celui d'où vient la page.
* Une "origine" = ``protocole + domaine + port``.

	Par exemple:
	* Page: ``https://example.com/page.html``
	* API: ``https://example.com/api/data``

C'est pour protéger les utilisateurs, et pas pour embêter les développeurs, même si on a souvent cette imression XD.

### 2. Pourquoi les erreurs CORS arrivent?
Quand tu fais une requête JS vers un domaine différent: ```fetch("https://...")``, le navigateur bloque automatiquement la réponse si le serveur ne permet pas cet accès avec l'erreur type:
* ````js
	Access to fetch at 'https://api.autre-site.com/users'
	from origin 'https://mon-site.com' has been blocked by CORS policy
	````

Pour éviter ces erreurs, le serveur doit répondre avec des headers CORS spéciaux:
````
Access-control-Allow-Origin: https://mon-site.com
Access-Control-Allow-Methods: GET/ POST/ PUT/ DELETE
Access-Control-Allow-Headers: Authorizaion, content-Type, etc.
````

### 3. Exemple d'attaque via Non CORS
Imagine ce scénario(Sans protection CORS):
1. Tu es connecté à ton compte __banque.com__. (Par défaut, le navigateur gère les cookies de session pour les requêtes vers le même domaine dans le ``header``).
2. Tu vas sur __site-pirate.com__.
3. Ce site contient du JS comme:
	````js
	fetch("https://banque.com/api/virement", {
		method: "POST",
		credentials: "include",
		body: JSON.stringify({ montant: 1000, vers: "compte-du-pirate" })
	})
	````
	* C'est à dire on appelle l'api d'une banque depuis un site arnaque avec les cookies généré par ta connexion).
4. Le navigateur envoie la requête avec ton cookie de session bancaire.
5. Le site pirate vole ton argent.

	Sans CORS, le JS de n'importe quel site pourrait faire des requêtes vers d'autres sites en se faisant passer pour toi.
	Grâce à CORS, il bloque automatiquement toute requête JS vers un autre domaine, sauf si le serveur dit:
	* Pas de souci, ce domaine est autorisé.

Grâce au 5 étapes ci-dessus, on peut se rendre compte que le risque de sécurité n'est pas uniquement le fait qu'un site externe puisse faire une requête... Mais surtout que le navigateur(comme Chrome) peut automatiquement ajouter les cookies de session dans le header ``Cookie`` si on ne met aucune protection.





