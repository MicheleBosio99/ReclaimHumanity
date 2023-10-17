# RECLAIM HUMANITY

Game Design Document v0.0:


- ## GAMEPLAY: An Exploration Adventure
    - Main idea is reading (paralyzed) humans mind throught the TranceSmitter;
    - Final goal being the creation of a "potion" that will wake up humans;
    - View is 2D top-down and angled;
    - The game will feature turn-based combat (still I am really not sure about how to do them efficiently and not boring), so, I think, it will be possible to change anytime to a run&gun gameplay if we see turn-based doesn't work out, since this one is much easier to implement and doesn't change anything about health/drops/actions/etc... that will be already implemented;
    - Recipe tree which has as the root the final "potion" that saves humanity -> means (many) items are needed. Items can be drop from enemies or can be found in the environment, interaction with items necessary for the character to be able to pick them up. Recipes are prepared in the lab only, there will be a precise place to do them;
    - Items can also be used to generate energy for the recipe creations. Every recipe has an amount of energy required and every item has a total amount of energy that can generate throught the Organic Generator (?);
    - 

- ## MAIN CHARACTER:
    - Wollo, a lonely robot which whishes to wake up his master; has the possibility of waking up humanity by beginning a trip which will lead him to many items. However he doesn't know the recipes for creating the final drug which will awake people, so must use the TranceSmitter to connect to paralyzed humans and read their mind hoping to find the recipes he needs;
    - 

- ## CONTEXT:
    - Place and timeline: Earth, at some point in the near future, all human beings got afflicted by a strange virus, which grants a sort of immortality but blocks every movement of a person, paralyzing her;
    - Environment: there is a laboratory which is where Wollo lives, that divides 3 biomes: an Overgrown Forest, a Ruined City and some near Nuclear Wastelands;
    - 

- ## ENEMIES:
    - At first I think it's best to have one type of enemy in the Overgrown Forest and one type in the Ruined City. For the nuclear wastelands we could use the same two enemies of the other biomes and just make them greenish and slightly more powerful (they got evolved by nuclear emissions). At last a big bad Nuclear Horror will be the final boss and once defeated him we'll have the unique item we need to complete the recipe for the wake-up-humans potion;


    - TODO: decide which enemies to put for each biomes. This mobs will have one/two drops we can use for recipes. We will later add some others, if time let us, which will not have drops for recipes but only for energy generation of the lab. Some examples:
        - Overgrown Forest: Rabid wolves, Aggressive carnivorous plants, Big (Walking Birds), ...
        - Ruined City: Large Rats, Hostile (Cleaning ?) Robots, ...
        - Wastelands: easy since we can use the more powerful version of whatever we have choosen in the other biomes + Nuclear Horror;
    
    - 

- ## ALLIES:
    - As "secondary missions" (absolutely very very very short) Wollo will be able to do something to wake up companions that will help him in combats. TODO decide if companions are woken up by finding them around the world map and by repairing them, or if they just wake up whenever the laboratory reaches a certain amount of energy;
    - TODO decide in minimal details what companions do:
    
        | WHat can they do? | PROs | CONs |
        | ----------- | ----------- | ----------- |
        | Fight side by side with Wollo |  |  |
        | Equip weapons |  |  |
        | Move in the world<br>(or only came out in battle) |  | Many more animations to do |
        |  |  |  |
        |  |  |  |

        in general I'll leave the companions thing to be implemented in the end if we have time. It is a really good idea, but it means many more sprites and animations are to be found and the whole combat will need to include them, which I don't think it's an easy task...;
    
    - If we have no time, but we manage to find sprites then we can add the "save robot friend" mission and have him either waiting for us in the lab or just following us around, which should be an easy task to implement even after the game is fully developed;
    - 


- ## TECNICAL DETAILS IMPLEMENTED:
    - TODO decide: 16x16 pixels tiles | OR | 32x32 pixels tiles? More seems excessive, less just no;
    - Screen resolution ? how many (n*n pixels) tiles should appear in the cam view? Prof used something around 24 horizontal tiles with 16:9 resolution which could be good for us too;
    - ...?

___

## GROUP MEMBERS: TODO COMPLETE
- MICHELE BOSIO: michele2.bosio@mail.polimi.it - Team Leader & Developer;
- ANASTASIA FAVERO: anastasia.favero@mail.polimi.it - ;
- ANDREA PESCI: andrea1.pesci@mail.polimi.it - ;
- RICCARDO MALPIEDI : riccardo.malpiedi@mail.polimi.it - ;
- IACOPO ROBERTO FERRARIO : iacoporoberto.ferrario@mail.polimi.it - ;

## DEADLINES:
- Before 20/10/23 > First Game Design Document Version: must contain most of the game main mechanics explained;
- Before 24/10/23 > Initial game development divided among the group, must be finalized for what is possible (animations and other are impopssible without final sprite). Splitting as follows:
    - Michele: Finding tilesets, first map of biomes drawn, with collisions correctly set;
    - Iacopo: first main character implementation, his movements and collisions. Animations not possible until we don't have the final sprites to deal with;
    - Anastasia: first implementation of the prefab that will generate enemies, with main stats which are health, actions set and damage. Also enemies spawn rules could be defined;
    - ?: Starting menu' implementation, with its scene. It should have a play button, an options one and a quit one. Those buttons must lead to the required scene/place they are designed for;
    - ?: Turn-based combat scene. It must have a menu where to choose the selected action (attack, run away, others ?) and a scene where it can be seen our character and the enemy found; TODO decide with the team how to deisgn this (main aspect I can think of: how to handle multiple enemies?, are there others things the player can do other that run or attack??); It will almost surely take more time to do this, since a lot of thinking must be done about it, since, starting make the scene for it and some UI is a good idea;

- LATER DL: Before 06/11/23: Game Design Document first version MUST BE FINALIZED and shared with the prof. IT WILL BE EVALUATED (I think so)?