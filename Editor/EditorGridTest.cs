using com.eyerunnman.gridsystem;
using com.eyerunnman.gridsystem.Editor;
using com.eyerunnman.interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameGrid))]
public class EditorGridTest : GridTileObject
{
    [SerializeField]
    EditorGridDataSO dataSO;
    [SerializeField]
    GameGrid gameGrid;
    [SerializeField]
    GridTileObject prefab;
    public override void OnTileDataInitialize()
    {
        if (dataSO != null&&gameGrid!=null)
        {
            ICommand<GameGrid> generateGridCommand = new GameGrid.Commands.GenerateGameGrid(dataSO.GridGenerationData.dimension,dataSO.GridGenerationData.tileDatalist,prefab);

            gameGrid.ExecuteCommand(generateGridCommand);
        }
        transform.localPosition = TileData.Center;
        transform.localScale = Vector3.one * 0.1f;
    }

    public override void OnTileDataUpdate()
    {
        transform.localPosition = TileData.Center;
        transform.localScale = Vector3.one* 0.1f;
    }
}
