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
    [SerializeField] public Text Money;
    public bool carOwned;
    public int price;
    public string name;

    [Header("Spawnpoint")]
    [SerializeField] public Transform carContainer;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button buyButton;

    [Header("Paint")]
    private int materialIndex = 0;
    List<Material> paint;
    private Material carMaterial;

    /// <param name="car"> Car: A scriptable object containing information about a car. </param>

    /// <summary>
    ///  This function is called to change the text and images on screen to those of the car.
    /// </summary>
    public void DisplayCar(Car car)
    {
        //This unlocks the saad 3-9, the starter car from the begining
        PlayerPrefs.SetInt("3-9", 1);

        //Texts are changed to the new car
        Money.text = (PlayerPrefs.GetInt("money").ToString() + " $");
        carName.text = car.carName;
        carDescription.text = car.carDescription;
        carOwned = PlayerPrefs.GetInt(car.name) >= 1;

        //Variables are set, so they can be used in other functions
        name = car.name;
        paint = car.carMaterial;

        //This enables/disables the buy/play button based on if the car is owned or not
        if (carOwned == false)
        {
            if (buyButton != null)
            {
                buyButton.interactable = true;
            }
            if (playButton != null)
            {
                playButton.interactable = false;
            }

            //if the car issnt owned then the price is displayed asweel as a lock icon
            lockIcon.SetActive(true);
            carPrice.text = car.carPrice + " $";
            price = car.carPrice;
        }
        else
        {
            if (buyButton != null)
            {
                buyButton.interactable = false;
            }
            if (playButton != null)
            {
                playButton.interactable = true;
            }

            lockIcon.SetActive(false);
            carPrice.text = "Owned";
        }

        //The previous car in the spawnpoint is destroyed
        if (carContainer.childCount > 0)
        {
            Destroy(carContainer.GetChild(0).gameObject);
        }

        //The new car is instantiated
        Instantiate(car.carModel, carContainer.position, carContainer.rotation, carContainer);

        //The color is changed
        ColorChange(0);

    }

    /// <param 
    /// name="change"> Int: The amount to change the paint index by.
    /// </param>
    /// <summary>
    ///  This function is used by the buttons on screen to change the paint of the car on screen
    /// </summary>
    public void ColorChange(int change)
    {
        materialIndex += change;

        //Makes sure the index doesn't exide the contents of the list
        if (materialIndex > paint.Count - 1)
        {
            materialIndex = 0;
        }
        else if (materialIndex < 0)
        {
            materialIndex = (paint.Count - 1);
        }
        //this replaces the currnet color of the car with a new one, and shows the name of it on screen
        carContainer.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = paint[materialIndex].color;
        carColor.text = paint[materialIndex].name;
    }
    public void BuyCar()
    {
        if (PlayerPrefs.GetInt("money") >= price && PlayerPrefs.GetInt(name) < 1)
        {
            PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") - price));
            PlayerPrefs.SetInt(name, 1);
            lockIcon.SetActive(false);
            carPrice.text = "Owned";
        }
        else
        {
            carPrice.text = "Insuficient Funds";
        }
        Money.text = (PlayerPrefs.GetInt("money").ToString() + " $");
    }

    /// <summary>
    ///  This function is used to load the selected scene
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("MapToLoad"));
    }
}
