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
        public abstract class GridTileObjectInternal : MonoBehaviour
        {
            private GameGrid.CachedGridTileData cachedTileData;

            /// <summary>
            /// Current Tile Data for the grid TileObject
            /// </summary>
            public GameGrid.IGridTileData TileData { get=> cachedTileData; }

            /// <summary>
            /// Getter for parentGrid Refrence
            /// </summary>
            protected GameGrid ParentGrid { get; private set; }

            public void InitializeTileObject(GameGrid parentGrid, IGridTileData tileData) {

                ParentGrid = parentGrid;
                cachedTileData = new GameGrid.CachedGridTileData(tileData);
            }

            public void UpdateTileObject(IGridTileData tileData)
            {
                cachedTileData = new GameGrid.CachedGridTileData(tileData);
                OnTileDataUpdate();
            }

            /// <summary>
            /// Is called whever tile data is updated
            /// </summary>
            public abstract void OnTileDataUpdate();

            /// <summary>
            /// Is called whenever tile data is initialized
            /// </summary>
            public abstract void OnTileDataInitialize();
        }


        public abstract class GridTileObject : GridTileObjectInternal
        {
            public new void InitializeTileObject(GameGrid parentGrid, IGridTileData tileData)
            {
                throw new Exception("Cannot Initialize a Tile Object Manually Only Game Grid Can Update Grid Tile Object ");
            }

            public new void UpdateTileObject(IGridTileData tileData)
            {
                if (tileData.TileNumber == TileData.TileNumber && tileData.Coordinates == TileData.Coordinates)
                {
                    if (ParentGrid)
                    {
                        ICommand<GameGrid> updateGameGrid = new Commands.UpdateGridTileData(tileData);

                        ParentGrid.ExecuteCommand(updateGameGrid);
                    }
                }
            }
        }

    }
}

