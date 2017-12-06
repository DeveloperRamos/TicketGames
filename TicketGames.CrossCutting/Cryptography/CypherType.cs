using System.ComponentModel;

namespace TicketGames.CrossCutting.Cryptography
{
    public enum CypherType : long
    {
        [Description("SHA512")]
        SHA512 = 1,

        [Description("SHA1")]
        SHA1 = 2,

        [Description("MD5")]
        MD5 = 3
    }
}
