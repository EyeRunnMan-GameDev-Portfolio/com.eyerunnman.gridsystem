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
        private List<GridTileData> TileDataList;


        public (Vector2Int dimension,List<GridTileData> tileDatalist) GridGenerationData => new(GridDimension, TileDataList);

        public void SetTilesInfo(List<GridTileData> tilesInfo)
        {
            TileDataList = new(tilesInfo);
        }
    }
}



