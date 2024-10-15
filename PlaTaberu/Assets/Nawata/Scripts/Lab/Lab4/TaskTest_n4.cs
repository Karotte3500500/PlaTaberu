using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;


public class TaskTest_n4 : MonoBehaviour
{
    public bool go = false;
    public int num = 0;
    async void Start()
    {
        UnityMainThreadDispatcher.CreateInstance();

        Debug.Log("非同期処理を開始するのです");
        await DoSomethingAsync();
        Debug.Log("非同期処理が完了したのです");
    }

    private void Update()
    {
        if (go)
        {
            Debug.Log("RFTGHJ");
            Tset();
            Debug.Log("ぬぬぬぬぬぬん  ");
            go = false;
        }
        num++;
    }

    public async void Tset()
    {
        Debug.Log("非同期処理を開始するのです");
        await DoSomethingAsync();
        Debug.Log("非同期処理が完了したのです");
    }

    async Task DoSomethingAsync()
    {
        // ここで時間のかかる処理をシミュレートするのです
        await Task.Delay(2000); // 2秒待つのです
        Debug.Log("2秒経過したのです");

        // メインスレッドに戻ってからUnityのAPIを使用するのです
        await Task.Run(() =>
        {
            // メインスレッドに戻す処理
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                Debug.Log("UnityのAPIを使用するのです");
            });
        });

        // ここで時間のかかる処理をシミュレートするのです
        await Task.Delay(2000); // 2秒待つのです
        Debug.Log("2秒経過したのです");
    }
}
