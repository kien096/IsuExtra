using System.Collections.Generic;

namespace IsuExtra.Charater
{
    public class OGNP
    {
        public int Id { get; set; }

        public List<Stream> Streams { get; } = new List<Stream>(); // потоки

        public void AddStudent(ExtraStudent student)
        {
            foreach (Stream stream in Streams)
            {
                stream.Students.Add(student);
            }
        }

        public void RemoveStudent(ExtraStudent student)
        {
            foreach (Stream stream in Streams)
            {
                stream.Students.Remove(student);
            }
        }
    }
}