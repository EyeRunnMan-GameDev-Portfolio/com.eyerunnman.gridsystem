using com.eyerunnman.enums;
using com.eyerunnman.gridsystem.Internal;
using com.eyerunnman.interfaces;
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
    public sealed partial class GameGrid : MonoBehaviour , IProxy<GameGrid>
    {
        private GridData gridData;
        private Dictionary<int, GridTileObjectController> tileObjectControllerDictionary = new();

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
        /// Get the list of all tile data 
        /// </summary>
        public List<IGridTileData> TileDataList => new(gridData.GridTileDataList);

        #region Public Getters Functions

        /// <summary>
        /// Returns a data of the tile in given direction from current tile
        /// </summary>
        /// <param name="tileId">index of the refrence tile</param>
        /// <param name="direction">direction of tile to look into</param>
        /// <returns>data of tile based on input params</returns>
        public IGridTileData GetTileDataInDirection(int tileId, Direction direction)
        {
            return gridData.GetTileDataInDirection(tileId, direction);
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
        public int GetTileNumberInDirection(int tileNumber, Direction direction)
        {
            return gridData.GetTileNumberInDirection(tileNumber, direction);
        }

        /// <summary>
        /// Get Tile Coordinates in Direction 
        /// </summary>
        /// <param name="coordinates">refrence coordinates</param>
        /// <param name="direction">direction</param>
        /// <returns>coordinates in direction</returns>
        public Vector2Int GetCoordinatesInDirection(Vector2Int coordinates, Direction direction)
        {
            return gridData.GetCoordinateInDirection(coordinates, direction);
        }

        #endregion

        #region Private Methods

        internal void RefreshGridData(GridData gridData)
        {
            this.gridData = new(gridData);
            RefreshGrid();
        }
        internal void GenerateGameGrid(GridData gridData, GridTileObject tileObjectPrefab)
        {
            RefreshGridData(gridData);
            InitializeTileObjectControllerDictionary(this.gridData, tileObjectPrefab);
        }
        internal void UpdateGridTileData(IGridTileData data)
        {
            UpdateTileObjectControllerDictionary(data);
        }
        private GridTileObjectController GetTileObjectControllerFromDictionary(int tileId)
        {
            if (tileObjectControllerDictionary.ContainsKey(tileId))
            {
                return tileObjectControllerDictionary[tileId];
            }
            else
            {
                return null;
            }
        }
        private void InitializeTileObjectControllerDictionary(GridData gridData, GridTileObject tileObjectPrefab)
        {
            foreach (int tileNumber in Enumerable.Range(0, gridData.NumberOfTiles))
            {

                IGridTileData gridTileData = gridData.GetTileFromNumber(tileNumber);
                GridTileObject gridTileObject = Instantiate(tileObjectPrefab, transform);

                GridTileObjectController tileObjectController = new(gridTileObject);
                tileObjectController.InitializeTileObject(this, gridTileData);

                tileObjectControllerDictionary[tileNumber] = tileObjectController;
            }
        }
        private void UpdateTileObjectControllerDictionary(IGridTileData gridTileData)
        {
            gridData.SetTileData(gridTileData);
            IGridTileData tileData = gridData.GetTileFromNumber(gridTileData.TileNumber);
            GridTileObjectController gridTileObjectController = GetTileObjectControllerFromDictionary(tileData.TileNumber);

            if(gridTileObjectController!=null)
            gridTileObjectController.UpdateTileObjectData();
        }
        private void RefreshGrid()
        {
            foreach (int tileNuber in tileObjectControllerDictionary.Keys)
            {
                UpdateTileObjectControllerDictionary(GetGridTileDataFromNumber(tileNuber));
            }
        }

        #endregion

        public void ExecuteCommand(ICommand<GameGrid> command)
        {
            command.Execute(this);
        }
    }
}
