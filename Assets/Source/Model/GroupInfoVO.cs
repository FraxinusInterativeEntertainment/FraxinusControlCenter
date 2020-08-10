using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupInfoVO
{
    public string name { get; set; }
    public string title { get; set; }
    public float capacity { get; set; }
    public float length { get; set; }

    public GroupInfoVO(string _name, string _title, float _capacity,float _length)
    {
        name = _name;
        title = _title;
        capacity = _capacity;
        length = _length;
    }
}
