﻿using SystemyWP.Data.Models.Abstractions;

namespace SystemyWP.Data.Models
{
    public class User : BaseModel<string>
    {
        public string Image { get; set; }
    }
}