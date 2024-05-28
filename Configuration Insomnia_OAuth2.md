### I - Configuration de Service
Si lors de lancement de service, la service n'arrive pas à se lancer, c'est parceque le port n'est pas autorisé à utiliser, tu dois lancer ce code en administrateur dans un terminal:
```c
netsh http add urlacl url=http://+:15308/vetting user=cyu
```
* Remplacer ``cyu`` par le nom que t'utilise pour connecter Medissys dans VDI.
* Remplacer ``15308`` par le port que tu vois dans les messages d'erreurs de Service launcher.
    Exemple  : ``"DTS\CYU - GET - http://localhost:15308/vetting/Activity -> 200 - OK"``

Une fois c'est fait, la service se lance, et un page de swagger s'est ouvert.

### II - Configuration de Insomnia
|Champ| Valeur|
|---|----|
|Grant Type| Implicit|
 |Authorization URL|https://login.microsoftonline.com/``[OAuth2_TenantId]``/oauth2/v2.0/authorize|
 |Client ID|``[OAuth2_SwaggerUiClientId]``| 
 |REDIRECT URL|http://localhost:15308/vetting/openapi/oauth2-redirect.html(S'il ne marche pas , demander autres)|
 |Response Type|Access Token|
 |Scope| openid ``[OAuth2_ApiScope]`` |

[OAuth2_TenantId] = 
````sql
select params_value from iom_svc_params 
where svc_name = 'MEDS_Vetting_WebApi' 
and params_name = 'OAuth2_TenantId'
````

[OAuth2_SwaggerUiClientId] =
````sql
select params_value from iom_svc_params 
where svc_name = 'MEDS_Vetting_WebApi' 
and params_name = 'OAuth2_SwaggerUiClientId'
````

[OAuth2_ApiScope] =
````sql
select params_value from iom_svc_params 
where svc_name = 'MEDS_Vetting_WebApi' 
and params_name = 'OAuth2_ApiScope'
````

# Qu'est ce que c'est OAuth2 ? 
__OAuth2__ est un système qui permet à une application (par exemple, une application mobile ou un site web) d'accéder à certaines de vos informations sans avoir à connaître votre mot de passe.

__Illustration simplifié__
1. __Vous(L'utilisateur)__: voulez utiliser une application qui nécessite l'accès à certaines informations (Par exemple, vos contacts Google)".
2. __L'application__ vous redirige vers Google pour que vous puissiez vous connecter et donner la permission à l'application d'accéder à vos contacts.
3. __Google__  vous demande si vous autorisez cette application à accéder à vos contacts après avoir connecté.  Si vous acceptez, Google donne à l'application __un jeton d'accès__ (une sorte de clé temporaire).
4. __L'application__ utilise ce jeton pour accéder à vos contacts sans jamais connaître votre mot de passe.