using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance;

    [SerializeField]
    private Transform m_mainPanelUIRoot;
    [SerializeField]
    private Transform m_UIPopupRoot;

    private ResourcesService m_resourcesService;

    private UIFormBase m_mainPanelForm;
    private UIFormBase m_mapPanel;

    [SerializeField]
    private PopupView m_warningPopup;
    private readonly Queue<PopupInfoVO> m_popupQueue = new Queue<PopupInfoVO>();

    private readonly Dictionary<string, UIFormBase> m_loadedMainPanelForms = new Dictionary<string, UIFormBase>();

    public static UIManager instance { get { return m_instance; } }
    
    void Awake()
    {
        DontDestroyOnLoad(this);

        m_instance = this;

        DontDestroyOnLoad(this.gameObject);
        m_resourcesService = new ResourcesService();

        //ShowMainPanelContent(Const.UIFormNames.GAME_STATUS_FORM);
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
}
