using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stophostsc : MonoBehaviour
{
    public void StopHost(){
        FindObjectOfType<_UI>().StopHostButton();
    }
}
