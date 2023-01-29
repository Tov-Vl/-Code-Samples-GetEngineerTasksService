using System;
using System.Collections.Generic;
using System.Text;

namespace WFM.GetEngineerTasksServiceLib.Helpers
{
    public class EngineerNotFoundException : Exception
    {
        private const string ExceptionMessage = "Агент не найден";

        public EngineerNotFoundException()
            : base(ExceptionMessage)
        {
        }

        public EngineerNotFoundException(string message)
            : base(message)
        {
        }

        public EngineerNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
