using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleItemView : UIViewBase
{
    public event System.Action OnSendSignalButtonClicked = delegate { };

    public const string MCU_MODULE_TYPE = "MCU";
    public const string SERVER_MODULE_TYPE = "SERVER";
    public readonly Color MCU_TYPE_COLOR = new Color(0.57f, 0.7f, 0.9f);
    public readonly Color SERVER_TYPE_COLOR = new Color(0.84f, 0.7f, 0.42f);

    public ControlSignalVO controlSignalVO;
    public MouseDetectionTool mouseDetectionTool;

    [SerializeField]
    private Text m_moduleName;
    [SerializeField]
    private Slider m_valueSlider;
    [SerializeField]
    private Text m_valueText;
    [SerializeField]
    private Button m_sendSignalButton;
    [SerializeField]
    private Image m_descImage;
    [SerializeField]
    private Text m_descText;
    [SerializeField]
    private Text m_ModuleTypeText;
    [SerializeField]
    private Image m_ModuleTypeImage;


    public ModuleItemView Init(McuModule _vo)
    {
        AppFacade.instance.RegisterMediator(new ModuleItemViewMediator(this, ModuleItemViewMediator.NAME + _vo.module_name));

        UpdateModuleVO(_vo);

        return this;
    }

    public void UpdateModuleVO(McuModule _vo)
    {
        controlSignalVO = new ControlSignalVO(_vo.mcu_name, _vo.module_name, _vo.min, _vo.module_type);
        SetUiText(_vo.title, _vo.min, _vo.max, _vo.desc);
        SetModuleType(_vo.module_type);
    }

    public void ShowModuleDescText()
    {
        m_descImage.gameObject.SetActive(true);
    }

    public void ConcealModuleDescText()
    {
        m_descImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        m_sendSignalButton.onClick.AddListener(() => { OnSendSignalButtonClicked(); });
        m_valueSlider.onValueChanged.AddListener((float _value) => { SetValue((int)_value); });
        SetValue((int)m_valueSlider.value);
    }

    private void OnDestroy()
    {
        AppFacade.instance.RemoveMediator(ModuleItemViewMediator.NAME);
    }

    private void SetValue(int _value)
    {
        controlSignalVO.value = _value;
        m_valueText.text = _value.ToString();
    }

    private void SetID(string _ID)
    {
        controlSignalVO.moduleName = _ID;
    }

    private void SetUiText(string _moduleName, int _minValue, int _maxValue,string _desc)
    {
        m_moduleName.text = _moduleName;
        m_valueSlider.minValue = _minValue;
        m_valueSlider.maxValue = _maxValue;
        m_descText.text = _desc;
    }

    private void SetModuleType(int _type)
    { 
        switch(_type)
        {
            case 0:
                m_ModuleTypeText.text = MCU_MODULE_TYPE;
                m_ModuleTypeImage.color = MCU_TYPE_COLOR;
                break;
            case 1:
                m_ModuleTypeText.text = SERVER_MODULE_TYPE;
                m_ModuleTypeImage.color = SERVER_TYPE_COLOR;
                break;
            default:
                break;
        }
    }

    private void LoadModules()
    { 
    
    }
}
