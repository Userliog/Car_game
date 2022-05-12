using UnityEngine;

[CreateAssetMenu(fileName = "New Paint", menuName = "Scriptable Object/Paint")]
public class Paint : ScriptableObject
{
    [Header("Description")]
    public string paintName;

    [Header("Paint")]
    public Material carPaint;

    public Color carPaint2;
}
