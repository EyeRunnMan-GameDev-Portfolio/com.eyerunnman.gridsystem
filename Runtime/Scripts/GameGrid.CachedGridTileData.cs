using UnityEngine;
using System;
using com.eyerunnman.enums;

namespace com.eyerunnman.gridsystem
{

    public partial class GameGrid
    {
        /// <summary>
        /// Cached Grid Tile Data which stores the computed vale so new values is not needed to be computed every time a value is accessed from the grid Tile Data
        /// </summary>
        private struct CachedGridTileData : IGridTileData
        {
            public CachedGridTileData(IGridTileData data)
            {
                this = new();

                this.TileNumber = data.TileNumber;
                this.Coordinates = data.Coordinates;
                this.SlantDirection = data.SlantDirection;
                this.Height = data.Height;
                this.SlantAngle = data.SlantAngle;
                this.Type = data.Type;
                this.UpVector = data.UpVector;
                this.ForwardVector = data.ForwardVector;
                this.RightVector = data.RightVector;
                this.DownVector = data.DownVector;
                this.BackVector = data.BackVector;
                this.LeftVector = data.LeftVector;
                this.TopLeftVertex = data.TopLeftVertex;
                this.TopRightVertex = data.TopRightVertex;
                this.BottomRightVertex = data.BottomRightVertex;
                this.BottomLeftVertex = data.BottomLeftVertex;
                this.Center = data.Center;
                this.LeadingEdgeHeight = data.LeadingEdgeHeight;
                this.IsUndefined = data.IsUndefined;
            }

            public int TileNumber { get; private set; }
            public Vector2Int Coordinates { get; private set; }

            public Direction SlantDirection { get; private set; }

            public float Height { get; private set; }

            public float SlantAngle { get; private set; }

            public TileType Type { get; private set; }

            public Vector3 UpVector { get; private set; }

            public Vector3 ForwardVector { get; private set; }

            public Vector3 RightVector { get; private set; }

            public Vector3 DownVector { get; private set; }

            public Vector3 BackVector { get; private set; }

            public Vector3 LeftVector { get; private set; }

            public Vector3 TopLeftVertex { get; private set; }

            public Vector3 TopRightVertex { get; private set; }

            public Vector3 BottomRightVertex { get; private set; }

            public Vector3 BottomLeftVertex { get; private set; }

            public Vector3 Center { get; private set; }

            public float LeadingEdgeHeight { get; private set; }

            public bool IsUndefined { get; private set; }
        }

    }

}

