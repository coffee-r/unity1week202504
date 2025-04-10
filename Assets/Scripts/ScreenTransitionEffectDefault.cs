using UnityEngine;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

/// <summary>
/// IScreenTransitionEffectの実装
/// </summary>
public class ScreenTransitionEffectDefault : MonoBehaviour, IScreenTransitionEffect
{
    [SerializeField, Header("画面を覆うImage")] Image overrayImage;

    [Button("Play Enter Animation")]
    public async UniTask PlayEnterAnimation()
    {
        overrayImage.rectTransform.localScale = new Vector3(0, 1, 1);
        overrayImage.rectTransform.SetPivotWithoutMoving(new Vector2(0.0f, 0.5f));

        await overrayImage
                .rectTransform
                .DOScaleX(1.0f, 2.0f)
                .SetEase(Ease.OutSine)
                .SetDelay(0.0f)
                .AsyncWaitForCompletion();
    }

    [Button("Play Exit Animation")]
    public async UniTask PlayExitAnimation()
    {
        overrayImage.rectTransform.localScale = new Vector3(1, 1, 1);
        overrayImage.rectTransform.SetPivotWithoutMoving(new Vector2(1.0f, 0.5f));

        await overrayImage
                .rectTransform
                .DOScaleX(0.0f, 2.0f)
                .SetEase(Ease.OutSine)
                .SetDelay(0.0f)
                .AsyncWaitForCompletion();
    }
}
