using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleItemView : UIViewBase
{
    public event System.Action OnSendSignalButtonClicked = delegate { };

    public ControlSignalVO controlSignalVO;

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
    public MouseDetectionTool mouseDetectionTool;

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

    public ModuleItemView Init(McuModule _vo)
    {
        AppFacade.instance.RegisterMediator(new ModuleItemViewMediator(this, ModuleItemViewMediator.NAME + _vo.module_name));

        UpdateModuleVO(_vo);

        return this;
    }

    public void UpdateModuleVO(McuModule _vo)
    {
        controlSignalVO = new ControlSignalVO(_vo.module_name, _vo.min);
        SetUiText(_vo.title, _vo.min, _vo.max, _vo.desc);
    }

    private void SetValue(int _value)
    {
        controlSignalVO.value = _value;
        m_valueText.text = _value.ToString();
    }

    private void SetID(string _ID)
    {
        controlSignalVO.moduleID = _ID;
    }

    private void SetUiText(string _moduleName, int _minValue, int _maxValue,string _desc)
    {
        m_moduleName.text = _moduleName;
        m_valueSlider.minValue = _minValue;
        m_valueSlider.maxValue = _maxValue;
        m_descText.text = _desc;
    }
    public void ShowModuleDescText()
    {
        m_descImage.gameObject.SetActive(true);
    }
    public void ConcealModuleDescText()
    {
        m_descImage.gameObject.SetActive(false) ;
    }


    private void LoadModules()
    { 
    
    }


}
