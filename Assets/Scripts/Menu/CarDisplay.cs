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

    public void DisplayCar(Car car)
    {
        //PlayerPrefs.SetInt(car.name, 0);

        Money.text = (PlayerPrefs.GetInt("money").ToString() +" $");
        carOwned = PlayerPrefs.GetInt(car.name) >= 1;
        name = car.name;
        carName.text = car.carName;
        carDescription.text = car.carDescription;
        paint = car.carMaterial;

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


        if (carContainer.childCount > 0)
        {
            Destroy(carContainer.GetChild(0).gameObject);
        }

        Instantiate(car.carModel, carContainer.position, carContainer.rotation, carContainer);
        
        ColorChange(0);
        
    }
    public void ColorChange(int change)
    {
        materialIndex += change;

        if (materialIndex > paint.Count - 1)
        {
            materialIndex = 0;
        }
        else if (materialIndex < 0)
        {
            materialIndex = (paint.Count - 1);
        }
        carContainer.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = paint[materialIndex].color;
        carColor.text = paint[materialIndex].name;
    }
    public void BuyCar()
    {
        if(PlayerPrefs.GetInt("money") >= price && PlayerPrefs.GetInt(name) < 1)
        {
            PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") -price));
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
    public void GetMoney()
    {
        PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + 1000));
        Money.text = (PlayerPrefs.GetInt("money").ToString() + " $");
    }
    public void Play()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("MapToLoad"));
    }
}
