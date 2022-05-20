using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Get a reference to this script from the script that gets input from the user
//This way you can call the SetMaterial method.
//Alternatively, include this functionality in the script that gets the input and instantiates the prefab.

public class PrefabManager : MonoBehaviour
{

    public GameObject Prefabs;
    public Material[] Materials;


    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    //call this method with any prefab index and any material index
    public void ChangeMaterial1(int change)
    {
        int materialIndex =+ change;
        //enderer = GetComponent<MeshRenderer>();
        GetComponent<MeshRenderer>().sharedMaterial.color = Materials[materialIndex].color;
        //enderer.sharedMaterial.color = blue.color;
    }
}
