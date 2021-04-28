using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebApi.Models
{
    [DataContract]
    public class CollectionResourceDto
    {
        [DataMember]
        public IEnumerable<IDictionary<string, object>> Value { get; }

        [DataMember]
        public PaginationDto Pagination { get; }

        public CollectionResourceDto(IEnumerable<IDictionary<string, object>> value, PaginationDto pagination)
        {
            this.Value = value;
            this.Pagination = pagination;
        }
    }
}
