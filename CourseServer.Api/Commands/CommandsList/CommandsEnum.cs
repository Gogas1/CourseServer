using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Api.Commands.CommandsList
{
    public enum RequestCommand
    {
        Test,
        GetByName
    }

    public enum ResponseCommand
    {
        TestAnswer,
        GetByNameAnswer
    }
}
