using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SandClock : MonoBehaviour
{
    [SerializeField] private Image imageToDecrease;
    private float fillDuration = 120f; // Duration in seconds to reach 0 fill amount
    private float fillAmount;

    void Start()
    {
        // Ensure the image is filled at the beginning
        fillAmount = 1f;
        imageToDecrease.fillAmount = fillAmount;

        // Start the coroutine to decrease the fill amount
        StartCoroutine(DecreaseFillAmount());
    }

    IEnumerator DecreaseFillAmount()
    {
        while (fillAmount > 0)
        {
            // Calculate the fill amount decrease per frame
            float decreasePerFrame = Time.deltaTime / fillDuration;

            // Decrease the fill amount
            fillAmount -= decreasePerFrame;
            imageToDecrease.fillAmount = fillAmount;

            // Wait for the next frame
            yield return null;
        }
    }
}
