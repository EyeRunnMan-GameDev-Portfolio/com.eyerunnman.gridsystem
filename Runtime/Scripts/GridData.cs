using System.Collections.Generic;
using UnityEngine;
using System;
using com.eyerunnman.enums;

namespace com.eyerunnman.gridsystem
{
    /// <summary>
    /// Grid Data which contatins dimension and stores data for each tile
    /// </summary>
    [Serializable]
    public class GridData
    {
        [SerializeField]
        private UnityEngine.Vector2Int gridDimension;

        [SerializeField]
        private List<GridTileData> GridTilesData;

        /// <summary>
        /// Grid dimension for `GridData`
        /// </summary>
        public Vector2Int GridDimension { get => gridDimension; }
        /// <summary>
        /// Dictionary from tile id to its respective Grid Tile Data 
        /// </summary>
        public Dictionary<int, GridTileData> GridTilesDataDictionary
        {
            get
            {
                Dictionary<int, GridTileData> keyValuePairs = new();

                int totalTiles = gridDimension.x * gridDimension.y;

                //set default data for tiles
                for (int tileNumber = 0; tileNumber < totalTiles; tileNumber++)
                {
                    int yCoordinate = (int)Mathf.Floor(tileNumber / gridDimension.x);
                    int xCoordinate = (tileNumber % gridDimension.x);

                    GridTileData tileData = new(tileNumber, new Vector2Int(xCoordinate, yCoordinate));

                    keyValuePairs.Add(tileNumber, tileData);
                }

                foreach (GridTileData gridTileData in GridTilesData)
                {
                    if (keyValuePairs.ContainsKey(gridTileData.TileId))
                    {
                        keyValuePairs[gridTileData.TileId] = gridTileData;
                    }
                    else if(!gridTileData.IsDefault)
                    {
                        throw new Exception("Illegal Values in list TileNumber : "+ gridTileData.TileId); 
                    }
                }

                return keyValuePairs;

            }
            set
            {
                GridTilesData = new();
                foreach (GridTileData tileData in value.Values)
                {
                    GridTilesData.Add(tileData);
                }
            }
        }

        /// <summary>
        /// Number of Columns in `GridData`
        /// </summary>
        public int Cols => GridDimension.x;
        /// <summary>
        /// Number of Rows in `GridData`
        /// </summary>
        public int Rows => GridDimension.y;

        /// <summary>
        /// Constructor takes in dimenstions and data of all tiles
        /// </summary>
        /// <param name="dimension">dimension of grid</param>
        /// <param name="gridTilesData">data for all tiles</param>
        public GridData(UnityEngine.Vector2Int dimension, List<GridTileData> gridTilesData)
        {
            gridDimension = dimension;
            GridTilesData = gridTilesData;
        }

        /// <summary>
        /// Constructor takes in dimension of grid and resets the tile data for all tiles to default
        /// </summary>
        /// <param name="dimension">dimension of grid</param>
        public GridData(UnityEngine.Vector2Int dimension)
        {
            gridDimension = dimension;
            GridTilesData = new();
        }

        /// <summary>
        /// Get tile in given direction from a refrence tile
        /// </summary>
        /// <param name="tileId">tile id of ref tile</param>
        /// <param name="direction">direction with respect to ref tile id</param>
        /// <returns></returns>
        public GridTileData GetTileDataInDirection(int tileId, Direction direction)
        {

            Vector2Int resultTileCoordinates;
            GridTileData refrenceTileData = GetTileByTileId(tileId);

            if (refrenceTileData.IsDefault) return GridTileData.Default;

            switch (direction)
            {
                case Direction.North:
                    resultTileCoordinates = refrenceTileData.Coordinates + Vector2Int.up;
                    break;
                case Direction.South:
                    resultTileCoordinates = refrenceTileData.Coordinates + Vector2Int.down;
                    break;
                case Direction.East:
                    resultTileCoordinates = refrenceTileData.Coordinates + Vector2Int.right;
                    break;
                case Direction.West:
                    resultTileCoordinates = refrenceTileData.Coordinates + Vector2Int.left;
                    break;
                default:
                    return GridTileData.Default;
            }

            if (resultTileCoordinates.x < 0 || resultTileCoordinates.x >= GridDimension.x || resultTileCoordinates.y < 0 || resultTileCoordinates.y >= GridDimension.y)
            {
                return GridTileData.Default;
            }

            int resultTileNumber = resultTileCoordinates.x + resultTileCoordinates.y * Cols;

            GridTileData resultTileData = GetTileByTileId(resultTileNumber);

            return resultTileData;
        }


        private GridTileData GetTileByTileId(int tileId)
        {
            if (GridTilesDataDictionary.ContainsKey(tileId))
            {
                return GridTilesDataDictionary[tileId];
            }
            else
            {
                return GridTileData.Default;
            }
        }
    }


}

