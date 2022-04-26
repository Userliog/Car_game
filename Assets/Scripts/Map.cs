using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Object/Map")]
public class Map : ScriptableObject
{
    public int mapIndex;
    public string mapName;
    public string mapDescrition;
    public Color nameColor;
    public Sprite mapImage;
    public Object sceneToLoad;
}
