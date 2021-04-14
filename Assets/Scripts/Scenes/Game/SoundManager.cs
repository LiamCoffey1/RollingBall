using Assets.Scripts;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region Exposed Input
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClipArray;
    #endregion

    private void SubscribeToGameSoundEvents()
    {
        Game.GetSingleton().OnCoinCollected += (player) => audioSource.PlayOneShot(audioClipArray[(int)SOUNDS.COIN_COLLECT]);
        Game.GetSingleton().OnStagePassed += (player) => audioSource.PlayOneShot(audioClipArray[(int)SOUNDS.HOLE_COLLISION]);
        Game.GetSingleton().OnGameFinished += (x, y) => audioSource.PlayOneShot(audioClipArray[(int)SOUNDS.GAME_OVER]);
    }

    #region Unity Callbacks
    void Start()
    {
        var enabled = PlayerPrefs.GetString("SoundEnabled").Equals("true");
        if(!enabled)
        {
            audioSource.volume = 0;
        }
        SubscribeToGameSoundEvents();
    }


    #endregion
}
