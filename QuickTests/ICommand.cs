using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTests
{
    public interface ICommand
    {
        void Execute(String[] args);
    }
}
