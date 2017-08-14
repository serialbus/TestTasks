using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Infrastructure.Common.Services;
using System.Windows.Input;

namespace Medicom.ViewModels.Services
{
    public class ApplicationCommandsService : IApplicationCommandsService
    {
        #region Constructor

        public ApplicationCommandsService()
        {
            AddNoteCommand = new CompositeCommand();
            AddWebAccountCommand = new CompositeCommand();
            AddCreditCardCommand = new CompositeCommand();
        }

        #endregion

        #region Fields And Properties

        public CompositeCommand AddNoteCommand { get; private set; }
        public CompositeCommand AddWebAccountCommand { get; private set; }
        public CompositeCommand AddCreditCardCommand { get; private set; }

        #endregion
    }
}
