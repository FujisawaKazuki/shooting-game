using UnityEngine;
using System.Collections;

/**
 * @class GameRootController
 * @called_by GameRoot(Empty)
 */
public class GameRootController : MonoBehaviour {
    public Camera mMainCamera = null;

    // Use this for initialization
    void Start () {
        // メインカメラを取得
        mMainCamera = Camera.main;
    }
    
    // Update is called once per frame
    void Update () {
    }
}
