using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.eyerunnman.gridsystem;
using System;
using UnityEditor;
using System.Linq;

namespace com.eyerunnman.gridsystem
{
    /// <summary>
    /// Grid Data Scriptable object which is used to store grid data
    /// </summary>
    [CreateAssetMenu(fileName = "GridData", menuName = "EyeRunnMan/GridSystem/GridData", order = 100)]
    public class GridDataSO : ScriptableObject
    {
        [SerializeField]
        private Vector2Int GridDimension;

        [SerializeField]
        private List<GameGrid.GridTileData> TilesInfo;


        public GameGrid.GridData GridData => new(GridDimension, TilesInfo);

        public void SetTilesInfo(List<GameGrid.IGridTileData> tilesInfo)
        {
            TilesInfo = new(Enumerable.Range(0,tilesInfo.Count).Select(idx=>new GameGrid.GridTileData(tilesInfo[idx])));
        }

    }
}



