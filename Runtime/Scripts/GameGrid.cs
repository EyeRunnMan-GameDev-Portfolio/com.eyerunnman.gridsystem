using com.eyerunnman.enums;
using System.Collections.Generic;
using System.Linq;
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
        /// Dimension of the `GameGrid`
        /// </summary>
        public Vector2Int GridDimension { get => gridData.GridDimension; }

        /// <summary>
        /// Number of columns in `GameGrid`
        /// </summary>
        public int Cols => gridData.Cols;

        /// <summary>
        /// Number of Rows in `GameGrid`
        /// </summary>
        public int Rows => gridData.Rows;

        /// <summary>
        /// Set Data for `GameGrid`
        /// </summary>
        /// <param name="gridData">the actual grid data</param>
        public void GenerateGameGrid(GridData gridData)
        {
            ResetGrid();
            this.gridData = gridData;
        }

        /// <summary>
        /// Generate `GameGrid` based on data and a prefab Grid Tile Object
        /// </summary>
        /// <param name="gridData">grid data </param>
        /// <param name="gridConfig">grid config</param>
        public void GenerateGameGrid(GridData gridData, GridTileObject tileObjectPrefab)
        {
            GenerateGameGrid(gridData);
            InitializeTileObjectDictionary(gridData, tileObjectPrefab);
        }

        /// <summary>
        /// Updated data of a given tile data.
        /// **NOTE:** it takes the index of tile to be updated from `data.TileId` .
        /// Will only update if `data.TileId` is present in `GameGrid`
        /// </summary>
        /// <param name="data">data of tile to be updated</param>
        public void UpdateTileData(IGridTileData data)
        {
            UpdateTileObjectDictionary(data);
        }

        /// <summary>
        /// Returns a data of the tile in given direction from current tile
        /// </summary>
        /// <param name="tileId">index of the refrence tile</param>
        /// <param name="direction">direction of tile to look into</param>
        /// <returns>data of tile based on input params</returns>
        public IGridTileData GetTileDataInDirection(int tileId , Direction direction)
        {
            return gridData.GetTileDataInDirection(tileId,direction);
        }

        /// <summary>
        /// Get GridTileData From Coordinates
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public IGridTileData GetGridTileDataFromCoordinates(Vector2Int coordinates)
        {
            return gridData.GetTileDataFromCoordinates(coordinates);
        }

        /// <summary>
        /// Get GridTileData From Coordinates
        /// </summary>
        /// <param name="tileNumber"></param>
        /// <returns></returns>
        public IGridTileData GetGridTileDataFromNumber(int tileNumber)
        {
            return gridData.GetTileFromNumber(tileNumber);
        }

        /// <summary>
        /// Get TileIndex In a given direction 
        /// </summary>
        /// <param name="tileNumber">refrence tile index</param>
        /// <param name="direction">direction</param>
        /// <returns></returns>
        public int GetTileNumberInDirection(int tileNumber,Direction direction)
        {
            return gridData.GetTileNumberInDirection(tileNumber, direction);
        }

        private GridTileObject GetTileObjectFromDictionary(int tileId)
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

        private void InitializeTileObjectDictionary( GridData gridData, GridTileObject tileObjectPrefab)
        {
            foreach (int tileNumber in Enumerable.Range(0, gridData.NumberOfTiles))
            {

                IGridTileData gridTileData = gridData.GetTileFromNumber(tileNumber);
                GridTileObject gridTileObject = Instantiate(tileObjectPrefab, transform);

                gridTileObject.Initialize(this, gridTileData);

                tileObjectDictionary[tileNumber] = gridTileObject;
            }
        }
        private void UpdateTileObjectDictionary(IGridTileData gridTileData)
        {
            gridData.SetTileData(gridTileData);
            IGridTileData tileData = gridData.GetTileFromNumber(gridTileData.TileNumber);
            GridTileObject gridTileObject = GetTileObjectFromDictionary(tileData.TileNumber);

            if (gridTileObject)
            {
                gridTileObject.UpdateTile();
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
