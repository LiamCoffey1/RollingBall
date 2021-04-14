using Assets.Scripts.Scenes.Save;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region Singleton Access
    private static ShopManager shop;

    public static ShopManager GetSingleton()
    {
        return shop;
    }
    #endregion

    #region Injectable Variables
    public TMPro.TextMeshProUGUI CoinText;
    #endregion

    private ShopData ShopData;

    public void SetCoinAmount()
    {
        CoinText.text = PlayerPrefs.GetInt("coins").ToString();
    }

}
