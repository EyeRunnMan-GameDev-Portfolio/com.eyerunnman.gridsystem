using com.eyerunnman.interfaces;
using System.Collections.Generic;
using System.Linq;
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
                GridTileObjectInternal tileObjectPrefab;

                public GenerateGameGrid(Vector2Int dimension,List<GridTileData> gridTileDataList, GridTileObjectInternal tileObjectPrefab)
                {
                    this.gridData = new(dimension, gridTileDataList);
                    this.tileObjectPrefab = tileObjectPrefab;
                }

                public GenerateGameGrid(Vector2Int dimension, List<GridTileData> gridTileDataList)
                {
                    this.gridData = new(dimension, gridTileDataList);
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
        }
    }
}
