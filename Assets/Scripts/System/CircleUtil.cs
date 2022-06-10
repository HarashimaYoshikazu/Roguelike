using UnityEngine;
//�L��������p
public static class CircleUtil
{
    /// <summary>
    /// �~���̈ʒu���擾����
    /// </summary>
    public static Vector3 GetCirclePosition(float angle, float radius)
    {
        return GetCirclePosition(angle, radius, radius);
    }
    /// <summary>
    /// �~���̈ʒu���擾���� x��y�̔��a�ʎw��
    /// </summary> 
    public static Vector3 GetCirclePosition(float angle, float radiusX, float radiusZ)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radiusX, 0f, Mathf.Sin(angle * Mathf.Deg2Rad) * radiusZ);
    }

    /// <summary>
    /// �v���C���[�̌��݂̏ꏊ����~�����擾
    /// </summary>
    public static Vector3 GetCirclePosition(float angle, float radius,Vector3 player)
    {
        return GetCirclePosition(angle, radius, radius) + player;
    }
}