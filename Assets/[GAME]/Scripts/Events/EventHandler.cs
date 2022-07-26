using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rise
{
    #region Player Input

    public static class EventHandler
    {
        // Pointer Click
        public static event Action<Vector3> PointerClickEvent;

        public static void CallPointerClickEvent(Vector3 mousePosition)
        {
            PointerClickEvent?.Invoke(mousePosition);
        }
        
        // Terrain Selected
        public static event Action<GameObject> TerrainSelectedEvent;

        public static void CallTerrainSelectedEvent(GameObject hex)
        {
            TerrainSelectedEvent?.Invoke(hex);
        }
    }

    #endregion
    
}
