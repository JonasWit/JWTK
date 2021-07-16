using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SystemyWP.Data.DataAccessModifiers;
using SystemyWP.Data.Models.General;
using SystemyWP.Data.Models.LegalAppModels.Access;

namespace SystemyWP.Data.Models.Abstractions
{
    public class AccessKeyBase<TKey> : TrackedModel<TKey>
    {
        [Required] 
        [MaxLength(50)] 
        public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public List<User> Users { get; set; } = new();
        public List<DataAccess> DataAccesses { get; set; } = new();
    }
}