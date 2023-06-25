using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    public GameObject coinMenu; // Assign the coinMenu GameObject in the Unity Inspector
    public GameObject coin;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is triggered by an object with the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Set the "coinMenu" GameObject active
            coinMenu.SetActive(true);
            coin.SetActive(false);
        }
    }
}
