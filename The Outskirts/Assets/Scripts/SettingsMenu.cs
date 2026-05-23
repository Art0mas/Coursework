using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; 
using TMPro; 
using System.Collections.Generic; 

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider masterSlider, musicSlider, sfxSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] resolutions; 

    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 80f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 100f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 100f);

        fullscreenToggle.isOn = Screen.fullScreen;

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
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMasterVolume(float volume)
    {
        mainMixer.SetFloat("MasterVol", volume);
        PlayerPrefs.SetFloat("MasterVol", volume); 
    }

    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("MusicVol", volume);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFXVol", volume);
        PlayerPrefs.SetFloat("SFXVol", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}