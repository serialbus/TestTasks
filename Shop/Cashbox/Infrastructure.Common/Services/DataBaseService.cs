using Infrastructure.Common.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
    public static class DataBaseService
    {
        public static IRepository Repository { get; set; }
    }
}
