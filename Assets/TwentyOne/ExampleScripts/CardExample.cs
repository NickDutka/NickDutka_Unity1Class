using UnityEngine;

public class CardExample : MonoBehaviour
{
    public Material redColor, blackColor;

    public Renderer circle, slab;

    public enum CardColor { Red, Black }

    public void SetColor(CardColor color)
    {
        // Do all color stuff!
        // Ternary operator
        // Thing value = bool ? true : false;
        Material mat = color == CardColor.Black ? blackColor : redColor;

        circle.material = mat;
        slab.material = mat;        
    }
}
