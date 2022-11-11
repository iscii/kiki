using System;

//arrays of waste in big small or medium.

[Serializable]
public class Wastes{
    //size of waste changes speed 
    //smaller is better
    public Waste[] small;
    public Waste[] medium;
    public Waste[] big;
}

[Serializable]
public class Waste{
    public string name;
    public string desc;
}
