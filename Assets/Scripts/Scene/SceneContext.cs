
using Cysharp.Threading.Tasks;

#nullable enable

/// <summary>
/// シーンを跨いで渡すデータを定義します
/// 1週間のゲームジャムである規模を踏まえて、どのシーンでもこのクラスを使います (なので渡す際にsetしない無駄なプロパティありますけど無視してください)
/// </summary>
public class SceneContext
{
    public float? Score { get; set; }
    public UniTaskCompletionSource? ModalClose {get; set;}

    public void Reset()
    {
        Score = null;
        ModalClose = null;
    }
}