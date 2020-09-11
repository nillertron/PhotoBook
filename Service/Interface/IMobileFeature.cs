using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IMobileFeature
    {
        Task<bool> CompressVideo(string inputPath, string outputPath);
    }
}
