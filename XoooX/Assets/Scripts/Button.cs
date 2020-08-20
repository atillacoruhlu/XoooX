using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    
    private GameObject plane;
    public Vector3 ofset;
    
    void OnMouseDown (){
        if (plane!=null)
        {
            Debug.Log("this move cannot be made");
            return;
        }
        //plane yerleştir hamle yap
        GameObject planeToBuild = GameMaster.instance.GetplaneBuild();
        plane= (GameObject)Instantiate(planeToBuild,transform.position + ofset , transform.rotation);
        GameMaster.instance.MoveNumber++;
    }
}
