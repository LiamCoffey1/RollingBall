using UnityEngine;

namespace Assets.Scripts
{
    class UI : MonoBehaviour
    {
        #region Exposed Input
        [SerializeField]
        private GameObject player = null;
        [SerializeField]
        private TMPro.TextMeshProUGUI text = null;
        [SerializeField]
        private TMPro.TextMeshProUGUI Score = null;
        [SerializeField]
        private TMPro.TextMeshProUGUI Coins = null;
        [SerializeField]
        private TMPro.TextMeshProUGUI CoinsEnd = null;
        [SerializeField]
        private TMPro.TextMeshProUGUI HighScore = null;
        [SerializeField]
        private TMPro.TextMeshProUGUI TotalCoins = null;
        [SerializeField]
        private GameObject EndGamePanel = null;
        #endregion


        #region UI Methods
        public void ShowEnd(int score, int coins)
        {
            EndGamePanel.SetActive(true);
            Score.text = score.ToString();
           
            HighScore.text = PlayerPrefs.GetInt("highscore").ToString();
            var total = PlayerPrefs.GetInt("coins");
            TotalCoins.text = total.ToString();
            CoinsEnd.text = coins.ToString();
        }
        public void UpdateCoinAmount(Player player)
        {
            Coins.text = player.coins.ToString();
        }
        public void UpdateStagesPassed(Player player)
        {
            text.text = player.StagesPassed.ToString();
        }
        #endregion

        private void SubscribeToGameEvents()
        {
            Game game = Game.GetSingleton();
            game.OnGameFinished += (score, coins) => ShowEnd(score, coins);
            game.OnCoinCollected += (player) => UpdateCoinAmount(player);
            game.OnStagePassed += (player) => UpdateStagesPassed(player);
        }
        private void UnSubscribeToGameEvents()
        {
            Game game = Game.GetSingleton();
            game.OnGameFinished -= ShowEnd;
            game.OnCoinCollected -= UpdateCoinAmount;
            game.OnStagePassed -= UpdateStagesPassed;
        }

        #region Unity Callbacks

        private void Start()
        {
            SubscribeToGameEvents();
        }

        private void OnDisable()
        {
            UnSubscribeToGameEvents();
        }

        #endregion
    }
}
