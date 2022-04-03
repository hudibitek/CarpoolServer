using CarPool.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace CarPool.DataAccess.Faker
{
    public class CarPoolFaker
    {        
        private readonly Bogus.Faker _faker;
        private readonly Random _random;

        public CarPoolFaker()
        {            
            _faker = new Bogus.Faker();
            _random = new Random();
        }

        public List<Employee> GenerateEmployees()
        {
            var employees = new List<Employee>();
            var employeesFaker = FakerFactory.CreateEmployeeFaker();
            for (int i = 0; i < 25; i++)
            {
                employees.Add(employeesFaker.Generate());
            }

            return employees;
        }

        public List<Car> GenerateCars()
        {
            var cars = new List<Car>();
            var carsFaker = FakerFactory.CreateCarFaker();
            for (int i = 0; i < 10; i++)
            {
                cars.Add(carsFaker.Generate());
            }

            return cars;
        }
    }
}
