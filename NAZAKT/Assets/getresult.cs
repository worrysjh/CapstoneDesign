using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System;

public class getresult : MonoBehaviour
{
  public static MySqlConnection conn = new MySqlConnection("SERVER = 175.114.255.210; port = 3306; DATABASE = capstone; UID = tester; PWD = P@ssw0rd;");

  public void viewacc()
  {
    string searchacc = "select count(*) from log_tb where user_id = && wo_name = && do_date = ";
    int acc = 0;

    try
    {
      conn.Open();
      MySqlCommand cmd = new MySqlCommand(searchacc, conn);
      
      using(MySqlDataReader reader = cmd.ExecuteReader())
      {
        acc = acc + 1;
      }

      //acc 값 출력하기
    }
    catch (Exception ex)
    {
      Debug.Log(ex.ToString());
    }
  }

  public void viewach()
  {
    string searchach = "select ((do_set -1) * set_count) + do_count from user_wo where user_id = && wo_name = && do_date =;";
    string searchach2 = "select set_set * set_count from user_wo where user_id = && wo_name = && do_date =;";
    int ach1, ach2, ach;

    try
    {
      conn.Open();
      MySqlCommand cmd = new MySqlCommand(searchach, conn);
      MySqlCommand cmd2 = new MySqlCommand(searchach2, conn);

      ach1 = cmd.ExecuteNonQuery();
      ach2 = cmd2.ExecuteNonQuery();

      ach = (ach1 / ach2) * 100;
      //ach 리턴하기
      conn.Close();
    }
    catch (Exception ex)
    {
      Debug.Log(ex.ToString());
    }
  }
}
