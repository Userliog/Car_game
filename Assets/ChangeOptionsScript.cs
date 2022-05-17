using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOptionsScript : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] ScriptableObjectsLights;
    [SerializeField] public Transform LightsContainer;
    private int currentTimeIndex;
    private int currentIndex;

    private void Awake()
    {
        Transform checkpointsTransform = transform.Find("Checkpoints");

        List<GameObject> Lights = new List<GameObject>();
        foreach (Transform Light in LightsContainer)
        {
            Lights.Add(LightsContainer.GetComponentsInChildren);
        }
        TimeChanger(1);
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
        currentIndex += change;
        if (currentIndex > 0)
        {
            Lights<currentIndex>.SetActive(true);
            currentIndex++;
        }
        else if (currentIndex > ScriptableObjectsLights.Length - 1)
        {
            currentIndex = 0;
        }
    }
}
