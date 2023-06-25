using System;
using System.Collections.Generic;

namespace Toolbox3Dream
{
    /// <summary>
    /// This abstract class allows to create emotions that are used to generate triggers conditions.
    /// For instance, a BaseTrigger object can be send when the emotion EmotionHappiness is greater than other emotions in an array.
    /// </summary>

    public abstract class BaseEmotion
    {

        // Type of the emotion
        private EmotionalType type = EmotionalType.Null;
        public EmotionalType Type
        {
            get { return type; }
            set { type = value; }
        }

        // Level of the classified emotion
        private float intensityLevel = 0.0f;
        public float IntensityLevel
        {
            get
            {
// DEBUG: To Debug the data allowing to IntensityValue be modified from outside
                // Read physiological descriptors to update the value of emotional intensity
                //intensityLevel = UpdateEmotionalIntensity();
// DEBUG
                ////////////////////////////////////////////////////////
                return intensityLevel;
            }
            set
            {
                intensityLevel = value;
            }
        }

        // Minimum possible value of the emotion's intensity
        private float minValue = 0.0f;
        public float MinValue 
        {
            get
            { return this.minValue; }
            set
            { this.minValue = value; }
        }

        // Maximum possible value of the emotion's intensity
        private float maxValue = 1.0f;
        public float MaxValue
        {
            get { return this.maxValue; }
            set { this.maxValue = value; }
        }

        // Physiological descriptors that are used to verify if an emotional state is present
        private List<BasePhysiologicalDescriptor> descriptors;
        public List<BasePhysiologicalDescriptor> Descriptors
        {
            get { return descriptors; }
            set { descriptors = value; }
        }

        // Conditions to set the intensity level. This is the only place where intensityLevel should (and must) be set
        protected abstract float UpdateEmotionalIntensity();

        /**************
         * CONSTRUCTOR
         **************/

        public BaseEmotion(EmotionalType _type)
        {
            Type = _type;
            Descriptors = new List<BasePhysiologicalDescriptor>();
            intensityLevel = 0.0f;
        }
    }

}
