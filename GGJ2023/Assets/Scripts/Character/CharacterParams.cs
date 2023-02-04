using UnityEngine;

[CreateAssetMenu(fileName = "CharacterParams", menuName = "CharacterParams")]
public class CharacterParams : ScriptableObject
{
    public int RadarsSpawnLimit;
    private int _currentRadarsSpawned;

    public bool CanSpawnRadar()
    {
        return _currentRadarsSpawned < RadarsSpawnLimit;
    }

    public void IncreaseRadarCounter()
    {
        _currentRadarsSpawned++;
    }

    public void RemoveRadar()
    {
        _currentRadarsSpawned--;
    }
    
    public void Reset()
    {
        _currentRadarsSpawned = 0;
    }
    

}
