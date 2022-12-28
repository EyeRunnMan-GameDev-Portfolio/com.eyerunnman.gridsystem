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
            GridEditor window = (GridEditor)EditorWindow.GetWindow(typeof(GridEditor));
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
        GridTileObject currentSelectedTileObject;
        GridTileData currentSelectedTileData;

        private void OnGUI()
        {
            currentSelectedTileData = GetCurrentSelectedTileData();



            debugGameGrid.SetupTile(currentSelectedTileData);
        }




        private GridTileObject GetCurrentSelectedTileObject()
        {
            return null;
        }

        private GridTileData GetCurrentSelectedTileData()
        {
            return GridTileData.Default;
        }

    }
}

