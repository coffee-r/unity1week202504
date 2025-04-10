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
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;
    
    [SerializeField] Vector3 imageStartPosition;
    [SerializeField] Vector3 imageEndPosition;
    [SerializeField] Ease imageEaseType;
    [SerializeField] float imageEasingDuration;
    [SerializeField] float imageEasingDelay;

    RectTransform rectTransform => image.GetComponent<RectTransform>();

    [Button("Play Enter Animation")]
    public async UniTask PlayEnterAnimation()
    {
        if (!Application.isPlaying) return;
        rectTransform.localPosition = imageStartPosition;
        await rectTransform
                .DOLocalMove(imageEndPosition, imageEasingDuration)
                .SetEase(imageEaseType)
                .SetDelay(imageEasingDelay)
                .AsyncWaitForCompletion();
        Debug.Log("End PlayEnterAnimation");
    }

    [Button("Play Exit Animation")]
    public async UniTask PlayExitAnimation()
    {
        if (!Application.isPlaying) return;
        rectTransform.localPosition = imageEndPosition;
        await rectTransform
                .DOLocalMove(imageStartPosition, imageEasingDuration)
                .SetEase(imageEaseType)
                .SetDelay(imageEasingDelay)
                .AsyncWaitForCompletion();
    }
}
