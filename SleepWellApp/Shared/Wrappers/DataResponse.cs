using SleepWellApp.Shared.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPCTech2023FavoriteMovie.Shared.Wrappers;

public class DataResponse<T> : Response
{
    public T Data { get; set; }
    public DataResponse() { }
    public DataResponse(T data)
    {
        Succeeded = true;
        Data = data;
    }
}
