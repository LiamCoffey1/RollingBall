using UnityEngine;

namespace Assets.Scripts
{
    class HoleCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.StagesPassed++;
                Game.GetSingleton().OnStagePassed?.Invoke(player);
            }
        }
    }
}
