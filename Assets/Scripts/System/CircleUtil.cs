using UnityEngine;
//記事から引用
public static class CircleUtil
{
    /// <summary>
    /// 円周の位置を取得する
    /// </summary>
    public static Vector3 GetCirclePosition(float angle, float radius)
    {
        return GetCirclePosition(angle, radius, radius);
    }
    /// <summary>
    /// 円周の位置を取得する xとyの半径別指定
    /// </summary> 
    public static Vector3 GetCirclePosition(float angle, float radiusX, float radiusZ)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radiusX, 0f, Mathf.Sin(angle * Mathf.Deg2Rad) * radiusZ);
    }

    /// <summary>
    /// プレイヤーの現在の場所から円周を取得
    /// </summary>
    public static Vector3 GetCirclePosition(float angle, float radius,Vector3 player)
    {
        return GetCirclePosition(angle, radius, radius) + player;
    }
}