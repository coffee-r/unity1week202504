using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LicencePresenter : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] TMP_Text licenceText;
    SceneContext sceneContext;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        sceneContext = ServiceLocator.Instance.Resolve<SceneContext>();

        // Modalを閉じたらSceneRouterに閉じたことが伝わるようにする
        closeButton
            .OnClickAsObservable()
            .Subscribe(_ => sceneContext?.ModalClose.TrySetResult())
            .AddTo(this);
        
        var operation = Resources.LoadAsync("LICENCE");
        await operation;
        licenceText.text = (operation.asset as TextAsset).text;
    }
}
