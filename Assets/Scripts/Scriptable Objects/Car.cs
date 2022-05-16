using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Scriptable Object/Car")]

public class Car : ScriptableObject
{
    //Scriptable objects are used when 
    [Header("Description")]
    public string carName;
    public string carDescription;

    [Header("Values")]
    public int carPrice;

    public bool owned;

    [Header("3D Model")]
    public GameObject carModel;

    [Header("3D Model")]
    public Material carPaint;
}
