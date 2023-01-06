using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.eyerunnman.gridsystem
{
    public abstract class GridDataSO : ScriptableObject
    {
        [SerializeField]
        protected Vector2Int GridDimension;

        [SerializeField]
        protected List<GridTileData> TileDataList;

        public (Vector2Int dimension, List<GridTileData> tileDatalist) GridGenerationData => new(GridDimension, TileDataList);

        public void SetTilesInfo(List<GridTileData> tilesInfo)
        {
            TileDataList = new(tilesInfo);
        }
    }
}
