using Assets.Scripts;
using UnityEngine;

public class Platforms : MonoBehaviour
{

    private Game g;

    static readonly int MAX_PLATFORMS = 2;

    [SerializeField]
    private GameObject stagePrefab;
    [SerializeField]
    private GameObject holePrefab;
    [SerializeField]
    private GameObject coinPrefab;

    private Vector3 StartPosition;


    private GameObject PreviousStage;
    private GameObject CurrentStage;
    private GameObject NextStage;

    public GameObject GetPreviousStage()
    {
        return PreviousStage;
    }
    public GameObject GetCurrentStage()
    {
        return CurrentStage;
    }
    public GameObject GetNextStage()
    {
        return NextStage;
    }

    private GameObject[] PlatformPool = new GameObject[MAX_PLATFORMS];

    private void Start()
    {
        g = Game.GetSingleton();
        FillPlatformPool();
        g.OnStagePassed += (Player) =>
        {
            GoToNextStage(Player);
        };
    }

    private void OnDisable()
    {
        g.OnStagePassed -= (Player) =>
        {
            GoToNextStage(Player);
        };
    }

    public GameObject GetAvailablePlatform()
    {
        foreach (GameObject platform in PlatformPool)
        {
            if (!platform.activeInHierarchy)
            {
                platform.SetActive(true);
                platform.transform.eulerAngles = new Vector3(0, 0, 0);
                return platform;
            }
        }
        return null;
    }

    private void FillPlatformPool()
    {
        for (int i = 0; i < MAX_PLATFORMS; i++)
        {
            PlatformPool[i] = CreateStage(0, -(i * 2), 0);
            PlatformPool[i].SetActive(false);
        }
        CurrentStage = PlatformPool[0];
        NextStage = PlatformPool[1];
        CurrentStage.SetActive(true);
        NextStage.SetActive(true);

    }


    GameObject CreateGameObject(Material material, GameObject prefab, Vector3 postion, Quaternion rotation)
    {
        GameObject g;
        g = Instantiate(prefab, postion, rotation);
        if(material != null)
            g.transform.GetChild(0).GetComponent<MeshRenderer>().material = material;
        return g;
    }

    public GameObject CreateStage(float x, float y, float z)
    {
        GameObject Stage;

        string PlatformSkin = PlayerPrefs.GetString("PlatformSkin");
        string PlatformSkinName = PlatformSkin != "" ? PlatformSkin : "Ground";
        Material PlatformMaterial = (Material)Resources.Load(PlatformSkinName);

        Stage = CreateGameObject(PlatformMaterial, stagePrefab, new Vector3(x, y, z), Quaternion.identity);

        string HoleSkin = PlayerPrefs.GetString("HoleSkin");
        string HoleSkinName = HoleSkin != "" ? HoleSkin : "Yellow-Trans";
        Material HoleMaterial = (Material)Resources.Load(HoleSkinName);

        var hole = CreateGameObject(HoleMaterial, holePrefab, new Vector3(Random.Range(-2.5f, 2.5f), y + 0.03f, Random.Range(-2.5f, 2.5f)), Quaternion.identity);
        hole.transform.parent = Stage.transform;
        hole.transform.Rotate(0, 0, 90);

        var coin = CreateGameObject(null, coinPrefab, new Vector3(Random.Range(-2, 2), y + 0.3f, Random.Range(-2, 2)), Quaternion.identity);
        coin.transform.parent = Stage.transform;

        return Stage;
    }

    Vector3 GetRandom(float y, float offset)
    {
        float range = 2.5f;
        return new Vector3(
            Random.Range(-range, range),
            y + offset,
            Random.Range(-range, range)
        );
    }

    public void GoToNextStage(Player player)
    {
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;

            PreviousStage = CurrentStage;
            PreviousStage.SetActive(false);
            CurrentStage = NextStage;
            NextStage = GetAvailablePlatform();
       
            float CurrentPlatformY = PreviousStage.transform.position.y;
            GameObject coin = NextStage.transform.GetChild(2).gameObject;
            coin.transform.position = GetRandom(CurrentPlatformY, 0.03f);

            GameObject hole = NextStage.transform.GetChild(3).gameObject;
            hole.transform.position = GetRandom(CurrentPlatformY, 0.3f);

            NextStage.SetActive(true);
            NextStage.transform.position = new Vector3(0, CurrentStage.transform.position.y - 2, 0);

    }




  



}
