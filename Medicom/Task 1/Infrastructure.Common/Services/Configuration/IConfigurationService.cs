using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Models.Configuration
{
    public interface IConfigurationService
    {
        string PathToRepositoryFile { get; set; }
    }
}
