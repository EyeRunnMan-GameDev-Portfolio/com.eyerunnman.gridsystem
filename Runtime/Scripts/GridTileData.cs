using UnityEngine;
using System;
using com.eyerunnman.enums;
using com.eyerunnman.gridsystem.Internal;

namespace com.eyerunnman.gridsystem
{

    /// <summary>
    /// Grid Tile Data contains data about the tile such as tile id , coordinates in grid , height at which the tile is and so on 
    /// </summary>
    [Serializable]
    public struct GridTileData : IGridTileData
    {
        [SerializeField]
        private int tileNumber;
        [SerializeField]
        private Vector2Int coordinates;
        [SerializeField]
        private float height;
        [SerializeField]
        private Direction slantDirection;
        [SerializeField]
        private float slantAngle;
        [SerializeField]
        private GridTileType type;

        /// <summary>
        /// The default Grid Tile Data
        /// </summary>
        public static GridTileData Undefined => new(-1,new Vector2Int(-1,-1));

        /// <summary>
        /// Check weather a tile is Default or not
        /// </summary>
        public bool IsUndefined => TileNumber == Undefined.TileNumber && Coordinates == Undefined.Coordinates;

        #region Constructors

        /// <summary>
        /// constructer for generating grid tile data with the corresponding values
        /// </summary>
        /// <param name="tileNumber">tile id</param>
        /// <param name="coordinates">coordinate position in grid</param>
        /// <param name="height">height</param>
        /// <param name="slantDirection">slant direction of tile</param>
        /// <param name="slantAngle">slant angle , this follows the slant direction</param>
        /// <param name="type">type of tile</param>
        public GridTileData(int tileNumber, Vector2Int coordinates, float height = 0, Direction slantDirection = Direction.North, float slantAngle = 0, GridTileType type = GridTileType.Undefined)
            {
                this.tileNumber = tileNumber;
                this.coordinates = coordinates;
                this.height = height;
                this.slantDirection = slantDirection;
                this.slantAngle = slantAngle;
                this.type = type;
            }

        /// <summary>
        /// Constructor to Generate a new tile data based on refrence tile data
        /// </summary>
        /// <param name="data">refrence tile data</param>
        public GridTileData(IGridTileData data) {

                if (data == (IGridTileData)Undefined)
                {
                    this = Undefined;
                    return;
                }

                this = new(data.TileNumber, data.Coordinates, data.Height, data.SlantDirection, data.SlantAngle, data.Type);

            }
        #endregion

        #region Properties

        /// <summary>
        /// TileId for tile data
        /// </summary>
        public int TileNumber => tileNumber;

        /// <summary>
        /// Coordinates of tile data
        /// </summary>
        public Vector2Int Coordinates => coordinates;

        /// <summary>
        /// slant direction of tile data
        /// </summary>
        public Direction SlantDirection
        {
            get => slantDirection;
            set => slantDirection = value;
        }

        /// <summary>
        /// vertical height of tile
        /// </summary>
        public float Height {
            get => height;
            set => height = value;
        }

        /// <summary>
        /// slant angle of tile data
        /// </summary>
        public float SlantAngle
        {
            get => slantAngle;
            set => slantAngle = value;
        }

        /// <summary>
        /// type of tile data
        /// </summary>
        public GridTileType Type
        {
            get => type;
            set => type = value;
        }

        /// <summary>
        /// The resultant up vector of tile based on slant angle and direction
        /// </summary>
        public Vector3 UpVector
        {
            get
            {
                Vector3 slantDirectionVector = new();

                switch (slantDirection)
                {
                    case Direction.North:
                        slantDirectionVector = Vector3.forward;
                        if (slantAngle == 90)
                        {
                            return Vector3.back;
                        }
                        break;
                    case Direction.South:
                        slantDirectionVector = Vector3.back;
                        if (slantAngle == 90)
                        {
                            return Vector3.forward;
                        }
                        break;
                    case Direction.East:
                        slantDirectionVector = Vector3.right;
                        if (slantAngle == 90)
                        {
                            return Vector3.left;
                        }
                        break;
                    case Direction.West:
                        slantDirectionVector = Vector3.left;
                        if (slantAngle == 90)
                        {
                            return Vector3.right;
                        }
                        break;
                    default:
                        break;
                }

                Vector3 aboutAxis = Vector3.Cross(slantDirectionVector, Vector3.up);

                Vector3 upwardVector = Quaternion.AngleAxis(slantAngle, aboutAxis) * Vector3.up;

                return upwardVector.normalized;
            }

        }

        /// <summary>
        /// The forward vector of tile based on slant angle and direction
        /// *Note:* this will always have a foraward component and will not change based on direction
        /// </summary>
        public Vector3 ForwardVector
            {
                get
                {
                    switch (slantDirection)
                    {
                        case Direction.North:
                            if (slantAngle == 90)
                            {
                                return Vector3.up;
                            }
                            break;
                        case Direction.South:
                            if (slantAngle == 90)
                            {
                                return Vector3.down;
                            }
                            break;
                        case Direction.East:
                            if (slantAngle == 90)
                            {
                                return Vector3.forward;
                            }
                            break;
                        case Direction.West:
                            if (slantAngle == 90)
                            {
                                return Vector3.forward;
                            }
                            break;
                        default:
                            break;
                    }


                    Vector3 aboutAxis = Vector3.Cross(UpVector, Vector3.forward);

                    Vector3 forwardVector = Quaternion.AngleAxis(90, aboutAxis) * UpVector;

                    return forwardVector.normalized;
                }
            }

        /// <summary>
        /// The right vector of tile based on slant angle and direction
        /// *Note:* this will always have a right component and will not change based on direction
        /// </summary>
        public Vector3 RightVector
        {
            get
            {
                switch (slantDirection)
                {
                    case Direction.North:
                        if (slantAngle == 90)
                        {
                            return Vector3.right;
                        }
                        break;
                    case Direction.South:
                        if (slantAngle == 90)
                        {
                            return Vector3.right;
                        }
                        break;
                    case Direction.East:
                        if (slantAngle == 90)
                        {
                            return Vector3.up;
                        }
                        break;
                    case Direction.West:
                        if (slantAngle == 90)
                        {
                            return Vector3.down;
                        }
                        break;
                    default:
                        break;
                }


                Vector3 aboutAxis = Vector3.Cross(UpVector, Vector3.right);

                Vector3 rightVector = Quaternion.AngleAxis(90, aboutAxis) * UpVector;

                return rightVector.normalized;
            }
        }

        /// <summary>
        /// Negative vector of up vector
        /// </summary>
        public Vector3 DownVector
        {
            get => -UpVector.normalized;
        }

        /// <summary>
        /// Negative vector of Forward vector
        /// </summary>
        public Vector3 BackVector
        {
            get => -ForwardVector.normalized;
        }

        /// <summary>
        /// Negative vector of Right vector
        /// </summary>
        public Vector3 LeftVector
        {
            get => -RightVector.normalized;
        }

        /// <summary>
        /// The Vector3 position of the top left Corner based on slant angle and direction
        /// *NOTE:* this is calculated considering the tile is at 0,0,0 and only angle and direction are considered
        /// </summary>
        public Vector3 TopLeftVertex
        {
            get
            {
                Vector3 Vertex = slantDirection switch
                {
                    Direction.North or Direction.South => (ForwardVector * (1 + SlantGap) + LeftVector) / 2,
                    Direction.East or Direction.West => (LeftVector * (1 + SlantGap) + ForwardVector) / 2,
                    _ => Vector3.zero,
                };
                return Vertex + Vector3.up * SlantHeightOffset + TileCenter;
            }
        }

        /// <summary>
        /// The Vector3 position of the top right Corner based on slant angle and direction
        /// *NOTE:* this is calculated considering the tile is at 0,0,0 and only angle and direction are considered
        /// </summary>
        public Vector3 TopRightVertex
        {
            get
            {
                Vector3 Vertex = slantDirection switch
                {
                    Direction.North or Direction.South => (ForwardVector * (1 + SlantGap) + RightVector) / 2,
                    Direction.East or Direction.West => (RightVector * (1 + SlantGap) + ForwardVector) / 2,
                    _ => Vector3.zero,
                };
                return Vertex + Vector3.up * SlantHeightOffset + TileCenter;
            }
        }

        /// <summary>
        /// The Vector3 position of the top left Corner based on slang angle and direction
        /// *NOTE:* this is calculated considering the tile is at 0,0,0 and only angle and direction are considered
        /// </summary>
        public Vector3 BottomRightVertex
        {
            get
            {
                Vector3 Vertex = slantDirection switch
                {
                    Direction.North or Direction.South => (BackVector * (1 + SlantGap) + RightVector) / 2,
                    Direction.East or Direction.West => (RightVector * (1 + SlantGap) + BackVector) / 2,
                    _ => Vector3.zero,
                };
                return Vertex + Vector3.up * SlantHeightOffset + TileCenter;
            }
        }

        /// <summary>
        /// The Vector3 position of the top left Corner based on slang angle and direction
        /// *NOTE:* this is calculated considering the tile is at 0,0,0 and only angle and direction are considered
        /// </summary>
        public Vector3 BottomLeftVertex
        {
            get
            {
                Vector3 Vertex =  slantDirection switch
                {
                    Direction.North or Direction.South => (BackVector * (1 + SlantGap) + LeftVector) / 2,
                    Direction.East or Direction.West => (LeftVector * (1 + SlantGap) + BackVector) / 2,
                    _ => Vector3.zero,
                };

                return Vertex + Vector3.up * SlantHeightOffset + TileCenter;
            }
        }

        /// <summary>
        /// The Center of Tile Data based on slant angle
        /// </summary>
        public Vector3 Center => new(Coordinates.x, Height + SlantHeightOffset, Coordinates.y);

        /// <summary>
        /// The Height of Leading Edge 
        /// </summary>
        public float LeadingEdgeHeight
        {
            get
            {
                float angle = slantAngle;
                float hypotenuse = 1 + SlantGap;

                float sinvalue = Mathf.Sin(angle * Mathf.Deg2Rad);

                return sinvalue * hypotenuse;
            }
            set
            {
                slantAngle = Vector2.Angle(Vector2.up * value + Vector2.right, Vector2.right);
                if (value < 0)
                {
                    this.height = height - Mathf.Abs(value);
                    this.slantDirection = slantDirection switch

                    {
                        Direction.North => Direction.South,
                        Direction.South => Direction.North,
                        Direction.East => Direction.West,
                        Direction.West => Direction.East,
                        _ => slantDirection,
                    };
                }
            }
        }

        private Vector3 TileCenter => new(Coordinates.x, Height, Coordinates.y);

        private float SlantHeightOffset
        {
            get
            {
                float angle = 90 - slantAngle;
                float hypotenuseLenght = 1 + SlantGap;

                float cosVal = Mathf.Cos(angle*Mathf.Deg2Rad);

                return cosVal * hypotenuseLenght/2;
            }
            
        }

        private float SlantGap
        {
            get
            {
                float hypotenuseLength = 1 / Mathf.Cos(slantAngle * Mathf.Deg2Rad);

                return (hypotenuseLength-1);
            }
        }


        #endregion

        #region Public Methods
        /// <summary>
        /// To Update tile orientation except tile id and coordinates
        /// </summary>
        /// <param name="data">refrence Data</param>
        public void CloneData(IGridTileData refrenceData)
        {
            this.height = refrenceData.Height;
            this.slantDirection = refrenceData.SlantDirection;
            this.slantAngle = refrenceData.SlantAngle;
            this.type = refrenceData.Type;
        }


        #endregion
    }

}

