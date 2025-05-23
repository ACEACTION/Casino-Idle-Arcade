using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


[CreateAssetMenu(menuName = "Data/Stack/Stack Money Data")]
public class StackMoneyData : ScriptableObject
{
    public int xSize = 5;
    public int ySize = 5;
    public int zSize = 5;
    public float xOffset;
    public float yOffset;
    public float zOffset;
    public float moneyMoveDuration;
    public float jumpPower;
    [SerializeField] float  minRotationDuration, maxRotationDuration;
    public Ease moneyMoveEase;
    public float goToPlayerDelay;

    public float GetRotationDuration() => Random.Range(minRotationDuration, maxRotationDuration);

}
