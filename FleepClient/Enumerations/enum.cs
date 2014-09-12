using System.Text;
using System;


public sealed class mk_direction
{
    private readonly String name;
    private readonly int value;

    public static readonly mk_direction ic_tiny = new mk_direction(1, "ic_tiny");
    public static readonly mk_direction ic_full = new mk_direction(2, "ic_full");
    public static readonly mk_direction ic_flow = new mk_direction(3, "ic_flow");
    public static readonly mk_direction ic_end = new mk_direction(4, "ic_end");
    public static readonly mk_direction ic_backward = new mk_direction(5, "ic_backward");
    public static readonly mk_direction ic_forward = new mk_direction(6, "ic_forward");
    public static readonly mk_direction empty = new mk_direction(0, "");

    private mk_direction(int value, String name)
    {
        this.name = name;
        this.value = value;
    }

    public override String ToString()
    {
        return name;
    }

}