using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using com.eyerunnman.enums;
using com.eyerunnman.interfaces;
using System.Linq;
using com.eyerunnman.gridsystem.Enums;

namespace com.eyerunnman.gridsystem.Editor
{
    public class GameGridEditor : EditorWindow
    {
        [MenuItem("EyeRunnMan/Grid System/Game Grid Editor")]
        static void OpenWindow()
        {
            GameGridEditor window = (GameGridEditor)EditorWindow.GetWindow(typeof(GameGridEditor));
            window.Show();
        }

        private GridDataSO SourceGridDataSO;
        private GridTileObject SourceGridTileObject;
        private GameGrid EditorGameGrid;

        private Stack<List<GridTileData>> GridTileDataHistory = new();

        IGridTileData editorTileData;

        private static class EditorTileData
        {
            public static float Height;
            public static Direction SlantDirection;
            public static float SlantAngle;
            public static GridTileType Type;
            public static float LeadingEdgeHeight;
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();

            SourceGridDataSO = (GridDataSO)EditorGUILayout.ObjectField("Grid Data",SourceGridDataSO, typeof(GridDataSO), true);
            SourceGridTileObject = (GridTileObject)EditorGUILayout.ObjectField("Editor Tile Prefab",SourceGridTileObject, typeof(GridTileObject), true);

            if(SourceGridDataSO==null || SourceGridTileObject == null )
            {
                return;
            }

            EditorGUILayout.Space();

            if (EditorGameGrid==null)
            {
                if (GUILayout.Button("Generate Grid"))
                {
                    GenerateGrid(SourceGridDataSO.GridGenerationData.dimension,SourceGridDataSO.GridGenerationData.tileDatalist,SourceGridTileObject);
                }
                else
                {
                    return;
                }
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Reset Grid Data"))
            {
                ResetGridSO();
            }

            EditorGUILayout.Space();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Save Grid"))
            {
                SaveGrid();
            }

            if (GUILayout.Button("Undo Grid"))
            {
                UndoGrid();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginDisabledGroup(!IsGridTileSelected());

            EditorGUILayout.LabelField("Editor Tile Data");
            EditorGUILayout.LabelField("History Depth : " + GridTileDataHistory.Count);


            editorTileData = new GridTileData(GetCurrentSelectedTileData());

            EditorTileData.Height = EditorGUILayout.Slider("Height", editorTileData.Height, -5, 10);
            EditorTileData.SlantDirection = (Direction)EditorGUILayout.EnumPopup("Slant Direction", editorTileData.SlantDirection);
            EditorTileData.SlantAngle = EditorGUILayout.Slider("Slant Angle", editorTileData.SlantAngle, 0, 90);
            EditorTileData.Type = (GridTileType)EditorGUILayout.EnumPopup("Type", editorTileData.Type);


            editorTileData = new GridTileData(editorTileData.TileNumber, editorTileData.Coordinates, EditorTileData.Height, EditorTileData.SlantDirection, EditorTileData.SlantAngle, EditorTileData.Type);

            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("snap North"))
                SnapTileInDirection(new(editorTileData), Direction.North);
            if (GUILayout.Button("snap South"))
                SnapTileInDirection(new(editorTileData), Direction.South);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("snap East"))
                SnapTileInDirection(new(editorTileData), Direction.East);
            if (GUILayout.Button("snap West"))
                SnapTileInDirection(new(editorTileData), Direction.West);
            EditorGUILayout.EndHorizontal();

            UpdateCurrentSelectedTilesData(new(editorTileData));

            EditorGUI.EndDisabledGroup();

        }

        private GridTileData GetCurrentSelectedTileData()
        {

            foreach (GameObject gameObjects in Selection.gameObjects)
            {
                if (gameObjects.GetComponent<GridTileObject>())
                {
                    GridTileObject tileOBject = gameObjects.GetComponent<GridTileObject>();

                    return new GridTileData(tileOBject.TileData);
                }
            }

            return GridTileData.Undefined;
        }
        
        private void UpdateCurrentSelectedTilesData(GridTileData data)
        {
            List<GridTileData> updatedData = new();

            foreach (GameObject gameObjects in Selection.gameObjects)
            {
                if (gameObjects.GetComponent<GridTileObject>())
                {
                    GridTileObject tileOBject = gameObjects.GetComponent<GridTileObject>();

                    GridTileData tileData = new(tileOBject.TileData);

                    tileData.CloneData(data);

                    updatedData.Add(tileData);
                }
            }

            UpdateGrid(updatedData);

        }

        private bool IsGridTileSelected()
        {
            foreach (GameObject gameObjects in Selection.gameObjects)
            {
                if (gameObjects.GetComponent<GridTileObject>())
                    return true;
            }
            return false;
        }

        private void UpdateGrid(List<GridTileData> updateData )
        {
            ICommand<GameGrid> updateGridCommand = new GameGrid.Commands.UpdateGridTileData(updateData.Cast<IGridTileData>().ToList());
            
            EditorGameGrid.ExecuteCommand(updateGridCommand);
        }

        private void SaveGrid()
        {
            GridTileDataHistory.Push(SourceGridDataSO.GridGenerationData.tileDatalist);
            SourceGridDataSO.SetTilesInfo(EditorGameGrid.TileDataList.Select(tiledata=>new GridTileData(tiledata)).ToList());
        }

        private void UndoGrid()
        {
            if (GridTileDataHistory.Count > 0)
            {
                SourceGridDataSO.SetTilesInfo(new());
                SourceGridDataSO.SetTilesInfo(GridTileDataHistory.Pop());
                ResetGameGrid(SourceGridDataSO.GridGenerationData.dimension, SourceGridDataSO.GridGenerationData.tileDatalist);
            }
        }

        private void ResetGridSO()
        {
            SourceGridDataSO.SetTilesInfo(new());
            ResetGameGrid(SourceGridDataSO.GridGenerationData.dimension,SourceGridDataSO.GridGenerationData.tileDatalist);
        }

        private void GenerateGrid(Vector2Int dimension,List<GridTileData> tileDataList,GridTileObject editorGridTilePrefab)
        {
            GridTileDataHistory.Clear();

            GameObject gameObject = new("--Editor_GridTile--");
            gameObject.AddComponent(typeof(GameGrid));
            EditorGameGrid = gameObject.GetComponent<GameGrid>();

            ICommand<GameGrid> generateGridCommand = new GameGrid.Commands.GenerateGameGrid(dimension,tileDataList,editorGridTilePrefab);

            EditorGameGrid.ExecuteCommand(generateGridCommand);
        }

        private void ResetGameGrid(Vector2Int dimension,List<GridTileData> tileDataList)
        {
            ICommand<GameGrid> updateGridData = new GameGrid.Commands.UpdateGridData(dimension,tileDataList);

            EditorGameGrid.ExecuteCommand(updateGridData);
        }

        private void SnapTileInDirection(GridTileData refrenceTile, Direction direction)
        {
            GridTileData toBeSnappedtiledata = new(EditorGameGrid.GetTileDataInDirection(refrenceTile.TileNumber,direction));

            switch (direction)
            {
                case Direction.North:
                    toBeSnappedtiledata.SlantDirection = Direction.South;
                    break;
                case Direction.South:
                    toBeSnappedtiledata.SlantDirection = Direction.North;
                    break;
                case Direction.East:
                    toBeSnappedtiledata.SlantDirection = Direction.West;
                    break;
                case Direction.West:
                    toBeSnappedtiledata.SlantDirection = Direction.East;
                    break;
                default:
                    toBeSnappedtiledata = GridTileData.Undefined;
                    break;
            }

            toBeSnappedtiledata.LeadingEdgeHeight = refrenceTile.Height - toBeSnappedtiledata.Height;
            
            UpdateGrid(new() { toBeSnappedtiledata });
        }
    }
}

