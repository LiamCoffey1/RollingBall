using UnityEngine;

namespace Assets.Scripts
{
    class CoinCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.coins++;
                gameObject.SetActive(false);
                Game.GetSingleton().OnCoinCollected?.Invoke(player);
            }
        }
    }
}