using System;

namespace OrderBouncer.GoogleSheets.Tests.Constants.ExpectedColors;

internal abstract class BaseExpectedColor
{
    public float Red {get; protected set;}
    public float Green {get; protected set;}
    public float Blue {get; protected set;}
    public float Alpha {get; protected set;}
}
