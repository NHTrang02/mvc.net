using System;
using System.Collections.Generic;
using System.Linq;

namespace Models {

    public class Person {
        public string gender {get; set;}
        public string FName{get; set;}

        public string LName{get; set;}
        public DateTime DateOfBirth {get;set;}
        public string phone {get;set;}
         public string BirthPlace {get;set;}
         public int Age {
             get { return calcAge();}
            set {}
         }
         public int calcAge() {
             int a = 0;
             int years = DateTime.Now.Year - DateOfBirth.Year;
             int month = DateTime.Now.Month - DateOfBirth.Month;
             int days = DateTime.Now.Day - DateOfBirth.Day;
            if((days == 0 && month == 0)|| (month > 0)){
                a = 1;
            }
            return years + a;
         }
        public string FullName => $"{LName} {FName}";
        public bool IsGraduated {get;set;}
        public int CaculateAge(){
            return int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(DateOfBirth.ToString("yyyyMMdd"));
        }
        
        }
}