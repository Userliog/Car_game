using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Object/Map")]
public class Map : ScriptableObject
{
    public string mapName;
    public string mapDescrition;
    public string raceType;
    public string trackType;
    public string prizeMoney;
    public float timeRequierment;
    public int laps;
    public Sprite mapImage;
    public Object sceneToLoad;
}
