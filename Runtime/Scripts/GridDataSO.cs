using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.eyerunnman.gridsystem;
using System;
using UnityEditor;

namespace com.eyerunnman.gridsystem
{
    /// <summary>
    /// Grid Data Scriptable object which is used to store grid data
    /// </summary>
    [CreateAssetMenu(fileName = "GridData", menuName = "EyeRunnMan/GridSystem/GridData", order = 100)]
    public class GridDataSO : ScriptableObject
    {
        /// <summary>
        /// Grid Data stored by `GridDataSO`
        /// </summary>
        public GridData GridData;
    }
}



