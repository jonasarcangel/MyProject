using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services.Interfaces
{
    public interface ISmsSenderService
    {
        Task SendSmsAsync(string number, string message);
    }
}
