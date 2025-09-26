using UnityEngine;

[CreateAssetMenu(menuName = "Game/Police Config")]
public class PoliceConfig : ScriptableObject
{
    public int maxUnits = 3;     // how many police units can be placed
    public int startBudget = 100; // how much money police start with
}
