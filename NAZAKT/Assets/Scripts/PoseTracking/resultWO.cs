using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class resultWO : MonoBehaviour
{
  public string userid;
  public string woname;
  public int setcnt, setset;
  public int realcnt, realset;
  
  public static MySqlConnection conn = new MySqlConnection("SERVER = 175.114.255.210; port = 3306; DATABASE = capstone; UID = tester; PWD = P@ssw0rd;");


  //userid = WOStatus.userId;
  // Start is called before the first frame update
  /*
  void Start()
    {
      Debug.Log("hahaha");
      WOStatus WOStat = GameObject.Find("WOStatus").GetComponent<WOStatus>();
      woname = WOStat.WOname;
      Debug.Log("lalala" + woname);
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

  void updatereuslt {
    string resultset = "update user_wo set do_set = , do_count where user_id = && wo_name = && do_date =;";
    try {
      conn.Open();
      MySqlCommand cmd = new MySqlCommand(resultset, conn);
      cmd.ExecuteNonQuery();
      conn.close();
    }
  }
}
