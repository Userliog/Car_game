using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Object/Map")]
public class Map : ScriptableObject
{
    public string mapName;
    public string mapDescrition;
    public string mapType;
    public string prizeMoney;
    public string prizeRequierment;
    public Sprite mapImage;
    public Object sceneToLoad;
}
