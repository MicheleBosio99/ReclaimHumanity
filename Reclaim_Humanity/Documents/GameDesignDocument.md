# RECLAIM;HUMANITY  
### Game Design Document V0.0

- # OVERVIEW
    RECLAIM;HUMANITY is a top down 2D RPG game in pixel art, in it gamers will play as Wollo, a cute little robot, which has to overcome the most difficult mission he has ever accomplished: save the whole humainty!
    ___
    ___

- # GAMEPLAY: An Exploration Adventure
    Humans has been affected by a terrible virus, which has paralyzed all of them, your mission will be to find the cure against this virus. Here are listed the main mechanics that the player will use to fulfill the task:

    - **EXPLORATION**: go around the world, search in every corner and find all the things you need to go on on your adventure, may these be items to generate energy or some knowledge about recipes you heard about a long time ago;
    
    - Whenever you find a human use your main tool, the **TRANCESMITTER** (KEYCODE.T), to speak to them. This will help you unlock more recipes which will lead, at the end, to the final cure;

    - Crafting is not easy, you must first find the correct **RECIPE** that generate that **ITEM** and second find all the ingredients you'll need for it. You will find items laying all around the map, but sometimes it will not be so easy and you'll have to fight to obtain them;

    - **COMBAT**: may it be that you need some items for your new recipes or that you need access to some mysterious place, sometimes combat will be necessary to advance in the adventure. Just pay attention not to die, otherwise who will save humanity? Combat will be TURN-BASED, most similar to a pokemon style, where Wollo will face enemies one action at a time;

    - There won't be a player **LEVEL** to carry on, but a "Laboratory" one, in form of **ENERGY** accumulated in the Lab. Every time you find yourself with an inventory full of useless items don't just drop them! Use those to generate energy in the laboratory in order to unlock POWER-UPS, new WEAPONS and many other things. Moreover recipes will need a certain amount of energy to be completed (they won't consumeit though) so be careful to this aspect of the game too;

    ___
    ___

- # CHARACTERS
    - MAIN CHARACTER: **Wollo**, a lonely robot which whishes to wake up his master; has the possibility of waking up humanity by beginning a trip which will lead him to many items. However he doesn't know the recipes for creating the final drug which will awake people, so must use the TranceSmitter to connect to paralyzed humans and read their mind hoping to find the recipes he needs;
    - HUMANS: All around the world will be placed humans, which are unfortunately paralyzed. You can interact with them only with the Trancesmitter, and they will contribute to your addventure as they can.
    - ENEMIES: There will be enemies scattered everywhere, so pay attention to where you go. Try not to face powerful creatures if you are not preapered to. Technically speaking we can add as many enemies as we want / have time to, but for starters one typology for each biome I think will be enough. These first enemies' drops will be used in recipes too, while possible future enemies will just have their drops used to generate energy in the lab.
    - DR.IDK: your creator, as all other humans, has been affected by the virus and now stands still in his room in the laboratory. When you'll finally complete your mission and have the cure use that on him to complete the game;
    - COMPANIONS: Along the way you may find companions (2 MAX) which will travel with you. They will be more than happy to help you, but they're shy and will come out only when you need the most: during fights;

___
___

- # GAME STORY
- The world 100 years before our (in game) time: humanity has rised to the top of civilization: people have found a solution for climate change, they now use only energy from renewable sources and they have solved world' hunger and wars problem. There is just a little extra thing that humans still don't have that will make them the perfect species: immortality. But how to accomplish that? All most famous biologists and doctors of the world have come together to make that a reality and are now working on this special virus that could be the final solution. They are really really close, but the current virus has a just little problem since it gives immortality, but it also paralyzes the victim. And, just like in the worse horror films, this virus manages to find a way to spread outside the lab it was contained in and, as we know, affect whole humanity. Wollo, which is immune (obv) to its effect, carries on his life like nothing has happened, but day by day he feels more and more lonely, but he can't do anything since he doesn't know nothing about medicine. Until one day, now, 100 years later, he discovers in the lab the TranceSmitter and now has a way to learn about the world by getting in touch with humans. It begins then our adventure to save everyone;

__
__

- # THE WORLD
    - The main location will be the **laboratory** where the player may generate energy for unlocking new things and craft recipes with items found.
    - The explorable world will feature 3 biomes: Overgrown Forest, Ruined City and Wastelands. Wollo will have to explore each one of them to find unique items and enemies that will help him finishing his mission. Possibly each biome will have a single unique item that will be found at the end of the biome exploration and that is necessary to make some specific recipe.
  ##### _TODO_
    - complete with description of which enemy will be found, what collectible items and what unique items will be found in each biome:
      - OVERGROWN FOREST:
      - RUINED CITY:
      - WASTELANDS:

___
___

- # MEDIA LIST
    ## SPRITES
  * Sprites used as tilesets and items sprites have been found on Itch.io and here is a list:
      - LimeZu: "Modern Interiors", "Modern Exteriors";
      - ELVGames: "Rogue Adventure World!";
  * Characters and enemies (with thier relative animations) have been created using a graphic program that can manage pixel art. 
  ## MUSIC
  - Music in background are a selection of tracks from videogames "Days Gone" and "The last of us 2";
  - Sound effects are downloaded from https://www.zapsplat.com/ and they are all free license.

___
___
___

# OTHER DETAILS:

- ## SCENES DETAILS:
    - LABORATORY:
        - **Energy Circles Status**: 5 circles are used to show the energy the lab has. The energy value is saved in a scriptable object called "LabEnergySO" which is under the "Scriptable Objects/General" folder. Every circle contains 100 energy points, and it is animated to change when the energy reaches 0/25/50/75/100 % of the circle. The animations for the 25/50/75 % are composed by 4 frames arranged in the Animation section, the animations for Full and Empty state are single sprites used as animation. I used for all an animation so that I could easily manage them in the Animator. In it I used a "AnimationToPlay" parameter to make the initial dispatcher (which has no animation) choose the right animation. Since energy can change when player is whithin the laboratory scene I put the parameter setter inside the Update function so that it is rightfully changed when needed. Last detail, there is a canvas near the energy circles which is not enabled until the player get close enough. This canvas shows the right amount of energy with a number which goes from 0 to 500.

        - **Recipe Inventory Show**: in the lab, in the Laboratory scene, there is a platform that is animated to attract the player. This platform is where the player has to go to complete recipes. When the player stands on top of it an "animated" sprite show us to click the "F" button on the keyboard. If clicked the canvas with the recipes TODO: should show up and let the player see what recipes are already unlocked and which one are not, let the player get items from its inventory and let him cook those recipes. TODO: should recipes consume energy??;

        - Everything else it's in the Laboratory scene is there just as furniture, nothing else does something (FOR NOW);

- ## TECHNICAL DETAILS IMPLEMENTED:
    - SCENES DETAILS:
        - **World map**: 4 scenes: 1 for each biome (Overgrown Forest, Ruined City/Village, (Nuclear) Wastelands), 1 for the internal view of the laboratory;
        - **Main menu**: 1 scene for the initial screen;
        - ?Inventory: 1 scene for the inventory?;
        - **Recipe tree**: 1 scene for the recipe tree?;
        - **Combats**: at least 1 scene for the combat system;
        - **Pause Panel**: it could be called in every scene pressing "p" or "Esc" button;

    - GRAPHIC DETAILS:
        - Using Unity 2D module URP;
        - 32x32 tiles and items. The characters will be bigger (48x48 pixels);
        - Biomes Maps are no more than 128x128 tiles (so 4096x4096 pixels), the internal lab is much smaller, it's 40x48 tiles;
        - Screen resolution: 16:9. (Still, how many (n*n pixels) tiles should appear in the cam view? Prof used something around 24 horizontal tiles with 16:9 resolution which could be good for us too);

    - PLAYING CHARACTER DETAILS:
        - Speed, Input method, Collider type, Size, (Animations details), ...?;
    
    - MAIN MENU DETAILS:
      - Main Menu consist in 3 buttons:
        1. Play &rarr; load the laboratory scene;
        2. (Settings &rarr; load the settings of the game);
        3. Quit &rarr; close the application;
  
    - PUASE MENU DETAILS:
      - Pause Menu consist in:
        - 2 buttons: "Resume" &rarr; disable PausePanel and continue to play; "Main Menu" &rarr; load MainMenu scene and interrupt game;
        - 3 sliders: "Master Volume", "Music Volume", "Sounds Volume" that sets the volume during the game;
    
    - ENEMIES DETAILS:
        - Base characteristics: type of enemy, level (?), health points (max and current), damage, speed of movement;
        - Chasing method: when player is detected (now it is still when player is close), enemy starts following him to fight;
    
    - SOUND DETAILS:
          - Sounds and Music are managed through a MixerManager that controls the AudioMixer;
          - AudioMixer has three channels: Master, Music, SoundFX;
          - Background music is reproduced with AudioSource component
          - Sound Effects are controlled by a script that instanciate a new GameObject everytime the sound is reproduced.
          - Player can manage volumes in Pause Panel
    
    - SCRIPTABLE OBJECTS:
          - TotalEnergy and CurrentEnergy of the lab are saved in the scriptable object called "LabEnergySO" which is under the "Scriptable Objects/General" folder;
          - Must use a SO for the inventory;
    

- ## POST GAME IDEAS:
    - Add here ideas which could be included after the main game is complete...
    - Add new enemies which drops will only be used as energy generation;




___
___
___


## GROUP MEMBERS: TODO COMPLETE
- MICHELE BOSIO: michele2.bosio@mail.polimi.it - Team Leader & Developer;
- ANASTASIA FAVERO: anastasia.favero@mail.polimi.it - ;
- ANDREA PESCI: andrea1.pesci@mail.polimi.it - Sound Engineer & Developer;
- RICCARDO MALPIEDI : riccardo.malpiedi@mail.polimi.it - ;
- IACOPO ROBERTO FERRARIO : iacoporoberto.ferrario@mail.polimi.it - ;

## DEADLINES:
- Before 20/10/23 > First Game Design Document Version: must contain most of the game main mechanics explained;
- Before 24/10/23 > Initial game development divided among the group, must be finalized for what is possible (animations and other are not without final sprite). Splitting as follows:
    - Michele: Finding tilesets, first map of biomes drawn, with collisions correctly set; -> X
    - Iacopo: first main character implementation, his movements and collisions. Animations not possible until we don't have the final sprites to deal with; -> X
    - Anastasia: first implementation of the prefab that will generate enemies, with main stats which are health, actions set and damage. Also enemies spawn rules could be defined; -> X
    - Riccardo: Starting menu' implementation, with its scene. It should have a play button, an options one and a quit one. Those buttons must lead to the required scene/place they are designed for; -> X
    - ?: Turn-based combat scene. It must have a menu where to choose the selected action (attack, run away, others ?) and a scene where it can be seen our character and the enemy found; TODO decide with the team how to deisgn this (main aspect I can think of: how to handle multiple enemies?, are there others things the player can do other that run away or attack??); It will almost surely take more time to do this, since a lot of thinking must be done about it, still, starting make the scene for it and some UI is a good idea; ->  X

- Before 31/10/23 > New deadlines to complete:
    - Michele: Finish loading sprites and tilesets we'll need, finish laboratory scene with details (but without animations for now), try to finish overgrown forest scene;
    - Anastasia: Finish enemies prefab;
    - Riccardo: Start (and possible finish) the scene used for combat;
    - ?: Search/Start design of main character sprites and animations;
    - ?: Complete GDD with all background decisions and structure it with the new discussed sections;

- Before 06/11/23: Game Design Document first version MUST BE FINALIZED and shared with the prof. IT WILL BE EVALUATED (I think so)?

- Before 14/11/23 > New deadlines to complete:
    - Make working inventory system;
    - Make recipes and energy systems in the laboratory;
    - Carry on combat system and scene;
    - Try to complete biomes scenes with map configuration and some details;
    - Tile sets for characters (and Animation?);
    - Other things?
