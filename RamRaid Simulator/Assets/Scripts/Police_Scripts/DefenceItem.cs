using UnityEngine;

[CreateAssetMenu(menuName = "Game/Defence Item")]
public class DefenceItem : ScriptableObject
{
    public string id;         // name of the item 
    public GameObject prefab; // what object to spawn in the world
    public int cost = 10;     // how much it costs from budget
}
