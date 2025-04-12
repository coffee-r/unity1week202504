using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LicencePresenter : MonoBehaviour
{
    [SerializeField] Button closeButton;
    SceneContext sceneContext;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneContext = ServiceLocator.Instance.Resolve<SceneContext>();

        // Modalを閉じたらSceneRouterに閉じたことが伝わるようにする
        closeButton
            .OnClickAsObservable()
            .Subscribe(_ => sceneContext?.ModalClose.TrySetResult())
            .AddTo(this);
    }
}
