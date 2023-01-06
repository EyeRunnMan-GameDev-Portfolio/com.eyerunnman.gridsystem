using com.eyerunnman.enums;
using com.eyerunnman.gridsystem.Internal;
using com.eyerunnman.interfaces;
using System;
using UnityEngine;

namespace com.eyerunnman.gridsystem
{
    public class GridTileObject : GridTileObjectInternal
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
                    ICommand<GameGrid> updateGameGrid = new GameGrid.Commands.UpdateGridTileData(tileData);
    
                    ParentGrid.ExecuteCommand(updateGameGrid);
                }
            }
        }
    }

}

