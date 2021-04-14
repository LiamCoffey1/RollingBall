using Assets.Scripts.Scenes.Save;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    #region Injectable Variables
    public GameObject SoundImage;
    public Sprite VolumeOn;
    public Sprite VolumeOff;
    #endregion

    #region Private Variables
    private bool SoundEnabled;
    #endregion

    #region Button Actions
    public void ToggleVolume()
    {
        SoundEnabled = PlayerPrefs.GetString("SoundEnabled").Equals("true");
        PlayerPrefs.SetString("SoundEnabled", SoundEnabled ? "false" : "true");
        SoundImage.GetComponent<UnityEngine.UI.Image>().sprite = SoundEnabled ? VolumeOff : VolumeOn;

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    void Start()
    {
        SoundEnabled = PlayerPrefs.GetString("SoundEnabled").Equals("true");
        SoundImage.GetComponent<UnityEngine.UI.Image>().sprite = SoundEnabled ? VolumeOn : VolumeOff;
        SaveManager.Instance.LoadGame();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
