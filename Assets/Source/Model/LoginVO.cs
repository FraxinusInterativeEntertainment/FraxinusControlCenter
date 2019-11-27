using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginVO
{
    public string userName { get; set; }
    public string password { get; set; }

    public LoginVO()
    {
        userName = "";
        password = "";
    }
}
