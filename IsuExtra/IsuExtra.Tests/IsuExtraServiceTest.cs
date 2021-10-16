using Isu.Charater;
using Isu.Tools;
using IsuExtra.Charater;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace IsuExtra.Tests
{
    public class IsuExtraServiceTest
    {
        private IsuExtraService isuExtra;
        private Group _group;

        [SetUp]
        public void Setup()
        {
            isuExtra = new IsuExtraService();
            OGNP ognp0 = isuExtra.AddOGNP();
            OGNP ognp1 = isuExtra.AddOGNP();

            _group = isuExtra.AddGroup(new GroupName() { X = 2, YY = 12 }, ognp0);  // group add M3212

            var stream = new Stream() { Name = "my speciality", OGNP = ognp0 };
            var stream1 = new Stream() { Name = "not my speciality", OGNP = ognp1 };

            var teacher = new Teacher() { Name = "Simple Name Teacher Group" };

            var lesson1 = new Lesson() { Group = _group as ExtraGroup, Teacher = teacher, Start = new DateTime(2021, 10, 10, 10, 0, 0), End = new DateTime(2021, 10, 10, 11, 0, 0), Classroom = new Classroom() { Name = "111" } };
            var lesson2 = new Lesson() { Stream = stream, Teacher = teacher, Start = new DateTime(2021, 10, 10, 10, 0, 0), End = new DateTime(2021, 10, 10, 11, 0, 0), Classroom = new Classroom() { Name = "121" } };
            var lesson3 = new Lesson() { Group = _group as ExtraGroup, Teacher = teacher, Start = new DateTime(2021, 10, 10, 12, 0, 0), End = new DateTime(2021, 10, 10, 13, 0, 0), Classroom = new Classroom() { Name = "131" } };
            var lesson4 = new Lesson() { Stream = stream, Teacher = teacher, Start = new DateTime(2021, 10, 10, 14, 0, 0), End = new DateTime(2021, 10, 10, 15, 0, 0), Classroom = new Classroom() { Name = "141" } };
            var lesson5 = new Lesson() { Stream = stream1, Teacher = teacher, Start = new DateTime(2021, 10, 10, 17, 0, 0), End = new DateTime(2021, 10, 10, 18, 0, 0), Classroom = new Classroom() { Name = "151" } };

            (_group as ExtraGroup).Lessons.Add(lesson1);
            (_group as ExtraGroup).Lessons.Add(lesson3);
            stream.Lessons.Add(lesson2);
            stream.Lessons.Add(lesson4);
            stream1.Lessons.Add(lesson5);

            ognp0.Streams.Add(stream);
            ognp1.Streams.Add(stream1);
        }

        [Test]
        public void AddOGNP()
        {
            isuExtra.AddOGNP();
            if (isuExtra.OGNPs.Count != 3)
            {
                throw new IsuException("OGNP not added");
            }
        }

        [Test]
        public void Subsribe()
        {
            ExtraStudent student = isuExtra.AddExtraStudent(_group, "Le Ba Kien");
            isuExtra.Subscribe(student, isuExtra.OGNPs[0]);
            isuExtra.Subscribe(student, isuExtra.OGNPs[1]);

            if (student.OGNPs[0] == null && student.OGNPs[1] == null)
            {
                throw new IsuException("OGNP not added");
            }
        }

        [Test]
        public void Unsubsribe()
        {
            ExtraStudent student = isuExtra.AddExtraStudent(_group, "Le Ba Kien");
            isuExtra.Subscribe(student, isuExtra.OGNPs[1]);

            isuExtra.Unsubscribe(student, isuExtra.OGNPs[1]);

            if (student.OGNPs[0] != null || student.OGNPs[1] != null)
            {
                throw new IsuException("OGNP is not empty");
            }
        }

        [Test]
        public void GetStreamsByOGNP()
        {
            var list = isuExtra.GetStreams(isuExtra.OGNPs[1]);

            if (!list.Any())
                throw new IsuException("Stream list is empty");
        }

        [Test]
        public void GetStudentsFromStream()
        {
            ExtraStudent student = isuExtra.AddExtraStudent(_group, "Le Ba Kien");
            isuExtra.Subscribe(student, isuExtra.OGNPs[1]);
            var list = isuExtra.GetStudentsFromStream(isuExtra.OGNPs[1].Streams[0]);

            if (!list.Any())
                throw new IsuException("Students list is empty");
        }

        [Test]
        public void GetUnsubStudent()
        {
            ExtraStudent student = isuExtra.AddExtraStudent(_group, "Le Ba Kien");
            isuExtra.Subscribe(student, isuExtra.OGNPs[1]);

            isuExtra.AddExtraStudent(_group, "Nguyen Tuan Kiet");
            isuExtra.AddExtraStudent(_group, "Tran Hoang Nam");
            isuExtra.AddExtraStudent(_group, "Phormenko");

            List<ExtraStudent> list = isuExtra.GetUnsubStudent();
            if (!list.Any())
                throw new IsuException("Students list is empty");
        }
    }
}
