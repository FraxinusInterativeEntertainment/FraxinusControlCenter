using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResponder
{
    void OnResult(object _data);
    void OnFault(object _data);
}
