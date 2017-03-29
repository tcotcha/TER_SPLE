import os,sys
import json
from random import sample
from random import randint
from noise import pnoise1

#Class for ennemies
class Ennemis(object):
	def __init__(self,x,y):
		self.x = x
		self.y = y

class Bumper(Ennemis):
	def __init__(self,x,y):
		Ennemis.__init__(self,x,y)
		self.direction = -1

class Tireur(Ennemis):
	def __init__(self,x,y):
		Ennemis.__init__(self,x,y)

#Class for platforms
class Plateforme(object):
	def __init__(self,l,x,y):
		self.largeur = l
		self.positionX = x
		self.positionY = y

class Immobile(Plateforme):
	def __init__(self,l,x,y,f):
		Plateforme.__init__(self,l,x,y)
		self.friable = f

class Mobile(Plateforme):
	def __init__(self,l,x,y,finX,finY):
		Plateforme.__init__(self,l,x,y)
		self.positionFinX = finX
		self.positionFinY = finY

#Class for items
class Items(object):
	def __init__(self,temp,x,y):
		self.temporaire = temp
		self.x = x
		self.y = y

class Inversement(Items):
	def __init__(self,d,x,y):
		Items.__init__(self,True,x,y)
		self.duree = d

class Invincibilite(Items):
	def __init__(self,d,x,y):
		Items.__init__(self,True,x,y)
		self.duree = d

class JumpBoost(Items):
	def __init__(self,d,x,y):
		Items.__init__(self,True,x,y)
		self.duree = d

class VieBonus(Items):
	def __init__(self,x,y):
		Items.__init__(self,False,x,y)

class VieMalus(Items):
	def __init__(self,x,y):
		Items.__init__(self,False,x,y)

#Pieges
class Pieges(object):
	def __init__(self,l,x):
		self.longueur = l
		self.positionX = x

#CheckPoint class
class CheckPoint(object):
	def __init__(self,actif,x,y):
		self.actif = False
		self.x = x
		self.y = y

#Player class
class Joueur(object):
	def __init__(self,x,y):
		self.nbVies = 3
		self.hauteurSaut = 1.5
		self.vitesse = 2
		self.x = x
		self.y = y

#Levele Class
class Niveau(object):
	def __init__(self,dif,t,hauteurB,joueur,checkpoint,pieges,plateformes,ennemis,items):
		self.difficulte = dif
		self.taille = t
		self.hauteurBlocs = hauteurB
		self.joueur = joueur
		self.checkpoint = checkpoint
		self.pieges = pieges
		self.plateformes = plateformes
		self.ennemies = ennemis
		self.items = items

def serialiseur(obj):
	if isinstance(obj,VieMalus):
		return {"type":obj.__class__.__name__,
				"temporaire":obj.temporaire,
				"x":obj.x,
				"y":obj.y}
	if isinstance(obj,VieBonus):
		return {"type":obj.__class__.__name__,
				"temporaire":obj.temporaire,
				"x":obj.x,
				"y":obj.y}
	if isinstance(obj,JumpBoost):
		return {"type":obj.__class__.__name__,
				"temporaire":obj.temporaire,
				"x":obj.x,
				"y":obj.y,
				"duree":obj.duree}
	if isinstance(obj,Invincibilite):
		return {"type":obj.__class__.__name__,
				"temporaire":obj.temporaire,
				"x":obj.x,
				"y":obj.y,
				"duree":obj.duree}
	if isinstance(obj,Inversement):
		return {"type":obj.__class__.__name__,
				"temporaire":obj.temporaire,
				"x":obj.x,
				"y":obj.y,
				"duree":obj.duree}
	if isinstance(obj,Mobile):
			return {"largeur":obj.largeur,
					"x":obj.positionX,
					"y":obj.positionY,
					"finX":obj.positionFinX,
					"finY":obj.positionFinY}
	if isinstance(obj,Immobile):
		return {"largeur":obj.largeur,
				"x":obj.positionX,
				"y":obj.positionY,
				"friable":obj.friable}
	if isinstance(obj,Tireur):
		return {"x":obj.x,
				"y":obj.y}
	if isinstance(obj,Bumper):
		return {"x":obj.x,
				"y":obj.y,
				"direction":obj.direction}
	if isinstance(obj,Joueur):
		return {"nbVies": obj.nbVies,
				"hauteurSaut":obj.hauteurSaut,
				"vitesse":obj.vitesse,
				"x":obj.x,
				"y":obj.y}
	if isinstance(obj,CheckPoint):
		return {"actif":obj.actif,
				"x":obj.x,
				"y":obj.y
				}
	if isinstance(obj,Pieges):
		return {"longueur":obj.longueur,
				"positionX":obj.positionX}
	if isinstance(obj,Niveau):
		return {"difficulte":obj.difficulte,
				"taille":obj.taille,
				"hauteurBlocs":obj.hauteurBlocs,
				"joueur":obj.joueur,
				"checkpoint":obj.checkpoint,
				"pieges":obj.pieges,
				"plateformes":obj.plateformes,
				"ennemies":obj.ennemies,
				"items":obj.items}

#get parameters
if len(sys.argv) == 3:
	path = sys.argv[1]
	pathOut = sys.argv[2]
else:
	print("need path of input Json file and path of output Json file")
	exit(1)

#open files
with open(path) as data_file:
	data = json.load(data_file)

#init values of level
if data["difficulte"] == "facile":
	difficulte = 1
elif data["difficulte"] == "moyen":
	difficulte = 2
elif data["difficulte"] == "difficile":
	difficulte = 3
else:
	difficulte = -1

taille = randint(data["taille"]["min"],data["taille"]["max"])
nbEnnemies = randint(data["ennemi"]["min"],data["ennemi"]["max"])
nbPlateformes = randint(data["plateforme"]["min"],data["plateforme"]["max"])
nbPieges = randint(data["piege"]["min"],data["piege"]["max"])
nbPowerUp = randint(data["power-up"]["min"],data["power-up"]["max"])

#calcul du sol
smoothness = randint(8,15)
seed = randint(0,10000)
sol = list(range(taille))
for i in sol:
	sol[i] = int(round(pnoise1(float(i)/smoothness,1)*3))+4

#placement des pieges
pieges = []
if nbPieges != 0:
	tmp = int(taille/nbPieges)
	for i in range(nbPieges):
		if i == 0:
			pos = randint(10+tmp*i,tmp*(i+1))
		elif i == nbPieges-1 :
			pos = randint(tmp*i,tmp*(i+1)-data["piege"]["largeurMax"]-1)
		else:
			pos = randint(tmp*i,tmp*(i+1))
		l = randint(data["piege"]["largeurMin"],data["piege"]["largeurMax"])
		while sol[pos-1] < sol[pos + l]:
			if i == 0:
				pos = randint(10+tmp*i,tmp*(i+1))
			elif i == nbPieges-1 :
				pos = randint(tmp*i,tmp*(i+1)-data["piege"]["largeurMax"]-1)
			else:
				pos = randint(tmp*i,tmp*(i+1))
			l = randint(data["piege"]["largeurMin"],data["piege"]["largeurMax"])
		pieges.append(Pieges(l,pos))

	tmp = 0
	while tmp < len(pieges):
		i = 0
		while i < pieges[tmp].longueur:
			sol[pieges[tmp].positionX + i] = 0
			i += 1
		tmp+=1


#write checkpoint
checkPointX = int((taille-1)/2)
while True:
	if sol[checkPointX] > 0:
		checkpoint = CheckPoint(False,checkPointX,sol[checkPointX]+1)
		break
	else:
		checkPointX+=1

#Player init
joueur = Joueur(0,sol[0]+1)

#Platforms creation
plateformes = []
for i in pieges:
	if i.longueur == data["piege"]["largeurMax"]:
		mob = randint(0,1)
		x = i.positionX+1
		y = sol[i.positionX-1]+0.5
		l = randint(data["plateforme"]["largeurMin"],data["plateforme"]["largeurMax"])
		if mob == 0:
			if difficulte == 1:
				plateformes.append(Immobile(l,x,y,False))
			elif difficulte == 3:
				plateformes.append(Immobile(l,x,y,True))
			else :
				p = randint(0,1)
				if p == 0:
					plateformes.append(Immobile(l,x,y,False))
				else:
					plateformes.append(Immobile(l,x,y,True))
		else:
			h = randint(0,1)
			v = randint(0,1)
			plateformes.append(Mobile(l,x,y,x+h*5,y+v*5))
		nbPlateformes -= 1

if nbPlateformes > 0:
	xPlatforms = sample(range(10, taille-1), nbPlateformes)
	for i in xPlatforms:
		if sol[i] == 0:
			i+=1
			while sol[i] == 0:
				i+=1
		mob = randint(0,1)
		y = sol[i]+0.5
		l = randint(data["plateforme"]["largeurMin"],data["plateforme"]["largeurMax"])
		if mob == 0:
			if difficulte == 1:
				plateformes.append(Immobile(l,i,y,False))
			elif difficulte == 3:
				plateformes.append(Immobile(l,i,y,True))
			else :
				p = randint(0,1)
				if p == 0:
					plateformes.append(Immobile(l,i,y,False))
				else:
					plateformes.append(Immobile(l,i,y,True))
		else:
			h = randint(0,1)
			v = randint(0,1)
			plateformes.append(Mobile(l,i,y,i+h*5,y+v*5))

#Ennmies creation
ennemis = []
ennemieX = sample(range(10,taille-1),nbEnnemies)
for i in ennemieX:
	if sol[i] == 0:
		i+=1
		while sol[i] == 0:
			i+=1
	tir = randint(0,1)
	if tir == 0:
		ennemis.append(Bumper(i,sol[i]+1))
	else:
		ennemis.append(Tireur(i,sol[i]+1))

#Items creation
items = []
itemsX = sample(range(10,taille-1),nbPowerUp)
for i in itemsX:
	if sol[i] == 0:
		i+=1
		while sol[i] == 0:
			i+=1
	itemName = data["power-up"]["type"][randint(0,len(data["power-up"]["type"])-1)]
	if itemName == "jumpBoost":
		items.append(JumpBoost(10,i,sol[i]+1))
	elif itemName == "invincible":
		items.append(Invincibilite(10,i,sol[i]+1))
	elif itemName == "inversementTouches":
		items.append(Inversement(10,i,sol[i]+1))
	elif itemName == "vieUp":
		items.append(VieBonus(i,sol[i]+1))
	elif itemName =="vieDown":
		items.append(VieMalus(i,sol[i]+1))


level = Niveau(difficulte,taille,sol,joueur,checkpoint,pieges,plateformes,ennemis,items)

with open(pathOut,'w+') as outfile:
	json.dump(level,outfile,indent=4,default=serialiseur)
