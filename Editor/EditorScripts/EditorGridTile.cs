using TMPro;
using UnityEngine;
using UnityEditor;
namespace com.eyerunnman.gridsystem.Editor
{
    [ExecuteInEditMode]
    public class EditorGridTile : GameGrid.GridTileObject
    {
        private Rect screenRect;

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

        public override void UpdateTile()
        {
            base.UpdateTile();
            SetPostition(TileData);
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

        }

        private void OnDrawGizmos()
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Hello World!");

            Gizmos.color = Color.cyan;


            Vector3 firstVertex = TileData.TopLeftVertex;
            Vector3 secondVertex = TileData.TopRightVertex;
            Vector3 thirdVertex = TileData.BottomRightVertex;
            Vector3 fourthVertex = TileData.BottomLeftVertex;

            Gizmos.DrawLine(firstVertex, secondVertex);
            Gizmos.DrawLine(secondVertex, thirdVertex);
            Gizmos.DrawLine(thirdVertex, fourthVertex);
            Gizmos.DrawLine(fourthVertex, firstVertex);

            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(firstVertex, 0.01f);
            Gizmos.DrawSphere(secondVertex, 0.01f);
            Gizmos.DrawSphere(thirdVertex, 0.01f);
            Gizmos.DrawSphere(fourthVertex, 0.01f);


        }
    }
}


