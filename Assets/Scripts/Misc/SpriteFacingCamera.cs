using UnityEngine;
using System.Collections;
public class SpriteFacingCamera : MonoBehaviour
{
    public float updateInterval = 1f;
    private WaitForSeconds wait;

    private void Start()
    {
        wait = new WaitForSeconds(updateInterval + Random.Range(-0.5f, 0.5f));
        StartCoroutine(RotateSprite());
    }

    private IEnumerator RotateSprite()
    {
        while(true)
        {
            transform.LookAt(Camera.main.transform, Camera.main.transform.up);
            yield return wait;
        }
    }
}
