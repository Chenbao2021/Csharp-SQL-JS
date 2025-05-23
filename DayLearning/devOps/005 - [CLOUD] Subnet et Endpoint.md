# I - Qu'est ce qu'un subnet(Sous-réseau)?
Un __subnet__(sous-réseau) est une __division logique__ d'un réseau IP plus grand, comme une __pièce dans une maison__.
Dans Azure, un __subnet__ est une __section d'une Virtual Network (VNet)__ dans laquelle tu vas __placer tes ressources réseau__: VM, App Service, Private Endpoint, etc.

### Exeple simple
Imaginons que notre __VNet__ a une plage d'adresses:
````bash
10.0.0.0/16 # = 65536 adresses, avec 16 premières bit fixes.
````
On peut la __diviser en subnets__, par exemple:
|Subnet|Plage IP|À quoi il sert?|
|--|--|--|
|``subnet-app``|10.0.1.0/24	(= 256 adresses )|Héberge les VMs applicatives|
|``subnet-dp``|10.0.2.0/24	|Héberge les bases de données|
|``subnet-pe``|10.0.3.0/24	|Heberge les Private Endpoints|

### Un subnet permet de :
1. Isoler des ressources: 
	Ex: VM dans un subnet, base de données dans un autre.
2. Contrôler le trafic:
	On peut appliquer un NSG (Network Security Group) par subnet.
3. Définir des règles de routage:
	On peut attacher une __Route Table__ à un subnet.
4. Restreindre les accès privés:
	On peut y placer des __Private Endpoints__.

### En Azure, un subnet appartient toujours à:
* Une __Virtual Network__ (VNet)
* Un __Resource Group__.
* Une __subscription__.

### Analogie
|Concept|Métaphore(maison)|
|--|--|
|VNet|Une maison complète(réseau global)|
|Subnet|Une pièce dans la maison(zone logique)|
|Ressources|Meubles dans les pièces(VM, PE, ...)|

# II - Qu'est ce qu'un Private Endpoint ?
## A. Qu'est ce qu'un Endpoint?
PS: Un __endpoint__(Point de terminaison) est un point d'accès identifiable à un service ou à une ressource, généralement via une adresse réseau(Souvent une IP + un port, ou une URL).

Illustration : Private Endpoint VS Public Endpoint
|Type|Exemple|Accessible depuis|
|--|--|--|
|Public Endpoint|``https://kv-monprojet.vault.azure.net	``|Internet(Ou restreint par IP)|
|Private Endpoint|``10.0.1.5``(via une NIC)|__Ton réseau privé uniquement__|
## B. Qu'est ce qu'un NIC(Network Interface Card) dans Azure?
Un __NIC__ est une __interface réseau(Point de contact ou de communication entre deux systèmes) virtuelle__ associée à une ressource Azure(Comme une VM, un Private Endpoint, etc)

Elle permet à cette ressource de __communiquer avec d'autres machines via un réseau(VNet)__.

En résumé:
|Élément|Valeur|
|--|--|
|Nom complet|Network Interface Card|
|Fonction|Donne une adresse IP à la ressource|
|Visible|Oui, c'est une ressource Azure à part|
|Exemple|Une VM ou un Private Endpoint à sa propre NIC|

## C. Qu'est ce qu'un Private Endpoint ?
Un __Private Endpoint__ est une ressource Azure qui te permet de connecter un service Azure(Ex: Key Vault, Storage Account) à ton __réseau privé(VNet)__ via un IP Privée, au lieu de passer par internet.
* Il crée automatiquement une NIC dans un subnet, qui représente le service distant(Key Vault, etc.) dans ton réseau privé.



# III - Comment le Key Vault, la NIC, le Private Endpoint et le Subnet sont connectés dans Azure?
Quand on parle de "Prvate Endpoint Subnet", on parle en réalité de:
* Un __subnet(sous-réseau) standard__, dans une __Virtual Network__.

````bash
Client (ex : VM dans ta VNet)
        │
        ▼
 DNS privé : kv-monprojet.vault.azure.net → 10.0.1.5
        │
        ▼
NIC (interface réseau créée par le PE)
        │
        ▼
Private Endpoint (ressource de connexion privée)
        │
        ▼
Key Vault (service cible dans Azure)
````
* Le __Private Endpoint__ est le tunnel privé entre ton réseau et le service.
* Le __NIC__ est __la carte réseau virtuelle__ qui porte l'IP du Private Endpoint.
* Le __Key Vault__ est le __service cible__ qui est rendu __accessible uniquement via cette IP privée__.



