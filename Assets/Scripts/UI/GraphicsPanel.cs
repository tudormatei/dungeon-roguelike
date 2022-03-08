using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace Dungeon.UI
{
    /// <summary>
    /// UI graphics settings panel manager.
    /// </summary>
    public class GraphicsPanel : MonoBehaviour
    {
        [Header("Graphic Settings")]
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown textureQualityDropdown;
        [SerializeField] private TMP_Dropdown antialiasingDropdown;
        [SerializeField] private TMP_Dropdown vSyncDropdown;

        private Resolution[] resolutions;

        private void OnEnable()
        {
            fullscreenToggle.onValueChanged.AddListener(delegate { OnFullScreenToggle(); });
            resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
            textureQualityDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
            antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntiAliasingChange(); });
            vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });

            OnTextureQualityChange();
            OnAntiAliasingChange();
            OnVSyncChange();

            resolutions = Screen.resolutions;

            foreach (Resolution resolution in resolutions)
            {
                resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
            }

            LoadSettings();
        }

        public void OnFullScreenToggle()
        {
            Screen.fullScreen = fullscreenToggle.isOn;
        }

        public void OnResolutionChange()
        {
            Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        }

        public void OnTextureQualityChange()
        {
            QualitySettings.masterTextureLimit = textureQualityDropdown.value;
        }

        public void OnAntiAliasingChange()
        {
            QualitySettings.antiAliasing = (int)Mathf.Pow(2, antialiasingDropdown.value);
        }

        public void OnVSyncChange()
        {
            QualitySettings.vSyncCount = vSyncDropdown.value;
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetInt("QualitySettingPreference",
                       textureQualityDropdown.value);
            PlayerPrefs.SetInt("ResolutionPreference",
                       resolutionDropdown.value);
            PlayerPrefs.SetInt("VSyncPrefrence",
                       vSyncDropdown.value);
            PlayerPrefs.SetInt("AntiAliasingPreference",
                       antialiasingDropdown.value);
            PlayerPrefs.SetInt("FullscreenPreference",
                       Convert.ToInt32(Screen.fullScreen));
        }

        private void LoadSettings()
        {
            Resolution[] resolutions = Screen.resolutions;

            int resIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == Screen.currentResolution.width
                      && resolutions[i].height == Screen.currentResolution.height)
                    resIndex = i;
            }

            if (PlayerPrefs.HasKey("QualitySettingPreference"))
                textureQualityDropdown.value =
                             PlayerPrefs.GetInt("QualitySettingPreference");
            else
                textureQualityDropdown.value = 3;
            if (PlayerPrefs.HasKey("ResolutionPreference"))
                resolutionDropdown.value =
                             PlayerPrefs.GetInt("ResolutionPreference");
            else
                resolutionDropdown.value = resIndex;
            if (PlayerPrefs.HasKey("VSyncPrefrence"))
                vSyncDropdown.value =
                             PlayerPrefs.GetInt("VSyncPrefrence");
            else
                vSyncDropdown.value = 2;
            if (PlayerPrefs.HasKey("AntiAliasingPreference"))
                antialiasingDropdown.value =
                             PlayerPrefs.GetInt("AntiAliasingPreference");
            else
                antialiasingDropdown.value = 1;
            if (PlayerPrefs.HasKey("FullscreenPreference"))
                Screen.fullScreen =
                Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
            else
                Screen.fullScreen = true;
        }
    }
}


