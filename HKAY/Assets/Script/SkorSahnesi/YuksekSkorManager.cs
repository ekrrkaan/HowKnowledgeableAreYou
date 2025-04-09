using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class YuksekSkorManager : MonoBehaviour
{

    public RectTransform content;


    public YuksekSkorList yuksekSkorList;

    public GameObject kullaniciModel;




    void Start()
    {
        StartCoroutine(skorlariGetir());
    }

 
    void Update()
    {
       

        
    }
    private void ContentAyarla(float carpan , int kullaniciSayisi)
    {
        content.sizeDelta = new Vector2(0,carpan*kullaniciSayisi);
    }

    IEnumerator skorlariGetir()
    {
        WWWForm form = new WWWForm();

        form.AddField("unity", "skorlariGetir");



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/HKAYstorage/database_controls.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {

                yuksekSkorList = JsonUtility.FromJson<YuksekSkorList>(www.downloadHandler.text);
                kullanicilarimiziDiz();




            }
        }
    }
    void kullanicilarimiziDiz()
    {
        ContentAyarla(82.5f, yuksekSkorList.butunSkorlar.Count);
        for (int i = 0; i < yuksekSkorList.butunSkorlar.Count; i++)
        {
            GameObject gecici = Instantiate(kullaniciModel);
            gecici.transform.SetParent(content.transform);
            gecici.transform.localScale = new Vector3(1,1,1);
            Image background_I = gecici.transform.GetChild(0).GetComponent<Image>();
            Image user_I = background_I.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI userNickname_TMP = background_I.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI skor_TMP = background_I.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI siralama_TMP = background_I.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            // kullanici_I

            YuksekSkorModel yuksekSkorModel = yuksekSkorList.butunSkorlar[i];
            userNickname_TMP.text = yuksekSkorModel.KullaniciNickname;
            skor_TMP.text = yuksekSkorModel.KullaniciSkor.ToString();
            siralama_TMP.text = "" + (i + 1);

        }
    }
}
