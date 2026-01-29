using UnityEngine;
using UnityEngine.UI;
public class PotionMenu : MonoBehaviour
{
    public GameObject potionMenu;
    //public GameObject settingsPanel;   // Panel_Settings
    public Image displayImage;
    public Sprite[] images;
    private int currentIndex = 0;
    public void ChangePotions()
    {
        potionMenu.SetActive(!potionMenu.activeSelf);
        //settingsPanel.SetActive(true);
    }

    void Start()
    {
        if (images.Length > 0)
            displayImage.sprite = images[currentIndex];
    }

    public void NextImage()
    {
        if (currentIndex < images.Length - 1) // Só vai até a última
        {
            currentIndex++;
            displayImage.sprite = images[currentIndex];
        }
    }

    public void PrevImage()
    {
        if (currentIndex > 0) // Só vai até a primeira
        {
            currentIndex--;
            displayImage.sprite = images[currentIndex];
        }
    }
}