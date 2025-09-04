using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed = 0.01f;          // 1フレームに動く距離
    public float scrollWidth = 5.44f;   // ループ幅

    void Start()
    {
        // 自分のスケールに合わせて「ループの幅」を計算
        scrollWidth *= transform.localScale.x;
    }

    // 物理更新時に処理
    void FixedUpdate()
    {
        // スクロール
        Vector3 pos = transform.position;
        pos.x -= speed;

        // ループ幅を超えて移動した場合
        if (-scrollWidth > pos.x)
        {
            // ループ幅分戻す
            pos.x += scrollWidth;
        }

        // 新しい位置を設定
        transform.position = pos;
    }
}
