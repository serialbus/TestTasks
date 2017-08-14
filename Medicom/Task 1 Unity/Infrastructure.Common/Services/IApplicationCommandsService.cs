using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Common.Services
{
    public interface IApplicationCommandsService
    {
        CompositeCommand AddNoteCommand { get; }
        CompositeCommand AddWebAccountCommand { get; }
        CompositeCommand AddCreditCardCommand { get; }
        CompositeCommand DeleteItemCommand { get; }
    }
}
