using System;

namespace CarPool.DataAccess.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDriver { get; set; }
    }
}
