using System.Collections;
using UnityEngine;

public class Spin : MonoBehaviour
{
    private void OnDisable()
    {
        StopCoroutine("SpinObject");
    }

    public void OnEnable()
    {
        StartCoroutine("SpinObject");
    }


    IEnumerator SpinObject()
    {
        while(true)
        {
            transform.Rotate(0, 0.5f, 0);
            yield return new WaitForEndOfFrame();
        }
        
    }
}
