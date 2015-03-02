using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * @class Block
 * @called_by GameRoot
 * 制御上のブロック
 */
 public class Block {
    // =================
    // Fields
    // =================
    public Vector3 mPos;

    // =================
    // ENUM
    // =================
    // ブロックの強度
    public enum STRENGTH {
        NONE = -1,
        LEVEL1 = 0,
        LEVEL2 = 1,
        LEVEL3 = 2,
        NUM     // 強度の種類数
    }

    // =================
    // Constructors
    // =================
    public Block(Vector3 pPos) {
        this.mPos = pPos;
    }

    // =================
    // Methods
    // =================
 }


/**
 * @class StageController
 * @called_by Block(prefab)
 */
public class StageController : MonoBehaviour {
    // ===========================
    // Constants
    // ===========================
    public static int BLOCK_NUM_X = 5; // 横
    public static int BLOCK_NUM_Y = 5; // 縦
    public static int BLOCK_NUM_Z = 2;  // 奥行き 
    public static int BLOCK_SIZE = 12;  // ブロックサイズ

    // ===========================
    // Fields
    // ===========================
    public GameObject mBlockPrefab = null; // ステージを構成するプレハブ
    private Block[,,] mBlocks; // ブロック配列 
    public int mRemainBlcokNum;

    // ===========================
    // Methods
    // ===========================

    // Use this for initialization
    void Start () {
        mRemainBlcokNum = BLOCK_NUM_X * BLOCK_NUM_Y * BLOCK_NUM_Z;
        this.makeBlocks();
    }
    
    // Update is called once per frame
    void Update () {
        // Scene上のブロック数を更新
        this.updateRemainBlockNum();

        if (mRemainBlcokNum == 0) {
            // 成功を表示
            GameObject canvas = GameObject.Find("SuccessText");
            canvas.SetActive(true);
            Text txt = canvas.GetComponent<Text>();
            txt.text = "SUCCESS!!";
            txt.color = Color.green;

        }
    }

    /**
     * ブロック作成
     */
    private void makeBlocks() {
        mBlocks = new Block[BLOCK_NUM_X, BLOCK_NUM_Y, BLOCK_NUM_Z];
        for (int i = 0; i < BLOCK_NUM_X; i++) {
            for (int j = 0; j < BLOCK_NUM_Y; j++) {
                for (int k = 0; k < BLOCK_NUM_Z; k++) {
                    Vector3 pos = new Vector3(i * BLOCK_SIZE, j * BLOCK_SIZE, k * BLOCK_SIZE); // 生成するブロックの座標

                    // Scene上にブロックを生成
                    GameObject obj = Instantiate(mBlockPrefab) as GameObject;
                    obj.transform.position = pos;
                    obj.name = "block(" + i + "," + j + "," + k + ")";

                    // Blockクラスを作成
                    Block block = new Block(pos);

                    // mBlocks配列に格納
                    mBlocks[i,j,k] = block;
                }
            }
        }
    }

    private void updateRemainBlockNum() {
        mRemainBlcokNum = GameObject.FindGameObjectsWithTag("block").Length;
    }
}