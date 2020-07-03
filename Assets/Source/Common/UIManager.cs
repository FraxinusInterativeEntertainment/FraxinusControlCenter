using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;

    [SerializeField]
    private Transform m_mainPanelUIRoot;
    [SerializeField]
    private Transform m_mapPanelUIRoot;
    [SerializeField]
    private Transform m_UIPopupRoot;

    private ResourcesService m_resourcesService;

    private UIFormBase m_mainPanelForm;
    private UIFormBase m_mapPanelForm;

    [SerializeField]
    private PopupView m_warningPopup;
    private readonly Queue<PopupInfoVO> m_popupQueue = new Queue<PopupInfoVO>();

    [SerializeField]
    private GameObject m_uiLockPanel;
    [SerializeField]
    private Text m_uiLockText;

    private readonly Dictionary<string, UIFormBase> m_loadedMainPanelForms = new Dictionary<string, UIFormBase>();
    private readonly Dictionary<string, UIFormBase> m_loadedMapPanelForms = new Dictionary<string, UIFormBase>();

    public static UIManager instance { get { return m_instance; } }
    
    void Awake()
    {
        DontDestroyOnLoad(this);

        m_instance = this;

        DontDestroyOnLoad(this.gameObject);
        m_resourcesService = new ResourcesService();
    }
    
    public void ShowMainPanelContent(string _formName)
    {
        if (!m_loadedMainPanelForms.ContainsKey(_formName))
        {
            GameObject uiFormGO = m_resourcesService.Load<GameObject>(_formName);
            UIFormBase uiForm = GameObject.Instantiate(uiFormGO).GetComponent<UIFormBase>();
            uiForm.transform.SetParent(m_mainPanelUIRoot);
            uiForm.Anchor(0, 0, 0);

            m_loadedMainPanelForms.Add(_formName, uiForm);
        }

        if (m_mainPanelForm != null)
        {
            m_mainPanelForm.Hide();
        }
        m_mainPanelForm = m_loadedMainPanelForms[_formName];
        m_mainPanelForm.Show();
    }

    public void ShowMapPanelContent(string _formName)
    {
        if (!m_loadedMapPanelForms.ContainsKey(_formName))
        {
            GameObject uiFormGO = m_resourcesService.Load<GameObject>(_formName);
            UIFormBase uiForm = GameObject.Instantiate(uiFormGO).GetComponent<UIFormBase>();
            uiForm.transform.SetParent(m_mapPanelUIRoot);
            uiForm.Anchor(0, 0, 0);

            m_loadedMapPanelForms.Add(_formName, uiForm);
        }

        if (m_mapPanelForm != null)
        {
            m_mapPanelForm.Hide();
        }
        m_mapPanelForm = m_loadedMapPanelForms[_formName];
        m_mapPanelForm.Show();
    }

    public void ShowPopup(PopupInfoVO _vo)
    {
        if (m_popupQueue.Count == 0)
        {
            m_warningPopup.Init(_vo);
            m_warningPopup.Show();

            m_popupQueue.Enqueue(_vo);
        }
        else
        {
            m_popupQueue.Enqueue(_vo);
        }
    }

    public void CheckPopupQueue()
    {
        if (m_popupQueue.Count > 0)
        {
            m_popupQueue.Dequeue();
        }
        if (m_popupQueue.Count > 0) 
        {
            m_warningPopup.Init(m_popupQueue.Dequeue());
            m_warningPopup.Show();
        }
    }

    public void LockUI(bool _value, string _text)
    {
        m_uiLockPanel.SetActive(_value);
        m_uiLockText.text = _text;
    }
}
