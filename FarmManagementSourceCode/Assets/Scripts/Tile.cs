using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Tile : MonoBehaviour
{
    public int row;
    public int col;
    public Vector3 tilePos;

    /// <summary>
    /// Set the position of this grid cell on the grid
    /// </summary>
    /// <param name="_row"> row index of particular tile </param>
    /// <param name="_col"> column index of particular tile</param>
    /// <param name="_pos"> Position of particular tile</param>
    /// <param name="demoCube"> Cube instantiated at tile position</param>
    public void SetTileData(int _row, int _col, Vector3 _pos, GameObject demoCube)
    {
        row = _row;
        col = _col;
        tilePos = _pos;
        //Debug.Log("<color=red>" + $" row { _row} col { _col} pos { _pos} " + "</color>");
        Instantiate(demoCube, tilePos,Quaternion.identity);
    }

}





