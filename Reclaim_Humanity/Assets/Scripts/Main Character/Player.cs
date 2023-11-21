using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //just attached this component to MainCharacter to retrieve info about its position and other stats

    public void SaveGame()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("Game saved successfully;\n");
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Vector3 position;
        position.x = data.positon[0];
        position.y = data.positon[1];
        position.z = data.positon[2];
        transform.position = position;

        Debug.Log("Game loaded successfully;\n");
    }


}
