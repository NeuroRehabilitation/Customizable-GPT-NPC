using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerReticleHitPosition : MonoBehaviour
{
    private XRRayInteractor rayInteractor;
    public bool deploy = false;
    public float timer = 5f;
    public GameObject currentItem;

    void Start()
    {
        // Get a reference to the XRRayInteractor component attached to this game object
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    void Update()
    {
        // Check if the ray is hitting anything
        bool hitSomething = rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

        // If the ray hit something, get the hit position and do something with it
        if (hitSomething)
        {
            Vector3 hitPosition = hit.point;
            // Check if deploy is true and start a timer to spawn the object on the hit pos
            if (deploy)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    deploy = false;
                    timer=5f;
                    Instantiate (currentItem,new Vector3 (hitPosition.x,hitPosition.y+5f,hitPosition.z), Quaternion.identity);
                }
            }
        }

        
    }
}
