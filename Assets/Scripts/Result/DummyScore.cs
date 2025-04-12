using UnityEngine;

[CreateAssetMenu(fileName = "DummyScoreScriptableObject", menuName = "Scriptable Objects/DummyScoreScriptableObject")]
public class DummyScore : ScriptableObject, IScore
{
    [Header("スコア")]
    public float score;

    public float GetScore() => score;
}
