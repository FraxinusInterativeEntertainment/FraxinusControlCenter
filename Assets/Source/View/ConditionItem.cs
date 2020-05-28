using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConditionItem : MonoBehaviour
{
    public event Action<ConditionVO> SubmitConditionValue;

    private ConditionVO m_conditionVO = new ConditionVO("N/A", -99,"","");
    private string m_newValue;

    [SerializeField]
    private Button m_editButton;
    [SerializeField]
    private InputField m_editValueInput;
    [SerializeField]
    private Text m_conditionNameText;
    [SerializeField]
    private Text m_conditionValueText;
    [SerializeField]
    private Image m_conditionDescImage;
    [SerializeField]
    private Text m_conditionDescText;
    //[SerializeField]   后续+
    private Text m_conditionTitle;
    [SerializeField]
    private MouseDetectionTool m_mouseDetectionTool;

    void Start()
    {
        m_editValueInput.onValueChanged.AddListener((_value) => { m_newValue = _value; });
        m_editButton.onClick.AddListener(() => { OnEditButtonClicked(); });

        m_mouseDetectionTool.AddMouseOverListener(ShowConditionDescText);
        m_mouseDetectionTool.AddMouseLeaveListener(ConcealConditionDescText);
    }

    public ConditionItem Init(ConditionVO _vo)
    {
        SetName(_vo.condition_name);
        SetValue(_vo.status);
        SetTitle(_vo.title);
        SetDescription(_vo.desc);
        return this;
    }

    public void OnEditButtonClicked()
    {
        if (m_editValueInput.text != "")
        {
            SubmitConditionValue(new ConditionVO
                (m_conditionVO.condition_name, int.Parse(m_newValue),m_conditionVO.desc, m_conditionVO.title));
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
    public void SetTitle(string _title)
    {
        m_conditionVO.title = _title;

        //TODO: 界面上加入title后记得将数据更新上去
        //m_conditionTitle.text = _title;
    }
    public void SetDescription(string _desc)
    {
        m_conditionVO.desc = _desc;
        m_conditionDescText.text = _desc;
    }
    public void AddOnEditedListener(Action<ConditionVO> _onEditAction)
    {
        SubmitConditionValue += _onEditAction;
    }
    public void ShowConditionDescText()
    {
        m_conditionDescImage.gameObject.SetActive(true);
    }
    public void ConcealConditionDescText()
    {
        m_conditionDescImage.gameObject.SetActive(false);
    }

}
