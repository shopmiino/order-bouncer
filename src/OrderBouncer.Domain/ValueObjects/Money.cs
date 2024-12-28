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

    public static Money operator +(Money m1, Money m2){
        if (!string.Equals(m1.Currency, m2.Currency)){
            throw new InvalidOperationException("Currencies must be same to make addition");
        }

        return new Money(m1.Amount + m2.Amount, m1.Currency);
    } 

    public static Money operator -(Money m1, Money m2){
        if (!string.Equals(m1.Currency, m2.Currency)){
            throw new InvalidOperationException("Currencies must be same to make substraction");
        }

        return new Money(m1.Amount - m2.Amount, m1.Currency);
    } 

    internal void MarkAsUnknown(){
        Unknown = true;
    }
}
