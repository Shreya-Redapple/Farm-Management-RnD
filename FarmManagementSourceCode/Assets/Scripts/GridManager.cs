using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int rowCount;
    [SerializeField] private int columnCount;
    [SerializeField] private int tileSize;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] private float yOffset;
    [SerializeField] private Tile[,] tileGrid;
    public GameObject demoCube;
    [SerializeField] private LineDrawer line;
    Vector3 colliderInitialPosition;
    void Start()
    {
        this.transform.position = initialPos;
        colliderInitialPosition = new Vector3(initialPos.x-.5f*tileSize,yOffset, initialPos.z - .5f* tileSize);
        CreateGrid();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                GetTileDataFromMouseInput(hit.point);
            }
        }
    }
    // Creates the grid when the game starts
    void CreateGrid()
      {
        tileGrid = new Tile[rowCount, columnCount];
        //Make the grid
        for (int r = 0; r < rowCount; r++)
        {
            for (int c = 0; c < columnCount; c++)
            {
                //Create a new gridspace 
                tileGrid[r, c] = new Tile();
                Vector3 tilePos = new Vector3((float)(initialPos.x + r * tileSize), yOffset, (float)(initialPos.y + c * tileSize));
                tileGrid[r, c].SetTileData(r, c, tilePos,demoCube);
            }
        }
        CreateBoxCollider();
        AddPointsToList();
    }
    void CreateBoxCollider()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        float centerX = rowCount % 2 != 0?(tileGrid[(rowCount - 1) / 2, 0].tilePos.x): (tileGrid[rowCount / 2, 0].tilePos.x + tileGrid[(rowCount / 2) - 1, 0].tilePos.x) / 2.0f;
        float centerZ = columnCount % 2 != 0?(tileGrid[0, (columnCount - 1) / 2].tilePos.z) :((tileGrid[0, columnCount / 2].tilePos.z + tileGrid[0, (columnCount / 2) - 1].tilePos.z) / 2.0f);
        boxCollider.center = new Vector3(centerX, yOffset, centerZ)-initialPos;
        boxCollider.size = new Vector3(rowCount * tileSize, tileSize, columnCount * tileSize);


    }

    void AddPointsToList()
    {
        float x = colliderInitialPosition.x;
        float z = colliderInitialPosition.z;
        Debug.Log(x + "" + z);
        for (int i = 0; i < 2*(columnCount+1); i++)
        {
            if (i > 0)
            {
                if (i % 2 != 0)
                {
                    if(x== colliderInitialPosition.x)
                    {
                        x = colliderInitialPosition.x + rowCount * tileSize;

                    }
                    else {
                        x = colliderInitialPosition.x;
                    }
                }
                else
                {
                    z = z + tileSize;
                }
            }
            line.AddPoints(new Vector3(x, yOffset+2, z));
        }
        for (int j = 0; j < 2 * (rowCount + 1); j++)
        {
            if (j > 0)
            {
                if (j % 2 != 0)
                {
                    if (z == colliderInitialPosition.z)
                    {
                        z = colliderInitialPosition.z + columnCount * tileSize;

                    }
                    else
                    {
                        z = colliderInitialPosition.z;
                    }
                }
                else
                {
                    x = x - tileSize;
                }
            }
            line.AddPoints(new Vector3(x, yOffset + 2, z));
        }
        line.DrawLine();
    }
    /// <summary>
    /// Gets the tile row column index from world position
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Tile GetTileDataFromMouseInput(Vector3 worldPosition)
    {
        //Debug.Log($" *** inputWorldPosition : {inputWorldPosition}");
        int x = (int)(worldPosition.x- colliderInitialPosition.x) /tileSize;
        int z = (int)(worldPosition.z - colliderInitialPosition.z) / tileSize;
        Debug.Log($"Input :  row { x} col { z}");
        if ((x >= 0 || x < rowCount) && (z>= 0  ||  z < columnCount))
            return tileGrid[x, z];
        else
            return null;
    }
  
}
