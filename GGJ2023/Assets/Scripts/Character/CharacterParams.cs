using UnityEngine;

[CreateAssetMenu(fileName = "CharacterParams", menuName = "CharacterParams")]
public class CharacterParams : ScriptableObject
{
    public int RadarsSpawnCount;
    private int _currentRadarsSpawned;

    public bool CanSpawnRadar()
    {
        return _currentRadarsSpawned < RadarsSpawnCount;
    }

    public void Reset()
    {
        _currentRadarsSpawned = 0;
    }
}
