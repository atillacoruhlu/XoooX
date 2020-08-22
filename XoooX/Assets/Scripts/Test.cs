using UnityEngine;
using Mirror;
public class Test : NetworkBehaviour
{
    [SyncVar]
    public int number;
}
