


/*using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject settingsPanel;   // Panel_Settings
    public GameObject CreditsPanel;   // Panel_Credits

    //Vol
    //public static SettingsMenu instance;
    //public AudioMixer mixer;

    //Res
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Awake()
        {
            // Garante que só exista um SettingsManager no jogo
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Mantém ao trocar de cena
            }
            else
            {
                Destroy(gameObject);
            }

            LoadVolume();
        }
    private void Start()
    {
        //Settings
        #region Settings

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (CreditsPanel != null)
            CreditsPanel.SetActive(false);

        #endregion Settings

        //Volume
        #region Volume

/*        if (volumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("volume", 1f);

            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = savedVolume;

            AudioListener.volume = savedVolume;

            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        //#endregion Volume

        //Resolution
        //#region Resolution

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        #endregion Resolution
    }

    #region Settings
    public void OpenSettings()
    {
            settingsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
            CreditsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
}

    #endregion Settings

    //Vol
    public void SetVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
    }
    public void LoadVolume()
    {
        float savedValue = PlayerPrefs.GetFloat("volume", 0.75f);
        mixer.SetFloat("MasterVolume", Mathf.Log10(savedValue) * 20);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
    }
} */


using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("References")]
    public GameObject settingsPanel;   // Panel_Settings
    public GameObject CreditsPanel;    // Panel_Credits

    [Header("Volume")]
    public Slider volumeSlider;        // Slider "Volume Geral"

    //Res
    [Header("Resolution")]
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Start()
    {
        // SETTINGS
        #region Settings

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (CreditsPanel != null)
            CreditsPanel.SetActive(false);

        #endregion

        // VOLUME
        #region Volume

        if (volumeSlider != null)
        {
            // Carrega volume guardado (ou 1 se não existir)
            float savedVolume = PlayerPrefs.GetFloat("volume", 1f);

            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = savedVolume;

            // Aplica logo ao começar
            AudioListener.volume = savedVolume;

            // Quando o slider mudar, chama SetVolume
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        #endregion

        // RESOLUTION
        #region Resolution

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        #endregion
    }

    #region Settings
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        CreditsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
    }
    #endregion

    // === VOLUME ===
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;          // controla volume global
        PlayerPrefs.SetFloat("volume", volume); // guarda para a próxima vez
    }

    // === RESOLUTION ===
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.FullScreenWindow);
    }
}
