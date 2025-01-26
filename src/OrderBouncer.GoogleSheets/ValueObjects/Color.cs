namespace OrderBouncer.GoogleSheets.ValueObjects;

public record class Color
{
    public string Name {get;}
    public float R {get;}
    public float G {get;}
    public float B {get;}
    public float A {get;}

    public Color(string name, float r, float g, float b, float a){
        Name = name;
        R = r;
        G = g;
        B = b;
        A = a;
    }
}
