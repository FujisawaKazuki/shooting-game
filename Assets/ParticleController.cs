using UnityEngine;
using System.Collections;

/**
 * @class ParicleController
 * @called_by Particle(Empty Object)
 */
public class ParticleController : MonoBehaviour {
    public float mRadius; // 爆発の半径
    public GameObject mGameRoot = null;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    /**
     * @param pPos:Vector3
     * 爆発アニメーションを実行
     */
    public void playEffect(Vector3 pPos) {
        particleSystem.Play();
        // 半径nメートル内のオブジェクトを取得
        Collider[] hitColliders = Physics.OverlapSphere(pPos, mRadius);
        foreach (Collider col in hitColliders) {
            if (col.gameObject.tag == "block") {
                Destroy(col.gameObject);
            }
        }

        StartCoroutine(delayDestroy());
    }

    /**
     * 非同期処理
     */
    private IEnumerator delayDestroy()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
