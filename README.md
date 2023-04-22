# Het opzetten van de scene

Om het Unity ML-Agents Jumping Agent project te kunnen testen, moeten we de verschillende game objecten opzetten in de Unity Editor. Hier is een stappenplan om dit te doen:

1. Maak een nieuwe 3D-scene in de Unity Editor.
2. Voeg een plane object toe aan de scene door te navigeren naar GameObject -> 3D Object -> Plane. De plane dient als `Ground` in dit project.
3. Maak drie kubus objecten aan door te navigeren naar GameObject -> 3D Object -> Cube. Deze zullen de muur, agent en het obstakel voorstellen. Pas de grootte van elk object aan in het inspector venster.
4. Verplaats de kubus objecten naar de juiste posities in de scene.
5. Geef elk object de juiste tags. De muur moet de tag "Wall" hebben, de agent moet de tag "Player" hebben en het obstakel moet de tag "Obstacle" hebben.
6. Voeg het `JumpingAgent` script toe aan het agent object. Stel de "Jump Force" waarde in op een geschikt niveau.
7. Voeg het `Obstacle` script toe aan het obstakel object. Stel de "Move Speed" waarde in op een geschikt niveau.
8. Sla de scene op en start de training.

![Voorbeeld scene](Images/Scene.png)

*Voorbeeld scene*

# Beschrijving code

Hieronder worden de beide scripten (Jumpingagent.cs en Obstacle.cs) beschreven die worden gebruikt in het project.

## JumpingAgent

Dit script is verantwoordelijk voor de implementatie van de agent die moet leren om over een obstakel heen te springen. Het script is gebaseerd op de Agent-klasse van Unity ML-Agents.

### Variabelen

- `jumpForce`: Dit is de kracht waarmee de agent omhoog springt. Deze variabele kan worden aangepast in de Unity Editor.
- `rigidBody`: Dit is een referentie naar de Rigidbody-component van het agent-object.
- `obstacle`: Dit is een referentie naar het obstakel-object.

### Methoden

- `Initialize()`: Deze methode wordt aangeroepen wanneer de agent wordt geïnitialiseerd. Het script haalt een referentie op naar de Rigidbody-component van het agent-object.
- `OnEpisodeBegin()`: Deze methode wordt aangeroepen wanneer een nieuwe episode begint. Het obstakel-object wordt teruggezet naar zijn oorspronkelijke positie en er wordt een nieuwe snelheid gegenereerd voor het obstakel-object.
- `OnActionReceived(ActionBuffers actions)`: Deze methode wordt aangeroepen wanneer de agent een actie heeft gekozen. Als de actie een sprong is (actie-index 0 is 1), wordt er een kracht toegepast op het agent-object om het omhoog te laten springen.
- `FixedUpdate()`: Deze methode wordt op een vaste tijdsinterval aangeroepen. Hier wordt gecontroleerd of de agent over het obstakel heen is gesprongen of niet. Als dit het geval is, krijgt de agent een positieve beloning en eindigt de episode. Als de agent nog niet over het obstakel is gesprongen, krijgt hij een kleine positieve beloning als hij zich dicht bij de grond bevindt.
- `OnCollisionEnter(Collision collision)`: Deze methode wordt aangeroepen wanneer het agent-object botst met een ander object. Als het object een obstakel is, krijgt de agent een negatieve beloning en eindigt de episode.

## Obstacle

Dit script is verantwoordelijk voor het bewegen van het obstakel-object en het detecteren van botsingen met de muur.

### Variabelen

- `moveSpeed`: Dit is de snelheid waarmee het obstakel-object beweegt. Deze variabele kan worden aangepast in de Unity Editor.
- `hit`: Deze bool geeft aan of het obstakel-object de muur heeft geraakt. Deze variabele is statisch, zodat deze kan worden gedeeld tussen verschillende instanties van het obstakel-object.
- `minMovespeed`: Dit is de minimale snelheid waarmee het obstakel-object kan bewegen. Deze variabele is statisch.
- `maxMovespeed`: Dit is de maximale snelheid waarmee het obstakel-object kan bewegen. Deze variabele is statisch.
- `random`: Dit is de willekeurig gegenereerde snelheid waarmee het obstakel-object beweegt. Deze variabele is statisch.

### Methoden

- `Update()`: Deze methode wordt elke frame aangeroepen. Het obstakel-object beweegt naar achteren met een snelheid die wordt bepaald door moveSpeed en random. Dit wordt gedaan met behulp van de Translate-methode van Unity, die de positie van het object verplaatst in de richting en afstand aangegeven door de opgegeven vector.
- `OnCollisionEnter()`: Deze methode wordt aangeroepen wanneer het `Obstacle` object botst met het `Wall` object. Wanneer deze botsen wordt de `hit` bool op `true` gezet. 
- `GenRandom()`: Deze methode genereert een willekeurige snelheid voor het obstakel.

# Trainen van een Unity ML-Agents omgeving met Anaconda

Dit stappenplan laat zien hoe je een Unity ML-Agents omgeving kunt trainen met Anaconda en de command line.

## Stap 1: Installeer Anaconda

Als je Anaconda nog niet hebt geïnstalleerd, download en installeer dan de juiste versie voor jouw besturingssysteem vanaf de [Anaconda downloadpagina](https://www.anaconda.com/products/individual).

## Stap 2: Maak een nieuwe conda environment

Open Anaconda Prompt en maak een nieuwe conda environment aan met de volgende commando's:

```
conda create --name env_name
conda activate env_name
```

Vervang `env_name` door de naam van de environment die je wilt maken.

## Stap 3: Installeer de benodigde packages

Installeer de packages die nodig zijn voor het trainen van Unity ML-Agents met de volgende commando's:

```
pip3 install torch~=1.7.1 -f https://download.pytorch.org/whl/torch_stable.html
python -m pip install mlagents==0.30.0
```

## Stap 4: Download en importeer de Unity environment

Download de Unity environment vanaf de Unity Asset Store en importeer deze in Unity. Zorg ervoor dat je de environment op de juiste manier instelt voor training met ML-Agents.

## Stap 5: Train de agent

Navigeer in de command line naar de map waarin de `mlagents-learn` executable staat en voer het volgende commando uit:

```
mlagents-learn path/to/config.yaml --run-id=name_of_run
```

Vervang `path/to/config.yaml` door het pad naar de YAML-configuratie van de Unity environment en `name_of_run` door de naam van de run.

## Stap 6: Monitor de training

Om de training te monitoren, kun je TensorBoard gebruiken. Navigeer in de command line naar de map waarin de `tensorboard` executable staat en voer het volgende commando uit:

```
tensorboard --logdir=path/to/runs
```

Vervang `path/to/runs` door het pad naar de map waarin de training runs worden opgeslagen.

## Stap 7: Stop de training

Om de training te stoppen, druk op `Ctrl+C` in de command line of sluit de command line af.

## Stap 8: Deactiveer de conda environment

Deactiveer de conda environment met het volgende commando:

```
conda deactivate
```
