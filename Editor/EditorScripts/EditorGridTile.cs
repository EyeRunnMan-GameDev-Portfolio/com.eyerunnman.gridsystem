using TMPro;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace com.eyerunnman.gridsystem.Editor
{
    [ExecuteInEditMode]
    public class EditorGridTile : GameGrid.GridTileObject
    {
        [SerializeField]
        private Color defaultColor;

        [SerializeField]
        private List<TileTypeDebug> TileTypeDebugColors;

        private Color currentColor;

        public override void Initialize(GameGrid parentGrid, GameGrid.IGridTileData tileData)
        {
            base.Initialize(parentGrid, tileData);
            SetPostition(tileData);
        }

        private void SetPostition(GameGrid.IGridTileData tileData)
        {
            transform.localPosition = tileData.Center;
            gameObject.name = "Tile : " + tileData.Coordinates;
            transform.up = tileData.UpVector;
        }

        private void UpdateGizmoColors(GameGrid.TileType tileType)
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
        }

        public override void OnTileDataUpdate(GameGrid.IGridTileData updatedData)
        {
            SetPostition(updatedData);
            UpdateGizmoColors(updatedData.Type);
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


    }

    [System.Serializable]
    public struct TileTypeDebug
    {
        public GameGrid.TileType TileType;
        public Color color;
    }
}


