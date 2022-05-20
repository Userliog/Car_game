using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CarDisplay : MonoBehaviour
{
    [Header("Description")]
    [SerializeField] public Text carName;
    [SerializeField] public Text carDescription;

    [SerializeField] public GameObject lockIcon;

    [Header("Values")]
    [SerializeField] public Text carPrice;
    [SerializeField] public Text carColor;

    [SerializeField] public bool carOwned;

    [Header("Spawnpoint")]
    [SerializeField] public Transform carContainer;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button nextButton;

    [Header("Extra Test Stuff")]
    public Material[] materialsScriptObject;
    public string[] names;
    private Color testColor;
    private int materialIndex;
    List<Material> paintTest;

    public void DisplayCar(Car car)
    {
        PlayerPrefs.SetInt("3-9", 1);
        PlayerPrefs.SetInt("3eo", 0);
        PlayerPrefs.SetInt("850", 1);
        PlayerPrefs.SetInt("evo 9", 1);

        print(car.name);
        bool carOwned = PlayerPrefs.GetInt(car.name, 0) >= 1;
        //lockIcon.SetActive(!carOwned);
        playButton.interactable = carOwned;

        testColor = new Color(1f, 0.5f, 0.8f, 1f);
        carName.text = car.carName;
        carDescription.text = car.carDescription;
        
        if (carOwned == true)
        {
            carPrice.text = "Owned";
        }
        else
        {
            carPrice.text = car.carPrice + " $";
        }
        

        if (carContainer.childCount > 0)
        {
            Destroy(carContainer.GetChild(0).gameObject);
        }

        Instantiate(car.carModel, carContainer.position, carContainer.rotation, carContainer);

        //List<Material> paintTest = new List<Material>();
        paintTest = car.carMaterial;

        //GameObject testObject = GameObject.FindWithTag("Player");
        
        //MeshRenderer testRendererObject = carContainer.GetComponentInChildren<MeshRenderer>();
        //print("player: "+ testRendererObject);

        //testRendererObject.sharedMaterial.color = paintTest[0].color;

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(PlayerPrefs.GetString("MapToLoad")));
    }
    public void ColorChangeTest(int change)
    {
        materialIndex += change;

        MeshRenderer testRendererObject = carContainer.GetComponentInChildren<MeshRenderer>();
        print("player: " + testRendererObject);

        testRendererObject.sharedMaterial.color = paintTest[materialIndex].color;
        carColor.text = paintTest[materialIndex].name;
        print(paintTest[materialIndex].name);
    }
   
}
