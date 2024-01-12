using System.Threading.Tasks;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public async void DeleteBall()
    {
        await Task.Delay(5000);
        DestroyImmediate(this);
    }
}