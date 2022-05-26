using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class BattleInterfaceControllerView : MonoBehaviour
{
    [SerializeField] private Button attackButton;
    [SerializeField] private Button skipButton;

    public Button AttackButton => attackButton;

    public Button SkipButton => skipButton;
}