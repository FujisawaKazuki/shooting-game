using UnityEngine;
using System.Collections;

/**
 * @class WallControler
 * @called_by Wall
 */
public class WallController : MonoBehaviour {

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    /**
     * WallにBulletが衝突したらBulletを削除する
     */
    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "bullet") {
            // Destroy(col.gameObject);
        }
    }
}
