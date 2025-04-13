using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;

public class ResultPresenter : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Button retryButton;
    [SerializeField] Button snsShareButton;

    SceneRouter sceneRouter;
    IScreenTransitionEffect iScreenTransitionEffect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IScore iscore = ServiceLocator.Instance.Resolve<IScore>();
        scoreText.text = iscore.GetScore().ToString();

        sceneRouter = ServiceLocator.Instance.Resolve<SceneRouter>();
        iScreenTransitionEffect = ServiceLocator.Instance.Resolve<IScreenTransitionEffect>("Default");

        retryButton
            .OnClickAsObservable()
            .SubscribeAwait(async (x, ct) => 
            {
                await sceneRouter.NavigateToAsync("Scenes/Level1", iScreenTransitionEffect, ct);
            }).AddTo(this);
        
        snsShareButton
            .OnClickAsObservable()
            .Subscribe(_ => 
            {
                naichilab.UnityRoomTweet.Tweet ("YOUR-GAMEID", "ツイートサンプルです。", "unityroom", "unity1week");
            }).AddTo(this);
    }
}
