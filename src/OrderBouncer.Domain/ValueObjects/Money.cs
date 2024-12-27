using System;
using System.ComponentModel.DataAnnotations;

namespace OrderBouncer.Domain.ValueObjects;

public class Money
{
    [Required]
    public decimal Amount {get;}
    [Required]
    public string Currency {get;} 
    public bool Unknown {get; private set;} = false;

    public Money (decimal amount, string currency){
        if(amount < 0){
            throw new ArgumentOutOfRangeException("Amount cannot be less than zero");
        }

        Amount = amount;
        Currency = currency;
    }

    internal void MarkAsUnknown(){
        Unknown = true;
    }
}
