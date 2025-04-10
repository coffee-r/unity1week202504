using Cysharp.Threading.Tasks;

/// <summary>
/// 画面遷移アニメーションのインターフェース
/// </summary>
public interface IScreenTransitionEffect
{
    /// <summary>
    /// 開始アニメーション
    /// </summary>
    /// <returns></returns>
    UniTask PlayEnterAnimation();

    /// <summary>
    /// 終了アニメーション
    /// </summary>
    /// <returns></returns>
    UniTask PlayExitAnimation();
}
