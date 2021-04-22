using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statmath.Application.Client.Common.Abstraction
{
    public interface IRequestHelper
    {
        Task<string> MakePostRequest(string action, dynamic payload);
        Task<T> MakeGetRequest<T>(string action, IEnumerable<KeyValuePair<string, dynamic>> parameters = null);
        Task<int> MakeDeleteRequest(string action, dynamic payload = null);
    }
}
