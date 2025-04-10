using UnityEngine;

public static class RectTransformExtensions
{
    /// <summary>
    /// Pivotを変更し、その変更に伴って位置を補正する
    /// </summary>
    public static void SetPivotWithoutMoving(this RectTransform rt, Vector2 newPivot)
    {
        if (rt == null) return;

        // 現在のサイズとピボット
        Vector2 size = rt.rect.size;
        Vector2 deltaPivot = newPivot - rt.pivot;

        // 見た目を保つための補正
        Vector3 deltaPosition = new Vector3(
            deltaPivot.x * size.x * rt.localScale.x,
            deltaPivot.y * size.y * rt.localScale.y
        );

        rt.pivot = newPivot;
        rt.localPosition += deltaPosition;
    }
}