using TMPro;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using com.eyerunnman.gridsystem.Enums;

namespace com.eyerunnman.gridsystem.Editor
{
    [ExecuteAlways]
    public class EditorGridTile : GridTileObject
    {
        [SerializeField]
        private Color defaultColor;

        private new Renderer renderer;
        private MaterialPropertyBlock materialPropertyBlock;

        [SerializeField]
        private List<TileTypeDebug> TileTypeDebugColors;

        private Color currentColor;

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
            materialPropertyBlock = new();
        }
        private void Reset()
        {
            renderer = GetComponent<Renderer>();
            materialPropertyBlock = new();
        }

        public override void OnTileDataInitialize()
        {
            SetPostition(TileData);
            OnUpdateTileType(TileData.Type);
        }

        public override void OnTileDataUpdate()
        {
            SetPostition(TileData);
            OnUpdateTileType(TileData.Type);
        }

        private void SetPostition(IGridTileData tileData)
        {
            transform.localPosition = tileData.Center;
            gameObject.name = "Tile : " + tileData.Coordinates;
            transform.up = tileData.UpVector;
        }

        private void OnUpdateTileType(GridTileType tileType)
        {
            currentColor = defaultColor;

            foreach (TileTypeDebug tileTypeDebug in TileTypeDebugColors)
            {
                if (tileTypeDebug.TileType == tileType)
                {
                    currentColor = tileTypeDebug.color;
                    break;
                }
            }

            renderer.GetPropertyBlock(materialPropertyBlock);
            
            materialPropertyBlock.SetColor("_BaseColor", currentColor);
            
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
            
        private void OnDrawGizmosSelected()
        {

            GUIContent tileName = new GUIContent
            {
                text = gameObject.name
            };

            GUIStyle style = new GUIStyle
            {
                fontStyle = FontStyle.BoldAndItalic,
                fontSize = 16
            };
            style.normal.textColor = Color.cyan;
            
            Handles.Label(transform.position + Vector3.up/2, tileName, style);

            Gizmos.color = currentColor;

            Vector3 firstVertex = TileData.TopLeftVertex;
            Vector3 secondVertex = TileData.TopRightVertex;
            Vector3 thirdVertex = TileData.BottomRightVertex;
            Vector3 fourthVertex = TileData.BottomLeftVertex;

            Gizmos.DrawLine(firstVertex, secondVertex);
            Gizmos.DrawLine(secondVertex, thirdVertex);
            Gizmos.DrawLine(thirdVertex, fourthVertex);
            Gizmos.DrawLine(fourthVertex, firstVertex);

            Gizmos.DrawLine(firstVertex, thirdVertex);
            Gizmos.DrawLine(secondVertex, fourthVertex);

        }

        [System.Serializable]
        public struct TileTypeDebug
        {
            public GridTileType TileType;
            public Color color;
        }
    }
}


