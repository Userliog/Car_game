using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] ScriptableObject[] paintsArray;

    public void Start()
    {
        foreach(var p in paintsArray)
        {
            //AddDropdownOptions(dropdown, p.Material);
            //print(p.Material);
        }
        //paintsArray = 
        //Array[string] options = new Array[string]() { "Option1", "Option2", "Option3" };
        //AddDropdownOptions(dropdown, options);
    }

    void AddDropdownOptions(Dropdown dropdown, Material options)
    {
        //dropdown.ClearOptions();
        //dropdown.AddOptions(options);
    }
}
