using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int rowCount;
    [SerializeField] private int colCount;
    [SerializeField] public int tileSize;
    [SerializeField] private float yOffset;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] public GridTile[,] tileGrid;
    [SerializeField] private LineDrawer line;
    public GameObject demoCube;
    Vector3 colliderInitialPosition;
    void Start()
    {
        this.transform.position = initialPos;
        colliderInitialPosition = new Vector3(initialPos.x-.5f*tileSize,yOffset, initialPos.z - .5f* tileSize);
        CreateGrid();
    }
    /// <summary>
    /// Creates the grid when the game starts
    /// </summary>
    void CreateGrid()
    {
        tileGrid = new GridTile[colCount, rowCount];
        //Make the grid
        for (int c = 0; c < rowCount; c++)
        {
            for (int r = 0; r < colCount; r++)
            {
                //Create a new gridspace 
                tileGrid[r, c] = new GridTile();
                Vector3 tilePos = new Vector3((float)(initialPos.x + r * tileSize), yOffset, (float)(initialPos.y + c * tileSize));
                tileGrid[r, c].SetTileData(c, r, tilePos,demoCube);
            }
        }
        CreateBoxCollider();
        AddPointsToList();
    }
    /// <summary>
    /// Create a Box Collider as a outer boundary
    /// </summary>
    void CreateBoxCollider()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        float centerX = colCount % 2 != 0?(tileGrid[(colCount - 1)/2, 0].tilePos.x): (tileGrid[colCount/2, 0].tilePos.x + tileGrid[(colCount/2) - 1, 0].tilePos.x)/2.0f;
        float centerZ = rowCount % 2 != 0?(tileGrid[0, (rowCount - 1)/2].tilePos.z) :((tileGrid[0, rowCount/2].tilePos.z + tileGrid[0, (rowCount/2) - 1].tilePos.z)/2.0f);
        boxCollider.center = new Vector3(centerX, yOffset-1, centerZ)-initialPos;
        boxCollider.size = new Vector3(colCount * tileSize, tileSize, rowCount * tileSize);
    }
    /// <summary>
    /// Create a list for Line Renderer
    /// </summary>
    void AddPointsToList()
    {
        float x = colliderInitialPosition.x;
        float z = colliderInitialPosition.z;
        //Debug.Log(x + "" + z);
        for (int j = 0; j < 2 * (colCount + 1); j++)
        {
            if (j > 0)
            {
                if (j % 2 != 0)
                {
                    z = (z == colliderInitialPosition.z) ? colliderInitialPosition.z + rowCount * tileSize : colliderInitialPosition.z;
                }
                else
                {
                    x = x + tileSize;
                }
            }
            line.AddPoints(new Vector3(x, yOffset, z));
        }

        for (int i = 1; i < 2 * (rowCount + 1); i++)
        {
            if (i > 0)
            {
                if (i % 2 != 0)
                {
                    x = (x == colliderInitialPosition.x) ? colliderInitialPosition.x + colCount * tileSize : colliderInitialPosition.x;
                }
                else
                {
                    z = (colCount % 2 == 0)?z - tileSize: z + tileSize;
                }
            }
            line.AddPoints(new Vector3(x, yOffset, z));
        }
        line.DrawLine();
    }
    /// <summary>
    /// Gets the tile row column index from world position
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public GridTile GetTileDataFromMouseInput(Vector3 worldPosition)
    {
        //Debug.Log($" *** inputWorldPosition : {inputWorldPosition}");
        int x = (int)(worldPosition.x- colliderInitialPosition.x) /tileSize;
        int z = (int)(worldPosition.z - colliderInitialPosition.z) / tileSize;
       //Debug.Log($"Clicked On :  row { z} col { x}");
        if ((z >= 0 || z < rowCount) && (x>= 0  ||  x < colCount))
            return tileGrid[x,z];
        else
            return null;
    }
}
