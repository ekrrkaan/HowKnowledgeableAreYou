using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public static class rastgeleSýrala
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}


public class OyunDongusu : MonoBehaviour
{
    float zaman;
    public Slider zamanSlider;
    public Image fill;
    public Gradient gradient;
    [Header("SORULAR")]
    public SorularList sorular;
    public List<int> sorularSýrasý;
    [Header("Soru Deðiþkenleri")]
    public TextMeshProUGUI soru_TMP;
    public Text A_T, B_T, C_T, D_T;
    public int soruNo;
    BirimSoruModel birimSoruModel;
    System.Random random = new System.Random();

    AudioSource sesKaynagi;
    public AudioClip dogruSesi, yanlisSesi;
    public GameObject GameOver_P , oyunSonu_P , Pause_P;
    public bool oyunBasladimi;

    JokerSistemi jokerSistemi;
    PuanSistemi puanSistemi;

    public int correctAnswer;
    bool x = false;
    int truecount = 0;
    public int passedquestion;

    void Start()
    {
        Time.timeScale = 1;
        sesKaynagi = GetComponent<AudioSource>();
        jokerSistemi= GetComponent<JokerSistemi>();
        puanSistemi = GetComponent<PuanSistemi>();
        StartCoroutine(sorulariGetir());


        zaman = 15; //ilk zamana atama
        soruNo = 0;
        passedquestion = 0;




    }
    void SorulariSormaSýrasi()
    {

        for (int i = 0; i < sorular.butunSorular.Count; i++)
        {
            sorularSýrasý.Add(i);
        }
        sorularSýrasý.Shuffle();
    }
    



    void Update()
    {
        if (oyunBasladimi)
        {
        if(zaman > 0) { 
         zaman -= Time.deltaTime;
        zamanSlider.value = zaman;
        fill.color = gradient.Evaluate(zaman/birimSoruModel.saniye);
        }
        else
        {
                
                GameOver();
            }
        }
        
       


    }

    private void GameOver()
    {
        
        GameOver_P.SetActive(true);
        Time.timeScale = 0;
        
    }

    IEnumerator sorulariGetir()
    {
        WWWForm form = new WWWForm();

        form.AddField("unity", "sorular");



        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/HKAYstorage/database_controls.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                sorular = JsonUtility.FromJson<SorularList>(www.downloadHandler.text);
                oyunBasladimi = true;
                SorulariSormaSýrasi();
                soruSor(soruNo);
            }
        }
    }
    void soruSor(int soruNo)
    {
        

        birimSoruModel = sorular.butunSorular[sorularSýrasý[soruNo]];
        soru_TMP.text = birimSoruModel.soru;
        A_T.text = birimSoruModel.a_Cevabi;
        B_T.text = birimSoruModel.b_Cevabi;
        C_T.text = birimSoruModel.c_Cevabi;
        D_T.text = birimSoruModel.d_Cevabi;

        zaman = birimSoruModel.saniye;
        zamanSlider.maxValue = birimSoruModel.saniye;

        jokerSistemi.sifirla();
        ciftCevap = false;
    }

    public void kontrolEt(int basilanCevap)
    {
        birimSoruModel = sorular.butunSorular[sorularSýrasý[soruNo]];
        if (birimSoruModel.dogruCevap == basilanCevap)
        {
            Debug.Log("Correct");
            sesKaynagi.PlayOneShot(dogruSesi);
            puanSistemi.dogruPuanVer(birimSoruModel.saniye);
            soruNo++;
            correctAnswer++;
            if (soruNo < sorular.butunSorular.Count)
            {
               soruSor(soruNo);
            }
            else
            {
                oyunSonu_P.SetActive(true);
                Time.timeScale = 0;
            }
            if(correctAnswer %5 == 0)
            {
                x = false;
                truecount = 0;

                for (int i = 0; i < 5; i++)
                {
                    int truecount = 0;
                    if(jokerSistemi.jokerlerim[i].interactable == true){
                        truecount++;
                        if(truecount == 5)
                        {
                            x = true;
                        }
                    }
                    else
                        while (x == false)
                        {

                            int secilen = random.Next(0, 5);

                            if (jokerSistemi.jokerlerim[secilen].interactable == false)
                            {
                                jokerSistemi.jokerlerim[secilen].interactable = true;
                                x = true;
                                Debug.Log("Joker Verildi");
                            }


                        }
                }

                

            }
            
        }
        else
        {
          //  Debug.Log("Incorrect");
            sesKaynagi.PlayOneShot(yanlisSesi);
            puanSistemi.yanlisPuanVer(birimSoruModel.saniye);
            jokerSistemi.secenekler[basilanCevap-1].GetComponent<Image>().color = jokerSistemi.yanlisRenk;
            
            

            if (!ciftCevap)
            {
                jokerSistemi.secenekler[birimSoruModel.dogruCevap - 1].GetComponent<Image>().color = jokerSistemi.dogruRenk;
                GameOver();
            }
            else
            {
                ciftCevap = false;
            }
        }


        

    }

    public void yuzde25Kullan()
    {

        jokerSistemi.yuzdeliJokerKullan(birimSoruModel.dogruCevap , "25" );
        jokerSistemi.jokerlerim[0].interactable = false;

    }

    public void yuzde50Kullan()
    {

        jokerSistemi.yuzdeliJokerKullan(birimSoruModel.dogruCevap, "50");
        jokerSistemi.jokerlerim[1].interactable = false;
    }
    public void pasKullan()
    {
        passedquestion++;
        soruNo++;
        soruSor(soruNo);
        jokerSistemi.jokerlerim[2].interactable = false;
        Debug.Log(passedquestion);

    }
    public void dogruCevapKullan()
    {

        jokerSistemi.secenekler[birimSoruModel.dogruCevap - 1].GetComponent<Image>().color = jokerSistemi.dogruRenk;
        jokerSistemi.jokerlerim[3].interactable = false;

    }
    public bool ciftCevap;
    public void ciftCevapKullan()
    {
        ciftCevap = true;
        jokerSistemi.jokerlerim[4].interactable = false;
    }

    public void YenidenOyna_B()
    {
        Time.timeScale = 1;
        correctAnswer = 0;
        sorularSýrasý.Shuffle();
        soruNo = 0;
        soruSor(soruNo);
        jokerSistemi.JokerleriYenile();
        GameOver_P.SetActive(false);
    }
    public void AnemenuDon_B()
    {
        SceneChanger.SceneChange(0);
    }

    public void YorumYap()
    {
        Application.OpenURL("");
    }

    public void Pause_B()
    {
        Pause_P.SetActive(true);
        oyunBasladimi = false;

    }
    public void ContinueGame_B()
    {
        Pause_P.SetActive(false);
        oyunBasladimi = true;
    }
    public void YenidenOynaPause_B()
    {
        Time.timeScale = 1;
        correctAnswer = 0;
        oyunBasladimi = true;
        sorularSýrasý.Shuffle();
        soruNo = 0;
        soruSor(soruNo);
        jokerSistemi.JokerleriYenile();
        Pause_P.SetActive(false);
    }
    public void YenidenOynaOyunSonu_B()
    {
        Time.timeScale = 1;
        correctAnswer = 0;
        oyunBasladimi = true;
        sorularSýrasý.Shuffle();
        soruNo = 0;
        soruSor(soruNo);
        jokerSistemi.JokerleriYenile();
        oyunSonu_P.SetActive(false);
    }
}
