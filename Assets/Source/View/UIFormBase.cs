using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIFormBase : MonoBehaviour
{
    [SerializeField]
    protected List<string> defaultViewNames = new List<string>();

    protected readonly Dictionary<string, UIViewBase> m_loadedViews = new Dictionary<string, UIViewBase>();
    protected readonly Stack<UIViewBase> m_viewStack = new Stack<UIViewBase>();

    protected virtual void Start()
    {

    }

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
        ShowAllViews();
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void ShowView(string _uiViewName)
    {
        if (!m_loadedViews.ContainsKey(_uiViewName))
        {
            m_loadedViews.Add(_uiViewName, LoadView(_uiViewName));
        }
        
        m_loadedViews[_uiViewName].Show();
        m_viewStack.Push(m_loadedViews[_uiViewName]);
    }

    public virtual void HideTopView()
    {
        if (m_viewStack.Count > 0)
        {
            m_viewStack.Pop().Hide();
        }
    }

    public virtual void Anchor(float _x, float _y, float _z)
    {
        this.transform.localPosition = new Vector3(_x, _y, _z);
    }

    protected virtual UIViewBase LoadView(string _uiViewName)
    {
        ResourcesService resourcesService = new ResourcesService();
        GameObject viewGO = resourcesService.Load<GameObject>(_uiViewName);

        UIViewBase uiView = GameObject.Instantiate(viewGO).GetComponent<UIViewBase>();

        uiView.transform.SetParent(this.transform);
        uiView.Anchor(0, 0, 0);

        return uiView;
    }

    protected void ShowAllViews()
    {
        foreach (string viewName in defaultViewNames)
        {
            ShowView(Const.UIViewNames.UI_VIEW_PATH + viewName);
        }
    }
}
