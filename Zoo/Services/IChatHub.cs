using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.Services
{
    public interface IChatHub
    {
        Task Refresh(string refresh = "Refresh");


    }
}
