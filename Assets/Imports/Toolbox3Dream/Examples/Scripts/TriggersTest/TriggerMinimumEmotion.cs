using Toolbox3Dream;
using System.Linq;

/// <summary>
/// Example of trigger that detect the maximum intensity level among a list of emotions and send the data when triggered.
/// </summary>
public class TriggerMinimumEmotion : TriggerTimed
{
    protected override void SetDataTriggerValues(DataTrigger data)
    {
        float[] emotionsLevel = new float[] { Emotions[EmotionalType.Anger].IntensityLevel,
            Emotions[EmotionalType.Happiness].IntensityLevel,
            Emotions[EmotionalType.Sadness].IntensityLevel,
            Emotions[EmotionalType.Fear].IntensityLevel,
        };

        float minValue = emotionsLevel.Min();
        int minIndex = emotionsLevel.ToList().IndexOf(minValue);

        data.EmotionType = (EmotionalType) (minIndex + 1); // Translate number to enum omitting first literal of enum
        data.InterpolationValue = minValue;
    }
}
