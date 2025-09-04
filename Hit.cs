using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit1 : MonoBehaviour
{
    public int energy = 3; // 残り体力

    // 爆発後に作成するプレハブの一覧
    public List<GameObject> leavePrefabs;
    // 爆発後に作成したゲームオブジェクトの相対位置
    public List<Vector3> leavePostions;

    // 衝突時の処理（トリガー）
    void OnTriggerEnter2D(Collider2D collider)
    {
        // タグが同じ場合は衝突させない
        if (tag == collider.gameObject.tag)
        {
            return;
        }

        // 体力を減らす
        energy--;

        // 体力が無くなった場合
        if (energy <= 0)
        {
            // プレハブを残す
            Vector3 pos = transform.position;
            for (int i = 0; i < leavePrefabs.Count; i++)
            {
                Instantiate(leavePrefabs[i], pos + leavePostions[i], Quaternion.identity);
            }

            // 自分を削除
            Destroy(gameObject);
        }
    }
}
