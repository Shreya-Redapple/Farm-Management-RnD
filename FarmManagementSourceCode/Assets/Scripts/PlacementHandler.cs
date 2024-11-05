using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementHandler : MonoBehaviour
{
    public GridManager gridManager;
    public static TileObject selectedTiles;
    int rowLength;
    int colLength;
    GridTile tiledata;
    bool canPlace = false;
    Vector3 initialPosition;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.point);
                tiledata = gridManager.GetTileDataFromMouseInput(hit.point);
                //Debug.Log($"TileData received :  row { tiledata.row} col { tiledata.col} tilePos { tiledata.tilePos}");
               // Debug.Log($"GridTileData received :  row { gridManager.tileGrid.GetLength(1)} col { gridManager.tileGrid.GetLength(0)} ");
                // For FDebugging all the grids
                //for (int j = 0; j < gridManager.tileGrid.GetLength(1); j++)
                //{
                //    for (int i = 0; i < gridManager.tileGrid.GetLength(0); i++)
                //{

                //        Debug.Log("<color=red>" + $"Tile Data :  row >> { gridManager.tileGrid[i, j].row } col >> { gridManager.tileGrid[i, j].col}  pos >> { gridManager.tileGrid[i, j].tilePos }" + "</color>");
                //    }
                //}
                if (selectedTiles != null)
                {
                Debug.Log($"SelectedTileData received :  row { selectedTiles.rowSize} col { selectedTiles.columnSize} type { selectedTiles.tileType}");
                    initialPosition = selectedTiles.transform.position;
                    rowLength = selectedTiles.rowSize / gridManager.tileSize;
                    colLength = selectedTiles.columnSize / gridManager.tileSize;
                    //Debug.Log($"SelectedTileData Data :  rowRequired {rowLength } colRequired { colLength} "); 
                    if (CheckPlacementOnTiles(rowLength, colLength, tiledata))
                    {
                        Debug.Log($"SelectedTileData Data :  rowRequired {rowLength } colRequired { colLength} "); 
                        PlaceTileOnGrid(rowLength, colLength, tiledata);
                    }
                    
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
          
            //if (!CheckPlacementOnTiles(rowLength, colLength, tiledata))
            //{
            //    selectedTiles.transform.position= initialPosition;
            //}
            //else 
            if (selectedTiles != null && canPlace)
            {
                MarkTileMarked(rowLength, colLength, tiledata);
                selectedTiles = null;
            }
        }
    }
    bool CheckPlacementOnTiles(int _rowLength, int _colLength, GridTile selectedtileData)
    {
        Debug.Log($"CheckPlacementOnTiles :  _rowLength {_rowLength } _colLength { _colLength} ");
        for (int i = selectedtileData.row; i < selectedtileData.row+_rowLength; i++)
        {
            for (int j = selectedtileData.col; j < selectedtileData.col+_colLength; j++)
            {
                //Debug.Log("<color=green>" + $"Tile Data :  row >> { gridManager.tileGrid[j, i].row }  col >> { gridManager.tileGrid[j, i].col}" + "</color>");
                Debug.Log("<color=green>" + $"Tile Data :  j >> { j }  i >> {i}" + "</color>");
                if (i < gridManager.tileGrid.GetLength(1) && j < gridManager.tileGrid.GetLength(0))
                {
                    if (gridManager.tileGrid[j, i].isOccupied)
                    {
                        return false;
                    }
                }
                else
                {
                    Debug.Log("<color=magenta>" + $"Tile Placement not Possible" + "</color>");
                    return false;
                }
                //return false;
            }
        }
        canPlace = true;
        //selectedTiles = null;
        return true;
    }
    void PlaceTileOnGrid(int _rowLength, int _colLength, GridTile selectedtileData)
    {
        int offset = gridManager.tileSize / 2;
        Vector3 tileBottomLeftCorner = selectedtileData.tilePos - new Vector3(offset, 0, offset);
        //Debug.Log("<color=yellow>" + $"tileBottomLeftCorner >> { tileBottomLeftCorner } " + "</color>");
        Debug.Log("<color=aqua>" + $"selectedTiles.rowSize >> { selectedTiles.rowSize } selectedTiles.columnSize >> { selectedTiles.columnSize } " + "</color>");
        //Debug.Log("<color=purple>" + $"tileBottomLeftCorner >> { tileBottomLeftCorner } " + "</color>");
        selectedTiles.transform.position = new Vector3(tileBottomLeftCorner.x +(selectedTiles.columnSize/2), selectedTiles.transform.position.y, tileBottomLeftCorner.z + (selectedTiles.rowSize / 2));
        //selectedTiles.transform.position = tiledata.tilePos;
        Debug.Log("<color=blue>" + $"SelectedTile Pos  >> { selectedTiles.transform.position } " + "</color>");
    }
    void MarkTileMarked(int _rowLength, int _colLength, GridTile selectedtileData)
    {
        for (int i = selectedtileData.row; i < selectedtileData.row + _rowLength; i++)
        {
            for (int j = selectedtileData.col; j < selectedtileData.col + _colLength; j++)
            {
                //Debug.Log("<color=green>" + $"Tile Data :  row >> { gridManager.tileGrid[j, i].row } col >> { gridManager.tileGrid[j, i].col} " + "</color>");
                if (i < gridManager.tileGrid.GetLength(1) && j < gridManager.tileGrid.GetLength(0))
                {
                    gridManager.tileGrid[j, i].isOccupied = true;
                }
            }
        }
        canPlace = false;
    }



}
