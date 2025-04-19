using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class TrekkingPresenter : MonoBehaviour
{
    ReactiveProperty<TrekkingStatus> trekkingStatus = new ReactiveProperty<TrekkingStatus>(TrekkingStatus.Init);
    [SerializeField] List<string> startMessages;
    [SerializeField] int startWaitForSeconds;
    [SerializeField] MessageText messageText;
    [SerializeField] GameObject readyUIGroup;
    [SerializeField] Button moveButton;
    [SerializeField] Button itemUseButton;
    [SerializeField] Button itemEquipmentButton;
    SceneRouter sceneRouter;
    IScreenTransitionEffect iScreenTransitionEffect;
    AudioManager audioManager;

    async void Start()
    {
        sceneRouter = ServiceLocator.Instance.Resolve<SceneRouter>();
        iScreenTransitionEffect = ServiceLocator.Instance.Resolve<IScreenTransitionEffect>("Default");
        audioManager = ServiceLocator.Instance.Resolve<AudioManager>();

        // 開始まで待つ
        await UniTask.WaitForSeconds(startWaitForSeconds);

        // 開幕のメッセージを流す
        await messageText.ShowInOrder(startMessages);

        // ゲーム開始
        trekkingStatus.Value = TrekkingStatus.Ready;

        // 移動ボタン
        moveButton
            .OnClickAsObservable()
            .Where(x => trekkingStatus.Value == TrekkingStatus.Ready)
            .SubscribeAwait(async (x, ct) => 
            {
                // 一部のUI非表示
                readyUIGroup.SetActive(false);

                // 移動する
                var moveResult = ServiceLocator.Instance.Resolve<Trekking>().Move();

                // 結果表示
                await messageText.ShowInOrder(moveResult.ResultMessages);

                // クリア時のシーン遷移
                if (moveResult.IsReachKatanokoya) {
                    await sceneRouter.NavigateToAsync("Scenes/Goal1", iScreenTransitionEffect, ct);
                    return;
                }
                if (moveResult.IsReachKitadake) {
                    await sceneRouter.NavigateToAsync("Scenes/Goal2", iScreenTransitionEffect, ct);
                    return;
                }
                if (moveResult.IsReachAinodake) {
                    await sceneRouter.NavigateToAsync("Scenes/Goal3", iScreenTransitionEffect, ct);
                    return;
                }

                // 遭難時のシーン遷移
                if (moveResult.IsGameOver) {
                    await sceneRouter.NavigateToAsync("Scenes/GameOver", iScreenTransitionEffect, ct);
                    return;
                }

                // 一部のUI表示復活
                readyUIGroup.SetActive(true);

                // 次のメッセージを表示
                // await messageText.ShowInOrderForReadyMessage("次のメッセージ");
            }).AddTo(this);
        
        itemUseButton
            .OnClickAsObservable()
            .Where(x => trekkingStatus.Value == TrekkingStatus.Ready)
            .SubscribeAwait(async (x, ct) => {

                // 一部のUI非表示
                readyUIGroup.SetActive(false);

                // アイテムモーダル
                audioManager.PlaySE("SE_SELECTED");
                var uts = new UniTaskCompletionSource();
                ServiceLocator.Instance.Resolve<SceneContext>().ModalClose = uts;
                var itemId = await sceneRouter.ShowItemUseModalAsync("Scenes/ItemUse", ct);

                if (itemId != null) {
                    var useResult = ServiceLocator.Instance.Resolve<Trekking>().UseItem(itemId);
                    await messageText.ShowInOrder(useResult.ResultMessages);
                }

                // 一部のUI表示復活
                readyUIGroup.SetActive(true);

            }).AddTo(this);
        
        itemEquipmentButton
            .OnClickAsObservable()
            .Where(x => trekkingStatus.Value == TrekkingStatus.Ready)
            .SubscribeAwait(async (x, ct) => {

                // 一部のUI非表示
                readyUIGroup.SetActive(false);

                // 装備モーダル
                audioManager.PlaySE("SE_SELECTED");
                var uts = new UniTaskCompletionSource();
                ServiceLocator.Instance.Resolve<SceneContext>().ModalClose = uts;
                await sceneRouter.ShowModalAsync("Scenes/ItemEquipment", ct);

                // 一部のUI表示復活
                readyUIGroup.SetActive(true);

            }).AddTo(this);
    }
}
