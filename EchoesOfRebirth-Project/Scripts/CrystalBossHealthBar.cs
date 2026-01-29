using UnityEngine;
using UnityEngine.UI;

public class CrystalBossHealthBar : MonoBehaviour
{
    public Slider slider;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (slider != null && maxHealth > 0)
        {
            float targetValue = (float)currentHealth / (float)maxHealth;
            
            // Force the value to something else and then back to 'targetValue' 
            // to jumpstart the UI if it's visually stuck.
            slider.value = targetValue;
            
            // Force the UI to rebuild immediately
            Canvas.ForceUpdateCanvases();
            
            Debug.Log($"Slider code ran. Value is now: {slider.value}");
        }
    }
}