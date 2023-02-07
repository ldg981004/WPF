using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ldg_Practice;
using ldg_Practice.EF_CON;
using static ldg_Practice.EF_CON.Model;




internal class Progrem
{
    private static void Main(string[] args)
    {
        using var db = new EFContext();
        {
            /*User us = new User();
            us.Name = "One";
            us.User_ID = 1;

            db.Add(us);
            db.SaveChanges();
            // DB에 데이터 삽입*/

            var qu = db.User.ToList();

            foreach (var a in qu)
            {
                Console.WriteLine("ID : " + a.User_ID + "  " + "이름 : " + a.Name);
                // Console에 Employees에 있는 요소를 띄움

            }
        }
    }
}

