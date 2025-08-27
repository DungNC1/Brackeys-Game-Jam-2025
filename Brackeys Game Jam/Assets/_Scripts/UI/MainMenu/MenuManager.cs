using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Tooltip("Enter here all of the different menus from the UI.")]
    public List<GameObject> Menus = new List<GameObject>();

    [Header("Sounds")]
    public AudioClip[] AudioClips;

    [Header("Settings UI References")]
    public Slider masterVolumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;

    void Start()
    {
        SetMenuActive(0);

        // Setup Volume
        if (masterVolumeSlider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
            masterVolumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume;
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        // Setup Resolutions
        if (resolutionDropdown != null)
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();
            int currentResolutionIndex = 0;

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
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
            resolutionDropdown.RefreshShownValue();

            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }

        // Setup Fullscreen
        if (fullscreenToggle != null)
        {
            bool savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
            fullscreenToggle.isOn = savedFullscreen;
            Screen.fullScreen = savedFullscreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
    }

    public void PlayAudio(int audioIndex)
    {
        if (audioIndex < 0 || audioIndex >= AudioClips.Length) return;

        GameObject audioObj = new GameObject("OneShotAudio");
        audioObj.transform.position = transform.position;

        AudioSource newAudio = audioObj.AddComponent<AudioSource>();
        newAudio.clip = AudioClips[audioIndex];
        newAudio.Play();

        Destroy(audioObj, newAudio.clip.length);
    }


    public void SetMenuActive(int menuIndex)
    {
        if (menuIndex < 0 || menuIndex >= Menus.Count)
            return;

        for (int i = 0; i < Menus.Count; i++)
            Menus[i].SetActive(i == menuIndex);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // === SETTINGS FUNCTIONS ===

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
