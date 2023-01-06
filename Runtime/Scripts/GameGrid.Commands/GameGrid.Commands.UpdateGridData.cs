using com.eyerunnman.gridsystem.Internal;
using com.eyerunnman.interfaces;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// grid system package namespace
/// </summary>
namespace com.eyerunnman.gridsystem
{
    public partial class GameGrid
    {
        public partial class Commands
        {
            public class UpdateGridData : ICommand<GameGrid>
            {
                private GridData gridData;

                public UpdateGridData(Vector2Int dimension, List<GridTileData> gridTileDataList)
                {
                    this.gridData = new(dimension, gridTileDataList);
                }

                public void Execute(GameGrid context)
                {
                    context.UpdateGameGridData(gridData);
                }
            }
        }
    }
}
