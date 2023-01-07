using com.eyerunnman.gridsystem.Internal;
using UnityEngine;

namespace com.eyerunnman.gridsystem
{
    /// <summary>
    /// Abstract class for grid tile object
    /// </summary>
    public class GridTileObject : MonoBehaviour
    {
        private CachedGridTileData cachedTileData;
    
        /// <summary>
        /// Current Tile Data for the grid TileObject
        /// </summary>
        public IGridTileData TileData { get=> cachedTileData; }
    
        /// <summary>
        /// Getter for parentGrid Refrence
        /// </summary>
        protected GameGrid ParentGrid { get; private set; }
    
        internal void InitializeTileObject(GameGrid parentGrid, IGridTileData tileData) {

                ParentGrid = parentGrid;
                cachedTileData = new CachedGridTileData(tileData);
                OnTileDataInitialize();
                OnTileDataInitialize(tileData);
        }

        internal void UpdateTileObject(IGridTileData tileData)
            {
                cachedTileData = new CachedGridTileData(tileData);
                OnTileDataUpdate();
                OnTileDataUpdate(tileData);
            }
    
        /// <summary>
        /// Is called whever tile data is updated
        /// </summary>
        public virtual void OnTileDataUpdate() { }
        public virtual void OnTileDataUpdate(IGridTileData tileData) { }


        /// <summary>
        /// Is called whenever tile data is initialized
        /// </summary>
        public virtual void OnTileDataInitialize() { }
        public virtual void OnTileDataInitialize(IGridTileData tileData) { }

    }

}

