using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptographer.Interfaces
{
    public interface IAesConfig
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
        public PaddingMode Padding { get; set; }
        public CipherMode Mode { get; set; }
    }
}
