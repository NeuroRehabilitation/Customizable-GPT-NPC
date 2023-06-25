using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox3Dream;
using System;

/// <summary>
/// Example of trigger that sends the intensity level of emotion Happiness when triggered.
/// </summary>
public class TriggerPositionTest : TriggerPosition
{
    protected override void SetDataTriggerValues(DataTrigger data)
    {
        data.EmotionType = EmotionalType.Happiness;
        data.InterpolationValue = Emotions[EmotionalType.Happiness].IntensityLevel;
    }
}
