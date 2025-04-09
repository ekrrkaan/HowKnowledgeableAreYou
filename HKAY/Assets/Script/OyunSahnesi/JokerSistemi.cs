using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JokerSistemi : MonoBehaviour
{
    public Color orjRenk , dogruRenk, yanlisRenk;
    public List<string> ihtimaller;
    public GameObject[] secenekler;
    public Button[] jokerlerim;

    static int oncekiTutulan = -1;

    public void JokerleriYenile()
    {
        for(int i = 0; i < jokerlerim.Length; i++)
        {
            jokerlerim[i].interactable = true;
        }
    }


    void Start()
    {
        
    }

    public int dogruCevap;

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            yuzdeliJokerKullan(dogruCevap,"50");
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            sifirla();
        }


    }


    public void sifirla()
    {
        for (int i = 0; i < secenekler.Length; i++)
        {
            secenekler[i].SetActive(true);
            secenekler[i].GetComponent<Image>().color = orjRenk;
        }
        oncekiTutulan = -1;
    }


    string secilen;
    public void yuzdeliJokerKullan(int dogruCevap , string JokerTuru) 
    {
        if (JokerTuru.Equals("25"))
        {
       ihtimaller = new List<string>()
        {
            "1","2","3",
            "0","2","3",
            "0","1","3",
            "0","1","2"
        };

        }
        else
        {
        ihtimaller = new List<string>()
        {
            "12","13","23",
            "02","03","23",
            "01","03","13",
            "01","02","12"
        };
        }

        if(oncekiTutulan == -1) {

            int random = 0;

            switch (dogruCevap)
            {
                case 1:
                    { //cevap A ise
                        random = Random.Range(0, 3); // 0 , 1 , 2 tutar
                        break;
                    }
                case 2:
                    { //cevap B ise
                        random = Random.Range(3, 6);
                        break;
                    }
                case 3:
                    { //cevap C ise
                        random = Random.Range(6, 9);
                        break;
                    }
                case 4:
                    { //cevap D ise
                        random = Random.Range(9, 12);
                        break;
                    }
            }
            secilen = ihtimaller[random];
            oncekiTutulan = random;

            
        }
        else //üst üste %25 - %50 kullanýmda ayný þýkký kapatmama
        {
            if(oncekiTutulan%3 == 0)
            {
                secilen = ihtimaller[oncekiTutulan + 2]; // 0>2
            }
            else if (oncekiTutulan % 3 == 1)
            {
                secilen = ihtimaller[oncekiTutulan]; // 1>1 4>4
            }
            else if (oncekiTutulan % 3 == 2)
            {
                secilen = ihtimaller[oncekiTutulan-2]; // 5->3 2->0
            }


            

        }
        for (int i = 0; i < secilen.Length; i++)
        {
            string gecici = secilen.Substring(i, 1); //1,2
            secenekler[int.Parse(gecici)].SetActive(false);
        }








    }
}
