using UnityEngine;

public class PhotographDirector : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private FlashDirector flash;

    private int timeCount = 0;
    private int takenNum = 0;
    private bool isTakable = false;

    public void Take()
    {
        isTakable = true;
    }

    private void Update()
    {
        if (isTakable)
            timeCount++;

        //30ƒtƒŒ[ƒ€–ˆ‚ÉB‰e
        if(timeCount % 30 == 0 && isTakable)
        {
            flash.Light(15);
            cameraController.Stop();
            takenNum++;
        }

        if(takenNum >= 10)
        {
            isTakable = false;
            Debug.Log("B‰eI—¹");
            timeCount = 0;
            takenNum = 0;
        }
    }
}
