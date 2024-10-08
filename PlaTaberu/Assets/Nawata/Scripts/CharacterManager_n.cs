using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager_n : MonoBehaviour
{
    //描写するキャラクターのID
    public int ID;
    private int characterID = -1;

    //レイヤー番号　※レイヤーIDとは異なる
    public int Layer = 1;
    private int layerNum = -1;

    private GameObject charObj;

    //プラタベルのオブジェクトを格納
    [SerializeField]
    private List<GameObject> plataberus;
    //背後からのプラタベルのオブジェクトを格納
    [SerializeField]
    private List<GameObject> backBerus;

    //キャラクターの動き
    public int CharacterAnimation = 0;
    //キャラクターの表情
    public int CharacterFace = 0;

    /*効果の有無*/
    public bool Exclamation = false;
    public bool Question = false;
    public bool tere = false;
    public bool douyo = false;

    //背面かどうか
    public bool back = false;
    private bool backed = true;

    private void Start()
    {
        ID = CharacterData._Plataberu.ID;
    }
    private void Update()
    {
        if(characterID != ID || back != backed)
        {
            if (charObj != null)
                Destroy(charObj);

            //キャラクターを描写する
            charObj = Instantiate(back ? backBerus[ID] : plataberus[ID], this.transform);
            characterID = ID;
            layerNum = -1;

            backed = back;
        }
        if(Layer != layerNum)
        {
            //レイヤーの修正と値を規格内へ修正
            Layer = OrderLayer(Layer);
            layerNum = Layer;
        }
    }

    //キャラクター同士の前後を操作　※1~3まで。値が大きい程手前になる
    private int OrderLayer(int layerNum)
    {
        /*規格外の値を修正*/
        if (layerNum < 1) layerNum = 1;
        if (layerNum > 3) layerNum = 3;

        //下層の全てのオブジェクトを取得
        var children = charObj.transform.GetComponentsInChildren<Transform>(true);
        foreach (var child in children)
        {
            GameObject obj = child.gameObject;
            if (obj.GetComponent<SpriteRenderer>())
            {
                //レイヤーの変更
                obj.GetComponent<SpriteRenderer>().sortingLayerName = $"Character{layerNum}";
            }
        }

        return layerNum;
    }
}
