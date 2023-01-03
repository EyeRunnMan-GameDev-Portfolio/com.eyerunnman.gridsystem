using com.eyerunnman.interfaces;
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

                public GenerateGameGrid(Vector2Int dimension)
                {
                    gridData = new(dimension);
                    this.tileObjectPrefab = null;
                }

                public void Execute(GameGrid gameGrid)
                {
                    gameGrid.GenerateGameGrid(gridData,tileObjectPrefab);
                }


            }

            public class UpdateGridData : ICommand<GameGrid>
            {
                private GameGrid.GridData gridData;

                public UpdateGridData(GameGrid.GridData gridData)
                {
                    this.gridData = new(gridData);
                }

                public void Execute(GameGrid context)
                {
                    context.UpdateGameGridData(gridData);
                }
            }
        }
    }
}
