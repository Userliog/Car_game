using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintDisplay : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] public Text paintName;

    [Header("Paint")]
    public Material carPaint;

    public Color carPaint2;

    [Header("Button")]
    public Button apply;

    public void DisplayPaint(Material paint)
    {
        //paintName.text=paint.paintarName;

        //apply.onClick.AddListener (() => carPaint.color = Color.red);
    }
}
