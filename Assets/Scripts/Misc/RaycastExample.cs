using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public float raycastLength = 10f;

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            // Create a ray pointing down from the current game object
            Vector3 rayDirection = Vector3.down;
            Vector3 rayStart;
            if(i==0)
            {
                 rayStart = transform.position + transform.right * (- 1.5f);
            }
                
            else if (i==1)
            {
                 rayStart = transform.position + transform.right * (1.5f);
            }
            else if (i==2)
            {
                rayStart = transform.position + transform.forward * (- 1.5f);
            }
            else
            {
                rayStart = transform.position + transform.forward * (1.5f);
            }
                
            Ray ray = new Ray(rayStart, rayDirection);
            Debug.DrawRay(ray.origin, ray.direction * raycastLength, Color.red);

            // Perform the raycast and check if it hits something
            if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))
            {
                // Get the hit normal and calculate the rotation based on it
                Vector3 hitNormal = hit.normal;
                Quaternion hitRotation = Quaternion.FromToRotation(Vector3.up, hitNormal);

                // Convert the quaternion to a Vector3 rotation
                Vector3 eulerAngles = hitRotation.eulerAngles;
                Debug.Log("Hit rotation: "+i+" " + eulerAngles);
            }
        }
    }
}
