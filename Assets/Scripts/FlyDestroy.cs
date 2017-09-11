using UnityEngine;
using System.Collections;

public class FlyDestroy : MonoBehaviour
{
    /// <summary>
    /// Class which sets the player projectile to be active or deative and this information is sent to the object pooler to determine which ones needs to be recycled
    /// </summary>

    void OnEnable()
    {
        Invoke("Destroy", 2f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void Disabled()
    {
        CancelInvoke();
    }
}
