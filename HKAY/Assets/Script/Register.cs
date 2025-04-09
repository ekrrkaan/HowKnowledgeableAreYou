using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Register : MonoBehaviour
{

    public TMP_InputField nick_IF;
    public TextMeshProUGUI hata_T;
    public GameObject Continue;

    // Start is called before the first frame update
    void Start()
    {
        Continue.SetActive(false);
        hata_T.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void control()
    {
        if (NetControl.internet)
        {
if (!nick_IF.text.Equals(""))
        {
            StartCoroutine(Registeration());
        }
        else
        {
           
                textYazdir("Invalid");
        }


        }
        else
        {
            
            textYazdir("Please check your internet");
        }


    }

    IEnumerator Registeration()
    {
        WWWForm form = new WWWForm();

        form.AddField("unity", "register");
        form.AddField("nickname", nick_IF.text);
        


        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/HKAYstorage/database_controls.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                textYazdir(www.downloadHandler.text);

                if (www.downloadHandler.text.Equals("Registeration Successfull"))
                {
                    PlayerPrefs.SetString("nickname", nick_IF.text);
                    nick_IF.interactable = false;
                    Continue.SetActive(true);
                }
            }
        }
    }


    void textYazdir(string mesaj)
    {
        hata_T.gameObject.SetActive(true);
        hata_T.text = mesaj;
        Invoke("Reset", 2.5f);
    }

    void Reset()
    {
        hata_T.text = "";
    }
}
