using com.eyerunnman.interfaces;

/// <summary>
/// grid system package namespace
/// </summary>
namespace com.eyerunnman.gridsystem
{
    public partial class GameGrid
    {
        public partial class Commands
        {
            public class GenerateGameGrid : ICommand<GameGrid>
            {
                GridData gridData;
                GridTileObject tileObjectPrefab;


                public GenerateGameGrid(GridData gridData, GridTileObject tileObjectPrefab)
                {
                    this.gridData = gridData;
                    this.tileObjectPrefab = tileObjectPrefab;
                }
                public GenerateGameGrid(GridData gridData)
                {
                    this.gridData = gridData;
                    this.tileObjectPrefab = null;
                }

                public void Execute(GameGrid gameGrid)
                {
                    if (gridData != null)
                    {

                        gameGrid.GenerateGameGrid(gridData,tileObjectPrefab);
                    }
                }


            }
        }
    }
}
