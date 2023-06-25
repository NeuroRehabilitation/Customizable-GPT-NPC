using UnityEngine;
using UnityEngine.EventSystems;

namespace Toolbox3Dream
{
    /// <summary>
    /// Interface that has to be implemented by a GameObject in order to become an observer of 3DreamTriggers.
    /// </summary>

    public interface ITriggered3Dream : IEventSystemHandler
    {
        // Allow to an object react to a triggered event in 3Dream Toolbox
        void OnTrigger3Dream(DataTrigger data);
    }
}