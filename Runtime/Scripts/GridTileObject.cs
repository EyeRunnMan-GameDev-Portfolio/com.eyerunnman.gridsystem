using UnityEngine;

namespace com.eyerunnman.gridsystem
{
    /// <summary>
    /// Abstract class for grid tile object
    /// </summary>
    public abstract class GridTileObject : MonoBehaviour
    {
        /// <summary>
        /// Getter for Getting out the tile data Data from tile object
        /// </summary>
        public GridTileData Data => data;

        private GridTileData data;

        /// <summary>
        /// Function to set up Grid Tile Object with passed tile data
        /// </summary>
        /// <param name="tileData"></param>
        public virtual void SetUpTile(GridTileData tileData)
        {
            data = tileData;
        }
    }
}

