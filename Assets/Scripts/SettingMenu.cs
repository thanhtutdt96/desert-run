using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
	public Slider volumeSlider;

	public Resolution[] resolutions;

	public Dropdown resolutionDropdown;

	void Start ()
	{
		if (PlayerPrefs.HasKey ("Volume"))
			volumeSlider.value = PlayerPrefs.GetFloat ("Volume");
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions ();

		List<string> options = new List<string> ();

		int currentResolutionIndex = 0;

		for (int i = 0; i < resolutions.Length; i++) {
			string option = resolutions [i].width + " x " + resolutions [i].height;
			options.Add (option);

			if (resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height) {
				currentResolutionIndex = i;
			}
		}
		resolutionDropdown.AddOptions (options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue ();
	}

	public void SetResolution (int resolutionIndex)
	{
		Resolution resolution = resolutions [resolutionIndex];
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel (qualityIndex);
	}

	public void SetFullscreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;	
	}

	public void ToStart ()
	{
		PlayerPrefs.SetFloat ("Volume", volumeSlider.value);
		SceneManager.LoadScene ("Start");
	}

}
