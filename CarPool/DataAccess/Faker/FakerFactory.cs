using System;
using System.Collections.Generic;
using Bogus;
using CarPool.DataAccess.Entities;

namespace CarPool.DataAccess.Faker
{
    public class FakerFactory
    {
        public static Faker<Employee> CreateEmployeeFaker()
        {
            return new Faker<Employee>()
                .RuleFor(e => e.Id, f => f.Random.Guid())
                .RuleFor(e => e.Name, f => f.Name.FirstName() + " " + f.Name.LastName())
                .RuleFor(e => e.IsDriver, f => f.Random.Bool());
        }

        public static Faker<Car> CreateCarFaker()
        {
            var carNames = new List<string> { "Desdamona", "Terminator", "Indestructible", "Bumblebee", "Party Wagon", "Arrowcar", "Underdog", "Turtle Taxi", "Road Sniper" };
            var carTypes = new List<string> { "Škoda Fabia", "Opel Insignia", "Ford Mondeo", "Honda Accord", "Fiat 500", "Kia Ceed", "Mercedes E220" };
            var colors = new List<string> { "black", "blue", "orange", "yellow", "grey", "white", "brown", "daphne blue" };
            return new Faker<Car>()
                .RuleFor(e => e.Id, f => f.Random.Guid())
                .RuleFor(c => c.Name, f => f.PickRandom(carNames))
                .RuleFor(c => c.CarType, f => f.PickRandom(carTypes))
                .RuleFor(c => c.Color, f => f.PickRandom(colors))
                .RuleFor(c => c.Plates, f => "ZG-" + f.Random.Int(1000,9999) + "-" + String.Concat(f.Random.Chars('A', 'Z', 2)))
                .RuleFor(c => c.NumberOfSeats, f => f.Random.Int(2, 5));
        }
    }
}
