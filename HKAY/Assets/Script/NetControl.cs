using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetControl : MonoBehaviour
{

    public static bool internet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NetControlCode());
    }



    IEnumerator NetControlCode()
    {
        WWWForm form = new WWWForm();


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/HKAYstorage/netcontrol.txt", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                internet = false;
            }
            else
            {
                internet = true;
                Debug.Log("Network connection is available");
            }
        
        yield return new WaitForSeconds(2);
        StartCoroutine(NetControlCode());
    }
    }

}
