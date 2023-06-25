using UnityEngine;

namespace Toolbox3Dream
{
    /// <summary>
    /// This abstract class allows to create physiological descriptors based on sensor data.
    /// For instance, DescriptorHearthRateVariability is an inherited class that describes hearth rate data based on one or multiple hardware devices.
    /// </summary>
    
    public abstract class BasePhysiologicalDescriptor
    {
        // Type of the physiological descriptor
        private PhysiologicalDescriptorType type = PhysiologicalDescriptorType.Null;
        public PhysiologicalDescriptorType Type
        {
            get { return type; }
            set { type = value; }
        }

        // Value of the physiological descriptr
        private float currentValue = 0.0f;
        public float CurrentValue
        {
            get
            {
                // Read the current value of the physiological descriptor
                currentValue = UpdateDescriptorValue();
                ////////////////////////////////////////////////////////
                return currentValue;
            }
        }

        // Minimum value of the descriptor
        private float minValue = 0.0f;
        public float MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }

        // Maximum value of the descriptor
        private float maxValue = 1.0f;
        public float MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        // Sampling frequency of the sensors
        private float samplingFrequency = 1.0f;
        public float SamplingFrequency
        {
            get { return samplingFrequency; }
            set { samplingFrequency = value; }
        }

        // Time window (in seconds) for analyzing input sensors before process data
        private float timeWindow = 10.0f;
        public float TimeWindow
        {
            get { return timeWindow; }
            set { timeWindow = value; }
        }

        // Conditions to set the intensity level. This is the only place where intensityLevel should (and must) be set
        protected abstract float UpdateDescriptorValue(); 


        /*****************
         * CONSTRUCTOR
         ****************/
        public BasePhysiologicalDescriptor()
        {
            Type = PhysiologicalDescriptorType.Null;
            currentValue = 0.0f;
        }

        public BasePhysiologicalDescriptor(PhysiologicalDescriptorType _type)
        {
            Type = _type;
            currentValue = 0.0f;
            
        }
    }
}
