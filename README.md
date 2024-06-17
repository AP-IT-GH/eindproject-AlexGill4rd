### SuckRush Reproductie Tutorial

## Inleiding

Welkom bij de SuckRush tutorial! In deze handleiding begeleiden we je stap voor stap bij het reproduceren van onze VR-game waarin je het opneemt tegen een AI-gestuurde Roomba-stofzuiger. Aan het einde van deze tutorial heb je een werkende versie van SuckRush en begrijp je hoe de verschillende onderdelen samenwerken.

## Benodigdheden

### Software
- **Unity Versie:** 2023.1.16f1
- **ML-Agents Versie:** 2.0.1

### Hardware
- **VR Headset:** Oculus Quest 2

## Stap 1: Installatie van Benodigde Software

### ML-Agents
1. Open Unity en maak een nieuw 3D-project aan.
2. Ga naar `Window > Package Manager`.
3. Voeg de ML-Agents package toe door `Add package from git URL...` te selecteren en de volgende URL in te voeren:
   ```
   com.unity.ml-agents
   ```

## Stap 2: Training van de AI

1. **Training Configuratie:**
   - Maak een `training_config.yaml` bestand aan waarin je de trainingsparameters definieert.
   - Voorbeelden van parameters zijn het aantal raycasts, de beloningswaarden, en de configuratie van de neural network layers.

2. **Start de Training:**
   - Open een terminal en navigeer naar de root directory van je Unity-project.
   - Voer het volgende commando uit om de training te starten:
     ```bash
     mlagents-learn training_config.yaml --run-id=SuckRushTraining
     ```

3. **Monitor de Vooruitgang:**
   - Gebruik Tensorboard om de voortgang van de training te volgen:
     ```bash
     tensorboard --logdir=results
     ```

## Resultaten van de Training

De training van de AI was uitdagend omdat er veel zaken mis konden gaan. We hebben talloze experimenten uitgevoerd met verschillende parameters en instellingen om tot een optimaal resultaat te komen. Hieronder volgen enkele observaties en resultaten van de training.

### Tensorboard Afbeeldingen

![image](https://github.com/AP-IT-GH/eindproject-DevinAI/assets/57497005/5ffc7ac4-cc3f-4eb6-9850-ae953e66980d)
![image](https://github.com/AP-IT-GH/eindproject-DevinAI/assets/57497005/7406e836-0782-4515-ba8a-8497484fca0c)
![image](https://github.com/AP-IT-GH/eindproject-DevinAI/assets/57497005/51e04064-88b8-456b-aa19-f780c83b6ef0)

### Uitleg Resultaten
- **Moeilijkheidsgraad:** De training was moeilijk vanwege de complexiteit van de omgeving en de vele mogelijke fouten, zoals botsingen met objecten.
- **Parameters en Instellingen:** We hebben geëxperimenteerd met het aantal raycasts, de beloningswaarden, en de configuratie van de neural network layers.
- **Optimalisatie:** Uiteindelijk hebben we een set parameters gevonden die de AI in staat stelde om efficiënt stof op te zuigen en obstakels te vermijden.

### Beloningen
+0.02 per opgezogen stofdeel, -1 bij botsing met een object, -0.001 per bewegingseenheid.

## Stap 3: SuckRush Speltutorial

Welkom bij de SuckRush speltutorial! In deze handleiding leggen we stap voor stap uit hoe je de game speelt en het meeste plezier haalt uit je VR-stofzuigervaring. Volg de onderstaande stappen om het spel te begrijpen en succesvol tegen de AI Roomba te spelen.

### Spelinstructies

#### Stap 1: Begin van het Spel
1. **Zoek de Stofzuiger:**
   - Wanneer het spel begint, bevind je je in een kamer.
   - Kijk rond om de stofzuiger op zijn statief te vinden.
   - Gebruik je VR-controllers om de stofzuiger op te pakken.

#### Stap 2: Start met Stofzuigen
1. **Activeer de Stofzuiger:**
   - Druk op de knop op je rechtercontroller om de stofzuiger te activeren.
   - Begin met het rondbewegen van de stofzuiger door de kamer.

2. **Bekijk je Score:**
   - Op je linkerhand wordt de score weergegeven, die laat zien hoeveel stof je hebt opgezogen.
   - Houd je score in de gaten om te zien hoe goed je het doet.

#### Stap 3: Interactie met Objecten
1. **Objecten Oppakken:**
   - In de kamer bevinden zich verschillende objecten zoals standbeelden.
   - Gebruik je VR-controllers om deze objecten op te pakken en te verplaatsen.

2. **Objecten Weggooien:**
   - Als bepaalde objecten in de weg staan, kun je ze weggooien door de grijpknoppen op je controllers in te drukken en los te laten.

#### Stap 4: Vermijd Obstakels
1. **Blijf Alert:**
   - Terwijl je stofzuigt, moet je obstakels vermijden om geen punten te verliezen.
   - De AI Roomba zal ook door de kamer bewegen, dus vermijd botsingen.

#### Stap 5: Timer en Ronden
1. **Timer op je Linkerhand:**
   - Naast de score zie je op je linkerhand ook een timer die aftelt.
   - Houd de timer goed in de gaten, want wanneer deze op 0 staat, eindigt de ronde.

2. **Eind van de Ronde:**
   - Probeer zoveel mogelijk stof op te zuigen voordat de timer afloopt.
   - Zodra de timer afloopt, eindigt de ronde en wordt je score vergeleken met die van de AI Roomba.

#### Stap 6: Geluidseffecten en Feedback
1. **Luister naar Geluidseffecten:**
   - De stofzuiger en andere objecten in de kamer hebben geluidseffecten die bijdragen aan de spelervaring.
   - Geluiden kunnen je ook helpen om te weten wanneer je stof opzuigt of obstakels raakt.

### Tips en Strategieën

- **Gebruik de Omgeving:**
  - Maak gebruik van interactieve objecten in de kamer om je weg vrij te maken en efficiënter te stofzuigen.

- **Observeer de AI:**
  - Let op de bewegingen van de AI Roomba om te zien hoe je je strategie kunt aanpassen voor betere resultaten.

## Onepager

### Projectnaam: SuckRush
## SuckRush: The Ultimate VR Cleaning Race
Welkom bij SuckRush, de spannende VR-stofzuiggame waarbij je de strijd aangaat tegen een intelligente AI om te zien wie de snelste en meest efficiënte stofzuiger is! Duik in een meeslepende virtuele wereld waarin je de ultieme schoonmaakuitdaging aangaat.
Doel:
Het doel van SuckRush is simpel: stofzuig de aangewezen ruimte zo snel en grondig mogelijk. Race tegen de klok en de AI om zoveel mogelijk vuil op te zuigen en de kamer brandschoon achter te laten.
## Gameplay:
-	Betreed samen met de AI de virtuele ruimte die klaar staat om schoongemaakt te worden.
-	Het doel is om binnen de gestelde tijdslimiet meer vuil op te zuigen dan de AI.
-	Gebruik je VR-controller als stofzuiger en race tegen de AI om zoveel mogelijk vuil op te ruimen.
-	Werk snel en strategisch om elk hoekje en gaatje van de ruimte grondig te reinigen voordat de tijd om is.
-	De speler die aan het einde van de tijdslimiet het meeste vuil heeft opgeruimd, wint de wedstrijd en wordt gekroond tot de ultieme stofzuigkampioen!

## AI:
De AI in SuckRush is ontworpen om een uitdagende en realistische tegenstander te bieden. Het maakt gebruik van geavanceerde algoritmes om beslissingen te nemen op basis van de locatie van het vuil, de snelheid van de speler en verschillende andere factoren. De AI past zich aan naarmate het spel vordert, waardoor elke race uniek en spannend blijft.
VR in SuckRush biedt een realistische oefenomgeving. Spelers kunnen stofzuigvaardigheden verbeteren door actief te oefenen in een meeslepende virtuele setting, wat praktische toepasbaarheid kan bieden voor het schoonmaken in het echte leven.

## Kenmerken:
-	Realistische VR-ervaring: Duik in een meeslepende virtuele wereld met realistische stofzuiggeluiden en -effecten.
-	Diverse omgevingen: Verken verschillende locaties, van huiskamers tot kantoren, en pas je schoonmaakstrategie aan op basis van de omgeving.

## Conclusie

### Samenvatting Project
SuckRush is een innovatieve VR-game waarin spelers het opnemen tegen een AI-gestuurde Roomba-stofzuiger. Het project combineert VR-technologie met machine learning om een uitdagende en meeslepende spelervaring te bieden.

### Overzicht Resultaten
De AI-training was uitdagend en vereiste veel experimenteren met verschillende parameters en instellingen. Uiteindelijk hebben we een effectieve configuratie gevonden die de AI in staat stelt om efficiënt stof op te zuigen en obstakels te vermijden.

### Persoonlijke Visie
De resultaten van onze training laten zien dat, ondanks de complexiteit, machine learning krachtige toepassingen heeft in gameontwikkeling. Het was fascinerend om te zien hoe de AI leerde van de omgeving en steeds beter werd in zijn taak.

### Toekomstige Verbeteringen
In de toekomst willen we:
- Fijn afstellen van beloning parameters voor nog efficiënter stofzuigen.
- Uitbreiding van interactieve objecten en complexere kamers.
- Optimalisatie van VR-ervaring voor betere gebruikersinteractie.
- Dust layer in plaats van stofdeeltjes. Dit zal de game ook sneller maken.
- Gebruik van shaders
- Verschillende levels
- Verschillende stofzuigers met snelheden
- Maximaal energie in batterij stofzuiger


### Gebruikte Bronnen en Assets
- **Assets voor de map, stofzuiger en Roomba-model:**
  - Roomba [Sketchfab](https://sketchfab.com/3d-models/robot-vacuum-cleaner-low-poly-7230d8d80e8b4a82b4a34a5e7926d0d3)
  - Stofzuiger [Sketchfab](https://sketchfab.com/3d-models/vacuumed-cleaner-f5739f98cabd4a6fa15b795cb8fb7f42)
  - 3D-modellen en omgevingsassets afkomstig van [Unity Asset Store](https://assetstore.unity.com/).

## Bronnen (APA-stijl)
- Unity Technologies. (n.d.). Unity. https://unity.com/
- Unity Technologies. (2023). ML-Agents Toolkit. GitHub. https://github.com/Unity-Technologies/ml-agents
- TensorFlow. (n.d.). TensorBoard. https://www.tensorflow.org/tensorboard
- Oculus. (n.d.). Oculus Quest 2. https://www.oculus.com/quest-2/
- Unity Asset Store. (n.d.). https://assetstore.unity.com/

