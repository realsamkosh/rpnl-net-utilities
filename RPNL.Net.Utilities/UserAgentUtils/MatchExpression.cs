using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RPNL.Net.Utilities.UserAgentUtils
{
    public class MatchExpression
    {
        public List<Regex> Regexes { get; set; }

        public Action<Match, object> Action { get; set; }
    }
}
