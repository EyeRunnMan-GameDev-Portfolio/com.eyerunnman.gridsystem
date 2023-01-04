using com.eyerunnman.enums;
using com.eyerunnman.interfaces;
using System;
using UnityEngine;

namespace com.eyerunnman.gridsystem
{
    public partial class GameGrid
    {
        /// <summary>
        /// Abstract class for grid tile object
        /// </summary>
        public abstract class GridTileObject : MonoBehaviour
        {
            /// <summary>
            /// Getter for Getting out the tile data Data from tile object
            /// Data can also be set given it has the validated tilenumber and coordinates
            /// </summary>
            public IGridTileData TileData
            {
                get => data;
                set
                {
                    if (value.TileNumber == data.TileNumber && value.Coordinates == data.Coordinates)
                    {
                        if (parentGrid)
                        {
                            ICommand<GameGrid> updateGridCommand = new Commands.UpdateGridTileData(value);

                            parentGrid.ExecuteCommand(updateGridCommand);
                        }
                    }
                }
            }

            private CachedGridTileData data = new(IGridTileData.Undefined);

            /// <summary>
            /// Get the refrence To Parent Grid of grid tile object
            /// </summary>
            public GameGrid ParentGrid => parentGrid;

            private GameGrid parentGrid = null;

            /// <summary>
            /// Initialize GridTile with parentGrid Refrence
            /// </summary>
            /// <param name="grid"></param>
            /// <param name="tileData"></param>
            public virtual void Initialize(GameGrid parentGrid, IGridTileData tileData)
            {
                this.parentGrid = parentGrid;
                data = new(tileData);
                UpdateTile();
            }

            public void UpdateTile()
            {
                if (parentGrid)
                {
                    data = new(parentGrid.GetGridTileDataFromNumber(data.TileNumber));
                    OnTileDataUpdate(data);
                }
            }

            public abstract void OnTileDataUpdate(IGridTileData tileData);


            /// <summary>
            /// Get tile Data in Direction
            /// </summary>
            /// <param name="direction">direction</param>
            /// <returns>the tile data in direction</returns>
            public virtual IGridTileData GetTileObjectInDirectionFromParent(Direction direction)
            {
                if (parentGrid)
                {
                    return parentGrid.GetTileDataInDirection(data.TileNumber, direction);
                }

                return IGridTileData.Undefined;
            }

            /// <summary>
            /// Update Gird Tile Data with given data
            /// </summary>
            /// <param name="gridTileData">data to be Upated in the parent Grid</param>
            public virtual void UpdateGridTileObjectInParent(IGridTileData gridTileData)
            {
                if (parentGrid)
                {
                    //parentGrid.UpdateTileData(gridTileData);
                }
            }

        }
    }
}

