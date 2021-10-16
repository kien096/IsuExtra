using System;
using System.Collections.Generic;
using System.Linq;
using Isu;
using Isu.Charater;
using IsuExtra.Charater;

namespace IsuExtra
{
    public class IsuExtraService : IsuService
    {
        private int _studentId;
        private int _ognpId;

        public IsuExtraService()
            : base()
        {
            _studentId = 0;
            _ognpId = 0;
            OGNPs = new List<OGNP>();
        }

        public List<OGNP> OGNPs { get; set; }

        // add new OGNP
        public OGNP AddOGNP()
        {
            var newOGNP = new OGNP() { Id = _ognpId++ };
            OGNPs.Add(newOGNP);
            return newOGNP;
        }

        // add new group
        public Group AddGroup(GroupName name, OGNP ognp)
        {
            var newGroup = new ExtraGroup() { GroupName = name, Students = new List<Student>(), Lessons = new List<Lesson>(), Base = ognp };
            Groups.Add(newGroup);
            return newGroup;
        }

        public ExtraStudent AddExtraStudent(Group group, string name)
        {
            var newStudent = new ExtraStudent() { Id = _studentId++, Name = name, Group = group };
            Students.Add(newStudent);
            group.Students.Add(newStudent);
            return newStudent;
        }

        public bool Subscribe(Student student, OGNP ognp)
        {
            if (student is ExtraStudent extraStudent)
            {
                return extraStudent.JoinOGNP(ognp);
            }

            return false;
        }

        public void Unsubscribe(Student student, OGNP ognp)
        {
            if (student is ExtraStudent extraStudent)
            {
                extraStudent.ExitOGNP(ognp);
            }
        }

        public List<Stream> GetStreams(OGNP ognp)
        {
            return ognp.Streams;
        }

        public void PrintStreams(OGNP ognp)
        {
            Console.WriteLine("Streams in OGNP " + ognp.Id.ToString() + " have :");
            foreach (Stream stream in GetStreams(ognp))
            {
                Console.WriteLine(stream.Name);
            }

            Console.WriteLine();
        }

        public List<ExtraStudent> GetStudentsFromStream(Stream stream)
        {
            return stream.Students;
        }

        public void PrintStudentsInStream(Stream stream)
        {
            Console.WriteLine("Students in " + stream.Name + " have :");
            foreach (ExtraStudent student in GetStudentsFromStream(stream))
            {
                Console.WriteLine(student.Name);
            }

            Console.WriteLine();
        }

        public void PrintStudentsInOGNP(OGNP ognp)
        {
            Console.WriteLine("Students in OGNP " + ognp.Id.ToString() + " have :");
            foreach (Stream stream in ognp.Streams)
            {
                PrintStudentsInStream(stream);
            }

            Console.WriteLine();
        }

        public void PrintOGNPs()
        {
            Console.WriteLine("OGNPs List :");
            foreach (OGNP ognp in OGNPs)
            {
                Console.WriteLine("OGNP " + ognp.Id);
            }

            Console.WriteLine();
        }

        public List<ExtraStudent> GetAllSubStudents()
        {
            var students = new List<ExtraStudent>();
            foreach (OGNP ognp in OGNPs)
            {
                foreach (Stream stream in ognp.Streams)
                {
                    foreach (ExtraStudent student in stream.Students)
                    {
                        students.Add(student);
                    }
                }
            }

            return students.Distinct().ToList();
        }

        public List<ExtraStudent> GetAllStudents()
        {
            var students = new List<ExtraStudent>();
            foreach (Group g in Groups)
            {
                if (g is ExtraGroup extraGroup)
                {
                    foreach (Student s in extraGroup.Students)
                    {
                        if (s is ExtraStudent extraStudent)
                            students.Add(extraStudent);
                    }
                }
            }

            return students.Distinct().ToList();
        }

        public List<ExtraStudent> GetUnsubStudent()
        {
            List<ExtraStudent> students = GetAllStudents();
            List<ExtraStudent> subStudents = GetAllSubStudents();

            students = students.Except(subStudents).ToList();
            return students;
        }

        public void PrintUnsubStudent()
        {
            Console.WriteLine("Students in no streams have :");
            List<ExtraStudent> students = GetAllStudents();
            List<ExtraStudent> subStudents = GetAllSubStudents();

            students = students.Except(subStudents).ToList();

            foreach (ExtraStudent s in students)
            {
                Console.WriteLine(s.Name);
            }
        }
    }
}