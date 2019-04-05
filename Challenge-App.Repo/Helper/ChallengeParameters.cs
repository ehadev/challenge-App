using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge_App.Repo.Helper
{
    public class ChallengeParameters
    {

        private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;


        private int pageSize = 10;
        public int PageSize { get => pageSize; set => pageSize = (value > MaxPageSize) ? MaxPageSize : value; }




    }
}
