using OneAndOne.POCO.Respones.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Converters
{
    public static class DVDConverter
    {
        public static UpdatedOperationServerResponse ConvertTOUpdatedOperations(this ServerResponse source)
        {
            return new UpdatedOperationServerResponse()
            {
                Id = source.Id,
                Name = source.Name
            };
        }
    }
}
