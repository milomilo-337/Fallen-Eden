using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickExample : MonoBehaviour
{
    public Button myButton;
    public bool GameGo = false;

    void Start()
    {        
        // �{�^���Ƀ��X�i�[��ǉ�
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // �{�^���������ꂽ���̏���
        GameGo = true;
        Debug.Log("�{�^����������܂����I");
    }
}
