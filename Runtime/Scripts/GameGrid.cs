using com.eyerunnman.enums;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// grid system package namespace
/// </summary>
namespace com.eyerunnman.gridsystem
{
    /// <summary>
    /// Grid Component for games
    /// </summary>
    public class GameGrid : MonoBehaviour
    {

        private GridData gridData;
        private Dictionary<int, GridTileObject> tileObjectDictionary = new();


        /// <summary>
        /// List of all the tiles in the `GameGrid` 
        /// </summary>
        public List<GridTileData> GridTileDataList
        {
            get => new(gridData.GridTilesDataDictionary.Values);
        }

        /// <summary>
        /// Dimension of the `GameGrid`
        /// </summary>
        public Vector2Int GridDimension { get => gridData.GridDimension; }

        /// <summary>
        /// Number of columns in `GameGrid`
        /// </summary>
        private int Cols => gridData.Cols;

        /// <summary>
        /// Number of Rows in `GameGrid`
        /// </summary>
        private int Rows => gridData.Rows;

        /// <summary>
        /// Generate `GameGrid` based on data and a prefab Grid Tile Object
        /// </summary>
        /// <param name="data">grid data </param>
        /// <param name="gridTileObjectPrefab">prefab object</param>
        public void GenerateGameGrid(GridData data,GridTileObject gridTileObjectPrefab)
        {
            ResetGrid();
            this.gridData = data;

            foreach (int tileNumber in gridData.GridTilesDataDictionary.Keys)
            {
                GridTileObject gridTileObject = Instantiate(gridTileObjectPrefab, transform);
                gridTileObject.SetUpTile(gridData.GridTilesDataDictionary[tileNumber]);
                
                tileObjectDictionary.Add(tileNumber, gridTileObject);
            }


        }
       

        /// <summary>
        /// Updated data of a given tile data.
        /// **NOTE:** it takes the index of tile to be updated from `data.TileId` .
        /// Will only update if `data.TileId` is present in `GameGrid`
        /// </summary>
        /// <param name="data">data of tile to be updated</param>
        public void SetupTile(GridTileData data)
        {
            GridTileObject tileObject = GetTileObjectFromId(data.TileId);

            if (tileObject)
            {
                tileObject.SetUpTile(data);
            }
        }


        /// <summary>
        /// Returns a data of the tile in given direction from current tile
        /// </summary>
        /// <param name="tileId">index of the refrence tile</param>
        /// <param name="direction">direction of tile to look into</param>
        /// <returns>data of tile based on input params</returns>
        public GridTileData GetTileDataInDirection(int tileId , Direction direction)
        {
            return gridData.GetTileDataInDirection(tileId,direction);
        }



        private GridTileObject GetTileObjectFromId(int tileId)
        {
            if (tileObjectDictionary.ContainsKey(tileId))
            {
                return tileObjectDictionary[tileId];
            }
            else
            {
                return null;
            }
        }

        private void ResetGrid()
        {
            foreach (GridTileObject tile in tileObjectDictionary.Values)
            {
                Destroy(tile);
            }

            tileObjectDictionary = new();
        }
    }
}
