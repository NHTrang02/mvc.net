using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System.Globalization;
using CsvHelper;
using Microsoft.Net.Http.Headers;

namespace New_folder__3_.Controllers
{
    public class RookiesController : Controller
    {
        static List<Person> members = new List<Person>
        {
                new Person()
                {
                    FName = "Trang ",
                    LName = "Nguyen Huyen",
                    gender = "famela",
                    DateOfBirth = new DateTime(2002,7,14),
                    BirthPlace = "Hai Duong",
                    Age = 19,
                    IsGraduated = false
                },
                new Person()
                {
                    FName = "Hoang",
                    LName = "Nguyen",
                    gender = "Male",
                    DateOfBirth = new DateTime(2000,1,1),
                    BirthPlace = "Ha Noi",
                    Age = 21,
                    IsGraduated = false
                },
                new Person()
                {
                    FName = "Hoa",
                    LName = "Nguyen",
                    gender = "famela",
                    DateOfBirth = new DateTime(1999,1,1),
                    BirthPlace = "Ha Noi",
                    Age = 21,
                    IsGraduated = false
                },
                new Person(){
                    FName = "Vy",
                    LName = "Tran Thi",
                    gender = "famela",
                    DateOfBirth = new DateTime(1999,2,12),
                    BirthPlace = "Ha Noi",
                    Age = 22,
                    IsGraduated = false
                },
        };
        private readonly ILogger<RookiesController> _logger;

        public RookiesController(ILogger<RookiesController> logger)
        {
            _logger = logger;
        }
        //BR1
        public IActionResult MaleList()
        {
            var list = members.Where(member => member.gender == "Male").ToList();
            var json = JsonSerializer.Serialize(list);
            return Content(json);
        }
        //BR2
        public IActionResult Oldest(){
            var max = members.Max(member => member.FullName + member.Age);
            var results = members.Where(member =>member.FullName + member.Age == max).ToList();
            return Json(results.Max());

        }
        //BR3
        public IActionResult FullName(){
            var fullNameList= members.Select(member => $"{member.LName} {member.FName} ").ToList();
            return Json(fullNameList);
        }

        //BR4
        public IActionResult GetListEqual(int yearsOfBirth){
            var equal = members.Where(member => member.DateOfBirth.Year == yearsOfBirth).ToList();
            return Json(equal);
        }
         public IActionResult GetListMore(int yearsOfBirth){
            var moreThan = members.Where(member => member.DateOfBirth.Year > yearsOfBirth).ToList();
            return Json(moreThan);
        }
         public IActionResult GetListLess(int yearsOfBirth){
            var lessThan = members.Where(member => member.DateOfBirth.Year < yearsOfBirth).ToList();
            return Json(lessThan);
        }
        public IActionResult FillterPerson(int yearsOfBirth, string type){
            switch(type){
                case "equal":
                    return GetListEqual(yearsOfBirth);
                case "moreThan":
                    return GetListMore(yearsOfBirth);
                case "lessThan":
                    return GetListLess(yearsOfBirth);
                default:
                    return new EmptyResult();
            }
        }
        //BR5
        public IActionResult Export(){
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream, Encoding.UTF8, 1024, true);
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
            {
                csvWriter.WriteRecords(members);
            }
            stream.Position = 0;
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/csv")){
                FileDownloadName = "data.csv"
            };
            
            
            
        }
        
    }
}
