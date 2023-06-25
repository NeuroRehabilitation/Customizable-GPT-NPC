using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class raycastEyes : MonoBehaviour
{
    public float maxDistance = 100f;
    public LayerMask layerMask;
    public string agentTag = "Agent";
    public float timeHittingAgent = 0f;
    public float timeHittingOther = 0f;
    public float lookingAtAgentPercentage = 0f;
    public TextMeshProUGUI percentage;

    void Update() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Draw the ray in the scene view
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.green);

        if (Physics.Raycast(ray, out hit, maxDistance, layerMask)) {
            if (hit.collider.CompareTag(agentTag)) {
                timeHittingAgent += Time.deltaTime;
            } else {
                timeHittingOther += Time.deltaTime;
            }
        }
    }
}



