using System.Collections.Generic;

namespace IsuExtra.Charater
{
    public class ExtraGroup : Isu.Charater.Group // обертка над группой из Isu
    {
        public List<Lesson> Lessons { get; set; } // список пар

        public OGNP Base { get; set; } // ОГНП основной
    }
}