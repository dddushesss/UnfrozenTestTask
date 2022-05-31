using System;
using Characters;
using DG.Tweening;
using Map;
using UnityEngine;

public class HitView : MonoBehaviour
{
    [SerializeField] private SpawnerView playerPos;
    [SerializeField] private SpawnerView enemyPos;

    public SpawnerView PlayerPos => playerPos;

    public SpawnerView EnemyPos => enemyPos;
}