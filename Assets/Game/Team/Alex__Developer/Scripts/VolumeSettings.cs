using AK.Wwise;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VolumeControl : MonoBehaviour
{
    public Slider fxSlider;
    public Slider musicSlider;
    public Slider uiSlider;

    public RTPC fxVolumeRTPC;
    public RTPC musicVolumeRTPC;
    public RTPC uiVolumeRTPC;

    private const string FX_PREF = "FxVolume";
    private const string MUSIC_PREF = "MusicVolume";
    private const string UI_PREF = "UiVolume";

    private void Start()
    {
        // Загружаем сохраняемые значения (или 50 по умолчанию)
        fxSlider.value = PlayerPrefs.GetFloat(FX_PREF, 50f);
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_PREF, 50f);
        uiSlider.value = PlayerPrefs.GetFloat(UI_PREF, 50f);

        AkUnitySoundEngine.SetRTPCValue("Fx_volume", fxSlider.value);
        AkUnitySoundEngine.SetRTPCValue("Music_volume", musicSlider.value);
        AkUnitySoundEngine.SetRTPCValue("UI_volume", uiSlider.value);

        // Подписываемся на изменение слайдеров
        fxSlider.onValueChanged.AddListener(OnFxVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        uiSlider.onValueChanged.AddListener(OnUiVolumeChanged);
    }

    private void OnFxVolumeChanged(float value)
    {
        AkUnitySoundEngine.SetRTPCValue("Fx_volume",value);
        PlayerPrefs.SetFloat(FX_PREF, value);
        PlayerPrefs.Save();
    }

    private void OnMusicVolumeChanged(float value)
    {
        AkUnitySoundEngine.SetRTPCValue("Music_volume", value);
        PlayerPrefs.SetFloat(MUSIC_PREF, value);
        PlayerPrefs.Save();
    }

    private void OnUiVolumeChanged(float value)
    {
        AkUnitySoundEngine.SetRTPCValue("UI_volume", value);
        PlayerPrefs.SetFloat(UI_PREF, value);
        PlayerPrefs.Save();
    }
}
