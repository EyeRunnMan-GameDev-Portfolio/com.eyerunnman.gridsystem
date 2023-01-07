namespace com.eyerunnman.gridsystem.Internal
{
    internal class GridTileObjectController 
    {
        GridTileObject gridTileObject;
        private GameGrid parentGrid ;
        private CachedGridTileData data;
    
        public GridTileObjectController(GridTileObject gridTileObject)
        {
            this.gridTileObject = gridTileObject;
            data = new(IGridTileData.Undefined);
        }
    
        public void InitializeTileObject(GameGrid parentGrid, IGridTileData tileData)
        {
            this.parentGrid = parentGrid;
            this.data = new(tileData);
            gridTileObject.InitializeTileObject(parentGrid, tileData);
            
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

