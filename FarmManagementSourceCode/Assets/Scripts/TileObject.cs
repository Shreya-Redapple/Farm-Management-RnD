using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public int rowSize;
    public int columnSize;
    public TileType tileType = TileType.none;
    public Vector3 initialPosition;

    public void SetInitialPosition()
    {
        transform.localPosition = initialPosition;
    }
}
