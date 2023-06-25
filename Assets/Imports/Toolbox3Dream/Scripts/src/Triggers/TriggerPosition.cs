using UnityEngine;

namespace Toolbox3Dream
{
    /// <summary>
    /// This abstract class allows to generate new triggers that are activated when a collider associated is entered, and send data to the observers
    /// based on the modifications done in the abstract method SetDataTriggerValues()
    /// </summary>
    public abstract class TriggerPosition : BaseTrigger
    {
        public GameObject player;
        public TriggerPosition() : base (TriggerType.Position)
        {

        }

        /**************
         * UNITY
         **************/
        void Start()
        {
            player = GameObject.Find("VRObject");
            if (Id.Length < 1 || Id == null)
                Debug.LogError("Should define a string Id for the triggers");
            else
                data.TriggerId = Id;

            if (gameObject.GetComponent<Collider>() == null)
                Debug.LogError("Collider not found in " + gameObject.name + ". Is mandatory to run Triggers controlled by colliders");
        }

        void Update()
        {
            if (player.transform.position.x > 0)
                ActivateTrigger3Dream();
        }
    }
}
