using com.eyerunnman.enums;
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
        /// Get a copy of grid data 
        /// </summary>
        public GridData GridDataCopy => new(gridData);

        #region Public Getters

        public List<IGridTileData> GetGridTileDataList {
            get{
                if (gridData!=null)
                    return gridData.GridTileDataList;
                return new();
            }
        }


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

        private void UpdateGameGridData(GridData gridData)
        {
            this.gridData = new(gridData);
            RefreshGrid();
        }
        private void GenerateGameGrid(GridData gridData, GridTileObject tileObjectPrefab)
        {
            UpdateGameGridData(gridData);
            InitializeTileObjectDictionary(this.gridData, tileObjectPrefab);
        }
        private void UpdateGridTileData(IGridTileData data)
        {
            UpdateTileObjectDictionary(data);
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
        private void InitializeTileObjectDictionary(GridData gridData, GridTileObject tileObjectPrefab)
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
        private void RefreshGrid()
        {
            foreach (int tileNuber in tileObjectDictionary.Keys)
            {
                UpdateTileObjectDictionary(GetGridTileDataFromNumber(tileNuber));
            }
        }

        #endregion

        public void ExecuteCommand(ICommand<GameGrid> command)
        {
            command.Execute(this);
        }
    }
}
