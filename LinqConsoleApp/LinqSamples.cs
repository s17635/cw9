using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            var res1 = from emp in Emps
                       where emp.Job == "Backend programmer"
                       select new
                       {
                           emp.Ename,
                           emp.Job
                       };

            PrintCollection(res1);

            //2. Lambda and Extension methods
            Console.WriteLine(". . . . . . ");
            var res2 = Emps.Where(emp => emp.Job == "Backend programmer").Select(e => new { e.Ename, e.Job });
            PrintCollection(res2);
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            //1. Query syntax (SQL)
            var res1 = from emp in Emps
                       where emp.Job == "Frontend programmer" && emp.Salary > 1000
                       orderby emp.Ename descending
                       select new
                       {
                           emp.Ename,
                           emp.Job,
                           emp.Salary
                       };

            PrintCollection(res1);

            //2. Lambda and Extension methods
            Console.WriteLine(". . . . . . ");
            var res2 = Emps.Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000).OrderByDescending(emp => emp.Ename).Select(e => new { e.Ename, e.Job, e.Salary });
            PrintCollection(res2);
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {
            //1. Query syntax (SQL)
            var res1 = (from emp in Emps
                        select emp.Salary).Max();

            Console.WriteLine(res1);

            //2. Lambda and Extension methods
            Console.WriteLine(". . . . . . ");
            var res2 = Emps.Select(e => e.Salary).Max();
            Console.WriteLine(res2);

        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Przyklad4()
        {
            //1. Query syntax (SQL)
            var res1 = from emp in Emps
                       where emp.Salary == ((from emp2 in Emps
                                             select emp2.Salary).Max())
                       select new
                       {
                           emp.Ename,
                           emp.Job,
                           emp.Salary
                       };

            PrintCollection(res1);

            //2. Lambda and Extension methods
            Console.WriteLine(". . . . . . ");
            var res2 = Emps.Where(emp => emp.Salary == (Emps.Select(e => e.Salary).Max())).Select(e => new { e.Ename, e.Job, e.Salary });
            PrintCollection(res2);
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            //1. Query syntax (SQL)
            var res1 = from emp in Emps
                       select new
                       {
                           Nazwisko = emp.Ename,
                           Praca = emp.Job
                       };

            PrintCollection(res1);

            //2. Lambda and Extension methods
            Console.WriteLine(". . . . . . ");
            var res2 = Emps.Select(e => new { Nazwisko = e.Ename, Praca = e.Job });
            PrintCollection(res2);

        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {
            //1. Query syntax (SQL)
            var res1 = from emp in Emps
                       join dept in Depts on emp.Deptno equals dept.Deptno
                       select new
                       {
                           emp.Ename,
                           emp.Job,
                           dept.Dname
                       };

            PrintCollection(res1);

            //2. Lambda and Extension methods
            Console.WriteLine(". . . . . . ");
            var res2 = Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new { emp.Ename, emp.Job, dept.Dname });
            PrintCollection(res2);

        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {
            //1. Query syntax (SQL)
            var res1 = from emp in Emps
                       group emp by emp.Job into empGroup
                       select new
                       {
                           Praca = empGroup.Key,
                           LiczbaPracownikow = empGroup.Count()
                       };

            PrintCollection(res1);

            //2. Lambda and Extension methods
            Console.WriteLine(". . . . . . ");
            var res2 = Emps.GroupBy(e => e.Job).Select(e1 => new { Praca = e1.Key, LiczbaPracownikow = e1.Count() });
            PrintCollection(res2);
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {
            //1. Query syntax (SQL)
            var res1 = (from emp in Emps
                        where emp.Job == "Backend programmer"
                        select emp.Job).Any();

            Console.WriteLine(res1);

            //2. Lambda and Extension methods
            Console.WriteLine(". . . . . . ");
            var res2 = Emps.Where(emp => emp.Job == "Backend programmer").Any();

            Console.WriteLine(res2);
        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Przyklad9()
        {
            //1. Query syntax (SQL)
            var res1 = (from emp in Emps
                        where emp.Job == "Frontend programmer"
                        orderby emp.HireDate descending
                        select new
                        {
                            emp.Ename,
                            emp.Job,
                            emp.HireDate
                        }).First();

            Console.WriteLine(res1);

            //2. Lambda and Extension methods
            Console.WriteLine(". . . . . . ");
            var res2 = Emps.Where(emp => emp.Job == "Frontend programmer").OrderByDescending(e => e.HireDate).Select(e => new { e.Ename, e.Job, e.HireDate }).First();

            Console.WriteLine(res2);
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10Button_Click()
        {
            DateTime? nullDate = null;
            string nullString = null;

            var res2 = Emps.Select(e => new { Name = e.Ename, Job = e.Job, HireDate = e.HireDate }).Union(Emps.Select(e => new { Name = "Brak wartości", Job = nullString, HireDate = nullDate }));

            PrintCollection(res2);
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {
            var res2 = Emps.Aggregate((emp1, emp2) => emp2.Salary > emp1.Salary ? emp2 : emp1);

            Console.WriteLine(res2);
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {
            var res2 = Emps.SelectMany(e => Depts, (e, d) => new { e.Ename, e.Job, d.Deptno, d.Dname });

            PrintCollection(res2);
        }

        public void PrintCollection(IEnumerable res)
        {
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }
    }
}
