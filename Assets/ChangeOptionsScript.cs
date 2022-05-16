using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOptionsScript : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] ScriptableObjectsLights;

    private void Awake()
    {
        TimeChanger(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TimeChanger(int change)
    {
        currentTimeIndex += change;
        if (currentIndex < 0)
        {
            currentIndex = ScriptableObjectsLights.Length - 1;
        }
        else if (currentIndex > ScriptableObjectsLights.Length - 1)
        {
            currentIndex = 0;
        }
    }
}
