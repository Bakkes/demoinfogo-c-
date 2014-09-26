using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoParserGUI
{
    class SteamUser
    {
        /*
         		optional int32 account_id = 1;
		optional int32 rank_old = 2;
		optional int32 rank_new = 3;
		optional int32 num_wins = 4;
		optional float rank_change = 5;
         */
        public string Username { get; set; }
        public int AccountID32 { get; set; }
        public int Rank { get; set; }
        public int WinCount { get; set; }
        public int lastVac { get; set; }
        public long AccountID64
        {
            get
            {
                return AccountID32 + 76561197960265728;
            }
        }

        public List<String> DemoFiles = new List<String>();

    }
}
