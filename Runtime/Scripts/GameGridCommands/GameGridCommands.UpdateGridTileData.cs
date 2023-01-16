using com.eyerunnman.gridsystem.Internal;
using com.eyerunnman.patterns;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// grid system package namespace
/// </summary>
namespace com.eyerunnman.gridsystem
{
    public partial class GameGridCommands
    {
        public class UpdateGridTileData : ICommand<GameGrid>
        {
            private List<IGridTileData> gridTileDataList;
    
            public UpdateGridTileData(List<GridTileData> gridTileDataList)
            {
                this.gridTileDataList = gridTileDataList.Cast<IGridTileData>().ToList();
            }
    
            public UpdateGridTileData(GridTileData gridTileData)
            {
                this.gridTileDataList = new() { gridTileData };
            }
    
            public void Execute(GameGrid gameGrid)
            {
                foreach (IGridTileData gridTileData in gridTileDataList)
                {
                    gameGrid.UpdateGridTileData(gridTileData);
                }
            }
    
        }
    }
}
