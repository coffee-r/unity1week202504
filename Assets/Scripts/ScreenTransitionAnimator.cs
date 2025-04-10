using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 画面遷移アニメーター
/// </summary>
public class ScreenTransitionAnimator : MonoBehaviour
{
    [SerializeField]
    IScreenTransitionEffect effect;

    /// <summary>
    /// アニメーションの種類を設定する
    /// </summary>
    public void SetAnimationEffect<T>() where T : MonoBehaviour, IScreenTransitionEffect
    {
        effect = GetComponentInChildren<T>();
    }

    /// <summary>
    /// 開始アニメーションを再生する
    /// </summary>
    public async UniTask PlayEnter(CancellationToken cancellation) => await effect.PlayEnterAnimation(cancellation);
    
    /// <summary>
    /// 終了アニメーションを再生する
    /// </summary>
    public async UniTask PlayExit(CancellationToken cancellation) => await effect.PlayExitAnimation(cancellation);
}

