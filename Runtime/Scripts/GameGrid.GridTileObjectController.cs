namespace com.eyerunnman.gridsystem
{
    public partial class GameGrid
    {
        private class GridTileObjectController 
        {
            GridTileObjectInternal gridTileObject;
            private GameGrid parentGrid ;
            private CachedGridTileData data;

            public GridTileObjectController(GridTileObjectInternal gridTileObject)
            {
                this.gridTileObject = gridTileObject;
                data = new(IGridTileData.Undefined);
            }

            public void InitializeTileObject(GameGrid parentGrid, IGridTileData tileData)
            {
                this.parentGrid = parentGrid;
                this.data = new(tileData);
                gridTileObject.InitializeTileObject(parentGrid, tileData);
                gridTileObject.OnTileDataInitialize();

            }

            public void UpdateTileObjectData()
            {
                if (gridTileObject)
                {
                    data = new(parentGrid.GetGridTileDataFromNumber(data.TileNumber));
                    gridTileObject.UpdateTileObject(data);
                }
            }


        }

    }
}

