using System;
namespace Entities
{
    public class TermDepositAccount : Account
    {
        public double Credit { get; set; }
        public int depositTerm { get; set; }
        public int remainingTerm { get; set; }
    }
}
