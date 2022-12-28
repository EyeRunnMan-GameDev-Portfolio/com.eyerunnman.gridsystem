using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.eyerunnman.gridsystem;
using System;
using UnityEditor;

namespace com.eyerunnman.gridsystem
{
    /// <summary>
    /// Grid Data Scriptable object which is used to store grid data
    /// </summary>
    [CreateAssetMenu(fileName = "GridData", menuName = "EyeRunnMan/GridSystem/GridData", order = 100)]
    public class GridDataSO : ScriptableObject
    {
        /// <summary>
        /// Grid Data stored by `GridDataSO`
        /// </summary>
        public GridData GridData;

        #region For UNITY_EDITOR

#if UNITY_EDITOR
        public Mesh GridMesh;

        public void EditorSetTileData(GameGrid editorGrid, GridTileData data)
        {
            editorGrid.SetupTile(data);

            Dictionary<int, GridTileData> updatedGridTileObjectData = GridData.GridTilesDataDictionary;

            updatedGridTileObjectData[data.TileId] = data;

            GridData.GridTilesDataDictionary = updatedGridTileObjectData;
        }

        public GridTileData EditorGetTileData(int tileNumber)
        {
            return GridData.GridTilesDataDictionary[tileNumber];
        }

        [ContextMenu("Generate Mesh")]
        public Mesh EditorGenerateMesh()
        {
            List<GridTileData> gridTilesData = new(GridData.GridTilesDataDictionary.Values);

            Mesh mesh = new();

            List<Vector3> vertices = new();
            List<int> Triangles = new();
            List<Vector2> uv = new();


            foreach (GridTileData tileData in gridTilesData)
            {
                int TopLeftCounter = AddVertex(vertices, tileData.TopLeftVertex);
                int TopRightCounter = AddVertex(vertices, tileData.TopRightVertex);
                int BottomRightCounter = AddVertex(vertices, tileData.BottomRightVertex);
                int BottomLeftCounter = AddVertex(vertices, tileData.BottomLeftVertex);

                Triangles.Add(TopLeftCounter);
                Triangles.Add(TopRightCounter);
                Triangles.Add(BottomRightCounter);

                Triangles.Add(TopLeftCounter);
                Triangles.Add(BottomRightCounter);
                Triangles.Add(BottomLeftCounter);
            }

            mesh.Clear();

            mesh.vertices = vertices.ToArray();
            mesh.triangles = Triangles.ToArray();

            GridMesh = mesh;
            return mesh;

            static int AddVertex(List<Vector3> vertices, Vector3 vertex)
            {
                vertices.Add(vertex);
                int TopLeftCounter = vertices.Count-1;
                return TopLeftCounter;
            }
        }
#endif
        #endregion

    }
}



