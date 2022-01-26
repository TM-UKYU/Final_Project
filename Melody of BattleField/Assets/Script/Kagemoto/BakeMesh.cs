using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeMesh : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer BaseMesh;    // ベイクする元のオブジェクト

    [SerializeField]
    GameObject BakeCloneObj;            // ベイクしたメッシュを格納するGameObject

    // BakeMeshObjをインスタンスした際のSkinnedMeshRendererリスト
    List<SkinnedMeshRenderer> BakeCloneMeshList;

    public int CloneCount = 4;       // 残像数
    public int FlameCountMax = 4;    // 残像を更新する頻度
    int FlameCount = 0;
    public bool isBake;

    void Start()
    {
        //BaseMesh = this.GameObject;
        BakeCloneObj = GameObject.Find("BakeCloneObj");

        BakeCloneMeshList = new List<SkinnedMeshRenderer>();

        // 残像を複製
        for (int i = 0; i < CloneCount; i++)
        {
            var obj = Instantiate(BakeCloneObj);
            BakeCloneMeshList.Add(obj.GetComponent<SkinnedMeshRenderer>());
        }

        isBake = false;
    }

    void FixedUpdate()
    {
        // 4フレームに一度更新
        FlameCount++;
        if (FlameCount % FlameCountMax != 0)
        {
            return;
        }

        // BakeしたMeshを１つ前にずらしていく
        for (int i = BakeCloneMeshList.Count - 1; i >= 1; i--)
        {
            BakeCloneMeshList[i].sharedMesh = BakeCloneMeshList[i - 1].sharedMesh;

            // 位置と回転をコピー
            BakeCloneMeshList[i].transform.position = BakeCloneMeshList[i - 1].transform.position;
            BakeCloneMeshList[i].transform.rotation = BakeCloneMeshList[i - 1].transform.rotation;
        }


        if (isBake)
        {
            // 今のスキンメッシュをBakeする
            Mesh mesh = new Mesh();
            BaseMesh.BakeMesh(mesh);

            BakeCloneMeshList[0].sharedMesh = mesh;

            // 位置と回転をコピー
            BakeCloneMeshList[0].transform.position = transform.position;
            BakeCloneMeshList[0].transform.rotation = transform.rotation;
        }
        else
        {
            // 残像を見えない場所に退避
            BakeCloneMeshList[0].transform.position = new Vector3(0, -30.0f, 0);
        }
    }
}