using UnityEngine;
using System.Collections;

/**
 * @class Bullet
 * @called_by Bullet
 * 弾丸の衝突検知や座標情報などを保持する
 */
public class Bullet : MonoBehaviour {
    // ==========================
    // Fields
    // ==========================
    public Vector3 mPosition;
    public Renderer mRenderer;
    public GameObject mParticlePrefab = null;
    public GameObject mGameRoot = null;

    // ==========================
    // Methods
    // ==========================

    // Use this for initialization
    void Start () {
        mGameRoot = GameObject.Find("GameRoot");
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    /**
     * 衝突検知
     * ぶつかったら爆発する
     * ついでにコントローラに爆発をお知らせする
     */
    void OnCollisionEnter(Collision col) {
        GetComponent<Collider>().enabled = false;

        // コントローラのフラグを変更
        mGameRoot.GetComponent<BulletController>().mCanShootFlag = true;
        mGameRoot.GetComponent<BulletController>().mCanSeparateFlag = false;

        Destroy(this.gameObject);
        // particleを生成
        instantiateParticle(this.gameObject.transform.position);
    }

    /**
     * @param pPos:Vector3
     * 指定した場所にparticleオブジェクトを作成し，アニメーションをPlay
     */
    private void instantiateParticle(Vector3 pPos) {
        GameObject particle = GameObject.Instantiate(this.mParticlePrefab) as GameObject;
        particle.transform.position = pPos;
        particle.GetComponent<ParticleController>().mGameRoot = GameObject.Find("GameRoot");
        particle.GetComponent<ParticleController>().playEffect(pPos);
    }
}
