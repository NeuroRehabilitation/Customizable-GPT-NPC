namespace Toolbox3Dream
{
    /// <summary>
    /// Conditions that defines the Sadness emotional state from physiological sensor array.
    /// </summary>
    public class EmotionSadness : BaseEmotion
    {
        public EmotionSadness() : base(EmotionalType.Sadness)
        {
            //Descriptors.Add(new DescriptorHearthRateVariability());
        }

        protected override float UpdateEmotionalIntensity()
        {
            // Linear interpolation to calculate emotion intensity
            return (Descriptors[0].CurrentValue - Descriptors[0].MinValue) / (Descriptors[0].MaxValue - Descriptors[0].MinValue);
        }
    }

}
