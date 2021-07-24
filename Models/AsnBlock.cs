using System;
using System.Collections.Generic;
using System.Net;

#nullable disable

namespace LocationIp
{
    public partial class AsnBlock
    {
        public ValueTuple<IPAddress, int>? Network { get; set; }
        public long? AutonomousSystemNumber { get; set; }
        public string AutonomousSystemOrganization { get; set; }
        public long AsnBlocksId { get; set; }
    }
}
