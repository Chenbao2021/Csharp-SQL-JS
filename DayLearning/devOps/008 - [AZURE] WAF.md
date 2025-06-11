# I - Qu'est ce qu'un WAF dans Azure?
Le WAF(Web Application Firewall) est un pare-feu applicatif qui protège tes applications web contre des attaues malveillants.

## A. Pourquoi on parle de "backend" dans un WAF ou un Application Gateway?
Quand on crée un __Application Gateway__(Ou Front Door), on ne code rien dedans. C'est un __service de reverse proxy__.
* Un proxy est un intermédiaire entre toi(Le client) et Internet.
	Par exemple:
	* On est dans une entreprise.
	* On veut accéder à ``google.com``
	* Notre ordinateur ne va pas directement sur Internet, mais il envoie la requête à un __proxy__.
	* Le proxy va chercher le site, et te renvoie le résultat.
	
	Donc, un proxy permet de filtrer le trafic sortant/entrant, et appliquer des règles de sécurité, comme "interdire ChatGPT".
* Un reverse proxy fait l'inverse, c'est un intermédiaire entre Internet(Les utilisateurs) et ton application web. Par exemple:
	* Un utilisateur vet accéder à ton site ``monapp.com``
	* Le trafic arrive d'abord au reverse proxy.
	* Le reverse proxy filtre, sécurise, et redirige la requête vers ton vrai serveur.(Ton backend).


Donc, pour fonctionner ce service de reverse proxy, WAF doit rediriger les trafic HTTP/HTTPS vers quelque chose, et c'est la qu'intervient la notion de "backend pool".

## B. Backend pool.
Un backend = Une destination pour le trafic entrant, c'est à dire, l'endroit où ce code est déployé -> C'est l'infrastructure cible du trafic HTTP. Ce qui peut être:
* Une __App Service__ (Comme une API en C# hébergée sur Azure).
* Une VM(Machine virtuelle).
* Un Azure Kubernetes Service(AKS).
* Un autre Applicatio Gateway ou Load Balancer.
* ou même une __IP publique__

## C. Exemple concret litteraire.
* Un App Service dans Azure est un service PaaS(Platform as a Service) pour héberger une application web sans gérer de machine virtuelle.
	* Il a toujours un URL publique par défaut(_Oui, c'est public! Faut désactiver l'accès direct à l'App Service ou ajouter un Private Endpoint_), et une adresse IP selon les besoins.
	* On peut déployer des API en C#, Node.js, sites web front-end, ou des functions, etc.
	* Donc , App Service est un type de resource pour héberger des applications web.

Imaginons qu'on a une Application React + une API C# déployée dans un App Service.(``monapi.azurewebsites.net``).
Si on mets un Application Gateway avec WAF, on fait:
* WAF écoute en HTTPs sur ``https://monapp.contoso.com``
* Il fait des règles WAF(Protection OWASP).
* Puis il redirige vers ton App Service: C'est ça le backend.

Comme on peut se dire, les utilisateurs ne doivent jamais connaître l'URL ``monapi.azurewebsites.net``. Ils n'accèdent qu'à ``monapp.contoso.com``, qui passe __obligatoirement par le WAF__. C'est ça la vraie protection.

### Mais s'il y a plusieurs sites/applications dans le backend?
Par exemple:
* Front-end React dans App Service 1: ``monfrontend.azurewebsites.net``
* API C# .NET: ``monapi.azurewebsites.net``

Mais les utilisateurs __accèdent uniquement via le WAF__: ``monapp.contoso.com``

Grâce aux rules de routages dans le __WAF/Application Gateway__. On peut router __par chemin__(path-based routing) ou par domaine(Si multi-sous-domaines):
````js
https://monapp.contoso.com/       → redirigé vers monfrontend.azurewebsites.net
https://monapp.contoso.com/api/*  → redirigé vers monapi.azurewebsites.net
````


## D. Comment configurer un application gateway en Terraform ?
Dans Application Gateway:
1. Listener écoute en HTTPS sur ``monapp.contoso.com``
2. On définis des __Rules__:
	* ``/api/*``: Backend Pool: API App Service.
	* ``/*``: Backend Pool: Front React App Service.
3. __WAF__ inspecte chaque requête(OWASP, signature XSS, etc.)
4. L'App Service n'accepte que le trafic du WAF(Grâce aux Access Restrictions)

Mais qu'est ce que Listener? Rule? OWASP?
### Qu'est ce qu'un Listener?
