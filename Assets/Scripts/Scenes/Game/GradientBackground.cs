using System.Collections;
using UnityEngine;

    class GradientBackground : MonoBehaviour
    {

    Camera mainCamera;
    public Gradient gradient;
    public GradientColorKey[] colorKey;
    public GradientAlphaKey[] alphaKey;
    private float duration = 20.0f;
    private void OnDisable()
        {
            StopCoroutine("ChangeBackground");
        }


    public void OnEnable()
        {
        mainCamera = Camera.main;
            StartCoroutine("ChangeBackground");
        }


        IEnumerator ChangeBackground()
        {
            while (true)
            {
                float t = Mathf.PingPong(Time.time, duration) / duration;
                mainCamera.backgroundColor = gradient.Evaluate((t) % 1.5f);
                yield return new WaitForEndOfFrame();
            }

        }
    }