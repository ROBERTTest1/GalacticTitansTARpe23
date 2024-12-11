using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(TitanDto dto, Titan domain);
        void UploadFilesToDatabase(AstralBodyDto dto, AstralBody domain);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
    }
}
