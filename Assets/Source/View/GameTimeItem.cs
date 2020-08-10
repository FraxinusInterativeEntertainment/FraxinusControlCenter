using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeItem : MonoBehaviour
{
    [SerializeField]
    private Text m_gameNameText;
    [SerializeField]
    private Slider m_timeLineSlider;
    [SerializeField]
    private Image m_fillImage;
    [SerializeField]
    private Text m_timeText;
    private float m_currentTime = 0;
    private float m_sumTime = 1;
    public void SetItemInfos(GroupInfoVO _infos,Color _color)
    {
        m_gameNameText.text = _infos.name;
        m_sumTime = _infos.length;
        m_fillImage.color = _color;
    }
    public void CalculateTimeLine(float _time)
    {
        m_timeText.text = _time.ToString();
        m_timeLineSlider.value = _time / m_sumTime;
    }
}
