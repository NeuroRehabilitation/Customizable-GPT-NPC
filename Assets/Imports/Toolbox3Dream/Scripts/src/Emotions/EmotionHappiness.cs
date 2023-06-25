namespace Toolbox3Dream
{
    /// <summary>
    /// Conditions that defines the Happiness emotional state from physiological sensor array.
    /// </summary>
    public class EmotionHappiness : BaseEmotion
    {
        public EmotionHappiness() : base(EmotionalType.Happiness)
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
