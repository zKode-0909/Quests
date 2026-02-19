using UnityEngine;

public class StringID
{
    string category;
    string name;
    string zone;
    string num;
    public string id;

    public StringID(string category,string name,string zone,string num) { 
        this.category = category;
        this.name = name;
        this.zone = zone;
        this.num = num;

        id = $"<{category}>.<{name}>.<{zone}>{num}";
    }
}
