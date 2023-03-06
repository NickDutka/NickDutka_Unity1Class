using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class D : MonoBehaviour
{
    static string k, k2 = "";
    static Image C;
    static Color color = Color.white;
    public GameObject P;
    public TMP_Text r, w;



    public void Cclick(RectTransform t)
    {
        S.F();
        C = t.transform.Find("C").GetComponent<Image>();
        if(color == C.color)
        {
            string name = t.name;
            int i, j;
            i = Convert.ToInt32((name.Split('&'))[0]);
            j = Convert.ToInt32((name.Split('&'))[1]);

            int co = -1;
            if (C.color == Color.red) co = 1;
            try
            {


                if (!S.grid[i + co, j - 1].transform.Find("C").GetComponent<Image>().enabled)
                {
                    S.grid[i + co, j - 1].transform.Find("k").GetComponent<Image>().enabled = true;
                }
                else if (S.grid[i + co, j - 1].transform.Find("C").GetComponent<Image>().color != C.color && !S.grid[i + (co * 2), j - 2].transform.Find("C").GetComponent<Image>().enabled)
                {
                    S.grid[i + (co * 2), j - 2].transform.Find("k").GetComponent<Image>().enabled=true;
                    k2 = (i + co) + " " + (j - 1);
                }
            }
            catch { }
            try
            {
                if (!S.grid[i + co, j + 1].transform.Find("C").GetComponent<Image>().enabled)
                {
                    S.grid[i + co, j + 1].transform.Find("k").GetComponent<Image>().enabled = true;
                }
                else if (S.grid[i + co, j + 1].transform.Find("C").GetComponent<Image>().color != C.color && !S.grid[i + (co * 2), j + 2].transform.Find("C").GetComponent<Image>().enabled)
                {
                    S.grid[i + (co * 2), j + 2].transform.Find("k").GetComponent<Image>().enabled = true;
                    k2 = (i + co) + " " + (j + 1);
                }
            }
            catch { }
            k = i + " " + j;
        }
    }


    public void Kclick(RectTransform t)
    {
        S.F();
        string name = t.name;
        int i, j;
        i = Convert.ToInt32((name.Split('&'))[0]);
        j = Convert.ToInt32((name.Split('&'))[1]);
        S.grid[i, j].transform.Find("C").GetComponent<Image>().color = color;
        S.grid[i, j].transform.Find("C").GetComponent<Image>().enabled = true;
        i = Convert.ToInt32((k.Split(' '))[0]);
        j = Convert.ToInt32((k.Split(' '))[1]);
        S.grid[i, j].transform.Find("C").GetComponent<Image>().enabled = false;
        if (k2 != "" && ((Convert.ToInt32((name.Split('&'))[1]) - 1) == Convert.ToInt32((k2.Split(' '))[1]) || (Convert.ToInt32((name.Split('&'))[1] + 1) == Convert.ToInt32((k2.Split(' '))[1]))))
        {
            i = Convert.ToInt32((k2.Split(' '))[0]);
            j = Convert.ToInt32((k2.Split(' '))[1]);
            S.grid[i, j].transform.Find("C").GetComponent<Image>().enabled = false;
            if (color == Color.white) A2('w');
            else A2('r');
            r.text = S.Cmp.x + "";
            w.text = S.Cmp.y + "";

            k2 = "";
        }
        if (color == Color.white) color = Color.red;
        else color = Color.white;
    }

    public void A2(char c)
    {
        if (c == 'w') S.Cmp.x++;
        else S.Cmp.y++;
        if (S.Cmp.x >= 12)
        {
            P.gameObject.SetActive(true);
            P.transform.Find("w").GetComponent<TMP_Text>().text = "You win white";

        }
        if (S.Cmp.y >= 12)
        {
            P.gameObject.SetActive(true);
            P.transform.Find("w").GetComponent<TMP_Text>().text = "You win red";
        }
    }

}
