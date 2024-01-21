using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

// ���ٴϴ� �ؽ�Ʈ (npc���� ��ȭ)
public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastshown;

    public void Show()
    {
        active = true;
        lastshown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
        {
            return;
        }
       
        if (Time.time - lastshown > duration)
        {
            Hide();
        }

        go.transform.position += motion * Time.deltaTime;

    }

}
