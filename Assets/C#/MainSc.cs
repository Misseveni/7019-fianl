using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainSc : MonoBehaviour
{
    public GameObject themod;
    public int modnum;
    public int scorenum;
    public Text scoretext;

    public GameObject falsetext, truetext;

    private bool isok=true;

    public Text modname;//��������

    void Start()
    {
        modname.text = themod.transform.GetChild(modnum).gameObject.name;
        themod.transform.GetChild(modnum).gameObject.SetActive(true);
    }
    public void Anext()
    {
        isok = true;
        if (modnum == themod.transform.childCount - 1)
        {

            GameObject obj = Instantiate(truetext, truetext.transform.parent);
            obj.transform.localPosition = truetext.transform.localPosition;
            obj.SetActive(true);
            obj.GetComponent<Text>().text = "You have completed all garbage sorting";
            return;
        }
        modnum++;
     
        modname.text = themod.transform.GetChild(modnum).gameObject.name;
        themod.transform.GetChild(modnum).gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&isok)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Ray ray01 = new Ray(Camera.main.transform.position, Vector3.forward);
            RaycastHit hit;
            //�ж��Ƿ���ײ������
            bool isCollider = Physics.Raycast(ray, out hit);
            //bool isCollider01= Physics.Raycast(Camera.main.transform.position, Vector3.forward, 
            //    10, LayerMask.GetMask("UI", "Enemy", "Player"));
            if (isCollider)
            {
                print(hit.collider.gameObject.name);

                if (hit.collider.gameObject.name == themod.transform.GetChild(modnum).gameObject.tag)
                {
                    //�÷�
                    scorenum++;
                    scoretext.text = "Score��" + scorenum;

                    GameObject obj = Instantiate(truetext, truetext.transform.parent);
                    obj.transform.localPosition = truetext.transform.localPosition;
                    obj.SetActive(true);
                    Destroy(obj, 1);
                }
                else
                {
                    //�۷�
                    scorenum--;
                    if (scorenum <= 0)
                    {
                        scorenum = 0;
                    }
                    scoretext.text = "Score��" + scorenum;

                    GameObject obj = Instantiate(falsetext, falsetext.transform.parent);
                    obj.transform.localPosition = falsetext.transform.localPosition;
                    obj.SetActive(true);
                    Destroy(obj, 1);
                }
                themod.transform.GetChild(modnum).gameObject.SetActive(false);
                isok = false;
                Invoke("Anext", 1) ;
            }

        }
    }
}
