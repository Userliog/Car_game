using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private float currentColor = 0.5f;
    private GameObject Car;
    public Material carPaint;
    // Start is called before the first frame update
    void Start()
    {
        Car= GameObject.FindWithTag("Player");
        Renderer Renderer= Car.GetComponent<Renderer>();
    }

    // Update is called once per frame
    public void ChangeScriptableObject(float change)
    {
        float test = change / 10;
        print("cahnge: " + (((test)) + currentColor));
        if ((test) + currentColor <= 1)
        {
            currentColor += (test);
            carPaint.color = new Color(currentColor, 1.0f, 1.0f);
            print(currentColor);
        }
        else
        {
            currentColor = 0;
            carPaint.color = new Color(currentColor, 1.0f, 1.0f);
        }
        print(" G: " + currentColor);
    }
}
