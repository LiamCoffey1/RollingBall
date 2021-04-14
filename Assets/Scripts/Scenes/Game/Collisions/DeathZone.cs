using UnityEngine;

namespace Assets.Scripts
{
    class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Game.GetSingleton().OnGameFinished?.Invoke(player.coins, player.StagesPassed);
            }
        }
    }
}
