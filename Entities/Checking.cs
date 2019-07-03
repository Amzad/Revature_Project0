using System;
namespace Entities
{
    public abstract class Checking : Account
    {
        public double Credit { get; set; }
        public double Debit { get; set; }
    }
}
