using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConditionItem : MonoBehaviour
{
    public event Action<ConditionVO> SubmitConditionValue;

    private ConditionVO m_conditionVO = new ConditionVO("N/A", -99);
    private string m_newValue;

    [SerializeField]
    private Button m_editButton;
    [SerializeField]
    private InputField m_editValueInput;
    [SerializeField]
    private Text m_conditionNameText;
    [SerializeField]
    private Text m_conditionValueText;


    void Start()
    {
        m_editValueInput.onValueChanged.AddListener((_value) => { m_newValue = _value; });
        m_editButton.onClick.AddListener(() => { OnEditButtonClicked(); });

    }

    public ConditionItem Init(ConditionVO _vo)
    {
        SetName(_vo.condition_name);
        SetValue(_vo.status);

        return this;
    }

    public void OnEditButtonClicked()
    {
        if (m_editValueInput.text != "")
        {
            SubmitConditionValue(new ConditionVO(m_conditionVO.condition_name, int.Parse(m_newValue)));
        }
    }

    public void SetValue(int _value)
    {
        m_conditionVO.status = _value;
        m_conditionValueText.text = _value.ToString();
    }

    public void SetName(string _name)
    {
        m_conditionVO.condition_name = _name;
        m_conditionNameText.text = _name;
    }

    public void AddOnEditedListener(Action<ConditionVO> _onEditAction)
    {
        SubmitConditionValue += _onEditAction;
    }
}
