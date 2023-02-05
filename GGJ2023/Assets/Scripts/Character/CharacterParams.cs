using UnityEngine;

[CreateAssetMenu(fileName = "CharacterParams", menuName = "GGJ/CharacterParams")]
public class CharacterParams : ScriptableObject
{
    public int RadarsSpawnLimit;
    public int CurrentRadarsSpawned;

    public bool CanSpawnRadar()
    {
        return CurrentRadarsSpawned < RadarsSpawnLimit;
    }

    public void IncreaseRadarCounter()
    {
        CurrentRadarsSpawned++;
    }

    public void RemoveRadar()
    {
        CurrentRadarsSpawned--;
    }
    
    public void Reset()
    {
        CurrentRadarsSpawned = 0;
    }
    

}
