using UnityEngine;
using System;

namespace Core.Loader
{
    public class SettingsLoader : MonoBehaviour
    {
        private void Start()
        {
            LoadSettings();
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
                QualitySettings.masterTextureLimit =
                             PlayerPrefs.GetInt("QualitySettingPreference");
            else
                QualitySettings.masterTextureLimit = 0;
            if (PlayerPrefs.HasKey("ResolutionPreference"))
                Screen.SetResolution(resolutions[PlayerPrefs.GetInt("ResolutionPreference")].width, resolutions[PlayerPrefs.GetInt("ResolutionPreference")].height, Screen.fullScreen);
            else
                Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, Screen.fullScreen);
            if (PlayerPrefs.HasKey("VSyncPrefrence"))
                QualitySettings.vSyncCount =
                             PlayerPrefs.GetInt("VSyncPrefrence");
            else
                QualitySettings.vSyncCount = 2;
            if (PlayerPrefs.HasKey("AntiAliasingPreference"))
                QualitySettings.antiAliasing =
                             (int)Mathf.Pow(2, PlayerPrefs.GetInt("AntiAliasingPreference"));
            else
                QualitySettings.antiAliasing = (int)Mathf.Pow(2, 2);
            if (PlayerPrefs.HasKey("FullscreenPreference"))
                Screen.fullScreen =
                    Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
            else
                Screen.fullScreen = true;
        }
    }
}

