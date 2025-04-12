using UnityEngine;

public class ResultDependencyRegister : MonoBehaviour
{
    [SerializeField] AppEnv appEnv;
    [SerializeField] DummyScore dummyScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (appEnv == AppEnv.Production) {
            var sceneContext = ServiceLocator.Instance.Resolve<SceneContext>();
            var score = sceneContext.Score ?? 0f;
            ServiceLocator.Instance.Register<IScore>(new Score(score));
        } else {
            ServiceLocator.Instance.Register<IScore>(dummyScore);
        }
    }

    void OnDestroy()
    {
        ServiceLocator.Instance.UnRegister<IScore>();
    }
}
