using UnityEngine;
using UnityEngine.UI;
using AK.Wwise;

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

    [SerializeField] private GameObject _audioObject;


    private void Start()
    {
        // Загружаем сохраняемые значения (или 50 по умолчанию)
        fxSlider.value = PlayerPrefs.GetFloat(FX_PREF, 50f);
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_PREF, 50f);
        uiSlider.value = PlayerPrefs.GetFloat(UI_PREF, 50f);

        // Устанавливаем RTPC с учетом gameObject, чтобы громкость применялась именно к этому объекту
        fxVolumeRTPC.SetValue(_audioObject, fxSlider.value);
        musicVolumeRTPC.SetValue(gameObject, musicSlider.value);
        uiVolumeRTPC.SetValue(gameObject, uiSlider.value);

        // Подписываемся на изменение слайдеров
        fxSlider.onValueChanged.AddListener(OnFxVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        uiSlider.onValueChanged.AddListener(OnUiVolumeChanged);
    }

    private void OnFxVolumeChanged(float value)
    {
        fxVolumeRTPC.SetValue(_audioObject, value);
        PlayerPrefs.SetFloat(FX_PREF, value);
        PlayerPrefs.Save();
    }

    private void OnMusicVolumeChanged(float value)
    {
        musicVolumeRTPC.SetValue(gameObject, value);
        PlayerPrefs.SetFloat(MUSIC_PREF, value);
        PlayerPrefs.Save();
    }

    private void OnUiVolumeChanged(float value)
    {
        uiVolumeRTPC.SetValue(gameObject, value);
        PlayerPrefs.SetFloat(UI_PREF, value);
        PlayerPrefs.Save();
    }
}
