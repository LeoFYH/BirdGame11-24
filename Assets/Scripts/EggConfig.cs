using UnityEngine;

[CreateAssetMenu(fileName = "EggConfig", menuName = "游戏配置/蛋配置")]
public class EggConfig : ScriptableObject
{
    public EggType eggType;
    public BirdConfig[] birds;
}
