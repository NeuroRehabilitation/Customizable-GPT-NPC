using UnityEngine;
using UnityEngine.InputSystem;

public class UserPresenceDetector : MonoBehaviour
{
    // Reference to the Camera.
    public Camera mainCamera;

    // Last recorded position of the camera
    Vector3 lastPosition;

    // Minimum amount of movement needed to reset the timer.
    private float movementThreshold = 0.0001f;

    // Time to wait before assuming headset is off.
    public float assumedOffTime = .5f;

    // Track when the last movement was detected.
    private float lastMovementTime;

    // Track the current state of the headset.
    private bool isHMDOn = true;

    private void Update()
    {
        // Retrieve current camera position.
        Vector3 currentPosition = mainCamera.transform.position;

        // Check if camera has moved.
        if (Vector3.Distance(lastPosition, currentPosition) > movementThreshold)
        {
            // Record the time of the last movement.
            lastMovementTime = Time.time;
            // Check if the headset is currently off.
            if (!isHMDOn)
            {
                isHMDOn = true;
                LogHMDState("HMD ON");
            }
        }

        // If the time since the last movement exceeds our threshold, we'll assume the headset is off.
        if (Time.time - lastMovementTime > assumedOffTime && isHMDOn)
        {
            isHMDOn = false;
            LogHMDState("HMD OFF");
        }

        // Update last recorded position.
        lastPosition = currentPosition;
    }

    private void LogHMDState(string state)
    {
        // Note: It is generally bad practice to use GameObject.Find in an Update function
        // because it is a relatively slow operation. It would be better to cache the 
        // Logger and StateController components in Start or Awake.
        GameObject.Find("StateController").GetComponent<Logger>().logEvent(GameObject.Find("StateController").GetComponent<StateController>().currScene, state);
    }
}
