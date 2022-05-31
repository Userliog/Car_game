using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Car", menuName = "Scriptable Object/Car")]

public class Car : ScriptableObject
{
    [Header("Description")]
    public string carName;
    public string carDescription;

    [Header("Values")]
    public int carPrice;

    [Header("3D Model")]
    public GameObject carModel;

    [Header("Paint")]
    public List<Material> carMaterial;
}