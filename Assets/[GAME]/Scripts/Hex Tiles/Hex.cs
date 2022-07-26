using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace Rise
{
    [SelectionBase]
    public class Hex : MonoBehaviour
    {
        private GlowHighlight _glowHighlight;
        private HexCoordinates _hexCoordiantes;

        public Enums.HexBonusType hexBonusType;

        public Vector3Int HexCoordinates => _hexCoordiantes.GetHexCoordinates();

        public bool IsSameHeight(Hex hex)
        {
            return hex.HexCoordinates.y == this.HexCoordinates.y;
        }

        private void Awake()
        {
            _hexCoordiantes = GetComponent<HexCoordinates>();
            _glowHighlight = GetComponent<GlowHighlight>();
        }

        public void EnableHighlight()
        {
            _glowHighlight.ToggleGlow(true);
        }
        
        public void DisableHighlight()
        {
            _glowHighlight.ToggleGlow(false);
        }

        public void ResetHighlight()
        {
            _glowHighlight.ResetGlowHighlight();
        }

        public void HighlightPath()
        {
            _glowHighlight.HighlightValidPath();
        }
    }
}
