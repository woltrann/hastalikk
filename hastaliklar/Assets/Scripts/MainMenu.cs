using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject MainMenuPanel;
    public GameObject GamePlayPanel;
    public GameObject GameChoosingPanel;
    public GameObject PauseMenuPanel;
    public GameObject SettingsMenuPanel;
    public GameObject GameOverMenuPanel;
    public GameObject[] Games;
    public RectTransform ParentPanel;

    [Header("Sound")]
    public AudioMixer mixer;
    public Slider MusicSlider;
    public Slider SFXSlider;

    [Header("Language")]
    public Button turkish;
    public Button english;

    void Start()
    {
        turkish.onClick.AddListener(() => SetLanguage("tr"));
        english.onClick.AddListener(() => SetLanguage("en"));
    }

    void Update()
    {
        MusicSlider.onValueChanged.AddListener(value => mixer.SetFloat("BGMusicVolume", Mathf.Log10(value) * 20));
        SFXSlider.onValueChanged.AddListener(value => mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20));
    }
    void SetLanguage(string localeCode)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);

    }

    public void GameChoosing() => GameChoosingPanel.SetActive(!GameChoosingPanel.activeSelf);
    public void GamePause() => PauseMenuPanel.SetActive(!PauseMenuPanel.activeSelf);
    public void Settings() => SettingsMenuPanel.SetActive(!SettingsMenuPanel.activeSelf);
    public void GameOver() => GameOverMenuPanel.SetActive(!GameOverMenuPanel.activeSelf);
    public void ParentsPanel () => ParentPanel.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    public void CloseParentPanel() => ParentPanel.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
    
    public void Exit() => Application.Quit();
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    public void GamesOpen(int index)
    {
        GamePlayPanel.SetActive(true);
        switch(index)
        {
            case 1:
                Games[0].SetActive(true);
                break;
            case 2:
                Games[1].SetActive(true);
                break;
            case 3:
                Games[2].SetActive(true);
                break;
            case 4:
                Games[3].SetActive(true);
                break;
            case 5:
                Games[4].SetActive(true);
                break;
        }
    }

}
