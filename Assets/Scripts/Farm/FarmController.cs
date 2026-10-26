using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable All

public class FarmController : MonoBehaviour
{
    public static FarmController Instance { get; private set; }

    private FarmField currentField;
    private Vector3Int origin;
    private bool isActive;
    private Dictionary<Vector3Int, bool> grid;
    List<Vector3Int> selectedCells = new List<Vector3Int>();
    private Vector3Int scale = new Vector3Int(2, 2, 2);
    private GameObject prefab;
    List<GameObject> prefabs = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    } 
    
    public void StartFarmMode(List<ItemEntry> item, FarmField farmField)
    {
        GameStateManager.SetState(GameState.Farming);
        currentField = farmField;
        grid = farmField.GetSlots();
        prefab = farmField.GetHighlightPrefab();
        
        bool found = false;

        foreach (Vector3Int candidate in grid.Keys)
        {
            origin = candidate;

            CreateCropShape(CropShape.Square, 2);

            if (selectedCells.All(cell => grid.ContainsKey(cell)))
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            selectedCells.Clear();
            Debug.Log("No valid 2x2 area found!");
            return;
        }

        GameStateManager.SetState(GameState.Farming);
        RefreshHighlights();
    }

    void Update()
    {
        if(GameStateManager.GetState()  != GameState.Farming) return;
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Trymove(Vector3Int.up);
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            Trymove(Vector3Int.down);
        }
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Trymove(Vector3Int.left);
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Trymove(Vector3Int.right);
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            currentField.Plant(selectedCells);
        }
    }

    private void Trymove(Vector3Int direction)
    {
        List<Vector3Int> proposedCells = new List<Vector3Int>();

        foreach (Vector3Int cell in selectedCells)
        {
            proposedCells.Add(cell + direction);
        }
        if (proposedCells.All(cell => grid.ContainsKey(cell)))
        {
            selectedCells = proposedCells;
            RefreshHighlights();
        }
    }
    
    private void RefreshHighlights()
    {
        ClearGrid();
        prefabs.Clear();

        foreach (Vector3Int cell in selectedCells)
        {
            Vector3 worldPosition = currentField.GetCellCenter(cell);
            GameObject obj = Instantiate(
                prefab,
                worldPosition,
                Quaternion.identity
            );
            prefabs.Add(obj);
        }
    }

    private void CreateCropShape(CropShape shape, int size)
    {
        if (grid == null || grid.Count == 0) return;
        Vector3Int anchor = grid.Keys.First();
        switch (shape)
        {
            case CropShape.Square:
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Vector3Int cell =
                            origin + new Vector3Int(i, j, 0);

                        selectedCells.Add(cell);
                    }
                }
                break;
        }
    }
    
    private void ClearGrid()
    {
        foreach (GameObject obj in prefabs)
        {
            if(obj != null)
                Destroy(obj);
        }
    }

    public void EndFarmMode()
    {
        GameStateManager.SetState(GameState.Gameplay);
    }
    
}