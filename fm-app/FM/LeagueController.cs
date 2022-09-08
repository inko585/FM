using FM.Models;
using FM.Models.Generic;
using FM.Models.Season;
using FootballPit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM
{
    public class LeagueController
    {
        private static LeagueController instance;
        public static LeagueController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LeagueController();
                }

                return instance;

            }
        }

        private LeagueController()
        {

        }


        

}
}
