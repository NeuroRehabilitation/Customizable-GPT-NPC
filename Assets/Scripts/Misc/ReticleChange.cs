using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReticleChange : MonoBehaviour
{
    private XRRayInteractor rayInteractor;

    private XRInteractorReticleVisual rayVisual;
    public GameObject[] reticles;

    public bool RightHand = true;

    void Start()
    {
        // Get a reference to the XRRayInteractor component attached to this game object
        rayInteractor = GetComponent<XRRayInteractor>();
        rayVisual = GetComponent<XRInteractorReticleVisual>();
    }

    void Update()
    {
        // Check if the ray is hitting anything
        bool hitSomething = rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

        // If the ray hit something, get the hit position and do something with it
        if (hitSomething)
        {
//            print("Direita " + RightHand + "LayerHit "+ LayerMask.LayerToName(hit.transform.gameObject.layer));
            if (RightHand)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain") && rayVisual.reticlePrefab != reticles[0])
                {
                    rayVisual.reticlePrefab = reticles[0];
                }
                if ((hit.transform.gameObject.layer == LayerMask.NameToLayer("UI") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Character") || hit.transform.gameObject.layer == LayerMask.NameToLayer("UINoOverlap")) && rayVisual.reticlePrefab != reticles[1])
                {
                    //print("Direita2 " + RightHand + "LayerHit "+ LayerMask.LayerToName(hit.transform.gameObject.layer));
                    rayVisual.reticlePrefab = reticles[1];
                }
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Grabable") && rayVisual.reticlePrefab != reticles[2])
                {
                    rayVisual.reticlePrefab = reticles[2];
                }
            }
            else
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain") && rayVisual.reticlePrefab != reticles[0])
                {
                    rayVisual.reticlePrefab = reticles[0];
                }
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    rayVisual.reticlePrefab = reticles[1];
                }
            }
            
        }

        
    }
}
