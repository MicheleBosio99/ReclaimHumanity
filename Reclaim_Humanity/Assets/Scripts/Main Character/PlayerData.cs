
[System.Serializable]
public class PlayerData
{
    //per ora salva solo la position, saranno da salvare poi tutte le stats e items del player
    public float[] positon;
    public PlayerData(Player player)
    {
        positon = new float[3];
        positon[0] = player.transform.position.x;
        positon[1] = player.transform.position.y;
        positon[2] = player.transform.position.z;
    }
}
