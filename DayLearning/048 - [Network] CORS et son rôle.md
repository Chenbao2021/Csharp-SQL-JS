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

Grâce au 5 étapes ci-dessus, on peut se rendre compte que le risque de sécurité n'est pas uniquement le fait qu'un site externe puisse faire une requête... __Mais surtout que le navigateur(comme Chrome) peut automatiquement ajouter les cookies de session dans le header ``Cookie`` si on ne met aucune protection__.

# III - Cookies dans CORS
### 1. Les cookies sont associés à un domaine spécifique
Quand un serveur (comme banque.com) envoie un cookie:
````http
Set-Cookie: session=abcd1234; Domain=banque.com
````
-> Le navigateur __stocke ce cookie__ et le lie à __banque.com__. C'est à dire lorsque __banque.com__ est le destinataire d'une requête.

### 2. Quand le navigateur envoie-t-il ce cookie?
Uniquement si la requête va vers banque.com, et:
* Si le domaine correspond(``banque.com``).
* Si le chemin(``path``) correspond.
* Si la requête est faite:
	* Par une page de banque.com (même origin)
	* Ou __par un script sur un autre domaine avec ``credentials: "include"`` Et si le serveur a autorisé ça via CORS__.

### 3. Exemple qui montre ce qu'il se passe vraiment:
1. Un utilisateur est connecté sur __banque.com__:
	* Il a un __cookie de session__: ``session= abcd1234``.
	* Ce cookie est stocké __dans son navigateur__, et lié à ``banque.com``.
2. Il va ensuite sur __pirate.com__, un site malveillant.
3. Le site pirate exécute ce JS:
	````js
	fetch("https://banque.com/api/vitement", {
		method: "POST",
		credentials: "include",
		body: JSON.stringify({ montant: 9999, vers: "compte-du-pirate" }),
		headers: {
			"Content-Type": "application/json"
		}
	})
	````
4. Le navigateur lit ça et dit:
	Ok, je fais une requête vers banque.com.
	Tu as dit ``credentials: "include"`` -> Je dois __ajouter les cookies de banque.com__ à la requête (Car ce sont MES cookies, pour banque.com).
5. Résultat: La requête est __envoyée avec le cookie de session__ de l'utilisateur, sans que lui le sache.


Cette attaque a un nom: CSRF [Cross-Site Request Forgery]

# IV - CORS n'est pas une sécurité du serveur.
Même si le navigateur bloque la réponse, le serveur a déjà exécuté l'action !
* Il faut ajouter __CSRF token__. (À regarder plus tard).
* Ou ``SameSite = Strict``: Le navigateur n'enverra jamais les cookies dans une requête cross-site, même avec ``credentials: "include"``.
	````js
	res.cookie("session", "abcd123", {
		httpOnly: true,
		sameSite: "Strict",
		secure: false
	})
	````
* Ou vérifier l'origine de la requête.
	````js
	const origin = req.get("origin")
	if( origin !== "http://localhost:4000") {
		return res.status(403).send("Origine non autorisée");
	}
	````







