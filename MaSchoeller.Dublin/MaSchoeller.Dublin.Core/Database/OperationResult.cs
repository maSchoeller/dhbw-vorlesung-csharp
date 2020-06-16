using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaSchoeller.Dublin.Core.Database
{
    public enum OperationResult
    {
        Success = 0,
        Lock = 1,
        NotFound = 2,
        SaveFailure = 3,
        CascadingDeleteError = 4,
        AlreadyExists = 5,
        NotAuthorized = 6,
        NotAuthenticated = 7,
        SaveConflict = 8,
        UnkownError = int.MaxValue,
    }
}
