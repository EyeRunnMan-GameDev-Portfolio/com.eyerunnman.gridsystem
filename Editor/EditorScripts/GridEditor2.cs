using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System;
using com.eyerunnman.enums;

namespace com.eyerunnman.gridsystem.Editor
{
    public class GridEditor2 : EditorWindow
    {
        [MenuItem("EyeRunnMan/GridSystem/GridEditor2")]
        static void OpenWindow()
        {
            GridEditor2 window = (GridEditor2)EditorWindow.GetWindow(typeof(GridEditor2));
            window.Show();
        }

        private GridDataSO SourceGridDataSO;
        private GridEditorConfigDataSO SourceGridEditorDebugDataSO;

        private static class SnapTile
        {
            public static bool NorthTile;
            public static bool SouthTile;
            public static bool EastTile;
            public static bool WestTile;

            public static void ResetSnapTile()
            {
                NorthTile = false;
                SouthTile = false;
                EastTile = false;
                WestTile = false;
            }
        }

        GameGrid debugGameGrid;
        List<GridTileData> currentSelectedTileDataList;
        GridTileData editorTileData;

        private void OnGUI()
        {
            //currentSelectedTileDataList = GetCurrentSelectedTilesData();

            GenerateGrid(new(Vector2Int.up,null), null);

            //UpdateGrid(editorTileData, currentSelectedTileDataList);
        }




        private List<GridTileData> GetCurrentSelectedTilesData()
        {
            return null;
        }

        private void UpdateGrid(GridTileData editorTileData,List<GridTileData> tileDataList)
        {
            foreach (GridTileData tileData in tileDataList)
            {
                tileData.CloneData(editorTileData);
                debugGameGrid.UpdateTileData(tileData);
            }
        }


        private GameGrid GenerateGrid(GridData data,GridTileObject gridTileObjectPrefab)
        {
            GameObject gameObject = new("--Editor_Grid--");
            gameObject.AddComponent(typeof(GridTileObject));
            GridTileObject gameGrid = gameObject.GetComponent<GridTileObject>();

            return null;

        }

    }
}

