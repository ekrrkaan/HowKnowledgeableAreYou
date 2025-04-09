using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;

public class PuanSistemi : MonoBehaviour
{

    public TextMeshProUGUI skor_TMP;
    public int skor;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(skorumuGetir());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dogruPuanVer(int saniye)
    {
        skor += saniye;
        skor_TMP.text = "" + skor;
        StartCoroutine(skorumuGuncelle());
    }


    public void yanlisPuanVer(int saniye)
    {
        switch (saniye)
        {
            case 5:
                {
                    skor -= 15;
                    break;
                }
            case 10:
                {
                    skor -= 10;
                    break;
                }
            case 15:
                {
                    skor -= 7;
                    break;
                }
            case 20:
                {
                    skor -= 4;
                    break;
                }
            default:
                {
                    skor -= 5;
                    break;
                }
        }

        skor_TMP.text = "" + skor;
        StartCoroutine(skorumuGuncelle());

    }
    IEnumerator skorumuGetir()
    {
        WWWForm form = new WWWForm();

        form.AddField("unity", "skorumuGetir");
        form.AddField("nickname", PlayerPrefs.GetString("nickname"));



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/HKAYstorage/database_controls.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {

                skor = int.Parse(www.downloadHandler.text);
                skor_TMP.text = "" + skor;

            }
        }
    }

    IEnumerator skorumuGuncelle()
    {
        WWWForm form = new WWWForm();

        form.AddField("unity", "skorumuGuncelle");
        form.AddField("nickname", PlayerPrefs.GetString("nickname"));
        form.AddField("skor", skor);



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/HKAYstorage/database_controls.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

            }
        }
    }
}




