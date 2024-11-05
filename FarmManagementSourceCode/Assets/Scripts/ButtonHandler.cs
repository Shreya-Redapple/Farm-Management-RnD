using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ButtonHandler : MonoBehaviour
{
    [Tooltip("Reference of all Tile Prefabs")]
    public List<TileObject> allTilePrefabs;

    [Tooltip("placed tile reference")]
    private TileObject placementTiles;

    [SerializeField] GameObject ButtonPrefab;

    void GenerateButtons()
    {
        for (int i = 0; i < allTilePrefabs.Count; i++)
        {
            GameObject tileButtons = Instantiate(ButtonPrefab,transform);
            TileType tileType = (TileType)(i+1);
            //Debug.Log(tileType.ToString());
            tileButtons.name = "Button" + tileType.ToString();
            tileButtons.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tileType.ToString();
            tileButtons.GetComponent<Button>().onClick.AddListener(() => OnClickTileButton(tileType));
        }
    }
   
    void Start()
    {
        GenerateButtons();
    }

    public void OnClickTileButton(TileType tileType)
    {
        for (int i = 0; i < allTilePrefabs.Count; i++)
        {
            if (tileType.Equals(allTilePrefabs[i].tileType))
            {
                placementTiles = Instantiate(allTilePrefabs[i]);
                PlacementHandler.selectedTiles = placementTiles;
                placementTiles.SetInitialPosition();
            }
        }
    }

}
