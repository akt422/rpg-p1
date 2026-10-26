using UnityEngine;

public class FieldSlot
{
    public Vector3Int Cell { get; private set; }
    public bool IsPlanted { get; set; }
    
    public  FieldSlot(Vector3Int cell, bool isPlanted)
    {
        Cell = cell;
        IsPlanted = isPlanted;
    }
}