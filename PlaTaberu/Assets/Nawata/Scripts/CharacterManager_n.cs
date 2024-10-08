using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager_n : MonoBehaviour
{
    //�`�ʂ���L�����N�^�[��ID
    public int ID;
    private int characterID = -1;

    //���C���[�ԍ��@�����C���[ID�Ƃ͈قȂ�
    public int Layer = 1;
    private int layerNum = -1;

    private GameObject charObj;

    //�v���^�x���̃I�u�W�F�N�g���i�[
    [SerializeField]
    private List<GameObject> plataberus;
    //�w�ォ��̃v���^�x���̃I�u�W�F�N�g���i�[
    [SerializeField]
    private List<GameObject> backBerus;

    //�L�����N�^�[�̓���
    public int CharacterAnimation = 0;
    //�L�����N�^�[�̕\��
    public int CharacterFace = 0;

    /*���ʂ̗L��*/
    public bool Exclamation = false;
    public bool Question = false;
    public bool tere = false;
    public bool douyo = false;

    //�w�ʂ��ǂ���
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

            //�L�����N�^�[��`�ʂ���
            charObj = Instantiate(back ? backBerus[ID] : plataberus[ID], this.transform);
            characterID = ID;
            layerNum = -1;

            backed = back;
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
