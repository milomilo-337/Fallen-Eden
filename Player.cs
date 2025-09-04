using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動速度の調整用
    [SerializeField] private float moveSpeed = 30f;

    // スプライトの参照
    public Sprite spriteNormal;         // プレイヤー通常スプライト
    public Sprite spriteDown;           // プレイヤー下降スプライト
    public Sprite spriteUp;             // プレイヤー上昇スプライト

    private SpriteRenderer spriteRenderer; // スプライトレンダラーのキャッシュ

    // 弾関連のフィールド
    public GameObject shotPrefab;       // 弾プレハブ
    public int maxShotCount = 20;       // 事前に弾を作成する数
    public float shotSpeed = 30f;       // 弾が1秒間に進む距離
    private List<GameObject> shotList;  // 弾キャッシュ（事前に作成する弾）
    private int nextShotIndex = 0;      // 次に発射する弾キャッシュの位置

    public GameObject shotFlash;        // 発射効果ゲームオブジェクト

    void Start()
    {
        // SpriteRenderer をキャッシュ
        spriteRenderer = GetComponent<SpriteRenderer>();

        // カメラの後ろ位置
        Vector3 hidePos = Camera.main.transform.position - Camera.main.transform.forward;

        // 弾を事前に作成し、カメラの後ろに配置
        shotList = new List<GameObject>();
        for (int i = 0; i < maxShotCount; i++)
        {
            // プレハブから弾を作成
            GameObject shot = Instantiate(shotPrefab);
            shot.transform.position = hidePos;
            shotList.Add(shot);

            // Rigidbody2D を設定
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // 初期速度をゼロに設定
            }
        }

        // 発射効果を非表示に設定
        if (shotFlash != null)
        {
            shotFlash.SetActive(false);
        }
    }

    void Update()
    {
        // マウス座標取得
        Vector3 pos = Input.mousePosition;
        // マウス座標をスクリーン座標に変換
        pos.z = -Camera.main.transform.position.z;
        // スクリーン座標をワールド座標に変換
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        // 現在位置からマウスワールド位置までの移動量
        Vector3 moveVec = (worldPos - transform.position) / moveSpeed;
        // Z座標を固定
        moveVec.z = 0;
        // 徐々にマウス座標に近づくよう、移動量を分割して加算
        transform.position += moveVec;

        // 上下の移動量に応じてスプライトを切り替え
        if (Mathf.Abs(moveVec.y) < 0.05f)
        {
            spriteRenderer.sprite = spriteNormal;
        }
        else
        {
            spriteRenderer.sprite = moveVec.y < 0 ? spriteDown : spriteUp;
        }

        // 弾発射の処理
        HandleShooting();
    }

    private void HandleShooting()
    {
        // 左クリックが押された瞬間に発射処理を行う
        if (Input.GetMouseButtonDown(0))
        {
            // 弾を発射
            GameObject shot = shotList[nextShotIndex];
            shot.transform.position = transform.position;
            shot.transform.rotation = transform.rotation;

            // Rigidbody2D を使用して弾を移動
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = transform.right * shotSpeed;
            }

            // 次の弾位置
            nextShotIndex = (nextShotIndex + 1) % maxShotCount;

            // 発射効果を一瞬だけ表示
            if (shotFlash != null)
            {
                StartCoroutine(ShowShotFlash());
            }
        }
    }

    private IEnumerator ShowShotFlash()
    {
        // 発射効果を表示
        shotFlash.SetActive(true);

        // 少し待機
        yield return new WaitForSeconds(0.05f); // 0.05秒間表示

        // 発射効果を非表示
        shotFlash.SetActive(false);
    }
}