using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class gotohome : MonoBehaviour
{


  public void gotoNazakt() {
    SceneManager.LoadScene("Nazakt", LoadSceneMode.Additive);
  }
}
