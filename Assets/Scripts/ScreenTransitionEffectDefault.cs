using UnityEngine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System.Threading;
using DG.Tweening;

/// <summary>
/// IScreenTransitionEffectの実装
/// </summary>
public class ScreenTransitionEffectDefault : MonoBehaviour, IScreenTransitionEffect
{
    [SerializeField, Header("画面を覆うImage")] Image overrayImage;
    private float screenWidth => overrayImage.rectTransform.rect.width;

    [Button("Play Enter Animation")]
    public async UniTask PlayEnterAnimation(CancellationToken cancellation)
    {
        overrayImage.rectTransform.anchoredPosition = new Vector2(-screenWidth, 0);

        await overrayImage
                .rectTransform
                .DOAnchorPosX(0, 1.0f)
                .SetEase(Ease.OutSine)
                .AsyncWaitForCompletion()
                .AsUniTask()
                .AttachExternalCancellation(cancellation);
    }

    [Button("Play Exit Animation")]
    public async UniTask PlayExitAnimation(CancellationToken cancellation)
    {
        await overrayImage
                .rectTransform
                .DOAnchorPosX(screenWidth, 1.0f)
                .SetEase(Ease.OutSine)
                .AsyncWaitForCompletion()
                .AsUniTask()
                .AttachExternalCancellation(cancellation);
    }
}
