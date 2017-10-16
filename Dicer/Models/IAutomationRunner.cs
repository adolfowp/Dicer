using System;
using System.Threading.Tasks;

namespace Dicer
{
    public interface IAutomationRunner
    {
        Task<int> Run(DiceSite Site);
        void Stop();
        bool CanExecute(DiceSite Site);
    }
}
