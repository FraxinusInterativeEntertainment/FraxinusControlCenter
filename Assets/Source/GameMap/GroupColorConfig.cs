using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupColorConfig
{
    private readonly Dictionary<GameGroup, Color> m_groupColors = new Dictionary<GameGroup, Color>();
    private readonly Color DEFAULT_COLOR = new Color(50f, 50f, 50f);

    public GroupColorConfig()
    {

    }

    public void AddGroupColor(GameGroup _group, Color _color)
    {
        if (m_groupColors.ContainsKey(_group))
        {
            m_groupColors[_group] = _color;
        }
        else
        {
            m_groupColors.Add(_group, _color);
        }
    }

    public Color GetGroupColor(GameGroup _group)
    {
        if (m_groupColors.ContainsKey(_group))
        {
            return m_groupColors[_group];
        }
        else
        {
            return DEFAULT_COLOR;
        }
    }
}
