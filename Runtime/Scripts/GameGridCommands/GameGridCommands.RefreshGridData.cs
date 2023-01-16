using com.eyerunnman.gridsystem.Internal;
using com.eyerunnman.patterns;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// grid system package namespace
/// </summary>
namespace com.eyerunnman.gridsystem
{
    public partial class GameGridCommands
    {
        public class RefreshGridData : ICommand<GameGrid>
        {
            private GridData gridData;
    
            public RefreshGridData(Vector2Int dimension, List<GridTileData> gridTileDataList)
            {
                this.gridData = new(dimension, gridTileDataList);
            }
    
            public void Execute(GameGrid context)
            {
                context.RefreshGridData(gridData);
            }
        }
    }
}
