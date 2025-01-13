using System;

namespace OrderBouncer.Domain.Models;

public class Row
{
    public int OrderId {get; private set;}
    public string? OrderCode {get; private set;}
    public DateTime OrderDate {get; private set;}
    public string? HairColor {get; private set;}
    public string? Gender {get; private set;}
    public bool Head {get; private set;} 
    public bool Body {get; private set;}
    public bool HeadPrint {get; private set;}
    public bool BodyPrint {get; private set;}
    public string? DeliveryStatus {get; private set;}
    public bool Delivered {get; private set;}
    public bool Urgent {get; private set;}
    public bool Accessory {get; private set;}
    public bool Pet {get; private set;}
    public bool Keychain {get; private set;}
    public string? ExtraNotes {get; private set;}
    public DateTime LatestShippingDate {get; private set;}
}
