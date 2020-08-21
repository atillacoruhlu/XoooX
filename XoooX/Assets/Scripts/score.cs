using UnityEngine;

public class Score : MonoBehaviour
{
    void Update()
    {
        GetComponent<TextMesh>().text="X :"+GameMaster.instance.Xcount+" O :"+GameMaster.instance.Ocount;
    }
}
