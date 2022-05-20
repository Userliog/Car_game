using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testscript2 : MonoBehaviour
{
    public GameObject Prefabs;
    public Material[] Materials;
    public int materialIndex;
    
    public void ChangeMaterial1(int change)
    {
        materialIndex += change;
        Prefabs.GetComponent<MeshRenderer>().sharedMaterial.color = Materials[materialIndex].color;
        print(Materials[materialIndex].name);
    }
}
