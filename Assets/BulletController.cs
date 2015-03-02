using UnityEngine;
using System.Collections;

/**
 * @class BulletController
 * @called_by GameRoot
 * 弾丸の生成や発射を制御する
 */
public class BulletController : MonoBehaviour {
    public GameObject mBulletPrefab = null;
    public float mForce; // 発射するときの力
    private Camera mMainCamera = null;
    public GameObject mBullet = null;
    public bool mCanShootFlag = true;
    public bool mCanSeparateFlag = false;

    // Use this for initialization
    void Start () {
        mMainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0) && mCanShootFlag) {
            // 世界座標を取得
            Vector3 screenPos = Input.mousePosition;

            // Rayを飛ばしてWallとぶつかった場所にプレハブを生成
            Ray ray = mMainCamera.ScreenPointToRay(screenPos);
            RaycastHit hitInfo; // ぶつかったオブジェクト情報

            // Rayを飛ばす
            // Rayがぶつかったら座標と初期位置のベクトルを方向としてAddForce
            if (Physics.Raycast(ray, out hitInfo)) {
                // 世界座標を取得
                // Vector3 worldPos = hitInfo.point;

                // カメラ位置にプレハブを生成
                mBullet = this.instantiateBullet(mMainCamera.transform.position);

                // 発射する方角を取得しぶっぱなす
                Vector3 direction = ray.direction.normalized;
                mBullet.rigidbody.AddForce(direction * mForce);

                // 1つぶっぱなした時は他に発射できない
                mCanShootFlag = false;
                mCanSeparateFlag = true;
            }
        }

         if (Input.GetKeyDown(KeyCode.A) && mCanSeparateFlag) {
            this.separate();
         }
    }

    /**
     * @param pPos:Vector3 作成する座標
     * 弾丸をScene上に生成する
     */
    private GameObject instantiateBullet(Vector3 pPos) {
        GameObject bullet = GameObject.Instantiate(this.mBulletPrefab) as GameObject;
        bullet.transform.position = pPos;
        return bullet;
    }

    /**
     * bulletを分割する
     */
    private void separate() {
        // 一度分割したらもう分割できない
        mCanSeparateFlag = false;
        GameObject newRightBullet = Instantiate(mBullet) as GameObject;
        newRightBullet.transform.position = mBullet.transform.position + Vector3.right * 10f;
        newRightBullet.rigidbody.velocity = mBullet.rigidbody.velocity;
        newRightBullet.rigidbody.velocity = Quaternion.Euler(Vector3.up * +10f) * newRightBullet.rigidbody.velocity;

        GameObject newLeftBullet = Instantiate(mBullet) as GameObject;
        newLeftBullet.transform.position = mBullet.transform.position + Vector3.left * 10f;
        newLeftBullet.rigidbody.velocity = mBullet.rigidbody.velocity;
        newLeftBullet.rigidbody.velocity = Quaternion.Euler(Vector3.up * -10f) * newLeftBullet.rigidbody.velocity;
        // Debug.Break();
    }
}
