using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rise
{
    public class SelectionManager : MonoBehaviour
    {
        private Camera _mainCamera;
        [SerializeField] private LayerMask selectionMask;

        private HexGrid _hexGrid;
        private List<Vector3Int> neighbours = new List<Vector3Int>();

        private void OnEnable()
        {
            EventHandler.PointerClickEvent += HandleClick;
        }

        private void OnDisable()
        {
            EventHandler.PointerClickEvent -= HandleClick;
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _hexGrid = FindObjectOfType<HexGrid>();
        }

        private void HandleClick(Vector3 mousePosition)
        {
            GameObject result;
            if (FindTarget(mousePosition, out result))
            {
                Hex selectedHex = result.GetComponent<Hex>();
                
                selectedHex.DisableHighlight();

                foreach (Vector3Int neighbour in neighbours)
                {
                    _hexGrid.GetTileAt(neighbour).DisableHighlight();
                }

                neighbours = _hexGrid.GetNeighboursFor(selectedHex.HexCoordinates);

                foreach (Vector3Int neighbour in neighbours)
                {
                    _hexGrid.GetTileAt(neighbour).EnableHighlight();
                }
            }
            /*
            GameObject result;
            if (FindTarget(mousePosition, out result))
            {
                if (IsTerrainSelected(result))
                {
                    EventHandler.CallTerrainSelectedEvent(result);
                }
            }
            */
        }

        private bool IsTerrainSelected(GameObject result)
        {
            return result.GetComponent<Hex>() != null;
        }

        private bool FindTarget(Vector3 mousePosition, out GameObject result)
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit, 100, selectionMask))
            {
                result = hit.collider.gameObject;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
    }
}
