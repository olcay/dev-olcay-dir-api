using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebApi.Models
{
    [DataContract]
    public class LinkedCollectionResourceDto
    {
        [DataMember]
        public IEnumerable<IDictionary<string, object>> Value { get; }

        [DataMember]
        public IEnumerable<LinkDto> Links { get; }

        [DataMember]
        public PaginationDto Pagination { get; }

        public LinkedCollectionResourceDto(IEnumerable<IDictionary<string, object>> value, IEnumerable<LinkDto> links, PaginationDto pagination)
        {
            this.Value = value;
            this.Links = links;
            this.Pagination = pagination;
        }
    }
}
