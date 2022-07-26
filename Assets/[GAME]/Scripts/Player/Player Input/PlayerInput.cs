using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rise
{
    public class PlayerInput : MonoBehaviour
    {
        private void Update()
        {
            DetectPlayerClick();
        }

        private void DetectPlayerClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                EventHandler.CallPointerClickEvent(mousePos);
            }
        }
    }
}
