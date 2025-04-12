using R3;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameMediator gameMediator;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.x >= 5.0f) {
            gameMediator.OnGoalReached.OnNext(Unit.Default);
        }
    }
}
