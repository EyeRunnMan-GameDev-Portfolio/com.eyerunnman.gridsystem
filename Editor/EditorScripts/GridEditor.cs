using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System;
using com.eyerunnman.enums;

namespace com.eyerunnman.gridsystem.Editor
{
    public class GridEditor : EditorWindow
    {
        [MenuItem("EyeRunnMan/GridSystem/GridEditor2")]
        static void OpenWindow()
        {
            GridEditor window = (GridEditor)EditorWindow.GetWindow(typeof(GridEditor));
            window.Show();
        }

        private GridDataSO SourceGridDataSO;
        //private GridTileObject SourceGridTileObject;

        private GameGrid EditorGameGrid;


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


        private void OnGUI()
        {
            //currentSelectedTileDataList = GetCurrentSelectedTilesData();


            //UpdateGrid(editorTileData, currentSelectedTileDataList);
        }



        //private List<GridTileData> GetCurrentSelectedTilesData()
        //{
        //    return null;
        //}
        //
        //private void UpdateGrid()
        //{
        //    SourceGridDataSO.GridData = EditorGameGrid.GridDataCopy;
        //}
        //
        //private void GenerateGrid()
        //{
        //    GameObject gameObject = new("--Editor_GridTile--");
        //    gameObject.AddComponent(typeof(GameGrid));
        //    EditorGameGrid = gameObject.GetComponent<GameGrid>();
        //
        //    //EditorGameGrid.GenerateGameGrid(SourceGridDataSO.GridData, SourceGridTileObject);
        //
        //}

    }
}

