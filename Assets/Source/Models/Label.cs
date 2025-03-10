using UnityEngine;

public class Label
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HexColor { get; set; }

    public Label(int id, string name, string hexColor)
    {
        Id = id;
        Name = name;
        HexColor = hexColor;
    }

    public override string ToString(){
        return $"Id: {Id}, Name: {Name}, HexColor: {HexColor}";
    }
}
