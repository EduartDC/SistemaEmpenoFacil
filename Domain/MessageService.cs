using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface MessageService
    {

        void ScanCode(string code);
        void Authorization(bool result);
    }
}
