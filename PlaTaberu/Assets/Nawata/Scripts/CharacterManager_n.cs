using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCharacterManagement;

public class CharacterManager_n : MonoBehaviour
{
    //�`�ʂ���L�����N�^�[��ID
    public int ID;
    private int characterID = -1;

    //���C���[�ԍ��@�����C���[ID�Ƃ͈قȂ�
    public int Layer = 1;
    private int layerNum = -1;

    private GameObject charObj;

    [SerializeField]
    private List<GameObject> plataberus;

    //�L�����N�^�[�̓���
    public int CharacterAnimation = 0;
    //�L�����N�^�[�̕\��
    public int CharacterFace = 0;

    /*���ʂ̗L��*/
    public bool Exclamation = false;
    public bool Question = false;
    public bool tere = false;
    public bool douyo = false;

    private void Start()
    {
        ID = CharacterData._Plataberu.ID;
    }
    private void Update()
    {
        if(characterID != ID)
        {
            if (charObj != null)
                Destroy(charObj);

            //�L�����N�^�[��`�ʂ���
            charObj = Instantiate(plataberus[ID], this.transform);
            characterID = ID;
            layerNum = -1;
        }
        if(Layer != layerNum)
        {
            //���C���[�̏C���ƒl���K�i���֏C��
            Layer = OrderLayer(Layer);
            layerNum = Layer;
        }
    }

    //�L�����N�^�[���m�̑O��𑀍�@��1~3�܂ŁB�l���傫������O�ɂȂ�
    private int OrderLayer(int layerNum)
    {
        /*�K�i�O�̒l���C��*/
        if (layerNum < 1) layerNum = 1;
        if (layerNum > 3) layerNum = 3;

        //���w�̑S�ẴI�u�W�F�N�g���擾
        var children = charObj.transform.GetComponentsInChildren<Transform>(true);
        foreach (var child in children)
        {
            GameObject obj = child.gameObject;
            if (obj.GetComponent<SpriteRenderer>())
            {
                //���C���[�̕ύX
                obj.GetComponent<SpriteRenderer>().sortingLayerName = $"Character{layerNum}";
            }
        }

        return layerNum;
    }
}
