using TMPro;
using UnityEngine;

namespace com.eyerunnman.gridsystem.Editor
{
    public class EditorGridTile : GameGrid.GridTileObject
    {

        [SerializeField] TMP_Text tileNumber;
        [SerializeField] TMP_Text coordinates;
        [SerializeField] TMP_Text height;
        [SerializeField] TMP_Text slantDirection;
        [SerializeField] TMP_Text slantAngle;
        [SerializeField] TMP_Text tileType;

        private void Update()
        {
            tileNumber.text = TileData.TileNumber.ToString();
            coordinates.text = TileData.Coordinates.ToString();
            height.text = TileData.Height.ToString();
            slantDirection.text = TileData.SlantDirection.ToString();
            slantAngle.text = TileData.SlantAngle.ToString();
            tileType.text = TileData.Type.ToString();
        }

        private void OnDrawGizmos()
        {
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

        public override void Reset()
        {
        }
    }
}


