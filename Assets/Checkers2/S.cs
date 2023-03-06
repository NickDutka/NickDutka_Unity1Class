using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S : MonoBehaviour
{
    public static int n = 8;
    public GameObject boardSquare;
    public static GameObject[,] grid = new GameObject[n, n];
    public static Vector2 Cmp;


    private void Start()
    {
        Vector2 cs = transform.gameObject.GetComponent<RectTransform>().sizeDelta, 
            size = boardSquare.GetComponent<RectTransform>().sizeDelta;

        cs.x /= 2;
        cs.y /= 2;
        float left = (cs.x - size.x) * -1, top = (cs.y - size.y);
        Color[] colors = new Color[] { Color.white, Color.black };
        Image drt = boardSquare.GetComponent<Image>(), circleImage = boardSquare.transform.Find("C").GetComponent<Image>();
        for (int i = 0; i < n; i++)
        {
            if (i % 2 == 0) { colors[0] = Color.black; colors[1] = Color.white; }
            else { colors[0] = Color.white; colors[1] = Color.black; }
            for (int j = 0; j < n; j++)
            {
                drt.color = colors[(((j % 2) == 0) ? 0 : 1)];
                if (i == (n / 2) - 1 || i == (n / 2) || drt.color == Color.white) circleImage.enabled = false;
                else circleImage.enabled = true;
                if (i < (n / 2)) circleImage.color = Color.red;
                else circleImage.color = Color.white;
                if (drt.color == Color.white) boardSquare.transform.Find("k2").GetComponent<Image>().enabled = false;
                else boardSquare.transform.Find("k2").GetComponent<Image>().enabled = true;
                grid[i, j] = Instantiate(boardSquare);
                grid[i, j].transform.SetParent(transform.Find("Panel1"));
                grid[i, j].transform.localPosition = new Vector3(left, top);
                grid[i, j].transform.name = i + "&" + j;
                left += size.x;
            }
            left = (cs.x - size.x) * -1;
            top -= size.y;
        }
    }
    public static void F()
    {
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++) grid[i, j].transform.Find("k").gameObject.GetComponent<Image>().enabled = false;
    }
        
}
