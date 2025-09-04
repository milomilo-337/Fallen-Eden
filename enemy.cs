using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 動きの種類
    public enum ENEMY_TYPE
    {
        LINE,  // まっすぐ進む
        CURVE  // 上下にカーブ
    }

    // 動きの種類
    public ENEMY_TYPE type = ENEMY_TYPE.CURVE;

    // 敵の移動速度
    [SerializeField] private float speed = 5f;

    // ランダム方向の範囲
    [SerializeField] private float randomMinDir = 0f;  // ランダム方向の最小値
    [SerializeField] private float randomMaxDir = 0f;  // ランダム方向の最大値

    // カーブの設定
    [SerializeField] private float curveLength = 1f; // カーブの振幅
    [SerializeField] private float cycleCount = 1f;  // 1秒間のカーブの周期

    private float centerY;       // 初期Y座標を保存
    private float cycleRadian;   // 現在のカーブの角度

    void Start()
    {
        // 初期Y座標を保存
        centerY = transform.position.y;
        cycleRadian = 0f;

        // ランダム範囲が指定されている場合
        if (randomMinDir != randomMaxDir)
        {
            // ランダム方向を設定
            float rotZ = Random.Range(randomMinDir, randomMaxDir);
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

    // 物理更新時に処理
    void FixedUpdate()
    {
        // 現在の座標を取得
        Vector3 pos = transform.position;

        // 進行方向に向かって直進
        pos += -transform.right * speed * Time.fixedDeltaTime;

        // 上下にカーブ
        if (type == ENEMY_TYPE.CURVE && cycleCount > 0)
        {
            cycleRadian += (cycleCount * 2 * Mathf.PI) * Time.fixedDeltaTime;
            pos.y = Mathf.Sin(cycleRadian) * curveLength + centerY;
        }

        // 新しい座標に変更
        transform.position = pos;
    }
}
