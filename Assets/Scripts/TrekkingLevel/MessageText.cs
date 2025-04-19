using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// テキストアドベンチャー風のテキスト表示を制御するクラス
/// </summary>
public class MessageText : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent; // テキストを表示するためのUI Text
    
    private CancellationTokenSource _cancellationTokenSource;
    private bool _isWaitingForClick = false;
    
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        _cancellationTokenSource = new CancellationTokenSource();
    }
    
    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
    
    /// <summary>
    /// メッセージリストを順番に表示します
    /// 各メッセージは改行され、改行ごとにクリック待ちします
    /// 1文字ごとに1フレーム待機します
    /// </summary>
    /// <param name="messages">表示するメッセージのリスト</param>
    public async UniTask ShowInOrder(List<string> messages)
    {
        textComponent.text = string.Empty;
        
        foreach (var message in messages)
        {
            await DisplayTextWithTypingEffect(message);
            await WaitForClick();
            
            // 新しいメッセージの前に改行を追加
            if (message != messages[messages.Count - 1])
            {
                textComponent.text += "\n";
            }
        }
    }
    
    /// <summary>
    /// テキストを1文字ずつタイピング効果で表示します
    /// </summary>
    /// <param name="text">表示するテキスト</param>
    private async UniTask DisplayTextWithTypingEffect(string text)
    {
        int currentLength = textComponent.text.Length;
        string originalText = textComponent.text;
        
        for (int i = 0; i < text.Length; i++)
        {
            // キャンセルされていたら中断
            if (_cancellationTokenSource.Token.IsCancellationRequested)
                return;
            
            // 現在のテキストに1文字追加
            textComponent.text = originalText + text.Substring(0, i + 1);
            
            // 1フレーム待機
            await UniTask.DelayFrame(1, cancellationToken: _cancellationTokenSource.Token);
        }
    }
    
    /// <summary>
    /// ユーザーのクリックを待ちます
    /// </summary>
    private async UniTask WaitForClick()
    {
        _isWaitingForClick = true;
        
        // クリック表示などの処理を追加するならここに
        textComponent.text += " ▼";
        
        // クリックを待機
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began, 
            cancellationToken: _cancellationTokenSource.Token);
        
        // クリック表示を削除
        textComponent.text = textComponent.text.Replace(" ▼", "");
        
        _isWaitingForClick = false;
        
        // クリック入力のクリア待ち
        await UniTask.WaitUntil(() => !(Input.GetMouseButton(0) || Input.touchCount > 0), 
            cancellationToken: _cancellationTokenSource.Token);
    }
    
    /// <summary>
    /// 現在表示中のテキストをすべてスキップします
    /// </summary>
    public void SkipCurrentText()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
    }
}