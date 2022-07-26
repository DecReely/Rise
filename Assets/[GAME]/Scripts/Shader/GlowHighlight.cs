using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rise
{
    public class GlowHighlight : MonoBehaviour
    {
        private Dictionary<Renderer, Material[]> _glowMaterialDictionary = new Dictionary<Renderer, Material[]>();
        private Dictionary<Renderer, Material[]> _originalMaterialDictionary = new Dictionary<Renderer, Material[]>();

        private Dictionary<Material, Material> _cachedGlowMaterials = new Dictionary<Material, Material>();

        [SerializeField] private Material glowMaterial;

        private bool _isGlowing = false;
        
        private Color _validSpaceColor = Color.green;
        private Color _originalGlowColor;

        private void Awake()
        {
            PrepareMaterialDictionaries();
            _originalGlowColor = glowMaterial.GetColor("_GlowColor");
        }

        private void PrepareMaterialDictionaries()
        {
            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            {
                Material[] originalMaterials = renderer.materials;
                _originalMaterialDictionary.Add(renderer, originalMaterials);
                Material[] newMaterials = new Material[renderer.materials.Length];
                for (int i = 0; i < originalMaterials.Length; i++)
                    {
                        Material mat = null;
                        if (_cachedGlowMaterials.TryGetValue(originalMaterials[i], out mat) == false)
                        {
                            mat = new Material(glowMaterial);
                            _cachedGlowMaterials[mat] = mat;
                        }

                        newMaterials[i] = mat;
                    }
                    
                    _glowMaterialDictionary.Add(renderer,newMaterials);
            }
        }

        public void ToggleGlow(bool state)
        {
            if (_isGlowing == state)
                return;
            else
            {
                _isGlowing = !state;
                ToggleGlow();
            }
        }

        public void ToggleGlow()
        {
            if (!_isGlowing)
            {
                ResetGlowHighlight();
                foreach (Renderer renderer in _originalMaterialDictionary.Keys)
                {
                    renderer.materials = _glowMaterialDictionary[renderer];
                }
            }
            
            else
            {
                foreach (Renderer renderer in _originalMaterialDictionary.Keys)
                {
                    renderer.materials = _originalMaterialDictionary[renderer];
                }
            }

            _isGlowing = !_isGlowing;
        }

        public void ResetGlowHighlight()
        {
            foreach (Renderer renderer in _glowMaterialDictionary.Keys)
            {
                foreach (Material material in _glowMaterialDictionary[renderer])
                {
                    material.SetColor("_GlowColor", _validSpaceColor);
                }
            }
        }

        public void HighlightValidPath()
        {
            foreach (Renderer renderer in _glowMaterialDictionary.Keys)
            {
                foreach (Material material in _glowMaterialDictionary[renderer])
                {
                    material.SetColor("_GlowColor", _validSpaceColor);
                }
            }
        }
    }
}
