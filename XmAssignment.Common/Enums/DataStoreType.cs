using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmAssignment.Common.Enums
{
    public enum DataStoreType
    {
        [Description("SQLite")]
        SQLite = 1,
        [Description("InMemoryDatabase")]
        InMemoryDatabase = 2,
        [Description("DockerContainerDatabase")]
        DockerContainerDatabase = 3
    }
}
