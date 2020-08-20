using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    
    void Update()
    {
        GetComponent<TextMesh>().text="X :"+GameMaster.instance.Xcount+" O :"+GameMaster.instance.Ocount;
    }
}
