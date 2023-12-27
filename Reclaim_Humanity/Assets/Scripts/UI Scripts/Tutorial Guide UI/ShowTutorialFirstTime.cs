using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTutorialFirstTime : MonoBehaviour {

    // private const string guideString = "<align=justified> <size=150%>• Laboratory:</size>\n\n\\t• Energy tab: you can find this room in the lower left corner of\n\\t  the lab. In it you can transform items found in the world to\n\\t  energy that, on certain levels, will unlock power-ups;\n\n\\t• Recipe tab: located in the middle-right zone of the lab, here\n\\t  you can find and craft all your unlocked recipes;\n\n\\t• Teleport tab: it's in the bottom center, use this tab to travel\n\\t  between all biomes available. Note: in this demo only the\n\\t  Overgrown Forest is unlocked;\n\n\\t• Restore Health tab: in the bedroom, in the upper-right corner,\n\\t  you will find the health tab, which will heal Wollo to full health.\n\n<size=150%>• Biomes:</size>\n\n\\t• Collectible Items: scattered all over the biomes you will find\n\\t  many items. Collect them all to be able to create enough\n\\t  energy and complete the entire recipe tree;\n\n\\t• Humans: in the world you will find several paralyzed humans,\n\\t  interact with them and find out if they can help you with your\n\\t  mission. Sometimes they will teach you a new recipe that will\n\\t  be unlocked in the Laboratory;\n\n\\t• Enemies: don't let enemies defeat you, but still, you will need\n\\t  to fight them in order to obtain some collectible items;\n\n\\t• For each biome a special item must be found. That will be\n\\t  a fundamental component to complete the final potion that will\n\\t  help save humanity. Typically you will find it at the last biome's\n\\t  zones;\n</align>";
    private const string tutorialStringLaboratory = "";
    private readonly Dictionary<string, string> tutorialStrings = new Dictionary<string, string>() {
        {"Laboratory", "<i>A more \"fun\" and interactive tutorial with not many writings is on its way, but for now this is what we have, sorry</i> :);\n\nWelcome to Reclaim Humanity!\nHere I'll teach you the basic of the game: you can skip this tutorial to add an extra level of difficulty to the game;\n\n   • Biomes: in the game you'll have to explore three different\n     biomes, main focus will be the search for recipes\n     and ingredients to complete the <u>final potion</u> that will help save\n     humanity from its illness. Are you ready? Let's go!\n\n   • You are now in the LAB, here you can do multiple things:\n\n\t• create energy by burning collected items: you'll need this to\n   \t  unlock power-ups, but it might take a while (since they are\n   \t  still not implemented :)); you'll find the energy menu in the\n\t  room on the left;\n\n\t• make recipes: will help you proceed focusing on the main\n\t  goal, being the <u>Final potion</u>. To cook recipe however you'll\n\t  need both the right ingredients and to talk with the human\n\t  that knows that recipe. So you need to explore the biomes.\n\t  You'll find the recipe menu in the room on the right;\n\n\t• teleport to other biomes: just below you you'll see the\n\t  teleport menu, as the name suggest you need to use it to\n\t  go in the other biomes;\n\n\t• finally in the upper-right room you will find the menu to cure\n\t  Wollo. It has infinite recharges, so use it whenever you exit\n\t  from a difficult combat. In this demo this is the only way to\n\t  heal yourself, but don't worry, other methods are being\n\t  developed right now;\n\nYou are ready now to explore the LAB, if you'll need this menu again just click on the upper-right question mark button!\n\n<i>If you have any doubt about game inputs there is the input map on the Reclaim Humanity itch.io page.</i>\n\nThanks for reading this tutorial, have a nice game experience <3."},
        {"OvergrownForest", "Welcome to the Overgrown Forest, here you'll find what you need, being that ingredients to use, humans to tech you recipes or enemies to defeat!\n\n<i>Since the map is still in development you'll have to explore it by just wandering around, but I can explain you a little about the world, without too many spoilers:</i>\n\n• First, just go right ahead to North, you'll find the Policeman which\n  will warn you about the forest dangers.\n\n• Then follow the North path until you'll find a strange guy, he will give you the first recipe and some advices.\n\n• You can then explore the map for yourself, but to reach the final area you'll need to go to the extreme West of the forest, then to the extreme North, crossing the ruined bridge, and then again to the extreme East, here you'll find the last girl with a gift for you.\n\nREMEMBER!! Pack of wolves might be around every corner, inside each bush and even behind the smallest trees! Just keep your eyes open.\n\nAgain, thanks for reading the tutorial, have a nice experience in the Overgrown Forest!"},
        {"RuinedCity", ""},
        {"Wastelands", ""}
    };
    private readonly Dictionary<string, float> tutorialTextSize = new Dictionary<string, float>() {
        {"Laboratory", -600.0f},
        {"OvergrownForest", -280.0f},
        {"RuinedCity", 0.0f},
        {"Wastelands", 0.0f}
    };

    private const string tutorialKey = "IsFirstTimePlayingEver";
    
    [SerializeField] private TextMeshProUGUI tutorialGuideText;
    [SerializeField] private GameObject playerMenu;
    [SerializeField] private string biome;
    
    private void Start() {
        tutorialGuideText.text = tutorialStrings[biome];
        ResizeTextField();
        
        var playerPrefsKey = $"{tutorialKey}_{biome}";
        
        if (PlayerPrefs.HasKey(playerPrefsKey)) return;
        playerMenu.GetComponent<ChangeMenuShowed>().GetKeyPressed("t"); PlayerPrefs.SetInt(playerPrefsKey, 1);
    }
    
    private void ResizeTextField() {
        var tutorialUIRectTransform = tutorialGuideText.GetComponent<RectTransform>();
        tutorialUIRectTransform.anchorMin = new Vector2(0.0f, 0.0f);
        tutorialUIRectTransform.anchorMax = new Vector2(1.0f, 1.0f);
        
        var height = tutorialUIRectTransform.rect.height;

        tutorialUIRectTransform.offsetMin = new Vector2(0.0f, tutorialTextSize[biome]);
        tutorialUIRectTransform.offsetMax = new Vector2(0.0f, 0.0f);
    }
}

