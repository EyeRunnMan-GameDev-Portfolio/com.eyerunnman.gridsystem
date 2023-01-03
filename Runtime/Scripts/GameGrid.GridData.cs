using System.Collections.Generic;
using UnityEngine;
using System;
using com.eyerunnman.enums;
using System.Linq;

namespace com.eyerunnman.gridsystem
{
    public partial class GameGrid
    {
        /// <summary>
        /// Grid Data which contatins dimension and stores data for each tile
        /// </summary>
        [Serializable]
        public sealed class GridData
        {
            [SerializeField]
            private Vector2Int gridDimension;


            private Dictionary<int, CachedGridTileData> gridTileDataDictionary=new();
            
            public List<IGridTileData> GridTileDataList
            {
                get
                {
                    if (gridTileDataDictionary!=null)
                    {
                        return new(gridTileDataDictionary.Select(pair => (IGridTileData)(pair.Value)));
                    }
                    else
                    {
                        return new();
                    }
                }
            }

            /// <summary>
            /// Grid dimension for `GridData`
            /// </summary>
            public Vector2Int GridDimension { get => gridDimension; }

            /// <summary>
            /// Number of Columns in `GridData`
            /// </summary>
            public int Cols => GridDimension.x;

            /// <summary>
            /// Number of Rows in `GridData`
            /// </summary>
            public int Rows => GridDimension.y;

            /// <summary>
            /// Total Number of tiles in GridData;
            /// </summary>
            public int NumberOfTiles => Cols * Rows;

            /// <summary>
            /// Constructor takes in dimenstions and data of all tiles
            /// </summary>
            /// <param name="dimension">dimension of grid</param>
            /// <param name="gridTilesData">data for all tiles</param>
            public GridData(Vector2Int dimension, List<GridTileData> gridTilesData)
            {
                gridDimension = dimension;
                gridTileDataDictionary = new();

                foreach (IGridTileData gridTileData in gridTilesData)
                {
                    UpdateDataInDictionary(gridTileData);
                }
            }

            public GridData(GridData gridData)
            {
                this.gridDimension = gridData.gridDimension;

                if (gridData.gridTileDataDictionary != null)
                this.gridTileDataDictionary = new(gridData.gridTileDataDictionary);

            }

            /// <summary>
            /// Constructor takes in dimension of grid and resets the tile data for all tiles to default
            /// </summary>
            /// <param name="dimension">dimension of grid</param>
            public GridData(Vector2Int dimension)
            {
                gridDimension = dimension;
                gridTileDataDictionary = new();
            }

            /// <summary>
            /// Get tile in given direction from a refrence tile
            /// </summary>
            /// <param name="tileNumber">tile id of ref tile</param>
            /// <param name="direction">direction with respect to ref tile id</param>
            /// <returns>grid data in given direction</returns>
            public IGridTileData GetTileDataInDirection(int tileNumber, Direction direction)
            {

                Vector2Int resultTileCoordinates;
                IGridTileData refrenceTileData = GetTileFromNumber(tileNumber);

                if (refrenceTileData.IsUndefined) return IGridTileData.Undefined;

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
                        return IGridTileData.Undefined;
                }

                if (resultTileCoordinates.x < 0 || resultTileCoordinates.x >= GridDimension.x || resultTileCoordinates.y < 0 || resultTileCoordinates.y >= GridDimension.y)
                {
                    return IGridTileData.Undefined;
                }

                int resultTileNumber = resultTileCoordinates.x + resultTileCoordinates.y * Cols;

                IGridTileData resultTileData = GetTileFromNumber(resultTileNumber);

                return resultTileData;
            }

            /// <summary>
            /// Get tile number in given direction of refrence tile number
            /// </summary>
            /// <param name="tileNumber">ref tile number</param>
            /// <param name="direction">direction</param>
            /// <returns>tile number of tile in number</returns>
            public int GetTileNumberInDirection(int tileNumber, Direction direction)
            {
                if (!IsTileNumberValid(tileNumber))
                {
                    return IGridTileData.Undefined.TileNumber;
                }

                Vector2Int refrenceCoordinates = GetCoordinatesFromNumber(tileNumber);

                Vector2Int finalCoordinates = GetCoordinateInDirection(refrenceCoordinates,direction);

                return GetNumberFromCoordinates(finalCoordinates);

            }

            /// <summary>
            /// Get coordinates in directionm from refrence coordinates
            /// </summary>
            /// <param name="refrenceCoordinates">refrence coordinates</param>
            /// <param name="direction">direction</param>
            /// <returns>coordinates of tile in direction</returns>
            public Vector2Int GetCoordinateInDirection(Vector2Int refrenceCoordinates, Direction direction)
            {
                Vector2Int finalCoordinates = direction switch
                {
                    Direction.North => refrenceCoordinates + Vector2Int.up,
                    Direction.South => refrenceCoordinates + Vector2Int.down,
                    Direction.East => refrenceCoordinates + Vector2Int.right,
                    Direction.West => refrenceCoordinates + Vector2Int.left,
                    _ => IGridTileData.Undefined.Coordinates,
                };

                if (IsTileCoordinateValid(finalCoordinates))
                {
                    return finalCoordinates;
                }
                else
                {
                    return IGridTileData.Undefined.Coordinates;
                }
            }

            /// <summary>
            /// To Set data for a given grid tile data
            /// </summary>
            /// <param name="data">data to setup</param>
            public void SetTileData(IGridTileData data)
            {
                UpdateDataInDictionary(data);
            }

            /// <summary>
            /// GetTileData from coordinates
            /// </summary>
            /// <param name="coordinates">coordinates of tile</param>
            /// <returns></returns>
            public IGridTileData GetTileDataFromCoordinates(Vector2Int coordinates)
            {
                int tileNumber = GetNumberFromCoordinates(coordinates);

                return GetTileFromNumber(tileNumber);
            }

            /// <summary>
            /// Get Tild Data for a given index if the number is invalid will return undefined tile
            /// </summary>
            /// <param name="tileNumber">the number of tile</param>
            /// <returns></returns>
            public IGridTileData GetTileFromNumber(int tileNumber)
            {
                if (!IsTileNumberValid(tileNumber))
                {
                    return IGridTileData.Undefined;
                }

                if (gridTileDataDictionary.ContainsKey(tileNumber))
                {
                    return gridTileDataDictionary[tileNumber];
                }
                else
                {
                    IGridTileData tileData = new GridTileData(tileNumber, GetCoordinatesFromNumber(tileNumber));
                    return tileData;
                }
            }

            private Vector2Int GetCoordinatesFromNumber(int tileNumber)
            {
                if (IsTileNumberValid(tileNumber))
                    return new((tileNumber % gridDimension.x), (int)Mathf.Floor(tileNumber / gridDimension.x));

                return IGridTileData.Undefined.Coordinates;
            }

            private int GetNumberFromCoordinates(Vector2Int coordinate)
            {
                if (IsTileCoordinateValid(coordinate))
                    return Cols * (coordinate.y) + coordinate.x;

                else

                    return IGridTileData.Undefined.TileNumber;
            }

            private bool IsTileCoordinateValid(Vector2Int coordinate)
            {
                return coordinate.x >= 0 && coordinate.y >= 0 && coordinate.x < gridDimension.x && coordinate.y < gridDimension.y;
            }

            private bool IsTileNumberValid(int tileNumber)
            {
                return (tileNumber >= 0 && tileNumber < NumberOfTiles);
            }

            private void UpdateDataInDictionary(IGridTileData gridTileData)
            {
                if (gridTileData.IsUndefined || !IsTileDataValid(gridTileData))
                {
                    return;
                }
                else
                {
                    gridTileDataDictionary[gridTileData.TileNumber] = new(gridTileData);
                }

            }

            private bool IsTileDataValid(IGridTileData gridTileData)
            {
                if (IsTileCoordinateValid(gridTileData.Coordinates) && IsTileNumberValid(gridTileData.TileNumber))
                {
                    Vector2Int validCoordinates = GetCoordinatesFromNumber(gridTileData.TileNumber);
                    if (validCoordinates == gridTileData.Coordinates)
                    {
                        return true;
                    }

                }

                return false;
            }
        }
    }
}

