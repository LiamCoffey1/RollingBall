using System.Collections;
using UnityEngine;

class CameraHandler : MonoBehaviour
{

    private Vector3 StartPosition;
    private Game game;

    void SubscribeToGameEvent()
    {
        game = Game.GetSingleton();
        game.OnStagePassed += (Player) =>
        {
            MoveToNextPlatform();
        };
    }

    private void Start()
    {
        SubscribeToGameEvent();
    }

    IEnumerator MoveCameraTo(Vector3 EndPosition)
    {
        float rate = 2f;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            game.Camera.transform.position = Vector3.Slerp(StartPosition, EndPosition, t);
            yield return null;
        }
    }

    int GetNextYValue(float cameraY, float GameY, float OFFSET)
    {
        Debug.Log((cameraY - OFFSET) - GameY);
        return (int)(game.GetCurrentPlatform().transform.position.y + OFFSET);
        if (((cameraY-OFFSET) - GameY) >= 2)
            return (int)(game.GetNextPlatform().transform.position.y - OFFSET);
        //else return (int)(game.GetCurrentPlatform().transform.position.y - OFFSET);
    }

    public void MoveToNextPlatform()
    {
        StopCoroutine("MoveCameraTo");
        StartPosition = game.Camera.gameObject.transform.position;
        float OFFSET = 1f;
        StartCoroutine("MoveCameraTo", new Vector3(StartPosition.x, GetNextYValue(StartPosition.y, game.GetCurrentPlatform().transform.position.y, 1f), StartPosition.z));
    }


}
