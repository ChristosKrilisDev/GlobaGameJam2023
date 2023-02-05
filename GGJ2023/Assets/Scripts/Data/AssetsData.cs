using UnityEngine;

[CreateAssetMenu(fileName = "AssetsData", menuName = "GGJ/AssetsData", order = 0)]
public class AssetsData : ScriptableObject
{
    public Sprite ShowTile;
    public Sprite HideTile;
    public Sprite MiddleTile;
    public Sprite BrokenRoot;
    public Sprite Root;

    [Space, Header("MUSIC")]
    public AudioClip ThemeSong;
    public AudioClip[] MouseClick;
    public AudioClip[] MouseOver;
    public AudioClip Dig;
    public AudioClip DigRoot;
    public AudioClip RadarSfx;
    public AudioClip[] UiClick;
    public AudioClip Win;
    public AudioClip Defeat;
    
}
