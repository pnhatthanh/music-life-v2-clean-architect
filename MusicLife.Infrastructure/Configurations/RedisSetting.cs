using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Infrastructure.Configurations
{
    public sealed class RedisSetting
    {
        public bool Enable {  get; set; }
        public string? ConnectionString {  get; set; }
        public string? Name {  get; set; }
    }
}
