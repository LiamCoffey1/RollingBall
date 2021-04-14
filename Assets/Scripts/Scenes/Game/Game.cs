using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    #region Singleton Access
    private static Game game;
    public static Game GetSingleton()
    {
        return game;
    }
    #endregion

    #region Exposed Input
    public GameObject prefab;
    public GameObject holePrefab;
    public GameObject coinPrefab;

    public GameObject Camera;

    public AudioSource AudioSource;

    public GameObject Player;

    private GameObject removeTile;
    #endregion

    #region Private Variables
    private float horizontal = 0, vertical = 0;
    private bool GameActive = false;
    #endregion

    public Action<int, int> OnGameFinished;
    public Action<Player> OnStagePassed;
    public Action<Player> OnCoinCollected;



    private Platforms platforms;

    public Platforms GetPlatforms()
    {
        return platforms;
    }

    public GameObject GetCurrentPlatform()
    {
        return platforms.GetCurrentStage();
    }
    public GameObject GetPreviousPlatform()
    {
        return platforms.GetPreviousStage();
    }
    public GameObject GetNextPlatform()
    {
        return platforms.GetNextStage();
    }


    public void HandleTouches()
    {
        int nbTouches = Input.touchCount;
        if (nbTouches > 0)
        {
            for (int i = 0; i < nbTouches; i++)
            {
                Touch touch = Input.GetTouch(i);

                TouchPhase phase = touch.phase;

                switch (phase)
                {
                    case TouchPhase.Began:
                        print("New touch detected at position " + touch.position + " , index " + touch.fingerId);
                        if (touch.position.y < Screen.height / 3)
                        {
                            vertical = -1;
                            break;
                        }
                        if (touch.position.y > (Screen.height / 3) * 2)
                        {
                            vertical = 1;
                            break;
                        }
                        if (touch.position.x < Screen.width / 3)
                        {
                            horizontal = 1;
                        }
                        if (touch.position.x > (Screen.width / 3) * 2)
                        {
                            horizontal = -1;
                        }

                        break;
                    case TouchPhase.Ended:
                        if (touch.position.x < Screen.width / 3)
                        {
                            horizontal = 0;
                        }
                        if (touch.position.x > (Screen.width / 3) * 2)
                        {
                            horizontal = 0;
                        }
                        if (touch.position.y < Screen.height / 3)
                        {
                            vertical = 0;
                        }
                        if (touch.position.y > (Screen.height / 3) * 2)
                        {
                            vertical = 0;
                        }
                        break;
                }
            }
        }
    }


    void RotatePlatform()
    {
        GameObject currentStage = GetCurrentPlatform();
        if (vertical > 0 && currentStage.transform.rotation.x > 0.17f
           || vertical < 0 && currentStage.transform.rotation.x < -0.17f)
        {
            vertical = 0;
        }
        if (horizontal < 0 && currentStage.transform.rotation.z < -0.17f
            || horizontal > 0 && currentStage.transform.rotation.z > 0.17f)
        {
            horizontal = 0;
        }
        currentStage.transform.Rotate(vertical, 0, horizontal);
    }


    #region Game Events

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadCharacter()
    {
        string SavedName = PlayerPrefs.GetString("MaterialName");
        string MaterialName = SavedName != "" ? SavedName : "Red";
        Material material = (Material)Resources.Load(MaterialName);
        Player.GetComponent<MeshRenderer>().material = material;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        GameActive = true;
        game = this;
    }


    void Start()
    {
        LoadCharacter();
        platforms = GetComponent<Platforms>();
    }



    // Update is called once per frame
    void Update()
    {
        HandleTouches();
        //vertical = Input.GetAxis("Vertical");
        //horizontal = Input.GetAxis("Horizontal");
        if (GameActive)
            RotatePlatform();
    }
    #endregion
}
