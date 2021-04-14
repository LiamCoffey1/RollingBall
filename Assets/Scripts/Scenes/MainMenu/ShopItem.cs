using Assets.Scripts.Scenes.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    public TMPro.TextMeshProUGUI NameText = null;
    public TMPro.TextMeshProUGUI PriceText = null;
    public GameObject PriceArea;
    public Image SelectedImage;

    public string type;

    private ShopData.DATA_MODEL DATA;


    void Start()
    {
        ShopData s = SaveManager.Instance.GetSave().ShopData;
        DATA = s.GetByTypeName(type)[id];
        PriceText.text = DATA.price.ToString();
        NameText.text = DATA.name;
        if (DATA.owned)
           PriceArea.SetActive(false);
        else NameText.gameObject.SetActive(false);
    }

    private bool CanAffordItem(int price)
    {
        return (PlayerPrefs.GetInt("coins") - price) >= 0;
    }

    public void BuyItem()
    {
        if(!CanAffordItem(DATA.price))
        {
            return;
        }
        Save s = SaveManager.Instance.GetSave();
        s.ShopData.GetByTypeName(type)[id].owned = true;
        PriceArea.SetActive(false);
        NameText.gameObject.SetActive(true);
        DATA.owned = true;
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - DATA.price);
        ShopManager.GetSingleton().SetCoinAmount();
        SaveManager.Instance.SetSave(s);
        SaveManager.Instance.SaveGame();
    }

    public void EnableItem()
    {
        PlayerPrefs.SetString(type, DATA.material);
    }

    public void OnSelect()
    {
        if (DATA.owned)
            EnableItem();
        else BuyItem();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetString(type) == DATA.material)
            SelectedImage.color = new Color32(56, 255, 0, 255);
        else SelectedImage.color = new Color32(91, 171, 169, 255);
    }
}
