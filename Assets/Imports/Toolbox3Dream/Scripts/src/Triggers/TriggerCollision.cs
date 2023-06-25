using UnityEngine;

namespace Toolbox3Dream
{
    /// <summary>
    /// This abstract class allows to generate new triggers that are activated when a collider associated is entered, and send data to the observers
    /// based on the modifications done in the abstract method SetDataTriggerValues()
    /// </summary>
    public abstract class TriggerCollision : BaseTrigger
    {
        public TriggerCollision() : base (TriggerType.Collision)
        {

        }

        /// <summary>
        /// Function to conditionate the emition of the trigger depending on the collision parameters.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        protected virtual bool ConditionsToActivateTrigger(Collider other)
        {
            if (other.gameObject.name =="XR Origin")
                return true;
            else
                return false;
        }

        /**************
         * UNITY
         **************/
        void Start()
        {
            if (Id.Length < 1 || Id == null)
                Debug.LogError("Should define a string Id for the triggers");
            else
                data.TriggerId = Id;

            if (gameObject.GetComponent<Collider>() == null)
                Debug.LogError("Collider not found in " + gameObject.name + ". Is mandatory to run Triggers controlled by colliders");
        }

        private void OnTriggerStay(Collider other)
        {
            // Trigger 3Dream is activated when collider is entered
            if(ConditionsToActivateTrigger(other))
                ActivateTrigger3Dream();
        }

        private void OnTriggerExit(Collider other)
        {
            IsActive = false;
        }
    }
}
