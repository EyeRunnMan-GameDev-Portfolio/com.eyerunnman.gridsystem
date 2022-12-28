using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.eyerunnman.gridsystem.Editor
{
    [CreateAssetMenu(fileName = "GridDebugData", menuName = "GridSystem/GridDebugData", order = 1)]
    public class GridEditorConfigDataSO : ScriptableObject
    {
        public EditorGridTile DebugTileObject;
        [Range(0,1)]
        public float TileHeightSnap = 1f;
    }
}

