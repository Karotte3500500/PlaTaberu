using UnityEngine;

public class StartDirector_n : MonoBehaviour
{
    private void Start()
    {
        ServerCommunication.SetAddress();

        //�f�o�b�O�p
        CharacterData._Plataberu.AddGrp(5000);
        CharacterData._Plataberu.LevelUp();
    }
}
