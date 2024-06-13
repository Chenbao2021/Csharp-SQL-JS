***
#### I - Envoyer un mail aux responsables pour demander une présentation des plans de tests que tu vas faire.
***
Envoyer un mail aux responsables pour leur demander une présentations.
* Sur quelle base à effectuer le test
* Faut __absolutement lire le plan de test avant le visio__ ! et poser toutes les questions pendant la visio, car après les responsables n'auront pas forcément l'humour ni le temps de te répondre . Et tu vas bloquer énormément des temps dessus quand les responsables ne te repondent pas.

***
#### II - Trouver le plan de test
***
1. Ouvrir HIGGINS
2. Plan de test
3. Haut-Droite: Le bouton "Ouvrir répertoire".

***
#### III - Que faire fin de plan test ?
***
1. Ouvrir HIGGINS
2. Plan de test
3. Trouver tes plan de test, et puis cliquer sur "Status", choisit l'état correspondant.
4. Cliquer sur "Det" -> "Ajouter" (Regarde comment les autres redigent). 
5. À la fin, clique sur "Générer récap", et tu envoies un mail à FAS et la responsable du plan de test.

***
#### IV Lancer Medissys sur différentes base de donnée
***
Le chemin d'accès pour trouver le medissys qui se lance sur différentes base de donnée est : 
S:\tfe\med\exploit\Liens utiles\Medissys\DBAD Medissys

***
#### V - Habillitation
***
Ne jamais modifier le profil administrative, le profil administrative a le droit (Habillitation, droit d'accéder, modifier, etc. ) sur tous les écrans.

Chaque fois quand un plan de test te demande de modifier le habillitation:
0. Lancer Medissys, puis :
1. module -> Database Administration -> Quatrième Icon(Profil management) -> écran "Profil Management" s'ouvre
2. Dans Profil, saisit: "ADMINISTRATION", puis "Duplicate" et mettre le nom "ADMIN_TonTricode".
3. Relancer Medissys et revients dans l'écran Database Administration
4. Reviens en point 1 -> Troisième îcon(User management) -> Saisir dans login code(Le mien : "chenbao"),  double clique le résultat -> Amend -> Profil : change "Administration" à "ADMIN_CYU", puis cliquer "OK" .
5. C'est bon, on peut commencer à tester.
