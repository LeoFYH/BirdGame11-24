using UnityEngine;

[CreateAssetMenu(menuName = "游戏配置/鸟配置", fileName = "BirdConfig")]
public class BirdConfig : ScriptableObject
{
    public BirdType birdType;
    public GameObject birdPrefab;
}
