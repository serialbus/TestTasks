using System.Windows.Input;

namespace Infrastructure.Common.Module
{
    public interface INavigationMenu
    {
        string Text { get; }
        ICommand Command { get; }
    }
}
