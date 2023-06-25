using UnityEngine;

public class Nav : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        transform.rotation = Quaternion.Euler(0f, cameraTransform.rotation.eulerAngles.y + 135f, 0f);
    }
}
