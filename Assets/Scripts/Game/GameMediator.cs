using R3;
using R3.Triggers;
using UnityEngine;

public class GameMediator : MonoBehaviour
{
    public ReactiveProperty<GameState> State { get; } = new(GameState.Ready);

    public Subject<Unit> OnGoalReached { get; } = new();

    public float Time  = 0;

    void Start()
    {
        OnGoalReached.Subscribe(_ => State.Value = GameState.Goal).AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => State.Value == GameState.Play)
            .Subscribe(_ => Time +=  UnityEngine.Time.deltaTime)
            .AddTo(this);
    }
}
