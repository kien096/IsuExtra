using System;
using Isu.Charater;
using IsuExtra.Charater;

namespace IsuExtra
{
    internal class Program
    {
        private static void Main()
        {
            IsuExtraService isuExtra = new IsuExtraService();

            var ognp0 = isuExtra.AddOGNP();
            var ognp1 = isuExtra.AddOGNP();

            Group group = isuExtra.AddGroup(new GroupName() { X = 2, YY = 12 }, ognp0);  // group add M3212
            Group group1 = isuExtra.AddGroup(new GroupName() { X = 3, YY = 11 }, ognp0); // add M3311

            Stream stream = new Stream() { Name = "my speciality", OGNP = ognp0 };
            Stream stream1 = new Stream() { Name = "not my speciality", OGNP = ognp1 };

            Teacher teacher = new Teacher() { Name = "Simple Name Teacher Group" };

            Lesson lesson1 = new Lesson() { Group = group as ExtraGroup, Teacher = teacher, Start = new DateTime(2021, 10, 10, 10, 0, 0), End = new DateTime(2021, 10, 10, 11, 0, 0), Classroom = new Classroom() { Name = "111" } };
            Lesson lesson2 = new Lesson() { Stream = stream, Teacher = teacher, Start = new DateTime(2021, 10, 10, 10, 0, 0), End = new DateTime(2021, 10, 10, 11, 0, 0), Classroom = new Classroom() { Name = "121" } };
            Lesson lesson3 = new Lesson() { Group = group as ExtraGroup, Teacher = teacher, Start = new DateTime(2021, 10, 10, 12, 0, 0), End = new DateTime(2021, 10, 10, 13, 0, 0), Classroom = new Classroom() { Name = "131" } };
            Lesson lesson4 = new Lesson() { Stream = stream, Teacher = teacher, Start = new DateTime(2021, 10, 10, 14, 0, 0), End = new DateTime(2021, 10, 10, 15, 0, 0), Classroom = new Classroom() { Name = "141" } };
            Lesson lesson5 = new Lesson() { Stream = stream1, Teacher = teacher, Start = new DateTime(2021, 10, 10, 17, 0, 0), End = new DateTime(2021, 10, 10, 18, 0, 0), Classroom = new Classroom() { Name = "151" } };

            (group as ExtraGroup).Lessons.Add(lesson1);
            (group as ExtraGroup).Lessons.Add(lesson3);
            stream.Lessons.Add(lesson2);
            stream.Lessons.Add(lesson4);
            stream1.Lessons.Add(lesson5);

            ognp0.Streams.Add(stream);
            ognp1.Streams.Add(stream1);

            // studen add
            ExtraStudent student = isuExtra.AddExtraStudent(group, "Le Ba Kien"); // 0
            ExtraStudent student2 = isuExtra.AddExtraStudent(group, "Nguyen Tuan Kiet"); // 1
            isuExtra.AddExtraStudent(group, "Tran Hoang Nam"); // 2
            isuExtra.AddExtraStudent(group, "Phormenko"); // 3

            isuExtra.AddStudent(group1, "Duc"); // 0

            isuExtra.PrintStudentList(isuExtra.FindStudents(group.GroupName));
            Console.WriteLine();

            isuExtra.PrintOGNPs();
            Console.WriteLine();

            var ognp2 = isuExtra.AddOGNP();

            isuExtra.PrintOGNPs();
            Console.WriteLine();

            isuExtra.PrintStudentsInOGNP(ognp0);
            isuExtra.PrintStudentsInOGNP(ognp1);
            Console.WriteLine();

            isuExtra.Subscribe(student2, ognp1);
            isuExtra.Subscribe(student, ognp1);

            isuExtra.PrintStudentsInOGNP(ognp0);
            isuExtra.PrintStudentsInOGNP(ognp1);
            Console.WriteLine();

            isuExtra.Unsubscribe(student, ognp1);

            isuExtra.PrintStudentsInOGNP(ognp0);
            isuExtra.PrintStudentsInOGNP(ognp1);
            Console.WriteLine();

            isuExtra.PrintStreams(ognp0);
            Console.WriteLine();

            isuExtra.PrintUnsubStudent();
        }
    }
}