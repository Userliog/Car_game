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

    [Header("Values")]
    [SerializeField] public Text carPrice;

    [SerializeField] public bool carOwned;

    [Header("Spawnpoint")]
    [SerializeField] public Transform carContainer;

    [Header("Stuff")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button nextButton;

    [Header("Extra Test Stuff")]
    [SerializeField] public Material carPaint;
    private Color testColor;

    public void DisplayCar(Car car)
    {
        testColor = new Color(1f, 0.5f, 0.8f, 1f);
        //Renderer carRenderer = car.transform.GetChild(1).gameObject.GetComponent<Renderer>();
        carName.text = car.carName;
        carDescription.text = car.carDescription;
        carPrice.text = car.carPrice + " $";
        //Renderer carRenderer = car.GetComponent<Renderer>();
        //carRenderer.material.SetColor("_Color", carPaint.color);
        carPaint.SetColor("color", testColor);

        if (carContainer.childCount > 0)
        {
            Destroy(carContainer.GetChild(0).gameObject);
        }

        Instantiate(car.carModel, carContainer.position, carContainer.rotation, carContainer);

        nextButton.onClick.AddListener(() => carPaint.SetColor("_Color", Color.red));
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(PlayerPrefs.GetString("MapToLoad")));
    }
}
