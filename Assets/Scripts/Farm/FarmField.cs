
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmField : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase tile;
    [SerializeField] private TileBase baseTile;
    private List<FieldSlot> fieldSlots = new List<FieldSlot>();
    private Dictionary<Vector3Int, bool> grid = new Dictionary<Vector3Int, bool>();
    [SerializeField] private GameObject highlightPrefab;
    private float waitTime = 5f;
    [SerializeField] private GameObject seedAnimationPrefab;
    [SerializeField] private GameObject saplingAnimationPrefab;
    [SerializeField] private GameObject fruitAnimationPrefab;

    public Dictionary<Vector3Int, bool> GetSlots()
    {
        return grid;
    }

    public GameObject GetHighlightPrefab()
    {
        return highlightPrefab;
    }
    
    public Vector3 GetCellCenter(Vector3Int cell)
    {
        return tilemap.GetCellCenterWorld(cell);
    }
    
    void Start()
    {
        foreach (Vector3Int cell in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3 position = tilemap.GetCellCenterWorld(cell);
            if (!tilemap.HasTile(cell))
                continue;
        
            // tilemap.SetTile(cell, tile);
            fieldSlots.Add(new FieldSlot(cell, false));
            grid.Add(cell, false);
        }
    }

    public bool TryPlant(List<Vector3Int> cells)
    {
        foreach (Vector3Int cell in cells)
        {
            if (!grid.ContainsKey(cell)) return false;
            if (grid[cell]) return false;
        }
        return true;
    }

    public void Plant(List<Vector3Int> cells)
    {
        if (!TryPlant(cells))
        {
            Debug.Log("Cannot plant here, find empty slot");
            return;
        }

        foreach (Vector3Int cell in cells)
        {
            Debug.Log(cell.x + " " + cell.y + " " + cell.z);
            StartCoroutine(PlantCoroutine(cell));
        }
    }

    IEnumerator PlantCoroutine(Vector3Int cell)
    {
        grid[cell] = true;
        GameObject seedAnim = Instantiate(seedAnimationPrefab, GetCellCenter(cell),  Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        Destroy(seedAnim);
        GameObject saplingAnim = Instantiate(saplingAnimationPrefab, GetCellCenter(cell),  Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        Destroy(saplingAnim);
        GameObject fruitAnim = Instantiate(fruitAnimationPrefab, GetCellCenter(cell),  Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        Destroy(fruitAnim);
        grid[cell] = false;
        tilemap.SetTile(cell, baseTile);
    }

    public void Reap(List<Vector3Int> cells)
    {
        foreach (Vector3Int cell in cells)
        {
            grid[cell] = false;
            tilemap.SetTile(cell, tile);
        }
    }

    // void Update()
    // {
    //     if (Mouse.current.leftButton.wasPressedThisFrame)
    //     {
    //         Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
    //
    //         // Convert Screen Space to World Space (Vector3)
    //         Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
    //
    //         // Convert World Space to Grid Cell Space (Vector3Int)
    //         Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
    //         cellPos.z = 0; 
    //         Plant(new Vector3Int[]{cellPos});
    //     }
    // }
    
}