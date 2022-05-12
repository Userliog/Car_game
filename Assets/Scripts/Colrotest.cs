using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colrotest : MonoBehaviour
{
    [SerializeField] private Material myMaterial;
    private int index=0;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    void Update()
    {
        //nextButton.onClick.AddListener(() => myMaterial.color=Color.red);
    }
}
