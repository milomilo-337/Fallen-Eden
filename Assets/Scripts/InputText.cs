using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputText : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;

    public void TextAppare()
    {
        //InputField�ɓ��͂��ꂽ�������擾
        Text FieldText = GameObject.Find("InputField/Text").GetComponent<Text>();

        //InputField�ɓ��͂��ꂽ�������e�L�X�g�G���A�ɕ\��
        text.text = FieldText.text;

        //InputField�ɕ\�����ꂽ����������
        InputField column = GameObject.Find("InputField").GetComponent<InputField>();
        column.text = "";
    }
}
