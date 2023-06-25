using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox3Dream
{
    /// <summary>
    /// Possible physiological descriptors. Please add new literals to this enumeration when new classes inherited by BasePhysiologicalDescriptor are implemented.
    /// </summary>
    public enum PhysiologicalDescriptorType
    {
        Null,
        HearthRate,
        RespiratorySinusArrhythmia,
        HeartRateVariability_PowerHF,
        // INFO: Add new literals here if needed, an implement a new class inheriting from BasePhysiologicalDescriptor with the proper PhysiologicalDescriptorType field
    }

    /// <summary>
    /// Possible emotions. Please add new literals to this enumeration when new classes inherited by BaseEmotion are implemented.
    /// </summary>
	public enum EmotionalType {
        Null,
		Anger,
		Happiness,
		Sadness,
		Fear,
        // INFO: Add new literals here if needed, an implement a new class inheriting from BaseEmotion with the proper EmotionalType field
    }

    /// <summary>
    /// Possible types of trigger. Please add new literals to this enumeration when new classes inherited by BaseTrigger are implemented.
    /// </summary>
    public enum TriggerType
    {
        Null,
        TimeBased,
        Collision,
        Position,
        // INFO: Add new literals here if needed, an implement a new class inheriting from BaseTrigger with the proper TriggerType field
    }
}



