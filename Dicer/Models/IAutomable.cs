using System;
using System.Threading.Tasks;

namespace Dicer
{
    public interface IAutomable
    {
        Task<int> AutomateAsync(IAutomationRunner Runner);
    }
}
