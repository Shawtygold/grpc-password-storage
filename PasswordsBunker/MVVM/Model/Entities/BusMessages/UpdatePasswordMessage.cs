using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordsBunker.MVVM.Model.Entities.BusMessages
{
    internal class UpdatePasswordMessage : PasswordMessage
    {
        public UpdatePasswordMessage(Password password) : base(password) { }
    }
}
