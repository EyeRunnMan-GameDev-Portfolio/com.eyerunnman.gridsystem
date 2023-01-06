using UnityEngine;
using com.eyerunnman.enums;
using com.eyerunnman.gridsystem.Enums;

namespace com.eyerunnman.gridsystem
{
    public interface IGridTileData
        {
            public int TileNumber { get; }

            /// <summary>
            /// Coordinates of tile data
            /// </summary>
            public Vector2Int Coordinates { get; }

            /// <summary>
            /// slant direction of tile data
            /// </summary>
            public Direction SlantDirection { get; }

            /// <summary>
            /// vertical height of tile
            /// </summary>
            public float Height { get; }

            /// <summary>
            /// slant angle of tile data
            /// </summary>
            public float SlantAngle { get; }

            /// <summary>
            /// type of tile data
            /// </summary>
            public GridTileType Type { get; }

            /// <summary>
            /// The resultant up vector of tile based on slant angle and direction
            /// </summary>
            public Vector3 UpVector { get; }

            /// <summary>
            /// The forward vector of tile based on slant angle and direction
            /// *Note:* this will always have a foraward component and will not change based on direction
            /// </summary>
            public Vector3 ForwardVector { get; }

            /// <summary>
            /// The right vector of tile based on slant angle and direction
            /// *Note:* this will always have a right component and will not change based on direction
            /// </summary>
            public Vector3 RightVector { get; }

            /// <summary>
            /// Negative vector of up vector
            /// </summary>
            public Vector3 DownVector { get; }

            /// <summary>
            /// Negative vector of Forward vector
            /// </summary>
            public Vector3 BackVector { get; }

            /// <summary>
            /// Negative vector of Right vector
            /// </summary>
            public Vector3 LeftVector { get; }

            /// <summary>
            /// The Vector3 position of the top left Corner based on slant angle and direction
            /// *NOTE:* this is calculated considering the tile is at 0,0,0 and only angle and direction are considered
            /// </summary>
            public Vector3 TopLeftVertex { get; }

            /// <summary>
            /// The Vector3 position of the top right Corner based on slant angle and direction
            /// *NOTE:* this is calculated considering the tile is at 0,0,0 and only angle and direction are considered
            /// </summary>
            public Vector3 TopRightVertex { get; }

            /// <summary>
            /// The Vector3 position of the top left Corner based on slang angle and direction
            /// *NOTE:* this is calculated considering the tile is at 0,0,0 and only angle and direction are considered
            /// </summary>
            public Vector3 BottomRightVertex { get; }

            /// <summary>
            /// The Vector3 position of the top left Corner based on slang angle and direction
            /// *NOTE:* this is calculated considering the tile is at 0,0,0 and only angle and direction are considered
            /// </summary>
            public Vector3 BottomLeftVertex { get; }

            /// <summary>
            /// The Center of Tile Data based on slant angle
            /// </summary>
            public Vector3 Center { get; }
            /// <summary>
            /// The Height of Leading Edge 
            /// </summary>
            public float LeadingEdgeHeight { get; }

            /// <summary>
            /// Flag if the tile data is defined or undefined
            /// </summary>
            public bool IsUndefined { get; }

            /// <summary>
            /// Static method which returns an undefined IGridTileData
            /// </summary>
            public static IGridTileData Undefined => GridTileData.Undefined;

            /// <summary>
            /// create a clone of refrence data without cloning tile number and coordinates
            /// </summary>
            /// <param name="refrenceData">refrence data</param>
            public void CloneData(IGridTileData refrenceData);

        }
}


